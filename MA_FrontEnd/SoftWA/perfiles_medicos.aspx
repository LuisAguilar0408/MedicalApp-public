<%@ Page Title="Perfiles de Médicos" Language="C#" MasterPageFile="~/SoftMA_Paciente.Master" AutoEventWireup="true" CodeBehind="perfiles_medicos.aspx.cs" Inherits="SoftWA.perfiles_medicos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="chpTitulo" runat="server">
    Perfiles de Médicos
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <div class="container mt-4">
        <div class="row mb-3">
            <div class="col">
                <h2><i class="fa-solid fa-users-medical me-2"></i>Conozca a Nuestros Médicos</h2>
                <hr />
            </div>
        </div>

        <asp:PlaceHolder ID="phNoMedicos" runat="server" Visible="false">
            <div class="alert alert-info" role="alert">
                <i class="fa-solid fa-circle-info me-2"></i>No hay perfiles de médicos disponibles en este momento.
            </div>
        </asp:PlaceHolder>

        <asp:Repeater ID="rptPerfilesMedicos" runat="server">
            <HeaderTemplate>
                <div class="row row-cols-1 row-cols-md-2 row-cols-lg-3 g-4">
            </HeaderTemplate>
            <ItemTemplate>
                <div class="col">
                    <div class="card h-100 shadow-sm medico-perfil-card">
                        <div class="card-body text-center">
                            <h5 class="card-title mt-2"><%# Eval("NombreCompleto") %></h5>
                            <p class="card-text text-muted mb-1">
                                <i class="fa-solid fa-stethoscope me-1"></i> <%# Eval("NombreEspecialidad") %>
                            </p>
                            <p class="card-text small">
                                <strong>Código de Médico:</strong> <%# Eval("CodigoMedico") %>
                            </p>
                        </div>
                        <div class="card-footer bg-transparent border-top-0 text-center pb-3">
                             <a href='<%# "cita_reserva.aspx?medicoId=" + Eval("IdMedico") + "&especialidadId=" + Eval("IdEspecialidad") %>' class="btn btn-primary btn-sm">
                                <i class="fa-solid fa-calendar-plus me-1"></i> Reservar Cita
                            </a>
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
        .medico-perfil-card {
            transition: transform 0.2s ease-in-out, box-shadow 0.2s ease-in-out;
        }
        .medico-perfil-card:hover {
            transform: translateY(-5px);
            box-shadow: 0 .5rem 1rem rgba(0,0,0,.15)!important;
        }
        .card-img-top {
            width: 150px; 
            height: 150px; 
            border-radius: 50%; 
            object-fit: cover; 
            margin: 1rem auto;
            border: 3px solid #eee;
        }
    </style>
</asp:Content>