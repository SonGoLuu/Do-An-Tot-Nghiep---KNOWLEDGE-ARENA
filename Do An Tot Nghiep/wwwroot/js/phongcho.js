﻿////"use strict";

////var connection = new signalR.HubConnectionBuilder().withUrl("/gameHub").build();

////// Kết nối tới server SignalR
////connection.start().then(function () {
////    document.getElementById("sendButton").disabled = false;
////    console.log("Connection established.");
////}).catch(function (err) {
////    console.error(err.toString());
////});

//////Disable send button until connection is established
////document.getElementById("sendButton").disabled = true;

////connection.on("ReceiveMessage", function (user, message) {
////    var li = document.createElement("li");
////    document.getElementById("messagesList").appendChild(li);
////    // We can assign user-supplied strings to an element's textContent because it
////    // is not interpreted as markup. If you're assigning in any other way, you 
////    // should be aware of possible script injection concerns.
////    li.textContent = `${user} says ${message}`;
////});

////document.getElementById("sendButton").addEventListener("click", function (event) {
////    var user = document.getElementById("userInput").value;
////    var message = document.getElementById("messageInput").value;
////    connection.invoke("SendMessage", user, message).catch(function (err) {
////        return console.error(err.toString());
////    });
////    event.preventDefault();
////});