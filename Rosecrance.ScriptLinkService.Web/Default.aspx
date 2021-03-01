<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Rosecrance.Diagnosis.Web._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Current WSDL</h1>
        <p class="lead">We are currently using the OptionObject2015 WSDL with myAvatar.</p>
        <a href="/Api/v3/Diagnosis.asmx?WSDL" class="btn btn-primary btn-lg">View now &raquo;</a>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Getting started</h2>
            <p>
                ScriptLinkService by Rosecrance Health Network is developed on ASP.NET (C#). The code is managed on Azure DevOps. To get started, launch Visual Studio and clone the Rosecrance.ScriptLinkService repository.
            </p>
            <p>
                <a class="btn btn-secondary" href="https://dev.azure.com/rosecrance/Rosecrance.ScriptLinkService" target="_blank">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>AvatarScriptLink.NET</h2>
            <p>
                AvatarScriptLink.NET is a free-to-use NuGet package to help accelerate ScriptLink development by simplifying the management and modification of OptionObjects.
            </p>
            <p>
                <a class="btn btn-secondary" href="https://rarelysimple.github.io/RarelySimple.AvatarScriptLink/" target="_blank">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Web Hosting</h2>
            <p>
                The application is hosted in the Netsmart cloud and deployed using Web Deploy within Visual Studio. As long you can reach the Netsmart web server and have been given permission, you can deploy right from Visual Studio.
            </p>
            <p>
                <a class="btn btn-secondary" href="https://www.iis.net/downloads/microsoft/web-deploy" target="_blank">Learn more &raquo;</a>
            </p>
        </div>
    </div>

</asp:Content>
