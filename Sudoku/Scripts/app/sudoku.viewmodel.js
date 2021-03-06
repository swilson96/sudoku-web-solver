﻿window.sudoku.model = (function(ko, solver) {

    var sudoku = ko.observableArray();

    for (var i = 0; i < 9 ; ++i) {
        sudoku()[i] = ko.observableArray();
        for (var j = 0; j < 9; ++j) {
            sudoku()[i]()[j] = ko.observable();
        }
    }

    return {
        sudoku: sudoku,
        errorMessage: ko.observable(),
        working: ko.observable(false),
        solve: function () {
            this.working(true);
            solver.solve(this);
        },
        setSolution: function (result) {
            for (var i = 0; i < 9 ; ++i) {
                this.sudoku()[i](result[i]);
            }
            this.working(false);
        },
        error: function (message) {
            this.errorMessage(message);
            this.working(false);
        }
    };

})(ko, window.sudoku.solver);

// Initiate the Knockout bindings
ko.applyBindings(window.sudoku.model);
