function display(message){
    if (message.length <= 50){
        console.log("Received: " + message);
    }
    else{
        console.log("Server receive tool nong message: " + message.length);
    }
}

export { display }