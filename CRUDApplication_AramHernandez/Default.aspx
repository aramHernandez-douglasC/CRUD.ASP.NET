<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" MasterPageFile="~/Site1.Master"

    Inherits="CRUDApplication_AramHernandez.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="server">
        <div>
            <asp:HiddenField ID="htId" runat="server" />
            <table class="table">
                <tr>
                    <td>
                        <asp:Label Text="Name" runat="server" />
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="name" runat="server" />

                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label Text="E-mail" runat="server" />
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="email" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label Text="Date Of Birth" runat="server" />
                    </td>
                    <td colspan="2">
                        <asp:Calendar ID="dob" runat="server" />

                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label Text="Gender" runat="server" />
                    </td>
                    <td colspan="2">
                        <asp:RadioButtonList ID="gender" runat="server" >
                            <asp:ListItem Value="male">Male</asp:ListItem>
                            <asp:ListItem Value="female">Female</asp:ListItem>
                            <asp:ListItem Value="other">Other </asp:ListItem>
                        </asp:RadioButtonList>
                        <div id="otherGender">
                            <asp:DropDownList ID="otherSelection" runat="server">
                            </asp:DropDownList>
                        </div>
                        


                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Label Text="Password" runat="server" />
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="password" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label Text="Repeat Password" runat="server" />
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="repPassword" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:CheckBox ID="role" Value="1" Text="Is Admin" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Button Text="Create" ID="btnCreate" OnClick="btnCreate_Click" runat="server" />
                        <asp:Button Text="Delete" ID="btnDelete" OnClick ="btnDelete_Click" runat="server" Enabled="false" />

                        <asp:Button Text="Clear Fields" ID="btnClear" OnClick ="btnClear_Click" runat="server"  />

                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Label Text="" ID="successMsg" runat="server" ForeColor="Green" />
                        <br />
                        <asp:Label Text="" ID="errorMsg" runat="server" ForeColor="Red" />
                    </td>

                </tr>
            </table>

        </div>
        <br />
        <div id="search">
            <asp:Label Text="Search" ID="srchLbl" runat="server"/>
                </br>
            <asp:TextBox ID="searchTxt" runat="server"/>
            <asp:Button ID="searchBtn" runat="server" Text="Search" OnClick="searchBtn_Click"  />
        </div>
        <div id="showResults">
            <asp:GridView ID="user_grid" runat="server" AutoGenerateColumns="False"
                AllowSorting="true" AllowPaging="True" PageSize="4" OnPageIndexChanging="userGrid_PageIndexChanging"
                OnSelectedIndexChanged="userGrid_SelectedIndexChanged" OnSorting="userGrid_Sorting">

                <Columns>
                    <asp:BoundField DataField="user_name" HeaderText="Name" SortExpression="user_name" />
                    <asp:BoundField DataField="user_email" HeaderText="E-mail" SortExpression="user_email" />
                    <asp:BoundField DataField="user_dob" HeaderText="DOB" SortExpression="user_dob"/>
                    <asp:BoundField DataField="user_gender" HeaderText="Gender" SortExpression="user_gender"/>
                    <asp:BoundField DataField="user_role" HeaderText="Role" SortExpression="user_role"/>

                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton Text="Select" ID="linkSelect" CommandArgument= '<%# Eval("userId") %>' runat="server" OnClick="link_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>


                </Columns>

            </asp:GridView>

        </div>
</asp:Content>
    

