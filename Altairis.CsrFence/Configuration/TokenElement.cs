using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Altairis.CsrFence.Configuration {
    public class TokenElement : ConfigurationElement {

        [ConfigurationProperty("fieldName", IsRequired = false, DefaultValue = "__CSRFTOKEN")]
        public string FieldName {
            get { return (string)this["fieldName"]; }
            set { this["fieldName"] = value; }
        }

    }
}
