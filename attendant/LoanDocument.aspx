<%@ Page Title="" Language="C#" MasterPageFile="siteA.master" AutoEventWireup="true" CodeFile="LoanDocument.aspx.cs" Inherits="LoanDocument" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    Loan Document | Attendant
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
                                <h2><i class="glyphicon glyphicon-product"></i>Inscrire un prêt d’un document</h2>
                            </div>
                            <br />

                            <div class="container mt-4">
                                 <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                                <div class="form-group mb-3">
                                    <asp:Label ID="lblMember" runat="server" Text="Member:" ></asp:Label>
                                    <asp:DropDownList ID="ddlMember" runat="server" class="form-control"></asp:DropDownList>
                                </div>

                                <div class="form-group mb-3">
                                    <asp:Label ID="lblDocument" runat="server" Text="Document:" ></asp:Label>
                                    <asp:DropDownList ID="ddlDocument" runat="server" class="form-control"></asp:DropDownList>
                                </div>

                                <div class="form-group mb-3">
                                    <asp:Label ID="lblDueDate" runat="server" Text="Due Date:" ></asp:Label>
                                    <asp:TextBox ID="txtDueDate" runat="server" TextMode="Date" class="form-control" required ></asp:TextBox>
                                </div>

                                <div class="mt-3">
                                    <asp:Button ID="btnLoan" runat="server" Text="prêter un document" OnClick="btnLoan_Click" CssClass="btn btn-success" />
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

