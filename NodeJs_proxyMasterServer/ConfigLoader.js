const fileName = "Config.txt";

const config = {
    IP: ""
}

function loadConfig(fs, callback){
    var newConfig = structuredClone(config);
    
    try {
        const data = fs.readFileSync(fileName, 'utf8');
        newConfig.IP = data;
    } catch (err) {
        console.error("Error ocured while loading " + fileName + " " + err);
    }

    return newConfig;
}

export { config, loadConfig }