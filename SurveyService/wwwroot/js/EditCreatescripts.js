function DescriptionInput() {
    var $this = $("#SurveyDescription");
    if ($this.val() == '') {
        $this.removeClass('surveydescriptioninputfilled');
    } else {
        $this.addClass('surveydescriptioninputfilled');
    }
}

function AddImage() {
    $('#uploadinput').click();
}

function MarginHead() {
    if ($("#questions").children().length === 0) {
        $(".header").css("margin-bottom", "5em");
    }
    else {
        $(".header").css("margin-bottom", "");
    }

}

function MakeSelectSlick(selectid) {
    $('#' + selectid).ddslick({
        width: 200,
        onSelected: function (selectedData) {
            var selectid = selectedData.original[0].id;
            var questid = $("#" + selectid).parent().parent().parent()[0].id;
            if (selectedData.selectedIndex === 0) {
                $("#options" + questid).empty();
                $("#" + questid + " #ratingoption").hide();
                $("#" + questid + " #another").hide();
                $("#" + questid + " #addnew").hide();
                $("#" + questid + " #addnew label").show();
                $("#" + questid + " #textoption").show();
            }
            else if (selectedData.selectedIndex === 1) {
                if ($("#" + questid + " #ratingoption").css("display") != "flex") {
                    $("#options" + questid).empty();
                    $("#" + questid + " #another").hide();
                    $("#" + questid + " #addnew").hide();
                    $("#" + questid + " #textoption").hide();
                    $("#" + questid + " #addnew label").show();
                    $("#" + questid + " #ratingoption").show();
                    $("#" + questid + " #ratingoption #minselect").ddslick('select', { index: 1 });
                    $("#" + questid + " #ratingoption #maxselect").ddslick('select', { index: 3 });
                }
            }
            else if (selectedData.selectedIndex === 2) {
                $("#" + questid + " #ratingoption").hide();
                $("#" + questid + " #textoption").hide();
                $("#" + questid + " #addnew").show();
                $("#" + questid + " #image").attr("src", "/images/radioboxoptions.png");
            }
            else if (selectedData.selectedIndex === 3) {
                $("#" + questid + " #ratingoption").hide();
                $("#" + questid + " #textoption").hide();
                $("#" + questid + " #addnew").show();
                $("#" + questid + " #image").attr("src", "/images/checkboxoptions.png");
            }
        }
    });

}

function AddAnother(id) {
    $("#" + id + " #another").show();
    $("#" + id + " #addnew label").hide();
}

function RemoveAnother(id) {
    $("#" + id + " #another").hide();
    $("#" + id + " #addnew label").show();
}

