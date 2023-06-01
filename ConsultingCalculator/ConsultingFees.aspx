<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConsultingFees.aspx.cs" Inherits="CIS325_Master_Project.Assignments.ConsultingFees" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<asp:Panel ID="ConsultPanel" runat="server" style="font-size: large">

<table class="nav-justified">
<tr>
<td class="text-center" colspan="2"><strong>Consulting Fees Calculator</strong></td>
</tr>
<tr>
<td style="width: 394px" class="modal-sm">&nbsp;</td>
<td>
<asp:Label ID="ErrorMsg" runat="server" ForeColor="Red"></asp:Label>
</td>
</tr>
<tr>
<td style="width: 394px" class="modal-sm">Customer Name<span style="color: #FF0000"><strong>*</strong></span>:</td>
<td>
<asp:TextBox ID="CustomerName" runat="server" Width="250px"></asp:TextBox>
<asp:RequiredFieldValidator ID="RFVCustomerName" runat="server" ControlToValidate="CustomerName" ErrorMessage="Please enter your name." ForeColor="Red"></asp:RequiredFieldValidator>
</td>
</tr>
<tr>
<td style="width: 394px" class="modal-sm">Job Title:</td>
<td>
    <asp:DropDownList ID="JobTitle" runat="server">
        <asp:ListItem>-Please Select- </asp:ListItem>
        <asp:ListItem Value="Developer">Developer</asp:ListItem>
        <asp:ListItem Value="Analyst">Analyst</asp:ListItem>
        <asp:ListItem Value="Architect">Architect</asp:ListItem>
        <asp:ListItem Value="Project Lead">Project Lead</asp:ListItem>
    </asp:DropDownList>
</td>
</tr>
<tr>
<td style="width: 394px" class="modal-sm">Microsoft Certified?:</td>
<td>
    <asp:RadioButtonList ID="Certification" runat="server" RepeatDirection="Horizontal">
        <asp:ListItem Value="Yes">Yes</asp:ListItem>
        <asp:ListItem Value="No">No</asp:ListItem>
    </asp:RadioButtonList>
</td>
</tr>
<tr>
<td style="width: 394px; height: 38px;" class="modal-sm">Hours Worked?:</td>
<td style="height: 38px">
    <asp:TextBox ID="HoursWorked" runat="server" Width="68px"></asp:TextBox>
    <asp:RangeValidator ID="HWValidate" runat="server" ControlToValidate="HoursWorked" ErrorMessage="Please enter hours worked between 0 and 100." ForeColor="Red" MaximumValue="100" MinimumValue="0" Type="Integer"></asp:RangeValidator>
</td>
</tr>
<tr>
<td style="width: 394px" class="modal-sm">&nbsp;</td>
<td>
    &nbsp;</td>
</tr>
<tr>
<td style="width: 394px; height: 342px;" class="modal-sm">Technical Skills:</td>
<td style="height: 342px">
<asp:CheckBoxList ID="SkillsList" runat="server">
<asp:ListItem Value="ASP.NET">ASP.NET</asp:ListItem>
<asp:ListItem>C#</asp:ListItem>
<asp:ListItem Value="XML">XML</asp:ListItem>
<asp:ListItem>SQL</asp:ListItem>
    <asp:ListItem>Python</asp:ListItem>
    <asp:ListItem>JavaScript</asp:ListItem>
    <asp:ListItem>PHP</asp:ListItem>
    <asp:ListItem>MySQL</asp:ListItem>
    <asp:ListItem>Other</asp:ListItem>
</asp:CheckBoxList>
</td>
</tr>
    <tr>
        <td class="modal-sm" style="width: 394px">&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
<tr>
<td style="width: 394px" class="modal-sm">Email Client?:</td>
<td>
    <asp:DropDownList ID="EmailClient" runat="server" OnSelectedIndexChanged="EmailClient_SelectedIndexChanged" AutoPostBack="True">
        <asp:ListItem Value="No">No</asp:ListItem>
        <asp:ListItem Value="Yes">Yes</asp:ListItem>
    </asp:DropDownList>
</td>
</tr>
<tr>
<td style="width: 394px" class="modal-sm">Name:</td>
<td>
    <asp:TextBox ID="ClientName" runat="server" Visible="False"></asp:TextBox>
    <br />
</td>
</tr>
<tr>
<td style="width: 394px" class="modal-sm">Email:</td>
<td>
    <asp:TextBox ID="ClientEmail" runat="server" Visible="False"></asp:TextBox>
</td>
</tr>
    <tr>
        <td class="modal-sm" style="width: 394px">&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
<tr>
<td style="width: 394px" class="modal-sm">&nbsp;</td>
<td>
    <asp:Button ID="Calculate" runat="server" BackColor="#A61F38" OnClick="Calculate_Click" Text="Calculate" />
</td>
</tr>
</table>

</asp:Panel>

<asp:Label ID="DisplayMsg" runat="server" style="font-size: large"></asp:Label>
</asp:Content>

