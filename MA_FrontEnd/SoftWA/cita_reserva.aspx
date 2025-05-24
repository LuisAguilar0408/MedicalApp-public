<%@ Page Title="" Language="C#" MasterPageFile="~/SoftMA_Paciente.Master" AutoEventWireup="true" CodeBehind="cita_reserva.aspx.cs" Inherits="SoftWA.cita_reserva" %>
<asp:Content ID="Content1" ContentPlaceHolderID="chpTitulo" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <div class="card">
    <div class="card-header">
        <h2> Reserve su cita</h2>
    </div>
        
    <%-- Primer campo de especialidades --%>
    <asp:UpdatePanel ID="updCitas" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <%-- El div de la fila de Especialidad --%>
            <div class="mb-3 row ms-3 mt-4">
                <asp:Label ID="lblEspecialidad" runat="server" Text="Seleccione una especialidad: " CssClass="col-sm-4 col-form-label"></asp:Label>
                <div class="col-sm-4">
                    <asp:DropDownList ID="ddlEspecialidad" runat="server" CssClass="form-select"
                        AppendDataBoundItems="true"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="ddlEspecialidad_SelectedIndexChanged">
                        <asp:ListItem Text="-- Seleccione una especialidad --" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            <%-- El div de la fila de Médico --%>
            <div class="mb-3 row ms-3">
                <asp:Label ID="lblMedico" runat="server" Text="Seleccione un Médico: " CssClass="col-sm-4 col-form-label"></asp:Label>
                <div class="col-sm-4">
                    <asp:DropDownList ID="ddlMedico" runat="server" CssClass="form-select" AppendDataBoundItems="true">
                        <asp:ListItem Text="-- Seleccione un médico --" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            
            <%-- Fila Fecha y Horario --%>
                <div class="mb-3 row ms-3">
                    <asp:Label ID="lblFecha" runat="server" Text="Seleccione una fecha:" CssClass="col-sm-4 col-form-label"></asp:Label>
                    <div class="col-sm-6 col-md-4">
                        <asp:Calendar ID="calFechaCita" runat="server"
                            OnSelectionChanged="calFechaCita_SelectionChanged"
                            OnDayRender="calFechaCita_DayRender">
                            <WeekendDayStyle BackColor="#F0F0F0" />
                            <SelectedDayStyle BackColor="#007bff" ForeColor="White" Font-Bold="true" />
                            <TodayDayStyle BackColor="#cfe2ff" />
                        </asp:Calendar>
                        <asp:Label ID="lblFechaSeleccionadaInfo" runat="server" CssClass="form-text mt-1" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="mb-3 row" id="divHorarios" runat="server" visible="false">
                    <asp:Label ID="lblHorario" runat="server" Text="Seleccione un horario:" CssClass="col-sm-4 col-form-label"></asp:Label>
                    <div class="col-sm-6 col-md-4">
                        <asp:RadioButtonList ID="rblHorarios" runat="server" CssClass="form-check">
                        </asp:RadioButtonList>
                        <asp:Label ID="lblErrorHorario" runat="server" CssClass="text-danger small" Visible="false"></asp:Label>
                    </div>
                </div>
            </ContentTemplate>
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlEspecialidad" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlMedico" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="calFechaCita" EventName="SelectionChanged" />
            </Triggers>
        </asp:UpdatePanel>

            
    </div>
    <div class="card-footer">
        <asp:Button ID="btnRegresar" CssClass="float-start btn btn-primary" runat="server" Text="Regresar" OnClick="btnRegresar_Click"/>
        <asp:Button ID="btnGuardar" CssClass="float-end btn btn-secondary" runat="server" Text="Buscar" OnClick="btnGuardar_Click"/>
    </div>
</asp:Content>
