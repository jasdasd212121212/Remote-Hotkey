var usernames = []
var connectedSockets = []

var connectionsCount = 0;

const maxConnectionsPerUser = 2;

function broadcast(message, server){
    server.clients.forEach(client => {
        client.send(message);
    });
}

function registerUser(username, socket){
    connectionsCount++; 

    try{
        if(countOfUsers(username) < maxConnectionsPerUser){
            var name = connectionsCount + "-" + username;

            usernames.push(name);
            connectedSockets.push(socket);
        }
        else{
            socket.close();
        }
    }
    catch{
        console.log("invalid username: " + username);
    }
}

function deleteUser(socket){
    var indexOfSocket = 0;
    var isFinded = false;

    for(var i = 0; i < connectedSockets.length; i++){
        if(socket == connectedSockets[i]){
            indexOfSocket = i;
            isFinded = true;
            break;
        }
    }

    if(isFinded === true){
        connectedSockets.splice(indexOfSocket, 1);
        usernames.splice(indexOfSocket, 1);
    }
    else{
        console.log("You try to remove not existed socket: " + socket);
    }
}

function sendToUser(username, data, wsOrigin){
    for(var i = 0; i < usernames.length; i++){
        if(extractUsername(usernames[i]) == username && connectedSockets[i] != wsOrigin){
            connectedSockets[i].send(data);
        }
    }
}

function extractUsername(fullName){
    var isAddin = false;
    var result = "";

    for(var i = 0; i < fullName.length; i++){
        if(isAddin === true){
            result += fullName[i];
        }

        if(fullName[i] == "-"){
            isAddin = true;
        }
    }

    return result;
}

function countOfUsers(username){
    var count = 0;

    for(var i = 0; i < usernames.length; i++){
        if(extractUsername(usernames[i]) == username){
            count++;
        }
    }

    return count;
}

export { broadcast, registerUser, deleteUser, sendToUser }