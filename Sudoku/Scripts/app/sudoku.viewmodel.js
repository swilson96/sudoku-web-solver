window.sudoku.model = (function(ko, solver) {

    var sudoku = ko.observableArray(),
        error = ko.observable(),
        solve = function() {
            solver.solve(this);
        };

    for (var i = 0; i < 9 ; ++i) {
        sudoku()[i] = ko.observableArray();
        for (var j = 0; j < 9; ++j) {
            sudoku()[i]()[j] = ko.observable();
        }
    }

    return {
        sudoku: sudoku,
        errorMessage: error,
        solve: solve
    };

})(ko, window.sudoku.solver);

// Initiate the Knockout bindings
ko.applyBindings(window.sudoku.model);
