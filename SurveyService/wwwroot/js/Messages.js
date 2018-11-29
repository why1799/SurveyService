function ErrorMessage2(type) {

    var error = "\
<div id=\"error-message2\" title=\"\" style=\"display:none\">\
    <p>\
        Опрос нельзя изменять, поскольку его уже проходили!\
    </p>\
</div>\
";

    $("body").append(error);

    var tit;
    if (type === 0) {
        tit = "Ошибка сохранение!";
    }
    else {
        tit = "Уведомление";
    }


    $("#error-message2").dialog({
        width: 280,
        height: 170,
        resizable: false,
        modal: true,
        draggable: false,
        title: tit,

        closeOnEscape: false,
        open: function (event, ui) {
            $(event.target).dialog('widget')
                .css("box-shadow", "0 10px 28px rgba(0,0,0,0.25), 0 0px 10px rgba(0,0,0,0.22)")
                .css("border-radius", "10px")
                .css({ position: 'fixed' })
                .position({ my: 'top', at: 'top', of: window })
                .css({ 'margin-top': '5em' });
            $(".ui-dialog-titlebar-close", ui.dialog | ui).hide();
        },

        close: function (event, ui) {
            $(event.target).dialog('widget')
                .css({ 'margin-top': '0em' });
            $("#error-message2").remove();
        },

        buttons: {
            Ok: function () {
                $(this).dialog("close");
            }
        }
    });
}

function ErrorMessage() {

    var error = "\
<div id=\"error-message\" title=\"Ошибка сохранение!\" style=\"display:none\">\
    <p>\
        Необходимо добавить вопрос!\
    </p>\
</div>\
";
    $("body").append(error);

    $("#error-message").dialog({
        width: 280,
        height: 150,
        resizable: false,
        modal: true,
        draggable: false,

        closeOnEscape: false,
        open: function (event, ui) {
            $(event.target).dialog('widget')
                .css("box-shadow", "0 10px 28px rgba(0,0,0,0.25), 0 0px 10px rgba(0,0,0,0.22)")
                .css("border-radius", "10px")
                .css({ position: 'fixed' })
                .position({ my: 'top', at: 'top', of: window })
                .css({ 'margin-top': '5em' });
            $(".ui-dialog-titlebar-close", ui.dialog | ui).hide();
        },

        close: function (event, ui) {
            $(event.target).dialog('widget')
                .css({ 'margin-top': '0em' });
            $("#error-message").remove();
        },

        buttons: {
            Ok: function () {
                $(this).dialog("close");
            }
        }
    });
}

function SaveMessage() {

    var error = "\
<div id=\"save-message\" title=\"Сохранение!\" style=\"display:none\">\
    <p>\
        Опрос сохраняется!\
    </p>\
    <img src=\"/images/loading.gif\" width=\"115\" height=\"115\" style=\"display: block;margin: 0 auto;\" />\
</div>\
";
    $("body").append(error);

    $("#save-message").dialog({
        width: 280,
        height: 205,
        resizable: false,
        modal: true,
        draggable: false,

        closeOnEscape: false,
        open: function (event, ui) {
            $(event.target).dialog('widget')
                .css("box-shadow", "0 10px 28px rgba(0,0,0,0.25), 0 0px 10px rgba(0,0,0,0.22)")
                .css("border-radius", "10px")
                .css({ position: 'fixed' })
                .position({ my: 'top', at: 'top', of: window })
                .css({ 'margin-top': '5em' });
            $(".ui-dialog-titlebar-close", ui.dialog | ui).hide();
        },

        close: function (event, ui) {
            $(event.target).dialog('widget')
                .css({ 'margin-top': '0em' });
            $("#save-message").remove();
        }
    });
}

