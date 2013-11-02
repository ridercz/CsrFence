using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Altairis.CsrFence.DemoTargetApp {
    public partial class Default : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void ButtonActionFormSubmit_Click(object sender, EventArgs e) {
            if (!this.IsValid) return;
            this.ResultLiteral.Text = string.Format(this.ResultLiteral.Text, this.NewEmailTextBox.Text);
            this.PageMultiView.SetActiveView(this.ResultView);
        }
    }
}