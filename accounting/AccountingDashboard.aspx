<%@ Page Title="" Language="C#" MasterPageFile="siteA.master" AutoEventWireup="true" CodeFile="AccountingDashboard.aspx.cs" Inherits="AccountingDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="Server">
    Accounting Dashboard | Accounting
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
                                <h2><i class="glyphicon glyphicon-product"></i>Amendes en attente </h2>
                            </div>
                            <br />

                            <div class="container mt-4">
                                <asp:GridView ID="gvFines" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover table-bordered">
                                    <Columns>
                                        <asp:BoundField DataField="Username" HeaderText="Member" />
                                        <asp:BoundField DataField="FineAmount" HeaderText="Fine Amount" DataFormatString="{0:C}" />
                                        <asp:BoundField DataField="FineDate" HeaderText="Date" DataFormatString="{0:MM/dd/yyyy}" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btnCollect" runat="server" Text="Encaisser le paiement" CommandArgument='<%# Eval("FineID") %>'
                                                    OnClick="btnCollect_Click" CssClass="btn btn-sm btn-success" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>



                        </div>
                    </div>
                </div>

            </div>
        </div>


    </div>
</asp:Content>

