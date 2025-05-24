<%@ Page Title="Agenda del Doctor" Language="C#" MasterPageFile="~/SoftMA_Doctor.Master" AutoEventWireup="true" CodeBehind="doctor_agenda.aspx.cs" Inherits="SoftWA.doctor_agenda" %>

<asp:Content ID="Content1" ContentPlaceHolderID="chpTitulo" runat="server">
    Agenda del Doctor
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <div class="container mt-4">
        <div class="row mb-3 align-items-center">
            <div class="col-md-8">
                <h2><i class="fa-solid fa-calendar-day me-2"></i>Agenda de Citas</h2>
                <p class="text-muted">Listado de sus próximas citas programadas.</p>
            </div>
            <div class="col-md-4 text-md-end">
                 <asp:Label ID="lblDoctorInfo" runat="server" CssClass="badge bg-info text-dark p-2" ToolTip="Doctor actualmente viendo la agenda"></asp:Label>
            </div>
        </div>
        <hr />

        <asp:PlaceHolder ID="phNoAgenda" runat="server" Visible="false">
            <div class="alert alert-warning" role="alert">
                <i class="fa-solid fa-circle-exclamation me-2"></i>No tiene citas pendientes
            </div>
        </asp:PlaceHolder>

        <asp:Repeater ID="rptAgendaDoctor" runat="server" OnItemCommand="rptAgendaDoctor_ItemCommand">
            <HeaderTemplate>
                <ul class="list-group shadow-sm">
            </HeaderTemplate>
            <ItemTemplate>
                <li class="list-group-item list-group-item-action agenda-item">
                    <div class="d-flex w-100 justify-content-between">
                        <h5 class="mb-1">
                           <i class="fa-solid fa-user me-2 text-primary"></i> <%# Eval("NombrePaciente") %>
                        </h5>
                        <p class="text-muted"><%# Eval("FechaCita", "{0:dd MMM yyyy}") %></p>
                    </div>
                    <p class="mb-1">
                        <i class="fa-solid fa-clock me-2 text-success"></i>
                        <strong>Horario: </strong><%# Eval("DescripcionHorario") %>
                        <br />
                        <i class="fa-solid fa-stethoscope me-2 text-info"></i>
                        <strong>Especialidad: </strong><%# Eval("NombreEspecialidad") %>
                    </p>
                     <div class="mt-2 text-end">
                        <%-- botones de las citas --%>
                         <asp:LinkButton ID="btnMarcarAtendido" runat="server" CommandName="MarcarAtendido"
                                        CommandArgument='<%# Eval("IdCita") %>' CssClass="btn btn-sm btn-outline-success"
                                         ToolTip="Marcar esta cita como completada">
                                        <i class="fa-solid fa-check me-1"></i>Atendido
                        </asp:LinkButton>
                    </div>
                </li>
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>

    </div>

    <style>
        .agenda-item {
            transition: background-color 0.2s ease-in-out;
            border-left: 5px solid #0dcaf0; 
            margin-bottom: 0.5rem; 
        }
        .agenda-item:hover {
            background-color: #f8f9fa; 
        }
    </style>
</asp:Content>