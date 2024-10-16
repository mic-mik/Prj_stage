<%@ Page Title="" Language="C#" MasterPageFile="siteA.master" AutoEventWireup="true" CodeFile="ManageUsers.aspx.cs" Inherits="ManageUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    User Management | Admin
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
                                <h2><i class="glyphicon glyphicon-product"></i>Gestion des utilisateurs</h2>
                            </div>
                            <br />

                            <div class="container mt-4">
                                <div class="mb-3">
                                    <asp:Button ID="btnAddUser" runat="server" Text="Ajouter un utilisateur" OnClick="btnAddUser_Click" CssClass="btn btn-success" />
                                </div>
                            <br />

                                <asp:ListView ID="lvUsers" runat="server" ItemPlaceholderID="PlaceHolder1" 
                                    DataKeyNames="UserID">
                                    <LayoutTemplate>
                                        <table class="table table-striped table-bordered bootstrap-datatable datatable responsive">
                                            <thead>
                                                <tr>
                                                    <th>Username</th>
                                                    <th>Role</th>
                                                    <th>Action</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                                            </tbody>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Label runat="server" Text='<%# Eval("Username") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label runat="server" Text='<%# Eval("RoleName") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" CommandArgument='<%# Eval("UserID") %>'
                                                    OnClick="btnEdit_Click" CssClass="btn btn-sm btn-warning me-2">
                                                      modifier
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("UserID") %>'
                                                    OnClick="btnDelete_Click" CssClass="btn btn-sm btn-danger"
                                                    OnClientClick="return confirm('Are you sure to delete?')">
                                                   supprimer
                                                </asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>

                                    <EmptyDataTemplate>
                                        <tr>
                                            <td colspan="3">No record available</td>
                                        </tr>
                                    </EmptyDataTemplate>
                                </asp:ListView>


                            </div>



                        </div>
                    </div>
                </div>

            </div>
        </div>


    </div>
</asp:Content>

