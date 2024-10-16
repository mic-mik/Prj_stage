<%@ Page Title="" Language="C#" MasterPageFile="siteA.master" AutoEventWireup="true" CodeFile="AddUser.aspx.cs" Inherits="AddUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    Add User | Admin 
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
                                <h2><i class="glyphicon glyphicon-product"></i>Ajouter un utilisateur</h2>
                            </div>
                            <br />

                            <div class="container mt-4">
                                <asp:Label ID="lblMessage" runat="server" ForeColor="Red"  role="alert"></asp:Label>

                                <div class="form-group">
                                    <asp:Label ID="lblUsername" runat="server" Text="Username:" CssClass="form-label"></asp:Label>
                                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" required ></asp:TextBox>
                                </div>

                                <div class="form-group">
                                    <asp:Label ID="lblPassword" runat="server" Text="Password:" CssClass="form-label"></asp:Label>
                                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" required CssClass="form-control"></asp:TextBox>
                                </div>

                                <div class="form-group">
                                    <asp:Label ID="lblRole" runat="server" Text="Role:" CssClass="form-label"></asp:Label>
                                    <asp:DropDownList ID="ddlRoles" runat="server" CssClass="form-select"></asp:DropDownList>
                                </div>

                                <div class="form-group mt-3">
                                    <asp:Button ID="btnSave" runat="server" Text="Sauvegarder" OnClick="btnSave_Click" CssClass="btn btn-primary" />
                                </div>
                            </div>


                        </div>
                    </div>
                </div>

            </div>
        </div>


    </div>

</asp:Content>

