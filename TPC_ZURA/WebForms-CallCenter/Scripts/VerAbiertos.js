
const Abiertos = function (url) {
    return {
        template: `
            <v-card>
                <v-dialog v-model="VerTickets" fullscreen hide-overlay transition="dialog-bottom-transition">
                  <v-card>
                    <v-toolbar dark color="primary">
                      <v-btn icon dark @click="VerTickets = false">
                        <v-icon>close</v-icon>
                      </v-btn>
                      <v-toolbar-title>Ticket/s seleccionado/s</v-toolbar-title>
                    </v-toolbar>
                    <h4 class="text-xs-center">Clickee en los que desee abrir</h4>
                    <v-expansion-panel v-model="Paneles" popout>
                      <v-expansion-panel-content
                        v-for="(item,i) in DatosSeleccionados"
                        :key="i"
                        class="primary lighten-2"
                      >
                        <template v-slot:header>
                          <div class="white--text"> {{ 'Ticket N° ' + item.id + ' - Titulo: ' + item.Titulo + 
                            ' - Incidencia: ' + item.incidencia.nombre + ' - Prioridad: ' + item.prioridad.nombre + 
                            ' - Estado: ' + item.estado.nombre + ' - Fecha de alta: ' + DateToDate(item.AltaReclamo) + 
                            ' - Re-abierto: ' + ReAbierto(item.Reabrio) }} </div>
                        </template>
                        <v-card>
                          <v-card-text>
                            <v-layout>
                                <v-flex class="xs6">
                                    
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
                  <v-btn color="primary">Alta</v-btn>
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
        computed: {
            DatosReclamos() {
                return this.datos
            },
            DatosSeleccionados() {
                return this.selected
            },
            SelectedText() {
                if (this.selected.length > 0) {
                    return `Ver ticket/s (${this.selected.length})`
                } else {
                    return `Ver ticket`
                }
            }
        },
        created() {
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
            })
        },
        methods: {
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
            }
        }
    }
}
    