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

   
 <div ng-app="myApp2" ng-controller="myCtrl">
     <div ng-repeat="salle in data.salles">
       <table id="diary" border= 1 width=500px bordercolor=#FBBF00 >
          <tr>
           <td ng-if="salle.Lundi.patient_number ==0" bgcolor="#CC4338" >{{salle.Lundi.status}} </td>  <%-- A1F081 开门无人     978e9d关门--%>
           <td ng-if="salle.Mardi.patient_number ==0" bgcolor="#978e9d"> {{salle.Mardi.status}} </td>
           <td ng-if="salle.Mercredi.patient_number ==0"   >{{salle.Mercredi.status}} </td>
           <td ng-if="salle.Jeudi.patient_number ==0" >{{salle.Jeudi.status}} </td>
           <td ng-if="salle.Vendredi.patient_number ==0" >{{salle.Vendredi.status}} </td>
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

<%--<style>
table, th , td {
  border: 1px solid grey;
  border-collapse: collapse;
  padding: 5px;
}
table tr:nth-child(odd) {
  background-color: #f1f1f1;
}
table tr:nth-child(even) {
  background-color: #ffffff;
}
</style>--%>

<script>
    var app = angular.module('myApp2', []).controller('myCtrl', function ($scope) {
        $scope.data = <%=data_json%>;
    });
</script>

 </asp:Content>
