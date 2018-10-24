<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="auditors.aspx.cs" Inherits="SchedulingApplication_10_23_2018.auditors" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Auditors</h1>
            <h2>Welcome to the auditors page</h2>
            <asp:HiddenField ID="hfAuditorID" runat="server" />
            <table>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Auditor Name"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Auditor Level"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtLevel" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Auditor Level"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtleveldemo" runat="server"></asp:TextBox>
                        <asp:RadioButtonList ID="rdLevel" runat="server" RepeatLayout="Flow">
                            <asp:ListItem Value="1">Level 1</asp:ListItem>
                            <asp:ListItem Value="2">Level 2</asp:ListItem>
                            <asp:ListItem Value="2">Level 3</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>

                    </td>
                    <td colspan="2">
                         <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                         <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />
                         <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
                    </td>
                </tr>
                <tr>
                    <td>

                    </td>
                    <td colspan="2">
                        <asp:Label ID="lblSuccessMessage" runat="server" Text="" ForeColor="Green"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>

                    </td>
                    <td colspan="2">
                        <asp:Label ID="lblErrorMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            <asp:GridView ID="gvAuditor" runat="server" AutoGenerateColumns="false" AllowSorting="True">
                <Columns>
                    <asp:BoundField DataField="AuditorID" HeaderText="ID" />
                    <asp:BoundField DataField="AuditorName" HeaderText="Name" />
                    <asp:BoundField DataField="AuditorLevel" HeaderText="Level" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkView" runat="server" CommandArgument='<%# Eval("AuditorID") %>' OnClick="lnk_OnClick" >Modify</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

        </div>
    </form>
</body>
</html>