function AddQuestion() {


    var newquestion = "\
<div id=\"newquest{ID}\" class=\"box\" style=\"margin-top:1em;\">\
\
    <div style=\"justify-content: center;display: flex;margin-top:1em;\">\
        <img id=\"movenewquest{ID}\" src=\"/images/questiondots.png\" style=\"cursor:move;\" width=\"20\" height=\"10\">\
    </div>\
\
\
    <div class=\"question-settings\">\
        <div class=\"leftdiv\" style=\"position: relative;\">\
            <textarea rows=\"1\" class=\"questiontitleinput\" type=\"text\" placeholder=\"\" required oninvalid=\"this.setCustomValidity('Введите название вопроса')\" oninput=\"setCustomValidity('')\"></textarea>\
            <label class=\"questiontitlelabel\">Название вопроса</label>\
        </div>\
        <div class=\"rightdiv\">\
            <select id=\"newquest{ID}select\" style=\"width:100%;display:none;\">\
                <option value=\"0\" data-imagesrc=\"/images/text.png\" data-description=\" \">Текст</option>\
                <option value=\"1\" data-imagesrc=\"/images/star.png\" data-description=\" \">Рейтинг</option>\
                <option value=\"2\" selected=\"selected\" data-imagesrc=\"/images/radiobox.png\" data-description=\" \">Один из списка</option>\
                <option value=\"3\" data-imagesrc=\"/images/checkbox.png\" data-description=\" \">Несколько из списка</option>\
            </select>\
        </div>\
    </div>\
\
    <div id=\"optionsnewquest{ID}\" name=\"options\">\
    </div>\
\
    <div id=\"textoption\" style=\"margin-top:1em;margin-left:5%;display:none\">\
        <input style=\"width:94.5%;height: 30px;font-size: 15px;\" disabled type=\"text\" placeholder=\"Текстовый ответ\">\
    </div>\
\
        <div id=\"ratingoption\" style=\"display:none\" class=\"ratingoption\">\
                    <div class=\"minselectdiv\">\
                        <select id=\"minselect\" class=\"minselect\">\
                            <option>0</option>\
                            <option>1</option>\
                        </select>\
                    </div>\
                    <label style=\"margin-left:1em;\">—</label>\
                    <div class=\"maxselectdiv\">\
                        <select id=\"maxselect\" class=\"maxselect\">\
                            <option>2</option>\
                            <option>3</option>\
                            <option>4</option>\
                            <option>5</option>\
                            <option>6</option>\
                            <option>7</option>\
                            <option>8</option>\
                            <option>9</option>\
                            <option>10</option>\
                        </select>\
                    </div>\
                </div>\
\
    <div id=\"another\" style=\"display:none;\">\
        <img id=\"image\" src=\"/images/radioboxoptions.png\" width=\"20\" height=\"20\" style=\"margin-left:5%;\">\
        <input class=\"option-input\" style=\"width:87%;\" disabled type=\"text\" placeholder=\"Другое...\">\
        <div title=\"Удалить другое\" name=\"picture\" style=\"background: url('/images/SurveyIcon.svg') no-repeat -5px -4061px; width: 14px; height: 14px; zoom:1.4; cursor: pointer;\" onclick=\"RemoveAnother('newquest{ID}')\"></div>\
    </div>\
\
\
    <div id=\"addnew\" class=\"addnew\" style=\"margin-top:1em\">\
        <img id=\"image\" src=\"/images/radioboxoptions.png\" width=\"20\" height=\"20\" style=\"margin-left:5%;\">\
        <input style=\"height: 30px;font-size: 15px;width:127px\" type=\"text\" placeholder=\"Добавить вариант\" onclick=\"AddOption('newquest{ID}')\"  onkeydown=\"OnChangeAdd('newquest{ID}')\">\
        <label style=\"display:normal;font-weight:normal\"> или <a style=\"cursor:pointer;font-weight:bold\" onclick=\"AddAnother('newquest{ID}')\">ДОБАВИТЬ ВАРИАНТ \"ДРУГОЕ\"</a></label>\
    </div>\
\
    <div class=\"hl\" style=\"margin-top:1em;\"></div>\
\
    <div class=\"beforequestionfooter\"></div>\
    <div class=\"questionfooter\" style=\"margin-top:0.5em;margin-bottom:0.5em;\">\
        <div title=\"Удалить вопрос\" name=\"picture\" style=\"background: url(/images/SurveyIcon.svg) no-repeat -5px -1203px; width: 14px; height: 18px;margin-right:2em;cursor: pointer;\" onclick=\"RemoveQuestion('newquest{ID}')\"></div>\
        <div class=\"vl\" style=\"margin-right:2em\"></div>\
        <div style=\"margin-right:0.5em\">\
            <label style=\"color:rgba(51, 51, 51, 0.78);\">Обязательный вопрос</label>\
        </div>\
        <div>\
            <input type=\"checkbox\" id=\"newquest{ID}required\" style=\"display:none\" />\
            <label for=\"newquest{ID}required\" class=\"toggle\"><span name=\"span\"></span></label>\
        </div>\
    </div>\
\
</div>\
";
    newquestion = newquestion.replace("{ID}", addedquest)
        .replace("{ID}", addedquest)
        .replace("{ID}", addedquest)
        .replace("{ID}", addedquest)
        .replace("{ID}", addedquest)
        .replace("{ID}", addedquest)
        .replace("{ID}", addedquest)
        .replace("{ID}", addedquest)
        .replace("{ID}", addedquest)
        .replace("{ID}", addedquest)
        .replace("{ID}", addedquest);


    $("#questions").append(newquestion);
    $('#newquest' + addedquest + ' #minselect').ddslick({
        width: 200,
        onSelected: function (selectedData) {
        }
    });

    $('#newquest' + addedquest + ' #maxselect').ddslick({
        width: 200,
        onSelected: function (selectedData) {
        }
    });
    MakeSelectSlick('newquest' + addedquest + 'select');
    autosize($('#newquest' + addedquest + ' div div textarea'));
    $('#optionsnewquest' + addedquest).sortable({
        axis: 'y',
        containment: '#optionsnewquest' + addedquest,
        opacity: 1,
        tolerance: 'pointer'
    });

    addedquest++;

    MarginHead();
}

