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

    <div id="description">
        <br />
        choose patient blocks：
        <select style="width: 200px;" name="patientsDoc" id="patientsDoc">
            <span style="white-space: pre"></span>
            <option value="patients2blocks">patients2blocks</option>
            <option value="patients2ors">patients2ors</option>
        </select>
    </div>
    
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
                <asp:Image ID="icuImage" runat="server"></asp:Image>
            </div>
                        
                <div>     
                  
                    <asp:Button ID="BttCancel"  runat="server" Text="fermer" OnClientClick="return HideBlock();" />
                </div>
            </div>
      </div> 

    <div>
        <img src =<%=getImagePath()%>>
    </div>
    <div id="test"><%=gethtml()%></div> <%-- 这里显示所有的手术表格数据--%>

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
            img.setAttribute('src', m[6]);//显示图片的链接



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
  

</asp:Content>
