window.sudoku = window.sudoku || {};

window.sudoku.solver = (function() {

    var solver = {
        solve: solve
    };

    return solver;

    function solve(model) {
        clearErrorMessage(model);
        return ajaxRequest("post", "api/Solution", model.sudoku)
            .done(function(result) {
                //NOP
                model.errorMessage("Success: " + result);
            })
            .fail(function() {
                model.errorMessage("+++MELON MELON MELON+++");
            });
    }

    // Private
    function clearErrorMessage(entity) { entity.errorMessage(null); }
    function ajaxRequest(type, url, data, dataType) { // Ajax helper
        var options = {
            dataType: dataType || "json",
            contentType: "application/json",
            cache: false,
            type: type,
            data: data ? ko.toJSON(data) : null
        };
        var antiForgeryToken = $("#antiForgeryToken").val();
        if (antiForgeryToken) {
            options.headers = {
                'RequestVerificationToken': antiForgeryToken
            };
        }
        return $.ajax(url, options);
    }

})();