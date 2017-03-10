<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PushBattle._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <div id="introcontainer" class="jumbotron">
        <h1 class="text-primary text-center">Welcome to PushBattle</h1>
        <h3 onclick="FakeSignIn()" class="text-warning text-center">Please register or sign in.</h3>
        <h3 class="text-center">Push Battle is the fun way to DDoS your friends until they disable this app</h3>
    </div>
    <div id="contentcontainer" class="jumbotron container" style="display:none">
        <div class="row">
            <div id="sidebar" class="col-md-4">
                <div id="userView" class="container">
                    <div id="userInfo" class="row modules">
                            <h2 id="usernameText">User Information</h2>
                            <ul class="list-group">
                                <li id="username" class="list-group-item"></li>
                                <li id="email" class="list-group-item"></li>
                                <li id="phone" class="list-group-item"></li>
                            </ul>
                    </div>
                    <div id="teamInfo" class="row modules" style="display:none">
                            <h2 id="teamText">Team Information</h2>
                            <ul class="list-group">
                                <li id="teamname" class="list-group-item"></li>
                                <li id="wins" class="list-group-item"></li>
                                <li id="losses" class="list-group-item"></li>
                            </ul>
                    </div>
                </div>
            </div>
            <div id="battlearea" class="col-md-8" style="display:none">
                <div id="battleView">
                    <h1>Current Battle</h1>
                    <div class="container">
                        <div class="row">
                            <div class="col-md-6">
                                <label class="labels">Team1</label>
                            </div>
                            <div class="col-md-6">
                                <label class="labels">Team1</label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div id='resultsContainer' style="display:none; border:solid 3px black">
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div id='resultsContainer2' style="display:none; border:solid 3px black">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
         </div>    
     </div>


  <%--  <div class="jumbotron">
        <h1>Load A Battle</h1>
        <p class="lead">Enter the battle id below</p>
        <input id="battleInput" type="text" value=""/>
        <input id="loadBattle" type="button" value="Load Battle" onclick="LoadBattle()"/>
    </div>

    <div class="jumbotron">
        <h1>Load Users By Team</h1>
        <p class="lead">This is a test</p>
        <input id="teamInput" type="text" value=""/>
        <input id="loadUsersButton" type="button" value="Load Users" onclick="LoadUsers()"/>
    </div>--%>

    <script src="Scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript">
        var token = "";
        function FakeSignIn() {
            var user = {
                "username": "alexisv6",
                "email": "alexisv6",
                "phoneNumber": "5098815865"
            }
            parseUserData(user);
            FakeTeam();
        }

        function FakeTeam() {
            var team = {
                "teamname": "the cool team",
                "wins": "12",
                "losses": "2"
            }
            parseTeamData(team);
            FakeBattle();
        }

        function FakeBattle() {
            var battle = {
                
            }
            parseBattleData(battle);
        }

        function LoadUsers() {
            var url = "api/users/team/" + $("#teamInput").val();
            $.get(url, function (items) {
                console.dir(items);
                console.log(items);
                parseUsers(items)
            })
        }

        function parseUsers(data) {
            var content = "<table class=\"table\">";
            for (var i = 0; i < data.length; i++) {
                if (i % 2 == 0) {
                    content += '<tr class="success">'
                } else {
                    content += '<tr>';
                }
                content += '<td style="font-size:15px"><code>' + data[i].username + '</code></td>';
                content += '<td style="font-size:15px"><code>' + data[i].teamId + '</code></td>';
                content += '</tr>'
            }
            content += "</table>";

            $('#resultsContainer').append(content);
            $('#resultsContainer').show();
        }

        function simpleAlert() {
            alert("Text");
        }
        function GetButton() {
            $.get("/api/users/test1", "",
                function (value) {
                    alert("Value: " + value.username);
                    token = value.username;
                }, "json")
        }
        function parseUserData(user) {
            console.dir(user);
            $("#introcontainer").hide();
            $("#contentcontainer").show();
            $("#username").html("Username: " + user.username);
            $("#email").html("Email: " + user.email);
            $("#phone").html("Phone #: " + user.phoneNumber);
        }

        function parseTeamData(team) {
            $("#teamInfo").show();
            $("#teamname").html("Teamname: " + team.teamname);
            $("#wins").html("Total Wins: " + team.wins);
            $("#losses").html("Total Losses: " + team.losses);
        }

        function parseBattleData(battle) {
            $("#battlearea").show();
        }
        // Always have this at the end
        //$(document).ready(simpleAlert());
    </script>

</asp:Content>
