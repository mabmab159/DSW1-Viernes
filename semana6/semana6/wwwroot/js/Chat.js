var cn = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

cn.on("ReceiveMessage", function (user, message) {
    let fecha = new Date().toLocaleDateString();
    let mensaje = "<div>" + fecha + ";" + user + ":" + message + "</div>";
    document.getElementById("messagesList").innerHTML += mensaje;
});

cn.start().then(function () {
    document.getElementById("messagesList").innerHTML = "";
}).catch(function (err) {
    return console.error(err.toString());
})

document.getElementById("sendButton").addEventListener("click", function (event) {
    let usuario = document.getElementById("userInput").value;
    let mensaje = document.getElementById("messageInput").value;

    cn.invoke("SendMessage", usuario, mensaje).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});