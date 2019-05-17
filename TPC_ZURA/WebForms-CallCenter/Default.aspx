﻿<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForms_CallCenter._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <nav>
        <div class="nav-wrapper blue">
            <a href="#!" class="brand-logo">Call Center</a>
            <a href="#" data-target="mobile-demo" class="sidenav-trigger"><i class="material-icons">menu</i></a>
            <ul class="right hide-on-med-and-down">
            <li><a href="sass.html">Sass</a></li>
            <li><a href="badges.html">Components</a></li>
            <li><a href="collapsible.html">Javascript</a></li>
            <li><a href="mobile.html">Mobile</a></li>
            </ul>
        </div>
    </nav>

    <ul class="sidenav" id="mobile-demo">
    <li><a href="sass.html">Sass</a></li>
    <li><a href="badges.html">Components</a></li>
    <li><a href="collapsible.html">Javascript</a></li>
    <li><a href="mobile.html">Mobile</a></li>
    </ul>

    <main>
        <div class="container">
            <div class="row">
                <div class="col s12">
                    <div class="card">
                        <div class="card-image waves-effect waves-block waves-light">
                          <img class="activator" src="https://materializecss.com/images/office.jpg">
                        </div>
                        <div class="card-content">
                          <span class="card-title activator grey-text text-darken-4">Card Title<i class="material-icons right">more_vert</i></span>
                          <p><a href="#">This is a link</a></p>
                        </div>
                        <div class="card-reveal">
                          <span class="card-title grey-text text-darken-4">Card Title<i class="material-icons right">close</i></span>
                          <p>Here is some more information about this product that is only revealed once clicked on.</p>
                        </div>
                      </div>
                </div>
            </div>
        </div>
    </main>

</asp:Content>
