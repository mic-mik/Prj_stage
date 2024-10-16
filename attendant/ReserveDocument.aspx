<%@ Page Title="" Language="C#" MasterPageFile="siteA.master" AutoEventWireup="true" CodeFile="ReserveDocument.aspx.cs" Inherits="ReserveDocument" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    Reserve Document | Attendant 
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
                                <h2><i class="glyphicon glyphicon-product"></i>Reserver un Document</h2>
                            </div>
                            <br />

                            <div class="container mt-4">
                                 <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                                <div class="form-group mb-3">
                                    <asp:Label ID="lblMemberID" runat="server" Text="Select Member:" ></asp:Label>
                                    <asp:DropDownList ID="ddlMembers" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>

                                <div class="form-group mb-3">
                                    <asp:Label ID="lblDocumentID" runat="server" Text="Select Document:" ></asp:Label>
                                    <asp:DropDownList ID="ddlDocuments" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>

                                <div class="mt-3">
                                    <asp:Button ID="btnReserve" runat="server" Text="Reserver un Document" OnClick="btnReserve_Click" CssClass="btn btn-primary" />
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

