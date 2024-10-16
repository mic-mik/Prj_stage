<%@ Page Title="" Language="C#" MasterPageFile="siteA.master" AutoEventWireup="true" CodeFile="ReturnDocument.aspx.cs" Inherits="ReturnDocument" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    Return Document | Attendant 
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
                                <h2><i class="glyphicon glyphicon-product"></i> Inscrire le retour d’un document</h2>
                            </div>
                            <div class="container mt-4">
                                  <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                                <div class="form-group mb-3">
                                    <asp:Label ID="lblLoan" runat="server" Text="Sélectionner un prêt:" ></asp:Label>
                                    <asp:DropDownList ID="ddlLoan" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLoan_SelectedIndexChanged" class="form-control"></asp:DropDownList>
                                </div>

                                <div class="form-group mb-3">
                                    <asp:Label ID="lblUsername" runat="server" Text="Nom du Membre:" ></asp:Label>
                                    <asp:Label ID="lblMemberName" runat="server" Text="" class="form-label"></asp:Label>
                                </div>

                                <div class="form-group mb-3">
                                    <asp:Label ID="lblDocumentTitle" runat="server" Text="Titre du document:" ></asp:Label>
                                    <asp:Label ID="lblDocTitle" runat="server" Text="" class="form-label"></asp:Label>
                                </div>

                                <div class="mt-3">
                                    <asp:Button ID="btnReturn" runat="server" Text="Retourner le document" OnClick="btnReturn_Click" CssClass="btn btn-primary" />
                                </div>
                            </div>
                            <br />

                        </div>
                    </div>
                </div>

            </div>
        </div>


    </div>

</asp:Content>

