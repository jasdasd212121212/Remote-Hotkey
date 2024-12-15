const packege = {
    action: "connection/send",
    message: "some message"
}

const actionParseChar = ":";
const usernameParseChar = "#";

function handle(message){
    const newPackege = structuredClone(packege);
    var isAddingAction = true;

    newPackege.action = "";
    newPackege.message = "";

    for(var i = 0; i < message.length; i++){
        if(message[i] === actionParseChar){
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