<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Altairis.CsrFence.DemoTargetApp.Default" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CSRF Demo Target Application</title>
</head>
<body>
    <form id="form1" runat="server">
        <h1>CSRF Demo Target Application</h1>
        <asp:MultiView ID="PageMultiView" ActiveViewIndex="0" runat="server">
            <asp:View ID="FormView" runat="server">
                <table class="form">
                    <tbody>
                        <tr>
                            <th>
                                <asp:Label runat="server" Text="New e-mail address:" AssociatedControlID="NewEmailTextBox" />
                            </th>
                            <td>
                                <asp:TextBox ID="NewEmailTextBox" runat="server" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="NewEmailTextBox" Display="Dynamic" Text="*" ErrorMessage="New e-mail address is missing" />
                            </td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr>
                            <td colspan="2">
                                <asp:ValidationSummary runat="server" />
                                <asp:Button ID="ButtonActionFormSubmit" runat="server" Text="Submit" OnClick="ButtonActionFormSubmit_Click" />
                            </td>
                        </tr>
                    </tfoot>
                </table>
            </asp:View>
            <asp:View ID="ResultView" runat="server">
                <asp:Literal ID="ResultLiteral" Text="Your e-mail address was changed to {0}." runat="server" />
            </asp:View>
        </asp:MultiView>
    </form>
</body>
</html>
