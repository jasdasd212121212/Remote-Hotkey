const WebSocket = require("ws");
const packegeHandler = require("./PackegeHandler");
const view = require("./View");
const sender = require("./Sender");

const server = new WebSocket.Server({
    ip: "192.168.0.200",
    port: 12345
}, () => console.log("Server started!"));

server.on("connection", (ws) => {
    console.log("Connected");

    ws.on("message", (message) => {
        view.display(message);
        handleSocket(message, ws);

        //sender.broadcast(message, server);
    });

    ws.on('close', () => {
        sender.deleteUser(ws);
    });
});

server.on("listening", () => {console.log("Server listening")});

function handleSocket(message, ws){
    var parsedMessage = "" + message;

    const packege = packegeHandler.handle(parsedMessage);
    const username = packegeHandler.extractUsername(packege.message);

    if(packege.action == "connection"){
        sender.registerUser(username, ws);
    }
    else if(packege.action == "send"){
        sender.sendToUser(username, packege.message);
    }
    else{
        console.log("Received undefind packege action: " + packege.action);
    }
}