function ShowDialog(caption, message, buttons) {
    $("#messageBox").modal("hide");
    $("#messageBox .modal-title").text(caption);
    $("#messageBox .modal-body #dialogText").text(message);
    if (buttons != undefined) {
        $("#messageBox #buttons").empty();
        jQuery.each(buttons, function (propertyName, value) {
            var btn = $('<input/>').attr({
                type: "button",
                id: propertyName,
                value: value.text,
                "class": value.styleClass
            });
            btn.bind("click", value.action);
            $("#messageBox #buttons").append(btn);
        });
    } else {
        $("#messageBox #buttons").empty();
        var btn = $('<input/>').attr({
            type: "button",
            id: "messageBoxOk",
            value: "Ok",
            "class": "btn btn-dfault"
        });
        btn.bind("click", function () {
            $("#messageBox").modal("hide");
        });
        $("#messageBox #buttons").append(btn);
    }
    $("#messageBox").modal("show");
}