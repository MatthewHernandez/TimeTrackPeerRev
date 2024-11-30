<%@ Page Title="Peer Review" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Review.aspx.cs" Inherits="tt_pr.Review" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        table {
            width: 100%;
            border-collapse: collapse;
        }
        th, td {
            padding: 15px;
            text-align: center;
        }
        th {
            background-color: #f2f2f2;
        }
        .submit-button {
            display: flex;
            justify-content: center;
            margin-top: 20px;
        }
    </style>
    <main aria-labelledby="title">
        <h2 id="title"><%: Title %></h2>
        <table>
            <thead>
                <tr>
                    <th></th>
                    <th>Quality of Work</th>
                    <th>Timeliness</th>
                    <th>Communication</th>
                    <th>Teamwork</th>
                    <th>Effort and Participation</th>
                </tr>
            </thead>
            <tbody>
                <% for (int i = 1; i <= 6; i++) { %>
                <tr>
                    <td>Member <%= i %></td>
                    <% for (int j = 0; j < 5; j++) { %>
                    <td>
                        <asp:DropDownList ID="DropDownList1" runat="server">
                            <asp:ListItem Text="0" Value="0"></asp:ListItem>
                            <asp:ListItem Text="1" Value="1"></asp:ListItem>
                            <asp:ListItem Text="2" Value="2"></asp:ListItem>
                            <asp:ListItem Text="3" Value="3"></asp:ListItem>
                            <asp:ListItem Text="4" Value="4"></asp:ListItem>
                            <asp:ListItem Text="5" Value="5"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <% } %>
                </tr>
                <% } %>
            </tbody>
        </table>
        <div class="submit-button">
            <asp:Button ID="SubmitButton" runat="server" Text="Submit" OnClientClick="alert('Submission received'); return false;" />
        </div>
    </main>
</asp:Content>