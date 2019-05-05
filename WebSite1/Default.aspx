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
                min-height: 70px;
                height: 70px;
                text-align: center;
                font-size: 20px;
            }
    </style>

    <%--  后台数据返回测试  -- %>
    <%--<asp:TextBox ID="tete" runat="server" Width="100%"></asp:TextBox>--%>
    <div id="description">
        <br />
        choose patient blocks：
        <select style="width: 200px;" name="patientsDoc" id="patientsDoc">
            <span style="white-space: pre"></span>
            <option value="patients2blocks">patients2blocks</option>
            <option value="patients2ors">patients2ors</option>
        </select>
    </div>
    <%-- <asp:Button ID="yyy" runat="server" OnClick="yyy_Click" /> --%>
    
   

     <div>        
         <input type="button" id="btn_ModifyNickName" runat="server" value="打开模态窗口"  style="width: 126px;" onclick="OpenSelectInfo()" />   
          
    </div>
    <div id="inforBlock" background-color: white; border: 1px solid black;">test悬浮窗</div>
    <select onclick="javascript:alert('event has been triggered')">
         <option  value ="1">1</option>
         <option  value ="2">2</option>
         <option  value ="3">3</option>
    </select>
    <div id="divNewBlock" style=" border:solid 5px;padding:10px;width:400px;
        position: absolute; display:none;top:15%;right:0%;">
            <div style="padding:3px 15px 3px 15px;text-align:left;vertical-align:middle;" >
                  <div class="row">
        <div class="col-md-2"></div>  
        <div class="col-md-5">
               <div class="row " style=" font-size:medium">Information de la patient</div>
             
            <table border="1">
                <tr style="background-color :#808080; color :white">
                    <td><b> patient numéro: </b></td>
                    <td>  <asp:TextBox ID="number" runat="server"></asp:TextBox></td>
                    </tr>
                <tr style="background-color :#808080; color :white"">
                    <td><b> patient name:  </b></td>
                    <td>  <asp:TextBox ID="name" runat="server"></asp:TextBox></td>
                </tr>
                <tr style="background-color :#808080; color :white"">
                    <td><b> patient departement: </b></td>
                    <td>    <asp:TextBox ID="departement" runat="server"></asp:TextBox></td>
                </tr>
                <tr style="background-color :#808080; color :white"">
                    <td><b>  urgency level: </b></td>
                    <td>    <asp:TextBox ID="urgencyLevel" runat="server"></asp:TextBox></td>
                </tr>
                <tr style="background-color :#808080; color :white"">
                    <td><b> waiting time: </b></td>
                    <td>    <asp:TextBox ID="waitingTime" runat="server"></asp:TextBox></td>
                </tr>
                <tr style="background-color :#808080; color :white"">
                    <td><b> max waiting time: </b></td>
                    <td>    <asp:TextBox ID="maxWaitingTime" runat="server"></asp:TextBox></td>
                </tr>

            </table>
            </div>
            </div>
                <div>     
                  
                    <asp:Button ID="BttCancel"  runat="server" Text="fermer" OnClientClick="return HideBlock();" />
                </div>
            </div>
      </div> 


    <div id="test"><%=gethtml()%></div> <%-- 这里显示所有的表格数据--%>
    <script>
        var infoDiv = document.getElementById("inforBlock");
        function displayPatientInfo(obj)
        {
            var x = selects.offsetLeft, y = obj.offsetTop, h = obj.offsetHeight, w = selects.offsetWidth;
            infoDiv.style.marginLeft = x + w; infoDiv.style.marginTop = y - 30;
            infoDiv.innerHTML="";
            infoDiv.style.display = 'block';
        }

        function vanishPatientInfo()
        {
            infoDiv.style.display = 'none';
        }
    </script>
    <script>
        function HideBlock() {
            document.getElementById("divNewBlock").style.display = "none";
            return false;
        }


        function ShowBlock(c) {
            var set = SetBlock();
            var m = c.split(",")
            document.getElementById("<%=number.ClientID%>").value = m[0];    
            document.getElementById("<%=name.ClientID%>").value = m[1];   
            document.getElementById("<%=departement.ClientID%>").value = m[2];   
            document.getElementById("<%=urgencyLevel.ClientID%>").value = m[3];   
            document.getElementById("<%=waitingTime.ClientID%>").value = m[4];   
            document.getElementById("<%=maxWaitingTime.ClientID%>").value = m[5];   

          

          
            document.getElementById("divNewBlock").style.display = "";
            return false;
        }



        function SetBlock() {
            var top = document.body.scrollTop;
            var left = document.body.scrollLeft;
            var height = document.body.clientHeight;
            var width = document.body.clientWidth;


            if (top == 0 && left == 0 && height == 0 && width == 0) {
                top = document.documentElement.scrollTop;
                left = document.documentElement.scrollLeft;
                height = document.documentElement.clientHeight;
                width = document.documentElement.clientWidth;
            }
            return { top: top, left: left, height: height, width: width };
        }


        function Operate() {
            return false;
        }
    </script>


    <script type="text/javascript">
        // fix for deprecated method in Chrome 37
        if (!window.showModalDialog) {
            window.showModalDialog = function (arg1, arg2, arg3) {

                var w;
                var h;
                var resizable = "no";
                var scroll = "no";
                var status = "no";

                // get the modal specs
                var mdattrs = arg3.split(";");
                for (i = 0; i < mdattrs.length; i++) {
                    var mdattr = mdattrs[i].split(":");

                    var n = mdattr[0];
                    var v = mdattr[1];
                    if (n) { n = n.trim().toLowerCase(); }
                    if (v) { v = v.trim().toLowerCase(); }

                    if (n == "dialogheight") {
                        h = v.replace("px", "");
                    } else if (n == "dialogwidth") {
                        w = v.replace("px", "");
                    } else if (n == "resizable") {
                        resizable = v;
                    } else if (n == "scroll") {
                        scroll = v;
                    } else if (n == "status") {
                        status = v;
                    }
                }

                var left = 100;
                var top = 100;
                var targetWin = window.open(arg1, arg1, 'toolbar=no, location=no, directories=no, status=' + status + ', menubar=no, scrollbars=' + scroll + ', resizable=' + resizable + ', copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
                targetWin.focus();
            };
        }
</script>
     <script type="text/javascript">

         function OpenSelectInfo() {
             var width = 50;  //模态窗口的宽度
             var height = 50;   //模态窗口的高度
             var url = "patient.aspx?id=3"; //模态窗口的url地址
             returnValue = window.showModalDialog(url, null, 'dialogWidth=' + width + 'px;dialogHeight=' + height + 'px;help:no;status:yes;center: yes');
             
         }
    </script>


<%--    <script>
        function sech(id) {//改变文件的选择时触发
            var aa = document.getElementById(id);
            var c = aa.selectedIndex;//获得改变后该省的索引号
            document.getElementById("HiddenField1").value = aa.options[c].text;//将选中的text赋值给HiddenField1的Value;
        }
    </script>--%>


    <%--<div  ng-app>
    Angularjs TEST
    Name: <input type=text ng-model="name">
    <br>
    Current user's name: {{name}}
    </div>--%>

   
    <div>----------------------------   angularJS Test----------------------------------------------</div>
    json 文件查看
    <div id="bar"><%=data_json%>
    <style type="text/css" scoped>
        .ferme {
            background-color: #978e9d;
            width: 30px;
        }

        .ouvert {
            background-color: #A1F081;
            width: 30px;
        }

        .occupe {
            background-color: #CC4338;
            width: 30px;
        }

        .default_select {
            width: 10px;
        }
    </style>

    <div ng-app="myApp2" ng-controller="myCtrl">
        <div>
            <table id="diary" border="1" bordercolor="#FBBF00">
                <tr>
                    <td></td>
                    <td>
                        <center>
                        Lundi</td>
                    <td>
                        <center>
                        Mardi  </td>
                    <td>Mecredi </td>
                    <td>Jeudi  </td>
                    <td>Vendredi  </td>
                </tr>
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
                                <option value="">{{salle.Mercredi.patient_number}} patients</option>
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