function RemoveQuestion(surveyquestionid) {
    $("#" + surveyquestionid).remove();
    MarginHead();
}

function AddOption(surveyquestionid) {
    AddOptionText(surveyquestionid, "");
}

function AddOptionText(surveyquestionid, text) {
    var newoption = "\
                <div id=\"newopt{ID}\" class=\"optionseparation\">\
\
                                <div class=\"optionleft\">\
                                    <img id=\"movenewopt{ID}\" src=\"/images/optiondots.png\" width=\"10\" height=\"20\" style=\"cursor:move;\">\
                                </div>\
\
                                <div class=\"optionright\">\
                                    <img id=\"image\" src=\"{TYPE}\" width=\"20\" height=\"20\">\
                                    <input class=\"option-input\" type=\"text\" placeholder=\"Ответ\" required=\"\" oninvalid=\"this.setCustomValidity('Введите ответ')\" oninput=\"setCustomValidity('')\" value=\"\">\
                                    <div title=\"Удалить ответ\" name=\"picture\" style=\"background: url('/images/SurveyIcon.svg') no-repeat -5px -4061px; width: 14px; height: 14px; zoom:1.4; cursor: pointer;\" onclick=\"RemoveOption('newopt{ID}')\"></div>\
                                </div>\
\
                            </div>";

    newoption = newoption.replace("{ID}", addedopt)
        .replace("{ID}", addedopt).replace("{ID}", addedopt).replace("{TYPE}", $("#" + surveyquestionid + " #addnew #image").attr('src'));

    $("#options" + surveyquestionid).append(newoption);

    $('#newopt' + addedopt + ' input').val(text);
    $('#newopt' + addedopt + ' input').focus();

    $("#" + surveyquestionid + " #addnew input")[0].setCustomValidity('');

    addedopt++;
}

function OnChangeAdd(surveyquestionid) {
    var val = $("#" + surveyquestionid + " #addnew input").val();
    $("#" + surveyquestionid + " #addnew input").val("");
    AddOptionText(surveyquestionid, val);
}

function RemoveOption(optionid) {
    $("#" + optionid).remove();
}

function crop(can, a, b) {
    // get your canvas and a context for it
    var ctx = can.getContext('2d');

    // get the image data you want to keep.
    var imageData = ctx.getImageData(a.x, a.y, b.x, b.y);

    // create a new cavnas same as clipped size and a context
    var newCan = document.createElement('canvas');
    newCan.width = b.x - a.x;
    newCan.height = b.y - a.y;
    var newCtx = newCan.getContext('2d');

    // put the clipped image on the new canvas.
    newCtx.putImageData(imageData, 0, 0);

    return newCan;
}

function OnScroll() {
    $(window).scroll(function (e) {
        $('#left-menu').css('top', $(window).scrollTop() + 60 + 'px')
    });
}

function OnReady() {
    addedquest = 0;
    addedopt = 0;
    autosize($('textarea'));

    MakeDropImage(document.querySelector('.box__upload'));

    MakeQuestionSortable();

    OnScroll();

    OnResize();

    MakeOptionsSortable();

    MakeMinSelectsSlick();

    MakeMaxSelectsSlick();
}

