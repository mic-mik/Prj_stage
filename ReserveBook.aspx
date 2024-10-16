<%@ Page Title="" Language="C#" MasterPageFile="siteA.master" AutoEventWireup="true" CodeFile="ReserveBook.aspx.cs" Inherits="ReserveBook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    Reserve Book | member
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger mt-3"></asp:Label>

        <asp:ListView ID="lvDocuments" runat="server">
            <ItemTemplate>
                <div class='col-lg-4 col-md-6 mb-4'>
                    <div class='card h-100 shadow-sm'>
                        <img src='<%# Eval("PhotoPath") %>' class='card-img-top' alt='Book Cover' 
                             data-bs-toggle="modal" data-bs-target="#imageModal" 
                             onclick="showImage('<%# Eval("PhotoPath") %>')">
                        <div class='card-body'>
                            <h5 class='card-title'><%# Eval("Title") %></h5>
                            <p class='card-text'><strong>Author:</strong> <%# Eval("Author") %></p>
                            <p class='card-text'><strong>ISBN:</strong> <%# Eval("ISBN") %></p>
                            <p class='card-text'><strong>Publisher:</strong> <%# Eval("Publisher") %></p>
                            <p class='card-text'><strong>Status:</strong> <%# Eval("Status") %></p>
                            <asp:LinkButton ID="btnReserve" runat="server" Text="Reserver" OnClick="btnReserve_Click" CssClass="btn btn-secondary mt-2" CommandArgument='<%# Eval("DocumentID") %>' />
                        </div>
                    </div>
                </div>
            </ItemTemplate>
            <EmptyDataTemplate>
                <div class="col-md-12 text-center">
                    <h5>No books available for reservation</h5>
                </div>
            </EmptyDataTemplate>
        </asp:ListView>

        <!-- Modal for displaying the image -->
        <div class="modal fade" id="imageModal" tabindex="-1" role="dialog" aria-labelledby="imageModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="imageModalLabel">Book Cover</h5>
                        <button type="button" class="close" data-bs-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <img id="modalImage" src="" class="img-fluid" alt="Book Cover">
                    </div>
                </div>
            </div>
        </div>
        
    </div>

    <script type="text/javascript">
        function showImage(imagePath) {
            document.getElementById('modalImage').src = imagePath;
        }
    </script>
</asp:Content>
