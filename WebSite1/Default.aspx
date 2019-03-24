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
<%--   开头 gethtml（） 返回的表格 的css格式 --%>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <style type="text/css" scoped>
        #diary {
            border-collapse: collapse;
            width: 75%;
            table-layout: fixed;
            
        }

            #diary td {
                width: 30%;
                height: 50px;
                text-align: center;
                font-size: 20px;
            }
            #diary tr {
                min-height:70px;
                height: 70px;
                text-align: center;
                font-size: 20px;
            }
    </style>

    <%--  后台数据返回测试  -- %>
    <%--<asp:TextBox ID="tete" runat="server" Width="100%"></asp:TextBox>--%>

    <%-- <asp:Button ID="yyy" runat="server" OnClick="yyy_Click" /> --%>
    <div id="test"><%=gethtml()%></div>
    <%--<div  ng-app>
    Angularjs TEST
    Name: <input type=text ng-model="name">
    <br>
    Current user's name: {{name}}
    </div>--%>
    
<%-- json 文件查看  --%>
<%--    <div id="bar"><%=data_json%></div>--%>
    <div>----------------------------   angularJS Test----------------------------------------------</div>

    <style type="text/css" scoped>
        .ferme {
            background-color: #978e9d;
            width : 30px;
        }

        .ouvert {
            background-color: #A1F081;
            width : 30px;
        }

        .occupe {
            background-color: #CC4338;
            width : 30px;
        }

        .default_select {
            width: 10px;
        }
    </style>

    <div ng-app="myApp2" ng-controller="myCtrl">
        <div >
            <table id="diary" border="1"  bordercolor="#FBBF00">
                <tr><td ></td><td ><center>Lundi</td><td><center>Mardi  </td><td>   Mecredi </td><td>   Jeudi  </td><td>   Vendredi  </td></tr>
                <tr ng-repeat="salle in data.salles">
                    <td>salle {{salle.Number}}:</td>
                    <td class="{{salle.Lundi.status}}">
                        <label>
                            <select width="80px" ng-model="myselect1" onchange="alert(this.value)" ng-if="salle.Lundi.patient_number != 0"
                                ng-options="patient.id as patient.id for patient in salle.Lundi.patients">
                                <option value="">{{salle.Lundi.patient_number}} patients</option>
                                <%--veuille--%>
                            </select>
                        </label>
                        {{salle.Lundi.status}}
                    </td>
                    <td class="{{salle.Mardi.status}}">
                        <label>
                            <select width="80px" ng-model="myselect1" onchange="alert(this.value)" ng-if="salle.Mardi.patient_number != 0"
                                ng-options="patient.id as patient.id for patient in salle.Mardi.patients">
                                <option value="">{{salle.Mardi.patient_number}} patients</option>
                            </select>
                        </label>
                        {{salle.Mardi.status}} </td>
                    <td class="{{salle.Mercredi.status}}">
                        <label>
                            <select width="80px" ng-model="myselect1" onchange="alert(this.value)" ng-if="salle.Mercredi.patient_number != 0"
                                ng-options="patient.id as patient.id for patient in salle.Mercredi.patients">
                                <option value=""> {{salle.Mercredi.patient_number}} patients</option>
                            </select>
                        </label>
                        {{salle.Mercredi.status}} </td>
                    <td class="{{salle.Jeudi.status}}">
                        <label>
                            <select width="80px" ng-model="myselect1" onchange="alert(this.value)" ng-if="salle.Jeudi.patient_number != 0"
                                ng-options="patient.id as patient.id for patient in salle.Jeudi.patients">
                                <option value="">{{salle.Jeudi.patient_number}} patients</option>
                            </select>
                        </label>
                        {{salle.Jeudi.status}} </td>
                    <td class="{{salle.Vendredi.status}}">
                        <label>
                            <select width="80px" ng-model="myselect1" onchange="alert(this.value)" ng-if="salle.Vendredi.patient_number != 0"
                                ng-options="patient.id as patient.id for patient in salle.Vendredi.patients">
                                <option value="">{{salle.Vendredi.patient_number}} patients</option>
                            </select>
                        </label>
               {{salle.Vendredi.status}} </td>
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