function RemoveMessage() {

    var error = "\
<div id=\"remove-message\" title=\"Удаление!\" style=\"display:none\">\
    <p>\
        Опрос удаляется!\
    </p>\
    <img src=\"/images/loading.gif\" width=\"115\" height=\"115\" style=\"display: block;margin: 0 auto;\" />\
</div>\
";
    $("body").append(error);

    $("#remove-message").dialog({
        width: 280,
        height: 205,
        resizable: false,
        modal: true,
        draggable: false,

        closeOnEscape: false,
        open: function (event, ui) {
            $(event.target).dialog('widget')
                .css("box-shadow", "0 10px 28px rgba(0,0,0,0.25), 0 0px 10px rgba(0,0,0,0.22)")
                .css("border-radius", "10px")
                .css({ position: 'fixed' })
                .position({ my: 'top', at: 'top', of: window })
                .css({ 'margin-top': '5em' });
            $(".ui-dialog-titlebar-close", ui.dialog | ui).hide();
        },

        close: function (event, ui) {
            $(event.target).dialog('widget')
                .css({ 'margin-top': '0em' });
            $("#remove-message").remove();
        }
    });
}

function RemoveSurvey(surveyid) {

    var error = "\
<div id=\"remove-ask-message\" title=\"Удалить опрос?\" style=\"display:none\">\
    <p>\
        Этот опрос будет удалён навсегда. Вы уверены?\
    </p>\
</div>\
";
    $("body").append(error);

    $("#remove-ask-message").dialog({
        width: 280,
        height: 170,
        resizable: false,
        modal: true,
        draggable: false,

        closeOnEscape: false,
        open: function (event, ui) {
            $(event.target).dialog('widget')
                .css("box-shadow", "0 10px 28px rgba(0,0,0,0.25), 0 0px 10px rgba(0,0,0,0.22)")
                .css("border-radius", "10px")
                .css({ position: 'fixed' })
                .position({ my: 'top', at: 'top', of: window })
                .css({ 'margin-top': '5em' })
            $(".ui-dialog-titlebar-close", ui.dialog | ui).hide();
        },

        close: function (event, ui) {
            $(event.target).dialog('widget')
                .css({ 'margin-top': '0em' });
            $("#remove-ask-message").remove();
        },

        buttons: {
            "Удалить опрос": function () {
                $(this).dialog("close");
                RemoveMessage();
                $.ajax({
                    url: "/Admin/RemoveSurvey",
                    type: "GET",
                    data: { id: surveyid },
                    success: function (data) {
                        window.location.href = '/Admin/Index';
                    }
                });
            },
            "Отмена": function () {
                $(this).dialog("close");
            }
        }
    });
}

function OnResize() {
    $(window).resize(function () {
        try {
            $("#save-message").dialog('widget').css({ 'margin-top': '0em' });
            $("#save-message").dialog("option", "position", { my: "top", at: "top", of: window });
            $("#save-message").dialog('widget').css({ 'margin-top': '5em' });
        }
        catch (e) { }
        try {
            $("#error-message").dialog('widget').css({ 'margin-top': '0em' });
            $("#error-message").dialog("option", "position", { my: "top", at: "top", of: window });
            $("#error-message").dialog('widget').css({ 'margin-top': '5em' });
        }
        catch (e) { }
        try {
            $("#error-message2").dialog('widget').css({ 'margin-top': '0em' });
            $("#error-message2").dialog("option", "position", { my: "top", at: "top", of: window });
            $("#error-message2").dialog('widget').css({ 'margin-top': '5em' });
        }
        catch (e) { }
        try {
            $("#remove-message").dialog('widget').css({ 'margin-top': '0em' });
            $("#remove-message").dialog("option", "position", { my: "top", at: "top", of: window });
            $("#remove-message").dialog('widget').css({ 'margin-top': '5em' });
        }
        catch (e) { }
        try {
            $("#remove-ask-message").dialog('widget').css({ 'margin-top': '0em' });
            $("#remove-ask-message").dialog("option", "position", { my: "top", at: "top", of: window });
            $("#remove-ask-message").dialog('widget').css({ 'margin-top': '5em' });
        }
        catch (e) { }
    });
}