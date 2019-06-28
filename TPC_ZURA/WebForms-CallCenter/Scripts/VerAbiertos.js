
const Abiertos = function (url, urlIncidencias, urlPrioridades, urlAsignar, urlAltaRec) {
    return {
        template: `
            <v-card>
                <v-snackbar v-model="alertat.estado" :color="alertat.color" top right :timeout="alertat.timeout"> {{ alertat.text }} </v-snackbar>
                <v-dialog v-model="Alta.dialog" persistent max-width="900px">
                  <v-card>
                    <v-card-title>
                      <span class="headline">Nuevo reclamo</span>
                    </v-card-title>
                    <v-card-text>
                      <v-container grid-list-md>
                        <v-layout wrap>
                          <v-flex xs12 sm6 md6>
                            <v-select v-model="Alta.SelectedIncidencia" :items="Alta.incidencias" label="Incidencia" item-text="nombre" item-value="tipo"></v-select>
                          </v-flex>
                          <v-flex xs12 sm6 md6>
                            <v-select v-model="Alta.SelectedPrioridades" :items="Alta.prioridades" label="Prioridad" item-text="nombre" item-value="tipo"></v-select>
                          </v-flex>
                          <v-flex xs12>
                            <v-text-field label="Titulo" v-model="Alta.Titulo" required></v-text-field>
                          </v-flex>
                          <v-flex xs12>
                            <v-textarea box auto-grow label="Problematica" v-model="Alta.Problematica" required></v-textarea>
                          </v-flex>
                          
                        </v-layout>
                      </v-container>
                      <small>Todos los campos son obligatorios</small>
                    </v-card-text>
                    <v-card-actions>
                      <v-spacer></v-spacer>
                      <v-btn color="blue darken-1" flat @click="Alta.dialog = false">Cerrar</v-btn>
                      <v-btn color="blue darken-1" dark @click="AltaReclamo()">Alta</v-btn>
                    </v-card-actions>
                  </v-card>
                </v-dialog>
                <v-dialog v-model="VerTickets" fullscreen hide-overlay transition="dialog-bottom-transition">
                  <v-card class="fondoTicket">
                    <v-toolbar dark color="primary">
                      <v-btn icon dark @click="VerTickets = false">
                        <v-icon>close</v-icon>
                      </v-btn>
                      <v-toolbar-title>Reclamo/s seleccionado/s</v-toolbar-title>
                    </v-toolbar>
                    <v-card>
                        <v-card-text>
                            <h4 class="text-xs-center">Clickee en los que desee abrir</h4>
                        </v-card-text>
                    </v-card>
                    <v-expansion-panel v-model="Paneles" popout>
                      <v-expansion-panel-content
                        v-for="(item,i) in DatosSeleccionados"
                        :key="i"
                        class=""
                      >
                        <template v-slot:header>
                          <span class="font-weight-bold"> {{ 'Reclamo N° ' + item.id }} </span>
                          <span class="font-weight-bold red--text"> Todavia se esta verificando su reclamo, pronto se le informara. </span>
                        </template>
                        <v-card>
                          <v-card-text>
                            <v-layout>
                                <v-flex class="xs4">
                                    <v-list>
                                      <template>
                                        <v-list-tile>
                                          <v-list-tile-content>
                                            <v-list-tile-title class="font-weight-bold">Titulo</v-list-tile-title>
                                            <v-list-tile-sub-title class="font-weight-bold"> {{ item.Titulo }} </v-list-tile-sub-title>
                                          </v-list-tile-content>
                                        </v-list-tile>
                                      </template>
                                    </v-list>
                                </v-flex>
                                <v-flex class="xs3">
                                    <v-list>
                                      <template>
                                        <v-list-tile>
                                          <v-list-tile-content>
                                            <v-list-tile-title class="font-weight-bold">Incidencia</v-list-tile-title>
                                            <v-list-tile-sub-title class="font-weight-bold"> {{ item.incidencia.nombre }} </v-list-tile-sub-title>
                                          </v-list-tile-content>
                                        </v-list-tile>
                                      </template>
                                    </v-list>
                                </v-flex>
                                <v-flex class="xs3">
                                    <v-list>
                                      <template>
                                        <v-list-tile>
                                          <v-list-tile-content>
                                            <v-list-tile-title class="font-weight-bold">Prioridad</v-list-tile-title>
                                            <v-list-tile-sub-title class="font-weight-bold"> {{ item.prioridad.nombre }} </v-list-tile-sub-title>
                                          </v-list-tile-content>
                                        </v-list-tile>
                                      </template>
                                    </v-list>
                                </v-flex>
                                <v-flex class="xs2">
                                    <v-list>
                                      <template>
                                        <v-list-tile>
                                          <v-list-tile-content>
                                            <v-list-tile-title class="font-weight-bold">Estado</v-list-tile-title>
                                            <v-list-tile-sub-title class="font-weight-bold"> {{ item.estado.nombre }} </v-list-tile-sub-title>
                                          </v-list-tile-content>
                                        </v-list-tile>
                                      </template>
                                    </v-list>
                                </v-flex>
                            </v-layout>
                            <v-layout>
                                <v-flex class="xs4">
                                    <v-list>
                                      <template>
                                        <v-list-tile>
                                          <v-list-tile-content>
                                            <v-list-tile-title class="font-weight-bold">Fecha Alta</v-list-tile-title>
                                            <v-list-tile-sub-title class="font-weight-bold"> {{ DateToDate(item.AltaReclamo) }} </v-list-tile-sub-title>
                                          </v-list-tile-content>
                                        </v-list-tile>
                                      </template>
                                    </v-list>
                                </v-flex>
                                <v-flex class="xs4">
                                    <v-list>
                                      <template>
                                        <v-list-tile>
                                          <v-list-tile-content>
                                            <v-list-tile-title class="font-weight-bold">Re-Abrio</v-list-tile-title>
                                            <v-list-tile-sub-title class="font-weight-bold"> {{ ReAbierto(item.Reabrio) }} </v-list-tile-sub-title>
                                          </v-list-tile-content>
                                        </v-list-tile>
                                      </template>
                                    </v-list>
                                </v-flex>
                            </v-layout>
                            <v-list>
                                
                            </v-list>
                            <h3>Detalle del problema:</h3>
                            <span> {{ item.problematica }} </span>
                          </v-card-text>
                        </v-card>
                      </v-expansion-panel-content>
                    </v-expansion-panel>
                  </v-card>
                </v-dialog>
                <v-card-title>
                  <v-btn color="primary" @click="Alta.dialog = true">Alta</v-btn>
                  <v-btn color="primary" :disabled="DatosSeleccionados.length == 0" @click="VerTickets = true"> {{ SelectedText }} </v-btn>
                  <v-spacer></v-spacer>
                  <v-text-field
                    v-model="search"
                    append-icon="fas fa-search"
                    label="Buscar"
                    single-line
                    hide-details
                  ></v-text-field>
                </v-card-title>
                <v-data-table
                  v-model="selected"
                  :headers="headers"
                  :items="DatosReclamos"
                  :search="search"
                  item-key="id"
                  select-all
                  prev-icon="fas fa-arrow-left"
                  next-icon="fas fa-arrow-right"
                  sort-icon="fas fa-arrow-down"
                  :loading="loadingTabla"
                  no-data-text = 'No se encontro datos hasta el momento'
                >
                  <template v-slot:items="props">
                    <td>
                        <v-checkbox
                          v-model="props.selected"
                          primary
                          hide-details
                        ></v-checkbox>
                      </td>
                    <td>{{ props.item.id }}</td>
                    <td>{{ props.item.cliente.idcliente }}</td>
                    <td>{{ props.item.incidencia.nombre }}</td>
                    <td>{{ props.item.prioridad.nombre }}</td>
                    <td>{{ props.item.Titulo }}</td>
                    <td>{{ props.item.estado.nombre }}</td>
                    <td>{{ DateToDate(props.item.AltaReclamo) }}</td>
                  </template>
                  <template v-slot:no-results>
                    <v-alert :value="true" color="error" icon="warning">
                      No hubo resultados para "{{ search }}".
                    </v-alert>
                  </template>
                </v-data-table>
              </v-card>
        `,
        data() {
            return {
                loadingTabla: true,
                alertat: {
                    estado: false,
                    timeout: 3000,
                    text: '',
                    color: ''
                },
                Alta: {
                    dialog: false,
                    incidencias: [],
                    SelectedIncidencia: 0,
                    prioridades: [],
                    SelectedPrioridades: 0,
                    Titulo: '',
                    Problematica: ''
                },
                Paneles: [],
                VerTickets: false,
                selected: [],
                mensaje: 'Aca estaria los reclamos abiertos',
                datos: [],
                search: '',
                headers: [
                    { text: 'Reclamo N°', value: 'id' },
                    { text: 'Cliente N°', value: 'cliente.idcliente' },
                    { text: 'Incidencia', value: 'incidencia.nombre' },
                    { text: 'Prioridad', value: 'prioridad.nombre' },
                    { text: 'Titulo', value: 'Titulo' },
                    { text: 'Estado', value: 'estado.nombre' },
                    { text: 'Fecha de alta', value: 'AltaReclamo' }
                ]
            }
        },
        props: {
            usuario: Object
        },
        computed: {
            DatosReclamos() {
                return this.datos
            },
            DatosSeleccionados() {
                return this.selected
            },
            SelectedText() {
                if (this.selected.length > 0) {
                    return `Ver Reclamo/s (${this.selected.length})`
                } else {
                    return `Ver Reclamo`
                }
            }
        },
        mounted() {
            document.title = 'Abiertos'
            console.log(this.usuario)
            this.TodoslosDatosdeTicket()
        },
        created() {
            
            fetch(urlIncidencias, {
                method: 'POST',
                body: `{}`,
                headers: {
                    'Content-Type': 'application/json'
                }
            })
            .then(response => {
                return response.json()
            }).then(res => {
                this.Alta.incidencias = res.d
            })
            fetch(urlPrioridades, {
                method: 'POST',
                body: `{}`,
                headers: {
                    'Content-Type': 'application/json'
                }
            })
            .then(response => {
                return response.json()
            }).then(res => {
                this.Alta.prioridades = res.d
            })
        },
        methods: {
            TodoslosDatosdeTicket() {
                fetch(url, {
                    method: 'POST',
                    body: `{}`,
                    headers: {
                        'Content-Type': 'application/json'
                    }
                })
                .then(response => {
                    return response.json()
                }).then(res => {
                    this.datos = res.d
                    this.loadingTabla = false
                })
            },
            all() {
                this.Paneles = [...Array(this.selected.length).keys()].map(_ => true)
            },
            none() {
                this.Paneles = []
            },
            DateToDate(fecha) {
                fecha = new Date(fecha.match(/\d+/)[0] * 1)
                return `${fecha.getDate()}-${fecha.getMonth() + 1}-${fecha.getFullYear()}`
            },
            ReAbierto(obj) {
                if (obj.id == 0) {
                    return 'No'
                } else {
                    return 'Si'
                }
            },
            SetSnack(texto, color) {
                this.alertat.text = texto
                this.alertat.color = color
                this.alertat.estado = true
            },
            LimpiarForm() {
                this.Alta.SelectedIncidencia = 0
                this.Alta.SelectedIncidencia = 0
                this.Alta.Titulo = ''
                this.Alta.Problematica = ''
                this.Alta.dialog = false;
            },
            AltaReclamo() {
                if (this.Alta.SelectedIncidencia == 0 || this.Alta.SelectedPrioridades == 0 || this.Alta.Titulo == ''
                    || this.Alta.Problematica == '') {
                    this.SetSnack('Algun campo sin completar', 'warning')
                } else {
                    fetch(urlAsignar, {
                        method: 'POST',
                        body: `{}`,
                        headers: {
                            'Content-Type': 'application/json'
                        }
                    }).then(res => {
                        return res.json()
                        }).then(res => {
                            console.log(`{idcliente: ${this.usuario.idcliente}, IDIncidencia: ${this.Alta.SelectedIncidencia},
                                    IDPrioridad: ${this.Alta.SelectedPrioridades}, Titulo: '${this.Alta.Titulo}',
                                    Problematica: '${this.Alta.Problematica}', IDAsignado: ${res.d.id}}`)
                        fetch(urlAltaRec, {
                            method: 'POST',
                            body: `{idcliente: ${this.usuario.idcliente}, IDIncidencia: ${this.Alta.SelectedIncidencia},
                                    IDPrioridad: ${this.Alta.SelectedPrioridades}, Titulo: '${this.Alta.Titulo}',
                                    Problematica: '${this.Alta.Problematica}', IDAsignado: ${res.d.id}}`,
                            headers: {
                                'Content-Type': 'application/json'
                            }
                        }).then(res => {
                            return res.json()
                        }).then(res => {
                            if (res.d == true) {
                                this.TodoslosDatosdeTicket()
                                this.LimpiarForm()
                                this.SetSnack('Alta Correcta', 'success')
                            } else {
                                this.SetSnack('Ocurrio un error al dar de alta el reclamo', 'error')
                            }
                        })
                    })
                }
            }
        }
    }
}
    