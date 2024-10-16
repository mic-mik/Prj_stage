<%@ Page Title="" Language="C#" MasterPageFile="siteA.master" AutoEventWireup="true" CodeFile="ReturnBook.aspx.cs" Inherits="ReturnBook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    Return Book | Member
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger mt-3" Visible="false"></asp:Label>

        <asp:ListView ID="lvLoans" runat="server">
            <ItemTemplate>
                <div class="col-lg-4 col-md-6 mb-4">
                    <div class="card h-100 shadow-sm">
                        <img src='<%# Eval("PhotoPath") %>' class='card-img-top' alt='Book Cover' 
                             data-bs-toggle="modal" data-bs-target="#imageModal" 
                             onclick="showImage('<%# Eval("PhotoPath") %>')">
                        <div class="card-body">
                            <h5 class="card-title">Loan ID: <%# Eval("LoanID") %></h5>
                            <p class="card-text"><strong>Title:</strong> <%# Eval("Title") %></p>
                        </div>
                        <div class="card-footer">
                            <asp:LinkButton ID="btnReturn" runat="server" CommandArgument='<%# Eval("LoanID") %>' Text="Return" CssClass="btn btn-primary" OnClick="btnReturn_Click" />
                        </div>
                    </div>
                </div>
            </ItemTemplate>
            <EmptyDataTemplate>
                <div class="col-md-12 text-center">
                    <h5>No loans available</h5>
                </div>
            </EmptyDataTemplate>
        </asp:ListView>

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
            // Set the source of the modal image to the clicked image's source
            document.getElementById('modalImage').src = imagePath;
        }
    </script>
</asp:Content>
