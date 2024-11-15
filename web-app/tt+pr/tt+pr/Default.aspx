<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="tt_pr._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .login-container {
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
        }
        .login-form {
            display: flex;
            flex-direction: column;
            align-items: center;
            width: 300px;
            padding: 20px;
            border: 1px solid #ccc;
            border-radius: 10px;
            background-color: #f9f9f9;
        }
        .login-form > * {
            margin-bottom: 15px;
            width: 100%;
        }
        .login-form label {
            text-align: left;
            width: 100%;
        }
        .login-form input[type="text"],
        .login-form input[type="password"] {
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }
        .login-form input[type="submit"] {
            padding: 10px;
            border: none;
            border-radius: 5px;
            background-color: #007bff;
            color: white;
            cursor: pointer;
        }
        .login-form input[type="submit"]:hover {
            background-color: #0056b3;
        }
    </style>
    <div class="login-container">
        <div class="login-form">
            <asp:Label ID="lblNetID" runat="server" Text="Net ID:"></asp:Label>
            <asp:TextBox ID="txtNetID" runat="server"></asp:TextBox>
            <asp:Label ID="lblUTDID" runat="server" Text="UTD-ID:"></asp:Label>
            <asp:TextBox ID="txtUTDID" runat="server" TextMode="Password"></asp:TextBox>
            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
        </div>
    </div>
</asp:Content>

