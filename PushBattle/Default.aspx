<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PushBattle._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<div id="introcontainer" class="jumbotron">
  <h1 class="text-primary text-center">Welcome to PushBattle</h1>
  <h3 onclick="FakeSignIn()" class="text-warning text-center">Please register or sign in.</h3>
  <h3 class="text-center">Push Battle is the fun way to DDoS your friends until they disable this app</h3>
</div>
<div class="jumbotron">
   <h1>ASP.NET</h1>
   <p id="TextOutput" class="lead" runat="server">ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS, and JavaScript.</p>
   <p>
    <asp:Button ID="GetUserButton" runat="server" OnClick="GetUserButton_Click" Text="Button" />

   </p>
</div>
    <div id="contentcontainer" class="jumbotron container" style="display:none">
        <div class="row">
            <div id="sidebar" class="col-md-4">
                <div id="userView" class="container">
                    <div id="userInfo" class="row panel panel-default">
                        <div class="panel-heading">
                            <h2 id="usernameText">User Information</h2>
                        </div>
                        <div class="panel-body">
                             <ul class="list-group">
                                <li id="username" class="list-group-item"></li>
                                <li id="score" class="list-group-item"></li>
                                <li class="list-group-item">Contributions<span id="contributionsAmt" class="badge"></span></li>
                            </ul>
                        </div>
                    </div>
                    <div id="teamInfo" class="row panel panel-default" style="display:none">
                        <div class="panel-heading">
                            <img width="70" height="70" src="https://s3.amazonaws.com/pushbattle/cat-icon-34376.png">
                        </div>
                        <div class="panel-body">
                            <ul class="list-group">
                                <li id="teamname" class="list-group-item"></li>
                            </ul>
                            <h2>Team Memebers</h2>
                            <div id="teamMemebers"></div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="battlearea" class="col-md-8" style="display:none">
                <div id="newBattleView" class="panel panel-primary">
                    <div class="panel-heading">
                        <h2>Start a battle!</h2>
                    </div>
                    <div class="panel-body">
                        <p><label onclick="SimulateNewBattle()">Choose a team to battle:</label></p>
                        <p><input type="radio" name="challengeTeam" value="Red">Red Team</p>
                        <p><input type="radio" name="challengeTeam" value="Blue">Blue Team</p>
                        <p><input type="radio" name="challengeTeam" value="Green">Green Team</p>
                        <p><input type="radio" name="challengeTeam" value="Yellow">YellowTeam</p>
                        <p><button class="btn-danger">Start A War</button></p>
                    </div>
                </div>
                <div id="battleView">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h2>Current Battle</h2>
                        </div>
                        <div class="panel-body">
                            <div class="col-md-6">
                                <h2 id="team1">Team1</h2>
                            </div>
                            <div class="col-md-6">
                                <h2 id="team2">Team1</h2>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-md-6">
                                <h2 id="score1"></h2>
                            </div>
                            <div class="col-md-6">
                                <h2 id="score2"></h2>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="col-md-6">
                                <div id='resultsContainer1' style="display:none;">
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div id='resultsContainer2' style="display:none">
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
        var token = "";
        function FakeSignIn() {
            var user = {
                "username": "alexisv6",
                "score": "20",
                "phoneNumber": "5098815865"
            }
            parseUserData(user);
            FakeTeam();
        }

        function FakeTeam() {
            var team = {
                "teamname": "firstteam",
                "teamMembers": ["alexisv6", "test1", "test2"],
                "losses": "2"
            }
            parseTeamData(team);
            FakeBattle();
        }

        function FakeBattle() {
            parseBattleData(null);
        }

        function SimulateNewBattle() {
            var battle = {
                "battleId": "testbattle",
                "participants": ["firstteam", "secondteam"],
                "scores": ["1320", "3210"]
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

        function parseTeamMembers(data) {
            var content = "<table class=\"table\">";
            for (var i = 0; i < data.length; i++) {
                if (i % 2 == 0) {
                    content += '<tr class="success">'
                } else {
                    content += '<tr>';
                }
                content += '<td style="font-size:12px"><code>' + data[i] + '</code></td>';
                content += '</tr>'
            }
            content += "</table>";

            $('#teamMemebers').append(content);
            $('#teamMemebers').show();
        }

        function parseUsers(data) {
            var content = "<table class=\"table\">";
            for (var i = 0; i < data.length; i++) {
                if (i % 2 == 0) {
                    content += '<tr class="success">'
                } else {
                    content += '<tr>';
                }
                content += '<td style="font-size:12px"><code>' + data[i].username + '</code></td>';
                content += '<td style="font-size:12px"><code>' + data[i].teamId + '</code></td>';
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
            $("#score").html("Score: " + user.score);
            $.get("/api/contributions/user/" + user.username, function (data) {
                $("#contributionsAmt").html(data.length);
            }, "json");
        }

        function parseTeamData(team) {
            $("#teamInfo").show();
            $("#teamname").html(team.teamname);
            parseTeamMembers(team.teamMembers);
        }

        function parseBattleData(battle) {
            $("#battlearea").show();
            if (battle == null) {
                $("#battleView").hide();
                $("#newBattleView").show();
            } else {
                $("#newBattleView").hide();
                $("#battleView").show();
                $("#team1").html(battle.participants[0]);
                $("#team2").html(battle.participants[1]);
                $("#score1").html("Score: " + battle.scores[0]);
                $("#score2").html("Score: " + battle.scores[1]);
                $.get("api/contributions/team/" + battle.participants[0] + "/battle/" + battle.battleId, function (data) {
                    printTeam1Contributions(data);
                });
                $.get("api/contributions/team/" + battle.participants[1] + "/battle/" + battle.battleId, function (data) {
                    printTeam2Contributions(data);
                });
            }
        }

        function printTeam1Contributions(data) {
            var content = "<table class=\"table\">";
            content += '<td style="font-size:20px">Contributions Made</td>';
            for (var i = 0; i < data.length; i++) {
                if (i % 2 == 0) {
                    content += '<tr class="success">'
                } else {
                    content += '<tr>';
                }
                content += '<td style="font-size:12px"><code>' + data[i].username + '</code></td>';
                content += '</tr>'
            }
            content += "</table>";

            $('#resultsContainer1').append(content);
            $('#resultsContainer1').show();
        }

        function printTeam2Contributions(data) {
            var content = "<table class=\"table\">";
            content += '<td style="font-size:20px">Contributions Made</td>';
            for (var i = 0; i < data.length; i++) {
                if (i % 2 == 0) {
                    content += '<tr class="success">'
                } else {
                    content += '<tr>';
                }
                content += '<td style="font-size:12px"><code>' + data[i].username + '</code></td>';
                content += '</tr>'
            }
            content += "</table>";

            $('#resultsContainer2').append(content);
            $('#resultsContainer2').show();
        }
        // Always have this at the end
        //$(document).ready(simpleAlert());
    </script>

</asp:Content>
