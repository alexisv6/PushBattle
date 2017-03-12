<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PushBattle._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="introcontainer" class="jumbotron">
  <h1 class="text-primary text-center">Welcome to PushBattle</h1>
  <h3 class="text-center">Push Battle is the fun way to DDoS your friends until they disable this app
        </h3>
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
                            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
                        </div>
                    </div>
                    <div id="teamInfo" class="row panel panel-default" style="display:none">
                        <div class="panel-heading">
                            <img id="teamImage" width="70" height="70"/>
                        </div>
                        <div class="panel-body">
                            <ul class="list-group">
                                <li id="teamname" class="list-group-item"></li>
                            </ul>
                            <h2>Team Members</h2>
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
                        <asp:RadioButtonList ID="ChallengeTeam" runat="server">
                            <asp:ListItem Text="Red" Value="Red" ></asp:ListItem>
                            <asp:ListItem Text="Blue" Value="Blue"></asp:ListItem>
                            <asp:ListItem Text="Green" Value="Green"></asp:ListItem>
                            <asp:ListItem Text="Yellow" Value="Yellow"></asp:ListItem>
                        </asp:RadioButtonList><%--<p><button class="btn-danger">Start A War</button></p>--%>
                        <p><asp:Button CssClass="btn-danger" ID="DoBattle" runat="server" Text="Start a War" OnClick="DoBattle_Click" /></p>
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

    <script src="Scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript">

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
                content += '<td style="font-size:10px"><code>' + data[i] + '</code></td>';
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
                content += '<td style="font-size:10px"><code>' + data[i].username + '</code></td>';
                content += '<td style="font-size:10px"><code>' + data[i].teamId + '</code></td>';
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
            $("#teamImage").attr("src", team.imageUrl);
            parseTeamMembers(team.members);
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
                $("#score1").html("Score: " + ((battle.scores[0] )*battle.offsets[0]));
                $("#score2").html("Score: " + ((battle.scores[1] )*battle.offsets[1]));
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
                content += '<td style="font-size:10px"><code>' + data[i].username + '</code></td>';
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
                content += '<td style="font-size:10px"><code>' + data[i].username + '</code></td>';
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
