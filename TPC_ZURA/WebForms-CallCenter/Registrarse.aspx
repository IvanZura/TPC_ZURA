<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registrarse.aspx.cs" Inherits="WebForms_CallCenter.Registrarse" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="https://fonts.googleapis.com/css?family=Material+Icons" rel="stylesheet">
    <link href="https://use.fontawesome.com/releases/v5.0.13/css/all.css" rel="stylesheet">
    <link href="https://cdn.jsdelivr.net/npm/vuetify/dist/vuetify.min.css" rel="stylesheet">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no, minimal-ui">
    <title>Registrarse</title>
    <style>
        .fondo {
            background: url('fondo22.png') no-repeat center center fixed;
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
                <v-dialog
                  v-model="carga"
                  hide-overlay
                  persistent
                  width="300"
                >
                  <v-card
                    color="primary"
                    dark
                  >
                    <v-card-text>
                      Espere
                      <v-progress-linear
                        indeterminate
                        color="white"
                        class="mb-0"
                      ></v-progress-linear>
                    </v-card-text>
                  </v-card>
                </v-dialog>
                <v-content class="fondo">
                    <v-container fluid fill-height>
                        <v-layout align-center justify-center>
                          <v-flex xs12 sm8 md8>
                            <v-stepper v-model="e1">
                                <v-stepper-header>
                                  <v-stepper-step :complete="e1 > 1" step="1">Verificar existencia</v-stepper-step>

                                  <v-divider></v-divider>

                                  <v-stepper-step :complete="e1 > 2" step="2">Validar Datos</v-stepper-step>

                                  <v-divider></v-divider>

                                  <v-stepper-step step="3">Registro</v-stepper-step>
                                </v-stepper-header>

                                <v-stepper-items>
                                  <v-stepper-content step="1">
                                    <v-card class="mb-5">
                                        <v-card-text>
                                            <v-text-field autofocus label="DNI" v-model="primerpaso" @keyup.enter="PasoUno()"></v-text-field>
                                        </v-card-text>
                                    </v-card>
                                      
                                    <v-btn color="primary" @click="PasoUno()">Continue</v-btn>

                                    <v-btn flat>Cancel</v-btn>
                                  </v-stepper-content>

                                  <v-stepper-content step="2">
                                    <v-card
                                      class="mb-5"
                                      color="grey lighten-1"
                                      height="200px"
                                    ></v-card>

                                    <v-btn
                                      color="primary"
                                      @click="e1 = 3"
                                    >
                                      Continue
                                    </v-btn>

                                    <v-btn flat>Cancel</v-btn>
                                  </v-stepper-content>

                                  <v-stepper-content step="3">
                                    <v-card
                                      class="mb-5"
                                      color="grey lighten-1"
                                      height="200px"
                                    ></v-card>

                                    <v-btn
                                      color="primary"
                                      @click="e1 = 1"
                                    >
                                      Continue
                                    </v-btn>

                                    <v-btn flat>Cancel</v-btn>
                                  </v-stepper-content>
                                </v-stepper-items>
                              </v-stepper>
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
                e1: 0,
                carga: false,
                primerpaso: '',
                alerta: {
                    estado: false,
                    timeout: 3000,
                    text: '',
                    color: ''
                },
                cliente: {}
            },
            methods: {
                SetSnack(texto, color) {
                    this.alerta.text = texto
                    this.alerta.color = color
                    this.alerta.estado = true
                },
                PasoUno() {
                    if (this.primerpaso == '') {
                        this.SetSnack('Complete el campo DNI', 'warning')
                    } else {
                        this.carga = true
                        fetch('<%= ResolveUrl("Registrarse.aspx/ExisteDNI") %>', {
                            method: 'POST',
                            body: `{DNI: ${this.primerpaso}}`,
                            headers: {
                                'Content-Type': 'application/json'
                            }
                        })
                        .then(response => {
                            return response.json()
                        }).then(res => {
                            res = res.d
                            if (res == null) {
                                this.SetSnack('Usted no es cliente', 'error')
                                this.carga = false
                            } else {
                                fetch('<%= ResolveUrl("Registrarse.aspx/ExisteUsuario") %>', {
                                    method: 'POST',
                                    body: `{idPersona: ${res.idpersona}}`,
                                    headers: {
                                        'Content-Type': 'application/json'
                                    }
                                })
                                .then(response => {
                                    return response.json()
                                }).then(resp => {
                                    console.log(resp.d)
                                    if (resp.d == true) {
                                        this.SetSnack('El cliente ya tiene un usuario', 'error')
                                        this.carga = false
                                    } else {
                                        this.cliente = res
                                        this.carga = false
                                        this.e1 = 2
                                    }
                                })
                            }
                        }).catch(err => {
                            this.carga = false
                            this.SetSnack('Ocurrio un error', 'error')
                        })
                    }
                }
            }
        })
    </script>
</body>
</html>
