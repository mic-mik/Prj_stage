<%@ Page Title="Pay Fine" Language="C#" MasterPageFile="siteA.master" AutoEventWireup="true" CodeFile="PayFine.aspx.cs" Inherits="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    Pay Fine | member
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <asp:ListView ID="lvFines" runat="server">
            <ItemTemplate>
                <div class='col-lg-4 col-md-6 mb-4'>
                    <div class='card h-100 shadow-sm'>
                        <div class='card-body'>
                            <p class='card-text'><strong>Fine Amount:</strong> $<%# Eval("FineAmount") %></p>
                            <p class='card-text'><strong>Due Date:</strong> <%# Eval("FineDate", "{0:MM/dd/yyyy}") %></p>
                            <asp:LinkButton ID="btnPay" runat="server" CssClass="btn btn-primary" CommandArgument='<%# Eval("FineID") %>' OnClick="PayFine" Text="Payer une amende" />
                        </div>
                    </div>
                </div>
            </ItemTemplate>
            <EmptyDataTemplate>
                <div class="col-md-12 text-center">
                    <h5>No fines available</h5>
                </div>
            </EmptyDataTemplate>
        </asp:ListView>

        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger mt-3" Visible="false"></asp:Label>
    </div>
</asp:Content>
