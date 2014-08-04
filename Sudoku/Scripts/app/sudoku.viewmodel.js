window.sudoku.model = (function(ko, solver) {

    var sudoku = ko.observable(),
        error = ko.observable(),
        solve = function() {
            solver.solve(this);
        };

    return {
        sudoku: sudoku,
        errorMessage: error,
        solve: solve
    };

})(ko, window.sudoku.solver);

// Initiate the Knockout bindings
ko.applyBindings(window.sudoku.model);
