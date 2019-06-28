<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebForms_CallCenter.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="https://use.fontawesome.com/releases/v5.0.13/css/all.css" rel="stylesheet">
    <link href="https://cdn.jsdelivr.net/npm/vuetify/dist/vuetify.min.css" rel="stylesheet">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no, minimal-ui">
    <title>Login</title>
    <style>
        .fondo {
            background: url('fondo.png') no-repeat center center fixed;
            -webkit-background-size: cover;
            -moz-background-size: cover;
            -o-background-size: cover;
            background-size: cover;
        }
    </style>
</head>
<body>
    <div id="app">
        <v-app id="inspire">
                <v-snackbar v-model="alerta.estado" :color="alerta.color" top right :timeout="alerta.timeout"> {{ alerta.text }} </v-snackbar>
                <v-content class="fondo">
                    <v-container fluid fill-height>
                        <v-layout align-center justify-center>
                          <v-flex xs12 sm8 md4>
                            <v-card class="elevation-12">
                              <v-toolbar dark color="primary">
                                <v-toolbar-title>Acceso</v-toolbar-title>
                                <v-spacer></v-spacer>
                              </v-toolbar>
                              <v-card-text>
                                <v-form>
                                  <v-text-field @keyup.enter="Login" autofocus prepend-icon="fas fa-user-circle" v-model="usuario" label="DNI o Usuario" type="text"></v-text-field>
                                  <v-text-field @keyup.enter="Login" id="password" prepend-icon="fas fa-lock" v-model="pass" label="Password" type="password"></v-text-field>
                                </v-form>
                                <v-progress-linear v-if="carga" :indeterminate="true"></v-progress-linear>
                              </v-card-text>
                              <v-card-actions>
                                <v-spacer></v-spacer>
                                <v-btn color="primary" @click="Login">Login</v-btn>
                                <v-btn color="primary" flat href="/Registrarse">Registrese</v-btn>
                              </v-card-actions>
                            </v-card>
                          </v-flex>
                        </v-layout>
                      </v-container>
                </v-content>
            </v-app>
    </div>
    <script src="https://cdn.jsdelivr.net/npm/vue@2.6.10/dist/vue.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/vuetify/dist/vuetify.js"></script>
    <script>
        let app = new Vue({
            el: '#app',
            data: {
                message: 'Hello Vue!',
                usuario: '',
                pass: '',
                carga: false,
                alerta: {
                    estado: false,
                    timeout: 3000,
                    text: '',
                    color: ''
                }
            },
            methods: {
                SetSnack(texto, color) {
                    this.alerta.text = texto
                    this.alerta.color = color
                    this.alerta.estado = true
                },
                Login() {
                    if (this.usuario == '' || this.pass == '') {
                        this.SetSnack('Algun campo vacio', 'warning')
                    } else {
                        this.carga = true
                        fetch('<%= ResolveUrl("Login.aspx/LoginUsu") %>', {
                            method: 'POST',
                            body: `{usu:'${this.usuario}', pass:'${this.pass}'}`,
                            headers: {
                                'Content-Type': 'application/json'
                            }
                        })
                        .then(response => {
                            return response.json()
                        }).then(res => {
                            res = res.d
                            if (res.id != 0) {
                                this.carga = false
                                location.reload();
                            } else {
                                this.SetSnack('Usuario o password incorrecto', 'error')
                                this.carga = false
                            }
                        }).catch(err => {
                            this.SetSnack('Ocurrio un error', 'error')
                            this.carga = false
                        })
                    }
                }
            }
        })
    </script>
</body>
</html>
