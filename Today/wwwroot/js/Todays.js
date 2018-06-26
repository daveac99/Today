$(document).ready(function () {

    setUpPageElements();

});

function setUpPageElements() {

    // jquery ui sortable and draggable setup
    $("#sortable-todays").sortable({
        placeholder: "ui-state-highlight",
        handle: ".dragdrop-handle",
        opacity: 0.5,
        cursor: "move",
        update: function (event, ui) {
            updateSortOrderinDB(event, ui, $(this));
        }
    });

    //allow descriptions to auto-save
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

    // event handlers

    $(".delete-handle").click(function () {
        deleteFromDB($(this).attr("today-id"));
    });

    $(".add-handle").click(function () {
        addNewToday($(this));
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

    $.post(getUrl("UpdateDone"), { id: id, done: done },
        function (data) {
            //move item into completed area or vice-versa
            var todayHtml;
            if (done) {
                todayHtml = $("#todayItem_" + id);
                todayHtml.remove();
                $("#completed-todays").append(todayHtml);

            } else {
                todayHtml = $("#todayItem_" + id);
                todayHtml.remove();
                $("#sortable-todays").append(todayHtml);
            }
            setUpPageElements();
        });


}

function deleteFromDB(id) {
    //remove the element
    $("#todayItem_" + id).remove();

    //delete from db
    $.post(getUrl("DeleteToday"), { id: id });

}

function addNewToday(element) {
    var description = element.prev("input").val();
    console.log(description);
    $.post(getUrl("AddNewToday"),
        { description: description },
        function (data) {
            //add new item onto existing html
            $("#sortable-todays").append(data);
            setUpPageElements();
        });
}

function updateSortOrderinDB(event, ui, element) {
    //  console.log(element);
    var todays = element.sortable("toArray");
    console.log(todays);
    $.post(getUrl("UpdateSortOrder"), { todays: todays });
}



//helper functions

function getUrl(action) {
    return "Todays/" + action;
}

//TODO ValidateAntiForgeryToke version of $.post