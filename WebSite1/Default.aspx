﻿
<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%--   开头 gethtml（） 返回的表格 的css格式 --%>
<%--   css for default arrangement table --%>
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
    
<%--  div for upload file--%>
    <div>
    choisir1 ficher patient infomations<asp:FileUpload ID="FileUpload1"  onchange="Text1.value=this.value" runat="server" ></asp:fileupload><br>
    choisir ficher patient icu information<asp:FileUpload ID="FileUpload2"    runat="server"></asp:fileupload><br>
    choisir ficher patient opération information<asp:FileUpload ID="FileUpload3"  Text="choisir ficher patient opération information" runat="server"></asp:fileupload><br>
    <%--choisir ficher time block<asp:FileUpload ID="FileUpload4"  Text="choisir ficher time block" runat="server"></asp:fileupload><br>--%>
    <asp:Button ID="Button1" runat="server" Text="télécharger" OnClick="Button1_Click" />
    <asp:Label ID="Label1" runat="server" Text="" Style="color: Red"></asp:Label>
        
    </div>  

    <%--  div for information of a patient after click id of patient--%>
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

    <%--  get arrangement table (generate in default.aspx.cs)--%>
    <div id="test"><%=gethtml()%></div>
    <br><br><br><br>
    
    <%--  show different image  (generate in default.aspx.cs)--%>
    <div id="Tableau" style="font-size:30px;text-align:center" >Tableau statistique</div>
    <div id ="resultImage1">
        <img src="<%=getSalleImagePath()%>">
        <img src="<%=getDayImagePath()%>">
    </div>

    <div>
        <img src="<%=getSpecialityImagePath()%>">
    </div>

    <%--script for show patient infomation --%>
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
            img.setAttribute('src', m[6]);// show image for patient icu information
            
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

</asp:Content>
