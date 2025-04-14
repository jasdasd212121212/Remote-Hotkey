const WebSocket = require("ws");
const packegeHandler = require("./PackegeHandler");
const view = require("./View");
const sender = require("./Sender");
const configLoader = require("./ConfigLoader");

const fs = require("fs");

const ip = configLoader.loadConfig(fs).IP;
const port = process.argv[2];

const server = new WebSocket.Server({
    host: ip,
    port: port
}, () => console.log("Server started on ip: " + ip));

server.on("connection", (ws) => {
    console.log("Connected");

    ws.on("message", (message) => {
        view.display(message);
        handleSocket(message, ws);
    });

    ws.on('close', () => {
        sender.deleteUser(ws);
    });
});

server.on("listening", () => {console.log("Server listening on port: " + port)});

function handleSocket(message, ws){
    var parsedMessage = "" + message;

    const packege = packegeHandler.handle(parsedMessage, message);
    const username = packegeHandler.extractUsername(packege.message);

    if(packege.action == "connection"){
        sender.registerUser(username, ws);
    }
    else if(packege.action == "send"){
        sender.sendToUser(username, packege.rawMessage, ws);
    }
    else{
        console.log("Received undefind packege action: " + packege.action);
    }
}