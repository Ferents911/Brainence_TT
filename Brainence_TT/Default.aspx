<%@ Page Title="Brainence TT" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Brainence_TT._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-12">
            <div class="file_upload">
                <button type="button">Choose</button>
                <div>File not choosen</div>
                <asp:FileUpload CssClass="btn_choose" ID="FileUpload1" runat="server" />
            </div>
            <div class="cont_wrapper mt-5">
                <asp:TextBox CssClass="search_box" ID="search_box" runat="server"></asp:TextBox>
                <asp:Button CssClass="btn_find mt-3" ID="Button1" runat="server" Text="Find" OnClick="Button1_Click" />
            </div>
            <asp:Label ID="Label1" runat="server"></asp:Label>
        </div>
    </div>  
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:dbConnectionString %>" SelectCommand="SELECT * FROM [Sentences_table]"></asp:SqlDataSource>
</asp:Content>
