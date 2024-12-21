const packege = {
    action: "connection/send",
    message: "some message",
    rawMessage: []
}

const actionParseChar = ":";
const usernameParseChar = "#";

function handle(message, source){
    const newPackege = structuredClone(packege);
    var isAddingAction = true;
    var startIndex = 0;

    newPackege.action = "";
    newPackege.message = "";
    newPackege.rawMessage = [];

    for(var i = 0; i < message.length; i++){
        if(message[i] === actionParseChar){
            if(isAddingAction === true){
                startIndex = i;
            }

            isAddingAction = false;
        }

        if(isAddingAction === true){
            if(message[i] !== actionParseChar){
                newPackege.action += message[i];
            }
        }
        else{
            if(message[i] !== actionParseChar){
                newPackege.message += message[i];
            }
        }
    }

    for(var i = startIndex + 1; i < source.length; i++){
        newPackege.rawMessage.push(source[i]);
    }

    return newPackege;
}

function extractUsername(message){
    var usernmae = "";

    for(var i = 1; i < message.length; i++){
        if(message[i] === usernameParseChar){
            break;
        }   

        usernmae += message[i];
    }

    return usernmae;
}

export { handle, packege, extractUsername };