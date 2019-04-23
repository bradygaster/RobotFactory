var connection = new signalR.HubConnectionBuilder()
    .withUrl('/heartbeat')
    .build();

connection.on('heartbeatReceived', function(message, robot) {
    document.getElementById('heartbeat').innerText = 'Robot ' + robot + ' is currently ' + message;
});

async function start() {
    try {
        await connection.start();
        console.log("connected");
    } catch (err) {
        console.log(err);
        setTimeout(() => start(), 5000);
    }
};

connection.onclose(async () => {
    await start();
});

start();