function MakeQuestionSortable() {
    $('#questions').sortable({
        axis: 'y',
        containment: '#questions',
        opacity: 1,
        tolerance: 'pointer'
    });
}

function MakeOptionsSortable() {
    $("div[name=options]").each(function () {
        $(this).sortable({
            axis: 'y',
            containment: this,
            opacity: 1,
            tolerance: 'pointer'
        });
    });
}

function MakeMinSelectsSlick() {
    $('.minselect').each(function () {
        $(this).ddslick({
            width: 200,
            onSelected: function (selectedData) {
            }
        });
    });
}

function MakeMaxSelectsSlick() {
    $('.maxselect').each(function () {
        $(this).ddslick({
            width: 200,
            onSelected: function (selectedData) {
            }
        });
    });
}

function MakeTypeSelectSlickOnReady(quests) {
    var firsttime = true;

    $('.typeselect').each(function () {
        $(this).ddslick({
            width: 200,
            onSelected: function (selectedData) {
                var selectid = selectedData.original[0].id;
                var questid = $("#" + selectid).parent().parent().parent()[0].id;
                if (selectedData.selectedIndex === 0) {
                    $("#options" + questid).empty();
                    $("#" + questid + " #ratingoption").hide();
                    $("#" + questid + " #another").hide();
                    $("#" + questid + " #addnew").hide();
                    $("#" + questid + " #addnew label").show();
                    $("#" + questid + " #textoption").show();
                }
                else if (selectedData.selectedIndex === 1) {
                    if ($("#" + questid + " #ratingoption").css("display") != "flex" || firsttime) {
                        $("#options" + questid).empty();
                        $("#" + questid + " #another").hide();
                        $("#" + questid + " #addnew").hide();
                        $("#" + questid + " #textoption").hide();
                        $("#" + questid + " #addnew label").show();
                        $("#" + questid + " #ratingoption").show();

                        if (firsttime) {
                            
                            for (var j = 0; j < quests.length; j++) {
                                if (questid === quests[j][0]) {
                                    $("#" + questid + " #ratingoption #minselect").ddslick('select', { index: parseInt(quests[j][1]) });
                                    $("#" + questid + " #ratingoption #maxselect").ddslick('select', { index: parseInt(quests[j][quests[j].length - 1]) - 2 });
                                }
                            }

                        }
                        else {
                            $("#" + questid + " #ratingoption #minselect").ddslick('select', { index: 1 });
                            $("#" + questid + " #ratingoption #maxselect").ddslick('select', { index: 3 });
                        }

                    }
                }
                else if (selectedData.selectedIndex === 2) {
                    $("#" + questid + " #ratingoption").hide();
                    $("#" + questid + " #textoption").hide();
                    $("#" + questid + " #addnew").show();
                    $("#" + questid + " #image").attr("src", "/images/radioboxoptions.png");
                }
                else if (selectedData.selectedIndex === 3) {
                    $("#" + questid + " #ratingoption").hide();
                    $("#" + questid + " #textoption").hide();
                    $("#" + questid + " #addnew").show();
                    $("#" + questid + " #image").attr("src", "/images/checkboxoptions.png");
                }
            }
        });
    });
    firsttime = false;
}

