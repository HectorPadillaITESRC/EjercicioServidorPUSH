self.addEventListener("push", async function (e) {
    let mensaje = e.data.text();
    let clientes = await self.clients.matchAll();

    if (clientes.length == 0) {
        self.registration.showNotification("Mensaje nuevo:", {
            body: mensaje,
            icon: "https://www.itesrc.edu.mx/logos/logoBlanco_WEB.jpg"
        });
    } else {
        clientes.forEach(c => {
            c.postMessage(mensaje);
        });
    }

});