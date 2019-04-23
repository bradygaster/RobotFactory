var connection = new signalR.HubConnectionBuilder()
    .withUrl('/heartbeat')
    .build();

connection.on('heartbeatReceived', function(message, timestamp) {
    document.getElementById('heartbeat').innerText = 'Last heartbeat message: ' + message;
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