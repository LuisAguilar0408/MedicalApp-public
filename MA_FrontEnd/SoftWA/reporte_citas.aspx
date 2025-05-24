<%@ Page Title="Reporte de Citas" Language="C#" MasterPageFile="~/SoftMA_Admin.Master" AutoEventWireup="true" CodeBehind="reporte_citas.aspx.cs" Inherits="SoftWA.reporte_citas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="chpTitulo" runat="server">
    Reporte de Citas Atendidas
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <div class="container mt-4">
        <%-- Título del Reporte --%>
        <div class="row mb-3 align-items-center">
            <div class="col">
                <h2><i class="fa-solid fa-calendar-check me-2"></i>Reporte de Citas Atendidas</h2>
                <p class="text-muted">Visualización de citas atendidas y estadísticas.</p>
            </div>
        </div>
        <hr />

        <%-- Listado de Citas --%>
        <h4><i class="fa-solid fa-list-ul me-2"></i>Detalle de Citas</h4>
        <asp:ListView ID="lvCitas" runat="server" ItemPlaceholderID="itemPlaceholder">
            <LayoutTemplate>
                 <div class="table-responsive mb-4 shadow-sm">
                    <table class="table table-hover table-sm align-middle">
                        <thead class="table-light">
                            <tr>
                                <th>ID Cita</th>
                                <th>Paciente</th>
                                <th>Especialidad</th>
                                <th>ID Doctor</th>
                                <th>Doctor</th>
                                <th>Fecha</th>
                                <th>Hora</th>
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
                    <td><%# Eval("IdCita") %></td>
                    <td><%# Eval("NombrePaciente") %></td>
                    <td><%# Eval("Especialidad") %></td>
                    <td><%# Eval("IdDoctor") %></td>
                    <td><%# Eval("NombreDoctor") %></td>
                    <td><%# Eval("FechaCita", "{0:dd/MM/yyyy}") %></td>
                    <td><%# Eval("Horario") %></td>
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate>
                 <div class="alert alert-info text-center">
                    No hay datos de citas atendidas para mostrar.
                 </div>
            </EmptyDataTemplate>
        </asp:ListView>

        <hr />
        <h4><i class="fa-solid fa-calculator me-2"></i>Estadísticas Resumen</h4>
        <div class="card shadow-sm mb-4">
            <div class="card-body">
                 <div class="row">
                     <div class="col-md-6 mb-2 mb-md-0">
                         <strong>Especialidad más solicitada:</strong>
                         <asp:Label ID="lblMasSolicitadaEspecialidad" runat="server" Text="N/A" CssClass="ms-2"></asp:Label>
                     </div>
                     <div class="col-md-6">
                         <strong>Doctor más solicitado:</strong>
                         <asp:Label ID="lblMasSolicitadoDoctor" runat="server" Text="N/A" CssClass="ms-2"></asp:Label>
                     </div>
                 </div>
            </div>
        </div>
    </div>
</asp:Content>