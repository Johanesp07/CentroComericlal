<%@ Page Title="" Language="C#" MasterPageFile="~/CapaVista/Menu.Master" AutoEventWireup="true" CodeBehind="RegistrarUsuarios.aspx.cs" Inherits="CentroComercial.CapaVista.RegistrarUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="app-content-header">
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-6">
                    <h3 class="mb-0">Editar Usuario</h3>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-end">
                        <li class="breadcrumb-item"><a href="/CapaPresentacion/RegistroViajeros/RegistrarViajeros.aspx">Usuarios</a></li>
                        <li class="breadcrumb-item active" aria-current="page">Editar Usuario</li>
                    </ol>
                </div>
            </div>
        </div>
    </div>

    <div class="container mt-5">
        <div class="row justify-content-start">
            <div class="col-md-6">
                <div class="card">
                    <div class="card-header">
                        <h3 class="text-center">Datos del Usuario</h3>
                    </div>
                    <div class="card-body">
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <asp:Label runat="server" AssociatedControlID="txtNombre" CssClass="form-label">Nombre</asp:Label>
                                <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" />
                            </div>
                            <div class="col-md-6">
                                <asp:Label runat="server" AssociatedControlID="txtPassword" CssClass="form-label" TextMode="Password">Contraseña</asp:Label>
                                <asp:TextBox runat="server" ID="txtPassword" CssClass="form-control"  TextMode="Password"/>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <asp:Label runat="server" AssociatedControlID="txtRol" CssClass="form-label">Rol</asp:Label>
                                <asp:TextBox runat="server" ID="txtRol" CssClass="form-control" />
                            </div>
                        <div class="row mb-3">
                            <div class="col-md-6">
                                <asp:Label runat="server" AssociatedControlID="ddlPersonal" CssClass="form-label">ID Personal</asp:Label>
                                <asp:DropDownList runat="server" ID="ddlPersonal" CssClass="form-select">
                                    <asp:ListItem Value="">Seleccione una personal</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div>
                            <hr />
                            <asp:Button ID="btnAgregar" class="btn btn-primary" runat="server" Text="Agregar" OnClick="Agregar_Click" Visible="true" />
                            &nbsp;&nbsp;&nbsp;
                           <asp:LinkButton runat="server" PostBackUrl="~/CapaVista/Inicio.aspx" CssClass="btn btn-warning">Volver</asp:LinkButton>
                            &nbsp;&nbsp;&nbsp;
                           <asp:Button ID="btnActualizar" class="btn btn-success" runat="server" Text="Actualizar" OnClick="Actualizar_Click" Visible="false" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row justify-content-start mt-4">
            <div class="col-md-6">
                <div class="card">
                    <div class="card-header">
                        <h3 class="text-center">Editar/Eliminar Usuario</h3>
                    </div>
                    <div class="card-body">
                        <div class="row mb-3">
                            <div class="col-md-12">
                                <asp:Label runat="server" AssociatedControlID="ddlUsuarios" CssClass="form-label">Seleccionar Usuario</asp:Label>
                                <asp:DropDownList runat="server" ID="ddlUsuarios" CssClass="form-select">
                                    <asp:ListItem Value="">Seleccione un usuario</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div>
                            <hr />
                            <asp:Button ID="btnEditar" class="btn btn-primary" runat="server" Text="Editar" OnClick="btnEditar_Click" />
                            &nbsp;&nbsp;&nbsp;
                           <asp:Button ID="btnEliminar" class="btn btn-danger" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
