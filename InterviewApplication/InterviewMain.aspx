<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InterviewMain.aspx.cs" Inherits="CIS325_Master_Project.InterviewApplication.WebForm1" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Home - Interviews</title>
    <script src="https://cdn.tailwindcss.com?plugins=forms"></script>
    <link rel="stylesheet" href="Interview.css" />
</head>
<body class="bg-gray-100">


    <div class="w-3/4 mx-auto">
        <h1 class="text-2xl font-bold text-center">Interview Applications</h1>
        <div class="flex justify-end">
            <a href="InterviewApplication.aspx" class="bg-red-800 hover:bg-gray-500 px-4 py-3 rounded text-white">New Interview </a>
        </div>

        <form class="w-full flex mt-3"   runat="server">
            <asp:TextBox placeholder="Search Interviews" runat="server" ID="search" CssClass="block w-full border rounded" />
            <asp:DropDownList runat="server" CssClass="ml-2 shrink-0 rounded border-b py-2 pl-3 pr-10" ID="filter">
                <asp:ListItem Text="Last Name, First Name (Exact Match)" Value="Name" />
                <asp:ListItem Text="Skills (Keyword)" Value="Skills" />
            </asp:DropDownList>
            <asp:Button runat="server" ID="submit" OnClick="Submit_Click" Text="Search" CssClass="border border-gray-500 rounded bg-gray-200 px-4 py-2 hover:bg-gray-500 text-gray-900 
                hover:text-white ml-2 hover:cursor-pointer" />
        </form>

        <table class="mt-3">
            <thead class="bg-orange-200 border-b-2 border-gray-300">
                <tr class="divide-x divide-gray-300">
                    <th>Interviewer Name</th>
                    <th>Interviewer Position</th>
                    <th>Date</th>
                    <th>Last Name</th>
                    <th>First Name</th>
                    <th>Communication Abilities</th>
                    <th colspan="3">Actions</th>
                </tr>
            </thead>
            <tbody class="bg-white">
                <% 
                   foreach (System.Data.DataRow row in userData.Rows)
                   {
                %>
                <tr class="divide-x border-b">
                    
                    <td><%=row["IName"] %></td>
                    <td><%=row["IPosition"] %></td>
                    <td><%=System.DateTime.Parse(row["IDate"].ToString()).ToShortDateString()%></td>
                    <td><%=row["LastName"] %></td>
                    <td><%=row["FirstName"] %></td>
                    <td><%=row["Communication"] %></td>
                    <!-- Action and ID are the query string properties -->
                    <td><a href="InterviewApplication.aspx?Action=View&ID=<%=row["AppID"]%>">View</a></td>
                    <td><a href="InterviewApplication.aspx?Action=Edit&ID=<%=row["AppID"]%>">Edit</a></td>
                    <td><a href="InterviewApplication.aspx?Action=Delete&ID=<%=row["AppID"]%>" class="del">Delete</a></td>
                </tr>
                <%
                   }
                %>
            </tbody>
        </table>
    </div>

</body>
</html>