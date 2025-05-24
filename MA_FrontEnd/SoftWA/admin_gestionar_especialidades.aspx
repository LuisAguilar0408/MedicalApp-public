<%@ Page Title="" Language="C#" MasterPageFile="~/SoftMA_Admin.Master" AutoEventWireup="true" CodeBehind="admin_gestionar_especialidades.aspx.cs" Inherits="SoftWA.gestionar_especialidades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="chpTitulo" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <div class="container mt-4">
    <div class="card-header">
        <h2>Administración de Especialidades</h2>
        <p>Agregar, editar o eliminar especialidades del sistema.</p>
    </div>

    <asp:PlaceHolder ID="phNoEspecialidad" runat="server" Visible="false">
        <div class="alert alert-info" role="alert">
            <i class="fa-solid fa-circle-info me-2"></i>No existen especialidades registradas.
        </div>
    </asp:PlaceHolder>

    <asp:Repeater ID="rptEspecialidades" runat="server">
        <HeaderTemplate>
            <div class="row row-cols-1 row-cols-md-2 row-cols-lg-3 g-4">
        </HeaderTemplate>
        <ItemTemplate>
            <div class="col">
                <div class="card h-100 shadow-sm cita-card">
                    <div class="card-body">
                        <h5 class="card-title"><i class="fa-solid fa-layer-group me-2"></i><%# Eval("NombreEspecialidad") %></h5>
                        <p class="card-text mb-1">
                            <i class="fa-solid fa-circle-info me-2 text-primary"></i>
                            <strong>ID:</strong> 000<%# Eval("ID") %>
                        </p>
                        <p class="card-text mb-1">
                            <i class="fa-solid fa-coins me-2 text-warning"></i>
                            <strong>Precio de Consulta:  </strong>S/. <%# Eval("PrecioConsulta") %>0
                        </p>
                    </div>
                    <div class="card-footer bg-light text-end">
                        <asp:LinkButton ID="btnEditarEspecialidad" runat="server"
                            CssClass="btn btn-link p-1 me-2"
                            CommandName="EditEspecialidad"
                            CommandArgument='<%# Eval("ID") %>' ToolTip="Editar Especialidad">
                            <i class="fa-solid fa-pen" style="color: black; font-size: 1.2em;"></i>
                        </asp:LinkButton>
                        <asp:LinkButton ID="btnEliminarEspecialidad" runat="server"
                            CssClass="btn btn-link p-1"
                            CommandName="EliminarEspecialidad"
                            CommandArgument='<%# Eval("ID") %>' ToolTip="Eliminar Especialidad"
                            OnClientClick="return confirm('¿Está seguro de que desea eliminar este médico?');">
                            <i class="fa-solid fa-trash" style="color: red; font-size: 1.2em;"></i>
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
        </ItemTemplate>
        <FooterTemplate>
            <%-- Tarjeta para Agregar Nueva Especialidad --%>
                <div class="col">
                    <asp:LinkButton ID="lnkAgregarNuevaEspecialidadCard" runat="server"
                        CssClass="card h-100 shadow-sm cita-card-new text-decoration-none"
                        OnClick="lnkAgregarNuevaEspecialidad_Click" 
                        ToolTip="Agregar Nueva Especialidad">
                        <div class="card-body d-flex flex-column justify-content-center align-items-center text-center">
                            <i class="fa-solid fa-plus fa-3x text-secondary mb-2"></i>
                            <h5 class="card-title text-secondary">Agregar Nueva</h5>
                        </div>
                    </asp:LinkButton>
                </div>
            </div>
            </FooterTemplate>
        </asp:Repeater>
    </div>

    <style>
        .cita-card {
            transition: transform 0.2s ease-in-out, box-shadow 0.2s ease-in-out;
            border-left: 5px solid #5bd3c5;
        }
        .cita-card:hover {
            transform: translateY(-5px);
            box-shadow: 0 .5rem 1rem rgba(0,0,0,.15)!important;
        }
        .card-title i {
            color: #5bd3c5;
        }

        .cita-card-new {
            border: 2px dashed #adb5bd;
            background-color: #f8f9fa;
            transition: transform 0.2s ease-in-out, box-shadow 0.2s ease-in-out, background-color 0.2s ease-in-out;
            cursor: pointer;
        }
        .cita-card-new:hover {
            transform: translateY(-5px);
            box-shadow: 0 .5rem 1rem rgba(0,0,0,.10)!important;
            background-color: #e9ecef;
            border-color: #6c757d;
        }
        .cita-card-new .fa-plus {
            transition: color 0.2s ease-in-out;
        }
        .cita-card-new:hover .fa-plus,
        .cita-card-new:hover .card-title {
            color: #0d6efd !important; 
        }
    </style>
</asp:Content>
