using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altairis.CsrFence.Configuration {

    public class CsrFenceSection : ConfigurationSection {

        [ConfigurationProperty("sessionId")]
        public SessionIdElement SessionId {
            get { return (SessionIdElement)this["sessionId"]; }
            set { this["sessionId"] = value; }
        }

        [ConfigurationProperty("token")]
        public TokenElement Token {
            get { return (TokenElement)this["token"]; }
            set { this["token"] = value; }
        }

    }

}
