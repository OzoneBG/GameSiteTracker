$("#pop").on("click", function () {
    $('#imagepreview').attr('src', $('#imageresource').attr('src')); // here asign the image to the modal when the user click the enlarge link
    $('#imagemodal').modal('show'); // imagemodal is the id attribute assigned to the bootstrap modal, then i use the show function
});

$(document).ready(function () {
    for (var i = 0; i < model.length; i++) {
        if (model[i].Category.indexOf(lastEntry) != -1) {
            var tr = $('<tr class="rowToDelete">');

            var id = $('<td>').html(model[i].Id);
            var category = $('<td>').html(model[i].Category);
            var content = $('<td>').html(model[i].Content);
            var createdOn = $('<td>').html(model[i].CreatedOn);

            tr.append(id);
            tr.append(category);
            tr.append(content);
            tr.append(createdOn);

            $(tr).insertAfter("tbody");
        }
    }
});

$("#searchField").keyup(function (event) {
    if ($("#searchField").val() != lastEntry) {
        lastEntry = $("#searchField").val();
        $(".rowToDelete").remove();
        for (var i = 0; i < model.length; i++) {
            if (model[i].Category.indexOf(lastEntry) != -1) {
                var tr = $('<tr class="rowToDelete">');

                var id = $('<td>').html(model[i].Id);
                var category = $('<td>').html(model[i].Category);
                var content = $('<td>').html(model[i].Content);
                var createdOn = $('<td>').html(model[i].CreatedOn);

                tr.append(id);
                tr.append(category);
                tr.append(content);
                tr.append(createdOn);

                $(tr).insertAfter("tbody");
            }
        }
    }

});