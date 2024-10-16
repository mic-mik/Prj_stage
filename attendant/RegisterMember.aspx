<%@ Page Title="" Language="C#" MasterPageFile="siteA.master" AutoEventWireup="true" CodeFile="RegisterMember.aspx.cs" Inherits="RegisterMember" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    Register Member | Attendant
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
                                <h2><i class="glyphicon glyphicon-product"></i>Inscrire un membre</h2>
                            </div>
                            <br />
                            <div class="container mt-4">
                                <div class="mt-2">
                                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                                </div>
                                <div class="form-group mb-3">
                                    <asp:Label ID="lblUsername" runat="server" Text="Username:" CssClass="form-label"></asp:Label>
                                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" required ></asp:TextBox>
                                </div>

                                <div class="form-group mb-3">
                                    <asp:Label ID="lblPassword" runat="server" Text="Password:" CssClass="form-label"></asp:Label>
                                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" required ></asp:TextBox>
                                </div>

                                <div class="mt-3">
                                    <asp:Button ID="btnRegister" runat="server" Text="Inscrire" OnClick="btnRegister_Click" CssClass="btn btn-success" />
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

