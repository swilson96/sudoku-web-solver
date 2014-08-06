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
                model.setSolution(result);
            })
            .fail(function (err) {
                if (err.status == 404) {
                    model.error("Can't find the server, did you lose the internet?");
                } else if (err.status == 400) {
                    model.error("Are you sure that's a valid Sudoku?");
                } else if (err.status == 501) {
                    model.error("This one is too hard for me! Well done!");
                } else {
                    model.error("+++MELON MELON MELON+++");
                }
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