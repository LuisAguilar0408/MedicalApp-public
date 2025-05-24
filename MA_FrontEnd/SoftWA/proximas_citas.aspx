<%@ Page Title="" Language="C#" MasterPageFile="~/SoftMA_Paciente.Master" AutoEventWireup="true" CodeBehind="proximas_citas.aspx.cs" Inherits="SoftWA.proximas_citas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="chpTitulo" runat="server">
    Próximas Citas
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <div class="container mt-4">
        <div class="row mb-3">
            <div class="col">
                <h2><i class="fa-solid fa-calendar-check me-2"></i>Sus Próximas Citas</h2>
                <hr />
            </div>
        </div>

        <asp:PlaceHolder ID="phNoCitas" runat="server" Visible="false">
            <div class="alert alert-info" role="alert">
                <i class="fa-solid fa-circle-info me-2"></i>No tiene citas programadas próximamente.
                <a href="cita_reserva.aspx" class="alert-link">Reserve una nueva cita aquí</a>.
            </div>
        </asp:PlaceHolder>

        <asp:Repeater ID="rptProximasCitas" runat="server">
            <HeaderTemplate>
                <div class="row row-cols-1 row-cols-md-2 row-cols-lg-3 g-4">
            </HeaderTemplate>
            <ItemTemplate>
                <div class="col">
                    <div class="card h-100 shadow-sm cita-card">
                        <div class="card-body">
                            <h5 class="card-title"><i class="fa-solid fa-stethoscope me-2"></i><%# Eval("NombreEspecialidad") %></h5>
                            <p class="card-text mb-1">
                                <i class="fa-solid fa-user-doctor me-2 text-primary"></i>
                                <strong>Médico:</strong> <%# Eval("NombreMedico") %>
                            </p>
                            <p class="card-text mb-1">
                                <i class="fa-solid fa-calendar-day me-2 text-success"></i>
                                <strong>Fecha:</strong> <%# Eval("FechaCita", "{0:dddd, dd 'de' MMMM 'de' yyyy}") %>
                            </p>
                            <p class="card-text">
                                <i class="fa-solid fa-clock me-2 text-warning"></i>
                                <strong>Horario:</strong> <%# Eval("DescripcionHorario") %>
                            </p>
                        </div>
                        <div class="card-footer bg-light text-end"> 
                            <asp:LinkButton ID="btnCancelarCita" runat="server"
                                CommandName="Cancelar"
                                CommandArgument='<%# Eval("IdCita") %>'
                                CssClass="btn btn-sm btn-outline-danger"
                                ToolTip="Cancelar esta cita"
                                OnClientClick="return confirm('¿Está seguro de que desea cancelar esta cita?');">
                                <i class="fa-solid fa-trash-can me-1"></i>Cancelar Cita
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
            <FooterTemplate>
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
    </style>
</asp:Content>