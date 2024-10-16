<%@ Page Title="" Language="C#" MasterPageFile="siteA.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    Home
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <style>
        .card:hover {
            transform: scale(1.05);
            box-shadow: 0 4px 20px rgba(0, 0, 0, 0.2);
        }

        .card-img-top {
            max-height: 300px; /* Optional: Set a max height for images */
            object-fit: cover; /* Ensures images cover the area without distortion */
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <asp:ListView ID="lvDocuments" runat="server">
            <ItemTemplate>
                <div class='col-lg-4 col-md-6 mb-4'>
                    <div class='card h-100 shadow-lg border-light'>
                        <img src='<%# Eval("PhotoPath") %>' class='card-img-top' alt='Book Cover'
                            data-bs-toggle="modal" data-bs-target="#imageModal"
                            onclick="showImage('<%# Eval("PhotoPath") %>')"
                            style="border-radius: 0.5rem; transition: transform 0.3s;">
                        <div class='card-body'>
                            <h5 class='card-title text-primary' style="font-weight: bold;"><%# Eval("Title") %></h5>
                            <p class='card-text'><strong>Auteur:</strong> <span class="text-muted"><%# Eval("Author") %></span></p>
                            <p class='card-text'><strong>ISBN:</strong> <span class="text-muted"><%# Eval("ISBN") %></span></p>
                            <p class='card-text'><strong>Éditeur:</strong> <span class="text-muted"><%# Eval("Publisher") %></span></p>
                            <p class='card-text'><strong>Status:</strong> <span class="text-success"><%# Eval("Status") %></span></p>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
            <EmptyDataTemplate>
                <div class="col-md-12 text-center">
                    <h5>No data available</h5>
                </div>
            </EmptyDataTemplate>
        </asp:ListView>
        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger mt-3" Visible="false"></asp:Label>
    </div>

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

    <script type="text/javascript">
        function showImage(imagePath) {
            document.getElementById('modalImage').src = imagePath;
        }
    </script>
</asp:Content>
