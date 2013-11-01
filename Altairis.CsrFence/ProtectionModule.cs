using Altairis.CsrFence.Configuration;
using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Web;
using System.Web.Security;
using System.Web.UI;

namespace Altairis.CsrFence {

    public class ProtectionModule : IHttpModule {
        private CsrFenceSection config;

        public void Dispose() {
            // NOOP
        }

        public void Init(HttpApplication context) {
            // Read configuration or use default
            this.config = ConfigurationManager.GetSection("altairis.csrFence") as CsrFenceSection;
            if (this.config == null) this.config = new CsrFenceSection();

            // Handle PostMapRequestHandler event
            context.PostMapRequestHandler += context_PostMapRequestHandler;
        }

        private void context_PostMapRequestHandler(object sender, EventArgs e) {
            var app = sender as HttpApplication;
            var page = app.Context.Handler as Page;

            if (page == null) return; // Handler is not Web Form

            if (!app.Context.Request.HttpMethod.Equals("POST", StringComparison.OrdinalIgnoreCase)) {
                // First request - create CSRF token
                CreateCsrfToken(page);
            }
            else {
                // Postback - verify CSRF token
                if (!VerifyCsrfToken(page)) throw new HttpRequestValidationException("CSRF protection token is missing or invalid.");
            }
        }

        private void CreateCsrfToken(Page page) {
            if (page == null) throw new ArgumentNullException("page");

            // Create signed token
            var key = GetCsrfSessionIdFromCookie(true);
            var token = MachineKey.Protect(key, config.Token.PurposeString);
            var tokenString = Convert.ToBase64String(token);

            // Add it to hidden form field
            page.ClientScript.RegisterHiddenField(this.config.Token.FieldName, tokenString);
        }

        private byte[] GetCsrfSessionIdFromCookie(bool createNew) {
            // Get cookie with CSRF session ID
            var idCookie = HttpContext.Current.Request.Cookies[this.config.SessionId.CookieName];
            if (idCookie != null) {
                try {
                    var sid = Convert.FromBase64String(idCookie.Value);
                    return sid;
                }
                catch (Exception) {
                    // Value cannot be parsed as Base64
                }
            }

            // No valid cookie present
            if (!createNew) throw new HttpRequestValidationException("No CSRF session ID cookie present.");

            // Create new cookie (for non-POST)
            using (var rng = new RNGCryptoServiceProvider()) {
                // Create random key
                var buffer = new byte[this.config.SessionId.Length];
                rng.GetBytes(buffer);

                // Save to cookie
                idCookie = new HttpCookie(this.config.SessionId.CookieName, Convert.ToBase64String(buffer));
                idCookie.HttpOnly = true;
                HttpContext.Current.Response.Cookies.Add(idCookie);

                // Return new key
                return buffer;
            }
        }

        private bool VerifyCsrfToken(Page page) {
            if (page == null) throw new ArgumentNullException("page");

            // Get session ID from cookie
            var key = GetCsrfSessionIdFromCookie(false);

            // Get token from hidden form field
            var tokenString = HttpContext.Current.Request.Form[this.config.Token.FieldName];
            if (string.IsNullOrWhiteSpace(tokenString)) return false;

            // Parse token as Base64
            byte[] suppliedKey;
            try {
                var token = Convert.FromBase64String(tokenString);
                suppliedKey = MachineKey.Unprotect(token, config.Token.PurposeString);
            }
            catch (Exception) {
                // Error while parsing or processing token
                return false;
            }

            // Compare key with supplied key
            if (key.Length != suppliedKey.Length) return false;
            for (int i = 0; i < key.Length; i++) {
                if (!key[i].Equals(suppliedKey[i])) return false;
            }

            return true;
        }
    }
}