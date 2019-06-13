<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="patient.aspx.cs" Inherits="patient" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">

    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-5">
            <div class="row " style="font-size: medium">Information de la patient</div>

            <table border="1">
                <tr style="background-color: #808080; color: white">
                    <td><b>patient numéro: </b></td>
                    <td>
                        <asp:TextBox ID="number" runat="server"></asp:TextBox></td>
                </tr>
                <tr style="background-color: #808080; color: white">
                    <td><b>patient name:  </b></td>
                    <td>
                        <asp:TextBox ID="name" runat="server"></asp:TextBox></td>
                </tr>
                <tr style="background-color: #808080; color: white">
                    <td><b>patient departement: </b></td>
                    <td>
                        <asp:TextBox ID="departement" runat="server"></asp:TextBox></td>
                </tr>
                <tr style="background-color: #808080; color: white">
                    <td><b>urgency level: </b></td>
                    <td>
                        <asp:TextBox ID="urgencyLevel" runat="server"></asp:TextBox></td>
                </tr>
                <tr style="background-color: #808080; color: white">
                    <td><b>waiting time: </b></td>
                    <td>
                        <asp:TextBox ID="waitingTime" runat="server"></asp:TextBox></td>
                </tr>
                <tr style="background-color: #808080; color: white">
                    <td><b>max waiting time: </b></td>
                    <td>
                        <asp:TextBox ID="maxWaitingTime" runat="server"></asp:TextBox></td>
                </tr>
            </table>
        </div>
    </div>
    <div>
        <asp:Image ID="icuImage" runat="server" ImageAlign="Middle"></asp:Image>
    </div>
    <div>
        <asp:Image ID="Image1" runat="server" ImageAlign="Middle" ImageUrl="~/temps/patient_1.jpeg"></asp:Image>
    </div>
    <asp:TextBox ID="bd" runat="server" Visible="false"></asp:TextBox>
</asp:Content>



