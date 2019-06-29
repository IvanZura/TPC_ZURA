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
                <v-dialog
                  v-model="dialogoFinal"
                  max-width="290"
                  persistent
                >
                  <v-card>
                    <v-card-title class="headline">Creado !</v-card-title>

                    <v-card-text>
                      Su usuario fue correctamente creado ! Pulse el boton aceptar para continuar
                    </v-card-text>
                    <v-card-actions>
                      <v-spacer></v-spacer>
                      <v-btn
                        color="primary"
                        dark
                        href="/Login"
                      >
                        Aceptar
                      </v-btn>
                    </v-card-actions>
                  </v-card>
                </v-dialog>
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

                                    <v-btn flat href="/Login">Cancel</v-btn>
                                  </v-stepper-content>

                                  <v-stepper-content step="2">
                                    <v-card class="mb-5">
                                        <h3> Para continuar se le pedira que introduzca su fecha de nacimiento para corroborar que es usted </h3>
                                        <v-menu v-model="inicial.menu" :close-on-content-click="false" :nudge-right="40" lazy transition="scale-transition" offset-y full-width min-width="290px">
                                          <template v-slot:activator="{ on }">
                                            <v-text-field v-model="inicial.date" label="Fecha nacimiento" prepend-icon="event" readonly v-on="on"></v-text-field>
                                          </template>
                                          <v-date-picker v-model="inicial.date" @input="inicial.menu = false"></v-date-picker>
                                        </v-menu>
                                    </v-card>

                                    <v-btn color="primary" @click="PasoDos()">Continue</v-btn>

                                    <v-btn flat @click="e1 = 1">Volver</v-btn>
                                  </v-stepper-content>

                                  <v-stepper-content step="3">
                                    <v-card class="mb-5">
                                        <v-card-title>
                                            Para finalizar, ingrese un usuario y contraseña que solo usted sepa.
                                        </v-card-title>
                                        <v-card-text>
                                            <v-text-field v-model="alta.usuario" label="Usuario" @keyup.enter="PasoTres()"></v-text-field>
                                            <v-text-field type="password" v-model="alta.pass" label="Contraseña" @keyup.enter="PasoTres()"></v-text-field>
                                        </v-card-text>

                                    </v-card>

                                    <v-btn color="primary" @click="PasoTres()">Registrar</v-btn>
                                    <v-btn flat @click="e1 = 2">Volver</v-btn>
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
                dialogoFinal: false,
                alta: {
                    usuario: '',
                    pass: ''
                },
                inicial: {
                    menu: false,
                    date: ''
                },
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
                },
                DateToDate(fecha) {
                    fecha = new Date(fecha.match(/\d+/)[0] * 1)
                    let año = `${fecha.getFullYear()}`
                    let mes
                    let dia
                    if (fecha.getMonth() + 1 < 10) {
                        mes = `0${fecha.getMonth() + 1}`
                    } else {
                        mes = `${fecha.getMonth() + 1}`
                    }
                    if (fecha.getDate() < 10) {
                        dia = `0${fecha.getDate()}`
                    } else {
                        dia = `${fecha.getDate()}`
                    }
                    return `${año}-${mes}-${dia}`
                    
                },
                PasoDos() {
                    if (this.inicial.date == '') {
                        this.SetSnack('Por favor ingrese una fecha', 'warning')
                    } else {
                        if (this.inicial.date == this.DateToDate(this.cliente.fnacimiento)) {
                            this.e1 = 3
                        } else {
                            this.SetSnack('La fecha no es correcta', 'error')
                            this.inicial.date = ''
                        }
                    }
                },
                PasoTres() {
                    if (this.alta.usuario == '' || this.alta.pass == '') {
                        this.SetSnack('Complete todos los campos', 'error')
                    } else {
                        fetch('<%= ResolveUrl("Registrarse.aspx/ExisteNombreUsuario") %>', {
                            method: 'POST',
                            body: `{usuario: '${this.alta.usuario}'}`,
                            headers: {
                                'Content-Type': 'application/json'
                            }
                        })
                        .then(response => {
                            return response.json()
                        }).then(res => {
                            res = res.d
                            if (res == true) {
                                this.SetSnack('El nombre de usuario ya esta registrado', 'error')
                            } else {
                                this.carga = true
                                fetch('<%= ResolveUrl("Registrarse.aspx/InsertarUsuario") %>', {
                                    method: 'POST',
                                    body: `{IDPersona: ${this.cliente.idpersona}, Usuario: '${this.alta.usuario}',
                                            Pass: '${this.alta.pass}'}`,
                                    headers: {
                                        'Content-Type': 'application/json'
                                    }
                                })
                                .then(response => {
                                    return response.json()
                                }).then(res => {
                                    res = res.d
                                    if (res == true) {
                                        this.carga = false
                                        this.alta.usuario = ''
                                        this.alta.pass = ''
                                        this.inicial.date = ''
                                        this.primerpaso = ''
                                        this.dialogoFinal = true
                                    } else {
                                        this.carga = false
                                        this.SetSnack('Ocurrio un error', 'error')
                                    }
                                })
                            }
                        })
                    }
                }
            }
        })
    </script>
</body>
</html>
