const Cerrados = function (url) {
    return {
        template: `<v-card>
                    <v-snackbar v-model="alertat.estado" :color="alertat.color" top right :timeout="alertat.timeout"> {{ alertat.text }} </v-snackbar>
                    
                    <v-dialog v-model="VerTickets" fullscreen hide-overlay transition="dialog-bottom-transition">
                      <v-card class="fondoTicketCerrado">
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
                                <h3>Solucion:</h3>
                                <span> {{ item.solucion }} </span>
                              </v-card-text>
                            </v-card>
                          </v-expansion-panel-content>
                        </v-expansion-panel>
                      </v-card>
                    </v-dialog>
                    <v-card-title>
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
                  </v-card>`,
        data() {
            return {
                loadingTabla: true,
                alertat: {
                    estado: false,
                    timeout: 3000,
                    text: '',
                    color: ''
                },
                Paneles: [],
                VerTickets: false,
                selected: [],
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
            document.title = 'Cerrados'
            this.TodoslosDatosdeTicket()
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
            }
        }
    }
}