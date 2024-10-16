<%@ Page Title="" Language="C#" MasterPageFile="siteA.master" AutoEventWireup="true" CodeFile="ReturnedBooksReport.aspx.cs" Inherits="ReturnedBooksReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    Returned Books Report | Manager 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="ch-container">
        <div class="row">
            <div id="content" class="col-lg-10 col-sm-10">
                <div class="row">
                    <div class="box col-md-12">
                        <div class="box-inner">
                            <div class="box-header well" data-original-title="">
                                <h2><i class="glyphicon glyphicon-product"></i>Rapport des livres retournés</h2>
                            </div>
                            <br />

                        <div class="container mt-4">
                                <asp:GridView ID="gvReturnedBooks" runat="server" AutoGenerateColumns="true" />

                            </div>
                            <br />
                        </div>
                    </div>
                </div>

            </div>
        </div>


    </div>

</asp:Content>

