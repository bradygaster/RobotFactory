var connection = new signalR.HubConnectionBuilder()
    .withUrl('/heartbeat')
    .build();

connection.on('heartbeatReceived', function(message, robot, batteryLevel) {
    document.getElementById('heartbeat').innerText = 'Robot ' + robot + ' is currently ' + message;
    if(document.getElementById(robot + '-img').getAttribute('src').endsWith('-off.png'))
    {
        document.getElementById(robot + '-img').setAttribute('src', 'img/' + robot + '-on.png');
    }
    else
    {
        document.getElementById(robot + '-img').setAttribute('src', 'img/' + robot + '-off.png');
    }
    document.getElementById(robot + '-status').innerText = message;
    document.getElementById(robot + '-level').innerText = batteryLevel;

    if(batteryLevel > 40)
    {
        document.getElementById(robot + '-name').classList.remove('bg-warning');
        document.getElementById(robot + '-name').classList.remove('bg-danger');
        document.getElementById(robot + '-name').classList.remove('bg-success');
        document.getElementById(robot + '-name').classList.add('bg-success');
    }
    else if(batteryLevel > 15)
    {
        document.getElementById(robot + '-name').classList.remove('bg-warning');
        document.getElementById(robot + '-name').classList.remove('bg-danger');
        document.getElementById(robot + '-name').classList.remove('bg-success');
        document.getElementById(robot + '-name').classList.add('bg-warning');
    }
    else
    {
        document.getElementById(robot + '-name').classList.remove('bg-warning');
        document.getElementById(robot + '-name').classList.remove('bg-danger');
        document.getElementById(robot + '-name').classList.remove('bg-success');
        document.getElementById(robot + '-name').classList.add('bg-danger');
    }
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