function SaveCommon(surveyid, url) {
    if (!$('#SurveyForm')[0].checkValidity()) {
        $("#submitButton").prop("disabled", false);
        $('#submitButton').click();
        $("#submitButton").prop("disabled", true);
        return;
    }

    SaveMessage();

    var title = $("#SurveyName").val();
    var description = $("#SurveyDescription").val();

    var questions = [];

    var questiondivs = $("#questions").children();

    for (var i = 0; i < questiondivs.length; i++) {
        var question = [];
        var id = questiondivs[i].id;
        question.push(id);
        question.push($("#" + id + " div div textarea").val());

        var select = $("#" + id + "select").data('ddslick').selectedIndex;;
        var type;
        if (select === 0) {
            type = "2"
        }
        else if (select === 1) {
            type = "3"
        }
        else if (select === 2) {
            type = "0"
        }
        else if (select === 3) {
            type = "1"
        }

        question.push(type);

        if ($('#' + id + ' #addnew label').css('display') == 'none') {
            question.push("True");
        }
        else {
            question.push("False");
        }

        if ($("#" + id + "required")[0].checked) {
            question.push("True");
        }
        else {
            question.push("False");
        }
        if (select === 1) {
            var minselect = $("#" + id + " #minselect").data('ddslick').selectedIndex;
            var maxselect = $("#" + id + " #maxselect").data('ddslick').selectedIndex + 2;
            for (var j = minselect; j <= maxselect; j++) {
                question.push("newoption" + j);
                question.push(j);
            }
        }
        if (select === 2 || select === 3) {
            var options = $("#options" + id).children();
            for (var j = 0; j < options.length; j++) {
                var optionid = options[j].id;
                question.push(optionid);
                question.push($("#" + optionid + " div input").val());
            }
        }
        questions.push(question);
    }

    if (questions.length == 0) {
        $("#save-message").dialog("close");
        $("#savebutton").effect("bounce");
        ErrorMessage();
        return;
    }

    for (var i = 0; i < questions.length; i++) {
        if (questions[i].length <= 5 && questions[i][2] != "2") {
            $("#" + questions[i][0] + " #addnew input")[0].setCustomValidity('Необходимо добавить ответ');
        }

        if (questions[i][2] === "2") {
            $("#" + questions[i][0] + " #addnew input")[0].setCustomValidity('');
        }
    }

    if (!$('#SurveyForm')[0].checkValidity()) {
        $("#save-message").dialog("close");
        $("#submitButton").prop("disabled", false);
        $('#submitButton').click();
        $("#submitButton").prop("disabled", true);
        return;
    }

    $("span[name=span]").each(function () {
        var a = $(this).css('transform');
        if (a.indexOf("20") >= 0) {
            $(this).css('transform', 'translateX(10px)');
        }

    })



    html2canvas(document.body.querySelector(".container.body-content"), {
        onclone: function (clonedDoc) {

            if ($('.imagediv', $(clonedDoc)).css("display") === "none") {
                $(".form-container", $(clonedDoc)).css("padding-top", "30px");
            }

            $('.modal-box', $(clonedDoc)).css("border", "solid 0.3px black");

            $('#deleteimagebutton', $(clonedDoc)).remove();

            $('#box__upload', $(clonedDoc)).remove();

            $(".addnew", $(clonedDoc)).each(function () {
                $(this).css('display', 'none');
            })

            $('.questionfooter', $(clonedDoc)).each(function () {
                $(this).css('display', 'none');
            })

            $('.hl', $(clonedDoc)).each(function () {
                $(this).css('display', 'none');
            })

            $(".beforequestionfooter", $(clonedDoc)).each(function () {
                $(this).css('display', 'block');
            })

            $('#left-menu', $(clonedDoc)).remove();
            //$(".container.body-content", $(clonedDoc)).css("zoom", "4");
            $(".container.body-content", $(clonedDoc)).css("width", "1000px");
            $(".container.body-content", $(clonedDoc)).css("height", "1000px");
            var color = $("body").css("background-color");
            $(".container.body-content", $(clonedDoc)).css("background-color", color);



            $("div[name=picture]", $(clonedDoc)).each(function () {
                $(this).css('zoom', '1');
            })
        }
    }).then(function (canvas) {

        $("span[name=span]").each(function () {
            var a = $(this).css('transform');
            if (a.indexOf("10") >= 0) {
                $(this).css('transform', '');
            }
        })

        //document.body.appendChild(canvas);

        canvas = crop(canvas, { x: 0, y: 0 }, { x: canvas.width, y: canvas.width });

        //document.body.appendChild(canvas);
        var img = canvas.toDataURL("image/png");
        img = img.replace('data:image/png;base64,', '');


        var addedimg = $("#addedimage").attr('src');
        if (addedimg != "") {
            var index = addedimg.indexOf(";base64,");
            if (index >= 0) {
                index += 8;
                addedimg = addedimg.replace(addedimg.substring(0, index), "");
            }
        }

        $.ajax(
            {
                url: '/Admin/Save',
                type: "POST",
                data: { 'id': surveyid, 'title': title, 'description': description, 'lines': questions, 'image': img, 'addedimage': addedimg },
                success: function (data) {
                    url = url.replace("--ID--", data.id);
                    window.location.href = url;
                },
                error: function () {
                    $("#save-message").dialog("close");
                }
            });
    });
}

