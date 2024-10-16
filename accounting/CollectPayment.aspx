<%@ Page Title="" Language="C#" MasterPageFile="siteA.master" AutoEventWireup="true" CodeFile="CollectPayment.aspx.cs" Inherits="CollectPayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    Collect Payment | Accounting 
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
                                <h2><i class="glyphicon glyphicon-product"></i>Collect Payment</h2>
                            </div>
                            <br />

                            <div class="container mt-4">
                                  <asp:Label ID="lblMessage" runat="server" ForeColor="Red"  role="alert"></asp:Label>
                                <div class="form-group mb-3">
                                    <asp:Label ID="lblFineDetails" runat="server" Text="Fine Details" ></asp:Label>
                                    <br />
                                    <asp:Label ID="lblPaymentMethod" runat="server" Text="Payment Method:" ></asp:Label>
                                    <br />
                                    <asp:DropDownList ID="ddlPaymentMethod" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="Credit Card" Value="Credit Card"></asp:ListItem>
                                        <asp:ListItem Text="Debit Card" Value="Debit Card"></asp:ListItem>
                                        <asp:ListItem Text="PayPal" Value="PayPal"></asp:ListItem>
                                        <asp:ListItem Text="Cash" Value="Cash"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="mt-3">
                                    <asp:Button ID="btnProcessPayment" runat="server" Text="Process Payment" OnClick="btnProcessPayment_Click" CssClass="btn btn-primary" />
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

