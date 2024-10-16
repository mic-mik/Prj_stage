<%@ Page Title="" Language="C#" MasterPageFile="siteA.master" AutoEventWireup="true" CodeFile="EditBook.aspx.cs" Inherits="EditBook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    Edit Book | Librarian
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
                                <h2><i class="glyphicon glyphicon-product"></i>Edit Book</h2>
                            </div>
                            <br />

                            <div class="container mt-4">
                                  <asp:Label ID="lblMessage" runat="server" ForeColor="Green" CssClass="mb-3"></asp:Label>
                                <div class="form-group mb-3">
                                    <asp:Label ID="lblCurrentPhoto" runat="server" Text="Current Photo:" CssClass="form-label"></asp:Label>
                                    <asp:Image ID="imgDocumentPhoto" runat="server" CssClass="img-thumbnail" Width="150" Height="150" Visible="false" />
                                </div>
                                <div class="form-group mb-3">
                                    <asp:Label ID="lblTitle" runat="server" Text="Title:" CssClass="form-label" ></asp:Label>
                                    <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" required ></asp:TextBox>
                                </div>



                                 <div class="form-group mb-3">
                                     <asp:Label ID="lblPublisher" runat="server" Text="Publisher:" CssClass="form-label"></asp:Label>
                                     <asp:TextBox ID="txtPublisher" runat="server" CssClass="form-control" required ></asp:TextBox>
                                   </div>



                                <div class="form-group mb-3">
                                    <asp:Label ID="lblAuthor" runat="server" Text="Author:" CssClass="form-label"></asp:Label>
                                    <asp:TextBox ID="txtAuthor" runat="server" CssClass="form-control" required ></asp:TextBox>
                                </div>

                                <div class="form-group mb-3">
                                    <asp:Label ID="lblISBN" runat="server" Text="ISBN:" CssClass="form-label"></asp:Label>
                                    <asp:TextBox ID="txtISBN" runat="server" CssClass="form-control" required ></asp:TextBox>
                                </div>

                                <div class="form-group mb-3">
                                    <asp:Label ID="lblDocumentType" runat="server" Text="Type:" CssClass="form-label"></asp:Label>
                                    <asp:TextBox ID="txtDocumentType" runat="server" CssClass="form-control" required ></asp:TextBox>
                                </div>

                                <div class="form-group mb-3">
                                    <asp:Label ID="lblPhoto" runat="server" Text="Update Photo:" CssClass="form-label"></asp:Label>
                                    <asp:FileUpload ID="fuDocumentPhoto" runat="server" CssClass="form-control" />
                                </div>
                                <div class="mt-3">
                                    <asp:Button ID="btnUpdate" runat="server" Text="Modifier" OnClick="btnUpdate_Click" CssClass="btn btn-primary me-2" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Annuler" OnClick="btnCancel_Click" CssClass="btn btn-secondary" />
                                </div>
                                <br />
                            </div>


                        </div>
                    </div>
                </div>

            </div>
        </div>


    </div>

</asp:Content>

