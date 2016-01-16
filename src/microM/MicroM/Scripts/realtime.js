function sendTest(){

    //gather data
    var values = {
        message:$("#Message").val()
    }

    $.ajax({
        url:"../../Home/SignalRTest",
        type:"POST",
        data:values,
        success:function(){
            console.log("message sent: ",values.message);
        },
        error:function(){
            console.log("error happened sending test");
        }
    });
}

$(function(){
    var con = $.hubConnection();
    var hub = con.createHubProxy("microHub");

    hub.on("testEvent", function(message){
        console.log("test event called data:",message);
        $("#sContainer").text(message);
    });

    con.start(function(){
        //get initial data/object
        //hub.invoke("MethodNameOnHub");
    }).done(function(){
        //optinal in case we need to invoke method @ hub
        //hub.invoke("MethodNameOnHub");
    });
});
