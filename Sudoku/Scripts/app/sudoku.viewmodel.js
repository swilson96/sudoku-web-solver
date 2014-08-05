window.sudoku.model = (function(ko, solver) {

    var sudoku = ko.observableArray(),
        error = ko.observable(),
        solve = function() {
            solver.solve(this);
        },
        toGrid = function() {
            var grid = [];
            sudoku().forEach(function(row) {
                grid.push(row());
            });
            return grid;
        };

    for (var i = 0; i < 9 ; ++i) {
        sudoku()[i] = ko.observableArray();
        for (var j = 0; j < 9; ++j) {
            sudoku()[i]()[j] = null;
        }
    }

    return {
        sudoku: sudoku,
        errorMessage: error,
        solve: solve,
        toGrid: toGrid
    };

})(ko, window.sudoku.solver);

// Initiate the Knockout bindings
ko.applyBindings(window.sudoku.model);
