<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

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
    <div id="divNewBlock" style="border: solid 5px; padding: 10px; width: 400px; position: absolute; display: none; top: 5%; right: 0%;">
        <div style="padding: 3px 15px 3px 15px; text-align: left; vertical-align: middle;">

            <div style="font-size: larger; text-align: center">Information de la patient</div>

            <table>
                <tr style="background-color: #808080; color: white">
                    <td><b>numéro: </b></td>
                    <td>
                        <asp:TextBox ID="number" runat="server"></asp:TextBox></td>
                </tr>
                <tr style="background-color: #808080; color: white">
                    <td><b>nom:  </b></td>
                    <td>
                        <asp:TextBox ID="name" runat="server"></asp:TextBox></td>
                </tr>
                <tr style="background-color: #808080; color: white">
                    <td><b>departement: </b></td>
                    <td>
                        <asp:TextBox ID="departement" runat="server"></asp:TextBox></td>
                </tr>
                <tr style="background-color: #808080; color: white">
                    <td><b>niveau d'urgence: </b></td>
                    <td>
                        <asp:TextBox ID="urgencyLevel" runat="server"></asp:TextBox></td>
                </tr>
                <tr style="background-color: #808080; color: white">
                    <td><b>temps d'attendre: </b></td>
                    <td>
                        <asp:TextBox ID="waitingTime" runat="server"></asp:TextBox></td>
                </tr>
                <tr style="background-color: #808080; color: white">
                    <td><b>max temps d'attendre: </b></td>
                    <td>
                        <asp:TextBox ID="maxWaitingTime" runat="server"></asp:TextBox></td>
                </tr>
            </table>
        </div>

        <div>
            <asp:Image ID="icuImage" runat="server"></asp:Image>
        </div>

        <div>
            <asp:Button ID="BttCancel" runat="server" Text="fermer" OnClientClick="return HideBlock();" />
        </div>
    </div>

    <div id="test"><%=gethtml()%></div>
    <br><br><br><br>
    
    <div id="Tableau" style="font-size:30px;text-align:center" >Tableau statistique</div>
    <div id ="resultImage1">
        <img src="<%=getSalleImagePath()%>">
        <img src="<%=getDayImagePath()%>">
    </div>

    <div>
        <img src="<%=getSpecialityImagePath()%>">
    </div>
    <script>
        var infoDiv = document.getElementById("inforBlock");
        function displayPatientInfo(obj) {
            var x = selects.offsetLeft, y = obj.offsetTop, h = obj.offsetHeight, w = selects.offsetWidth;
            infoDiv.style.marginLeft = x + w; infoDiv.style.marginTop = y - 30;
            infoDiv.innerHTML = "";
            infoDiv.style.display = 'block';
        }

        function vanishPatientInfo() {
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
            var img = document.getElementById("<%=icuImage.ClientID%>");
            img.setAttribute('src', m[6]);//显示图片
            
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
</asp:Content>
