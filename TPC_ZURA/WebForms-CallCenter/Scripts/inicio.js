const Inicio = function () {
    return {
        template: `
            <div>
                <v-card>
                    <v-img
                      src="center.png"
                    ></v-img>
                    <v-card-title><h3>Inicio</h3></v-card-title>
                    <v-card-text>
                        <p>Bienvenido al panel de clientes de nuestro Call Center. Aquí usted podra elevar y consultar sus reclamos con mayor comodidad.</p>
                        <p>A su <strong>izquierda</strong> encontrara el menu para navegar sobre sus reclamos.</p>
                    </v-card-text>
                </v-card>
            </div>`,
        data() {
            return {
                mensaje: ''
            }
        }
    }
}