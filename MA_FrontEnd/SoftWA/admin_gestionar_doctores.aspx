<%@ Page Title="" Language="C#" MasterPageFile="~/SoftMA_Admin.Master" AutoEventWireup="true" CodeBehind="admin_gestionar_doctores.aspx.cs" Inherits="SoftWA.admin_gestionar_doctores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="chpTitulo" runat="server">
    Gestión de Doctores
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <asp:UpdatePanel ID="updGestionDoctores" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="container mt-4">
                <div class="row mb-3 align-items-center">
                    <div class="col-md-9">
                        <h2><i class="fa-solid fa-user-doctor me-2"></i>Administración de Doctores</h2>
                        <p class="text-muted">Agregar, editar o eliminar doctores del sistema.</p>
                    </div>
                    <div class="col-md-3 text-md-end">
                        <asp:Button ID="btnShowAddPanel" runat="server" Text="Agregar Doctor"
                            CssClass="btn btn-primary" OnClick="btnShowAddPanel_Click" />
                    </div>
                </div>
                <hr />

                <%-- agregar editar doctores (oculto) --%>
                <asp:Panel ID="pnlAddEditDoctor" runat="server" Visible="false" CssClass="card shadow-sm mb-4">
                    <div class="card-header">
                        <h5 class="mb-0">
                            <asp:Label ID="lblFormTitle" runat="server" Text="Agregar Nuevo Doctor"></asp:Label>
                        </h5>
                    </div>
                    <div class="card-body">
                        <asp:HiddenField ID="hfDoctorId" runat="server" Value="0" />
                        <div class="row g-3">
                            <div class="col-md-6">
                                <label for="<%=txtNombre.ClientID%>" class="form-label">Nombre:</label>
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre"
                                    ErrorMessage="El nombre es obligatorio." CssClass="text-danger small" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-6">
                                <label for="<%=txtApellido.ClientID%>" class="form-label">Apellido:</label>
                                <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvApellido" runat="server" ControlToValidate="txtApellido"
                                    ErrorMessage="El apellido es obligatorio." CssClass="text-danger small" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-6">
                                <label for="<%=ddlEspecialidadAddEdit.ClientID%>" class="form-label">Especialidad:</label>
                                <asp:DropDownList ID="ddlEspecialidadAddEdit" runat="server" CssClass="form-select" AppendDataBoundItems="true">
                                    <asp:ListItem Text="-- Seleccione --" Value=""></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvEspecialidad" runat="server" ControlToValidate="ddlEspecialidadAddEdit"
                                     InitialValue="" ErrorMessage="Seleccione una especialidad." CssClass="text-danger small" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-6">
                                <label for="<%=txtEmail.ClientID%>" class="form-label">Email:</label>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" MaxLength="100" TextMode="Email"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                                     ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                     ErrorMessage="Ingrese un email válido." CssClass="text-danger small" Display="Dynamic"></asp:RegularExpressionValidator>
                            </div>
                            <div class="col-md-6">
                                <label for="<%=txtTelefono.ClientID%>" class="form-label">Teléfono (Opcional):</label>
                                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                            </div>
                        </div>
                        <div class="mt-3 text-end">
                             <asp:Button ID="btnGuardarDoctor" runat="server" Text="Guardar"
                                 CssClass="btn btn-success me-2" OnClick="btnGuardarDoctor_Click" ValidationGroup="AddEditDoctor"/>
                             <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary"
                                 OnClick="btnCancelar_Click" CausesValidation="false"/>
                        </div>
                         <asp:ValidationSummary ID="vsAddEditDoctor" runat="server" CssClass="text-danger mt-2" HeaderText="Por favor corrija los siguientes errores:" ValidationGroup="AddEditDoctor"/>
                    </div>
                </asp:Panel>

                <asp:ListView ID="lvDoctores" runat="server"
                    ItemPlaceholderID="itemPlaceholder"
                    OnItemCommand="lvDoctores_ItemCommand"
                    OnItemDeleting="lvDoctores_ItemDeleting"> 
                    <LayoutTemplate>
                         <div class="table-responsive">
                            <table class="table table-hover table-sm align-middle">
                                <thead class="table-light">
                                    <tr>
                                        <th>ID</th>
                                        <th>Nombre Completo</th>
                                        <th>Especialidad</th>
                                        <th>Email</th>
                                        <th>Teléfono</th>
                                        <th class="text-center">Acciones</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                                </tbody>
                            </table>
                        </div>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("IdDoctor") %></td>
                            <td><%# Eval("NombreCompleto") %></td> 
                            <td><%# Eval("NombreEspecialidad") %></td>
                            <td><%# Eval("Email") %></td>
                            <td><%# Eval("Telefono") %></td>
                            <td class="text-center">
                                <asp:LinkButton ID="btnEdit" runat="server" CommandName="EditItem" CommandArgument='<%# Eval("IdDoctor") %>'
                                    CssClass="btn btn-sm btn-outline-primary me-1" ToolTip="Editar">
                                    <i class="fa-solid fa-pencil"></i>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnDelete" runat="server" CommandName="DeleteItem" CommandArgument='<%# Eval("IdDoctor") %>'
                                    CssClass="btn btn-sm btn-outline-danger" ToolTip="Eliminar"
                                    OnClientClick='<%# Eval("IdDoctor", "return confirm(\"¿Está seguro de que desea eliminar al doctor con ID {0}?\");") %>'>
                                    <i class="fa-solid fa-trash-can"></i>
                                </asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                         <div class="alert alert-info text-center">
                            No hay doctores registrados en el sistema. Haga clic en "Agregar Doctor" para comenzar.
                         </div>
                    </EmptyDataTemplate>
                </asp:ListView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>