const connection = new signalR.HubConnectionBuilder()
    .withUrl("/orderBookHub")
    .build();

connection.on("OrderAdded", function (order) {
    console.log("Order added:", order);
});

connection.on("OrderUpdated", function (order) {
    console.log("Order updated:", order);
});

connection.on("OrderDeleted", function (orderId) {
    console.log("Order deleted:", orderId);
});

connection.on("ReceiveAllOrders", function (orders) {
    console.log("All orders:", orders);
});

connection.on("ReceiveOrderById", function (order) {
    console.log("Order:", order);
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});

// Example function to call AddOrder method
function addOrder() {
    const order = { /* Fill in the order details here */ };
    connection.invoke("AddOrder", order).catch(function (err) {
        return console.error(err.toString());
    });
}