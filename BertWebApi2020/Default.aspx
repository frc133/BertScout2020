﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BertWebApi2020.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bert Web API 2020</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <a href="./api/FRCEvents">FRCEvents</a>
        </div>
        <div>
            <a href="./api/Teams">Teams</a>
        </div>
        <div>
            <a href="./api/EventTeams">Teams in each Event</a>
        </div>
        <div>
            <a href="./api/EventTeamMatches">Show matches for teams by event</a>
        </div>
        <div>
            <a href="./api/EventTeamMatches?batchInfo=NEDIST_SOUTHERNNH|0|10">Show first 10 matches for NEDIST_SOUTHERNNH</a>
        </div>
    </form>
</body>
</html>
