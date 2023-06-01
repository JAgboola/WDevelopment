<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InterviewApplication.aspx.cs" Inherits="CIS325_Master_Project.InterviewApplication.InterviewApplication" %>

<!DOCTYPE html>
<html>
<head>
    <title>New Interview - Interview App</title>
    <script src="https://cdn.tailwindcss.com?plugins=forms"></script>
    <style type="text/css">
        .auto-style1 {
            font-size: large;
        }
        .auto-style2 {
            font-weight: normal;
            font-size: large;
        }
    </style>
     <link rel="stylesheet" href="Interview.css" />
</head>
<body class="bg-orange-200">

    <div class="max-w-screen-md mx-auto">
        <h1 class="text-2xl font-bold text-center">Interview Application</h1>


        <form id="InterviewForm" method="post" runat="server">
            <div id="MainFormSection" runat="server">
            <h2 class="auto-style2"><strong>Interviewer Section:</strong></h2>
            
            <div class="mb-2">
                <label for="IName">Full Name</label>
                <div class="mt-1">
                    <asp:TextBox
                        ID="IName"
                        CssClass=""
                        runat="server" />
                </div>
            </div>

                <div class="mb-2">
                <label for="IPosition">Position</label>
                <div class="mt-1">
                    <asp:TextBox
                        ID="IPosition"
                        CssClass=""
                        runat="server" />
                </div>
            </div>

                <div class="mb-2">
                <label for="IDate">Date</label>
                <div class="mt-1">
                    <asp:TextBox
                        ID="IDate"
                        CssClass=""
                        runat="server" TextMode="Date" />
                </div>
            </div>
             
              <h2 class="auto-style2"><strong>Candidate Section:</strong></h2>

                <div class="mb-2">
                <label for="LastName">Last Name</label>
                <div class="mt-1">
                    <asp:TextBox
                        ID="LastName"
                        CssClass=""
                        runat="server" />
                </div>
            </div>

                <div class="mb-2">
                <label for="FirstName">First Name</label>
                <div class="mt-1">
                    <asp:TextBox
                        ID="FirstName"
                        CssClass=""
                        runat="server" />
                </div>
            </div>

                <div class="mb-2">
                <label for="Communication"><strong><span class="auto-style1">Communication Abilities</span></strong></label>
      
                    <asp:RadioButtonList ID="Communication" runat="server">
                        <asp:ListItem>Good</asp:ListItem>
                        <asp:ListItem>Fair</asp:ListItem>
                        <asp:ListItem>Bad</asp:ListItem>
                    </asp:RadioButtonList>
                 
                </div>
         

                <div class="mb-2">
                <label for="Skills"><strong><span class="auto-style1">Skills</span></strong></label>
                <div class="mt-1">
                    <asp:CheckBoxList ID="Skills" runat="server">
                        <asp:ListItem>ASP.NET</asp:ListItem>
                        <asp:ListItem>SQL Server</asp:ListItem>
                        <asp:ListItem>IIS</asp:ListItem>
                        <asp:ListItem>PHP</asp:ListItem>
                        <asp:ListItem>MySQL</asp:ListItem>
                        <asp:ListItem>Linux</asp:ListItem>
                        <asp:ListItem>Apache</asp:ListItem>
                        <asp:ListItem>Other</asp:ListItem>
                    </asp:CheckBoxList>
                </div>
            </div>

                <div class="mb-2">
                <label for="BusinessKnowledge"><strong><span class="auto-style1">Business Knowledge</span></strong></label>
                <div class="mt-1">
                    <asp:TextBox
                        ID="BusinessKnowledge"
                        runat="server" Height="117px" TextMode="MultiLine" Width="294px" />
                </div>
            </div>

                <div class="mb-2">
                <label for="Comments"><strong><span class="auto-style1">Comments</span></strong></label>
                <div class="mt-1">
                    <asp:TextBox
                        ID="IComments"
                        runat="server" Height="144px" TextMode="MultiLine" Width="288px" />
                </div>
            </div>
            </div>

             <asp:label runat="server" ID="DeleteMsg" Visible="false" />
            <asp:Button
                ID="SubmitButton"
                OnClick="SubmitButton_Click"
                Text="Save Interview"
                CssClass="bg-orange-600 hover:bg-orange-400 px-4 py-3 rounded text-white"
                runat="server"
                />
            <asp:Button
                ID="UpdateButton"
                OnClick="UpdateButton_Click"
                Text="Update"
                CssClass="bg-orange-600 hover:bg-orange-400 px-4 py-3 rounded text-white"
                runat="server"
                />
            <asp:Button
                ID="CancelButton"
                OnClick="CancelButton_Click"
                Text="Cancel"
                CssClass="bg-transparent border-2 hover:bg-gray-50 px-4 py-3 rounded text-gray-700"
                runat="server"
                />
    
            <asp:Button
                ID="DeleteButton"
                OnClick="DeleteButton_Click"
                Text="Delete"
                CssClass="bg-red-500 hover:bg-red-700 px-4 py-3 rounded text-white"
                runat="server"
                />
        </form>

        
            <div class="mb-2">
            <label for="ResultMsg"></label>
                <div class="mt-1">
                    <asp:Label
                        ID="ResultMsg"
                        runat="server"/>
                </div>
            </div>

            <div class="mb-2">
            <label for="ErrorMsg"></label>
                <div class="mt-1">
                    <asp:Label
                        ID="ErrorMsg"
                        runat="server"/>
                </div>
            </div>
    </div>

</body>
</html>