function ImageLoaded() {
    $("#deleteimagebutton").show();
    //$(".form-container").css("padding-top", "");
}

function DeleteAddedImage() {
    $("#addedimage").attr('src', '');
    $("#deleteimagebutton").hide();
    $(".imagediv").hide();
    //$(".form-container").css("padding-top", "30px");
    $('#file').val("")
    $(".box__upload").show();
}

function MakeDropImage(form) {
    var input = form.querySelector('input[type="file"]'),
        label = form.querySelector('label'),
        errorMsg = form.querySelector('.box__error span')
        droppedFiles = false,
        triggerFormSubmit = function () {
            var event = document.createEvent('HTMLEvents');
            event.initEvent('submit', true, false);
            form.dispatchEvent(event);
        },
        addFile = function (file) {
            var preview = document.querySelector('#addedimage');
            var reader = new FileReader();

            reader.addEventListener("load", function () {
                preview.src = reader.result;
            }, false);

            if (file) {
                reader.readAsDataURL(file);
                $(".imagediv").show();
                $(".box__upload").hide();
            }
        },
        isAdvancedUpload = function () {
            var div = document.createElement('div');
            return (('draggable' in div) || ('ondragstart' in div && 'ondrop' in div)) && 'FormData' in window && 'FileReader' in window;
        }();

    // automatically submit the form on file select
    input.addEventListener('change', function (e) {
        form.classList.remove('is-error');

        var file = e.target.files[0];
        var fileType = file["type"];
        if (fileType.indexOf("image/") != 0) {
            errorMsg.innerText = "Можно загружать только изображения.";
            form.classList.add('is-error');
            return;
        }

        addFile(e.target.files[0]);
    });

    // drag&drop files if the feature is available
    if (isAdvancedUpload) {
        form.classList.add('has-advanced-upload'); // letting the CSS part to know drag&drop is supported by the browser

        ['drag', 'dragstart', 'dragend', 'dragover', 'dragenter', 'dragleave', 'drop'].forEach(function (event) {
            form.addEventListener(event, function (e) {
                // preventing the unwanted behaviours
                e.preventDefault();
                e.stopPropagation();
            });
        });
        ['dragover', 'dragenter'].forEach(function (event) {
            form.addEventListener(event, function () {
                form.classList.add('is-dragover');
                form.classList.remove('is-error');
            });
        });
        ['dragleave', 'dragend', 'drop'].forEach(function (event) {
            form.addEventListener(event, function () {
                form.classList.remove('is-dragover');
            });
        });
        form.addEventListener('drop', function (e) {
            droppedFiles = e.dataTransfer.files; // the files that were dropped

            if (droppedFiles.length > 1) {
                errorMsg.innerText = "Нельзя загружать больше одного файла.";
                form.classList.add('is-error');
                return;
            }

            var file = droppedFiles[0];
            var fileType = file["type"];
            if (fileType.indexOf("image/") != 0) {
                errorMsg.innerText = "Можно загружать только изображения.";
                form.classList.add('is-error');
                return;
            }

            addFile(droppedFiles[0]);
        });
    }

    // Firefox focus bug fix for file input
    input.addEventListener('focus', function () { input.classList.add('has-focus'); });
    input.addEventListener('blur', function () { input.classList.remove('has-focus'); });

}