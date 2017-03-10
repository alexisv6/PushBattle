<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PushBattle._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="jumbotron">
        <h1>ASP.NET</h1>
        <p id="TextOutput" class="lead" runat="server">ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS, and JavaScript.</p>
        <p>
            <asp:Button ID="GetUserButton" runat="server" OnClick="GetUserButton_Click" Text="Button" />

        </p>
    </div>

    <div class="col-md-4">
        <div class="row-md-4">
            <h2>Getting started</h2>
            <p>
                ASP.NET Web Forms lets you build dynamic websites using a familiar drag-and-drop, event-driven model.
            A design surface and hundreds of controls and components let you rapidly build sophisticated, powerful UI-driven sites with data access.
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301948">Learn more &raquo;</a>
            </p>
        </div>
        <div class="row-md-4">
            <h2>Get more libraries</h2>
            <p>
                NuGet is a free Visual Studio extension that makes it easy to add, remove, and update libraries and tools in Visual Studio projects.
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301949">Learn more &raquo;</a>
            </p>
        </div>
        <div class="row-md-4">
            <h2>Web Hosting</h2>
            <p>
                You can easily find a web hosting company that offers the right mix of features and price for your applications.
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301950">Learn more &raquo;</a>
            </p>
        </div>
    </div>


    <script src="Scripts/jquery-1.10.2.min.js"></script>
                <script type="text/javascript">
                function GetButton() {
//                    var usrName = @HttpContext.Current.User.Identity.Name;
                    
                    var usrName = "test1";
                    $.get("/api/users/" + usrName, "",
                        function (value) {
                            alert("Name: " + usrName);
                            alert("Value: " + value.username);
                        }, "json");
                }
            </script>
    <script type="text/javascript">
        
        function simpleAlert(user) {
            alert("Text" + user.username);
            
        }

        // Always have this at the end
        //$(document).ready(simpleAlert());
    </script>

</asp:Content>
