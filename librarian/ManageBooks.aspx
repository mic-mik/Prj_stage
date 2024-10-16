<%@ Page Title="" Language="C#" MasterPageFile="siteA.master" AutoEventWireup="true" CodeFile="ManageBooks.aspx.cs" Inherits="ManageBooks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    Manage Books | Librarian
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
                                <h2><i class="glyphicon glyphicon-product"></i>Manage Books</h2>
                            </div>
                            <br />

                            <div class="container mt-4">
                                <div class="mb-3">
                                    <asp:Button ID="btnAddBook" runat="server" Text="Ajouter un nouveau livre" OnClick="btnAddBook_Click" CssClass="btn btn-success" />
                                </div>
                                <br />
                                <asp:ListView ID="lvBooks" runat="server" ItemPlaceholderID="PlaceHolder1" DataKeyNames="DocumentID">
                                    <LayoutTemplate>
                                        <table class="table table-striped table-bordered bootstrap-datatable datatable responsive">
                                            <thead>
                                                <tr>
                                                    <th>Book ID</th>
                                                    <th>Titre</th>
                                                    <th>Autheur</th>
                                                    <th>ISBN</th>
                                                    <th>Type</th>
                                                    <th>Photo</th>
                                                    <!-- New column for photo -->
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
                                                <asp:Label runat="server" Text='<%# Eval("DocumentID") %>'></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" Text='<%# Eval("Title") %>'></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" Text='<%# Eval("Author") %>'></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" Text='<%# Eval("ISBN") %>'></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" Text='<%# Eval("DocumentType") %>'></asp:Label></td>
                                            <td>
                                                <asp:ImageButton
                                                    ID="imgBookPhoto"
                                                    runat="server"
                                                    ImageUrl='<%# Eval("PhotoPath") %>'
                                                    Width="50px"
                                                    Height="50px"
                                                    OnClientClick='<%# string.Format("showModal(\"{0}\"); return false;", Eval("PhotoPath")) %>'
                                                    Visible='<%# !string.IsNullOrEmpty(Eval("PhotoPath").ToString()) %>' />

                                            </td>
                                            <!-- Display photo if it exists -->
                                            <td>
                                                <asp:Button ID="btnEdit" runat="server" Text="Modifier" CommandArgument='<%# Eval("DocumentID") %>' OnClick="btnEdit_Click" CssClass="btn btn-warning btn-sm me-2" />
                                                <%--<asp:Button ID="btnDelete" runat="server" Text="Delete" CommandArgument='<%# Eval("DocumentID") %>' OnClick="btnDelete_Click" CssClass="btn btn-danger btn-sm" />--%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        <tr>
                                            <td colspan="7">No records available</td>
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

    <!-- Bootstrap Modal for displaying the larger photo -->
    <!-- Modal Structure -->
    <div class="modal fade" id="imageModal" tabindex="-1" role="dialog" aria-labelledby="imageModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="imageModalLabel">Book Photo</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <img id="modalImage" src="" alt="Book Photo" class="img-fluid" />
                </div>
            </div>
        </div>
    </div>


    <script type="text/javascript">
        function showModal(photoPath) {
            var modalImage = document.getElementById('modalImage');
            modalImage.src = photoPath;  // Set the source of the image in the modal
            $('#imageModal').modal('show'); // Show the modal
        }
</script>
</asp:Content>
