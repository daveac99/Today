$(document).ready(function () {

    $(".add-handle").click(function() {
        addNewToday($(this));
    });

    $("#todayAdd").keypress(function(event) {
        if (event.which === 13) {
            $(".add-handle").click();
        }
    });

    setUpRemovableOrAddableElements();

});

function setUpRemovableOrAddableElements() {

    setupJqueryUiSortableAndDraggable();
    allowDescriptionsToAutoSave();
    setupEventHandlers();
}

function setupJqueryUiSortableAndDraggable() {
    $("#sortable-todays").sortable({
        placeholder: "ui-state-highlight",
        handle: ".dragdrop-handle",
        opacity: 0.5,
        cursor: "move",
        update: function (event, ui) {
            updateSortOrderinDB(event, ui, $(this));
        }
    });
}

function allowDescriptionsToAutoSave() {
    var timeoutId;
    $("input[name='Description']").on("input", function () {
        clearTimeout(timeoutId);
        var element = $(this);
        timeoutId = setTimeout(function () {
            var id = element.attr("today-id");
            var description = element.val();
            updateDescriptionInDB(id, description);
        }, 1000);

    });
}

function setupEventHandlers() {
    $(".delete-handle").click(function () {
        deleteFromDB($(this).attr("today-id"));
    });

    $("input:checkbox[Name='Done']").click(function () {

        var done = ($(this).is(":checked"));
        SetDoneValueInDB($(this).attr("today-id"), done);
    });
}

//functions to update database

function updateDescriptionInDB(id, description) {
    $.post(getUrl("UpdateDescription"), { id: id, description: description });

}

function SetDoneValueInDB(id, done) {

    //move item into completed area or vice-versa
    var todayHtml;
    if (done) {
        todayHtml = $("#todayItem_" + id);
        todayHtml.remove();
        $("#completed-todays").prepend(todayHtml);

    } else {
        todayHtml = $("#todayItem_" + id);
        todayHtml.remove();
        $("#sortable-todays").append(todayHtml);
    }

    $.post(getUrl("UpdateDone"), { id: id, done: done },
        function (data) {
            setUpRemovableOrAddableElements();
        });


}

function deleteFromDB(id) {
    //remove the element
    $("#todayItem_" + id).remove();

    //delete from db
    $.post(getUrl("DeleteToday"), { id: id });

}

function addNewToday(jqueryElement) {
    var newTodayItem = jqueryElement.prev("input");
    var description = newTodayItem.val();
    newTodayItem.val("");
    $.post(getUrl("AddNewToday"),
        { description: description },
        function (data) {
            //add new item onto existing html
            $("#sortable-todays").append(data);
            setUpRemovableOrAddableElements();
        });
}

function updateSortOrderinDB(event, ui, element) {
    var todays = element.sortable("toArray");
    console.log(todays);
    $.post(getUrl("UpdateSortOrder"), { todays: todays });
}



//helper functions

function getUrl(action) {
    return "Todays/" + action;
}

//TODO ValidateAntiForgeryToke version of $.post