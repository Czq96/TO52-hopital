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
         <td ng-if="$odd" style="background-color:#f1f1f1">{{salle.Lundi.status}} </td>
         <td ng-if="$odd" style="background-color:#f1f1f1">{{salle.Mardi.status}} </td>
         <td ng-if="$odd" style="background-color:#f1f1f1">{{salle.Mercredi.status}} </td>
         <td ng-if="$odd" style="background-color:#f1f1f1">{{salle.Jeudi.status}} </td>
         <td ng-if="$odd" style="background-color:#f1f1f1">{{salle.Vendredi.status}} </td>
     </div>
   <%--Full Name: {{data.salles[0].Lundi.status+ " " + lastName}}--%>

</div>
    
<script>
    var app = angular.module('myApp2', []).controller('myCtrl', function ($scope) {
        $scope.data = <%=data_json%>;
    });
</script>

 </asp:Content>
