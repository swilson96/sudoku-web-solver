"use client";

import React, { useState } from 'react';
import { GridValue } from '../types';
import Grid from './grid';
import SudokuSolver from '../logic/solver';

const initialState: GridValue = new Array();
const untouchedState: boolean[][] = new Array();
for (let i = 0; i < 9; ++i) {
    initialState[i] = new Array();
    untouchedState[i] = new Array();
    for (let j = 0; j < 9; ++j) {
        initialState[i][j] = null;
        untouchedState[i][j] = false;
    }
}

export default function Solver() {
    const [gridState, setGridState] = useState<GridValue>(initialState);
    const [touchedState, setTouchedState] = useState<boolean[][]>(untouchedState);
    const [error, setError] = useState("");

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        setError("");
        try {
            const solver = new SudokuSolver();
            const solution = solver.solve(gridState);
            setGridState(solution);
        } catch (e) {
            setError(e instanceof Error ? e.message : String(e));
        }
    };

    const clearUntouched = () => {
        setGridState([...gridState.map((row, rowIndex) => [...row.map((colVal, colIndex) => touchedState[rowIndex][colIndex] ? colVal : null)])]);
    };

    const clearAll = () => {
        setGridState(initialState);
        setTouchedState(untouchedState);
    };

    return (
        <form onSubmit={handleSubmit}>
            {!!error && <p className="p-3 text-red-600">{error}</p>}
            {!error && <p className="p-3">Enter what you know and click &quot;Solve&quot;</p>}
            <Grid value={gridState} setValue={setGridState} touched={touchedState} setTouched={setTouchedState} />
            <button type="submit" className="mx-3 mt-12 px-8 py-4 bg-gray-700 hover:bg-gray-600">Solve</button>
            <button type="button" onClick={clearUntouched} className="mx-3 mt-12 px-8 py-4 bg-gray-800 hover:bg-gray-600">Clear solution</button>
            <button type="button" onClick={clearAll} className="mx-3 mt-12 px-8 py-4 bg-gray-800 hover:bg-gray-600">Clear all</button>
        </form>
    );
}