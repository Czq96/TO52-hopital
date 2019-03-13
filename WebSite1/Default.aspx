<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%--<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
          <hgroup class="title">
                <h1><%: Title %>.</h1>
                <h2>Modify this template to jump-start your ASP.NET application.</h2>
            </hgroup>
            <p>
                To learn more about ASP.NET, visit <a href="https://asp.net" title="ASP.NET Website">https://asp.net</a>. 
                The page features <mark>videos, tutorials, and samples</mark> to help you get the most from 
                ASP.NET. If you have any questions about ASP.NET visit 
                <a href="https://forums.asp.net/18.aspx" title="ASP.NET Forum">our forums</a>.
            </p>
      </div>
    </section>
</asp:Content>--%>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <style type="text/css" scoped>
        #diary {
				border-collapse: collapse;
				width: 50%;
				}
        #diary td{
                    width: 10%;
				    height: 50px;
					text-align: center;
					font-size:20px;
        }
    </style>

    <asp:TextBox ID="tete" runat="server" Width="100%"></asp:TextBox>
    
    <%-- <asp:Button ID="yyy" runat="server" OnClick="yyy_Click" /> --%>
    <div id ="test"><%=gethtml()%></div>
    <%--<div  ng-app>
    Angularjs TEST
    Name: <input type=text ng-model="name">
    <br>
    Current user's name: {{name}}
    </div>--%>
  <%--<div id="bar"><%=data_json%></div>--%>
  <div>---------------------------------------------------------------------------------------------</div>

<style>
    .ferme {
        background-color:#978e9d;
        bgcolor:#978e9d;
        }
    .ouvert {
        background-color:#A1F081;
        bgcolor:#A1F081;
    }
    .occupe {
        background-color:#CC4338;
    }
</style>
 
 <div ng-app="myApp2" ng-controller="myCtrl">
     <div ng-repeat="salle in data.salles">
       <table id="diary" border= 1 width=500px bordercolor=#FBBF00 >
          <tr>
           <td class="{{salle.Lundi.status}}">{{salle.Lundi.status}} </td>  <%-- #CC4338有人   A1F081 开门无人     978e9d关门--%>
           <td class="{{salle.Mardi.status}}"> {{salle.Mardi.status}} </td>
           <td class="{{salle.Mercredi.status}}">{{salle.Mercredi.status}} </td>
           <td class="{{salle.Jeudi.status}}">{{salle.Jeudi.status}} </td>
           <td class="{{salle.Vendredi.status}}">{{salle.Vendredi.status}} </td>
          </tr> 
    <%--<tbody bs-loading-overlay bs-loading-overlay-reference-id=="{{filter.name}}" bs-loading-overlay-delay="filter.loadingDelay">
        <tr ng-if="filter.match_items.length == 0 && !filter.firstRun">
            <td class="bg-white" colspan="{{filter.colspan}}" style="min-height:200px;">
                <h2 class="text-center">
                    <span ng-include="filter.cfg.info_tpl"></span>
                </h2>
            </td>
        </tr>
        <!--<tr ng-class="filter.cfg.row_classes" st-select-row="row" st-select-mode="multiple" ng-repeat="row in filter.match_items" ng-include="filter.cfg.row_tpl"></tr>-->
        <tr ng-class="{{filter.cfg.row_classes}}" ng-repeat="row in filter.match_items" ng-include="filter.cfg.row_tpl"></tr>
    </tbody>--%>
</table> 
     </div>
   <%--Full Name: {{data.salles[0].Lundi.status+ " " + lastName}}--%>

</div>

<script>
    var app = angular.module('myApp2', []).controller('myCtrl', function ($scope) {
        $scope.data = <%=data_json%>;
    });
</script>

 </asp:Content>
