const WebSocket = require("ws");

const server = new WebSocket.Server({
    ip: "192.168.0.200",
    port: 12345
}, () => console.log("Server started!"));

server.on("connection", (ws) => {
    console.log("Connected");

    ws.on("message", (message) => {
        broadcast(message);

        if (message.length <= 50){
            console.log("Received " + message);
        }
        else{
            console.log("Server receive tool nong message: " + message.length);
        }
    });
});

server.on("listening", () => {console.log("Server listening")});

function broadcast(message){
    server.clients.forEach(client => {
        client.send(message);
    });
}