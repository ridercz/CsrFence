using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Altairis.CsrFence.Configuration {
    public class SessionIdElement : ConfigurationElement {

        [ConfigurationProperty("length", IsRequired = false, DefaultValue = 64)]
        [IntegerValidator(MinValue = 32, MaxValue = 128)]
        public int Length {
            get { return (int)this["length"]; }
            set { this["length"] = value; }
        }

        [ConfigurationProperty("cookieName", IsRequired = false, DefaultValue = ".CSRFENCE")]
        public string CookieName {
            get { return (string)this["cookieName"]; }
            set { this["cookieName"] = value; }
        }

    }
}
