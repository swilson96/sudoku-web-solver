"use client";

import { useState } from 'react';
import { CellValue } from './cell';
import Grid from './grid'

const initialState: CellValue[][] = new Array();
for (let i = 0; i < 9; ++i) {
    initialState[i] = new Array();
    for (let j = 0; j < 9; ++j) {
        initialState[i][j] = null;
    }
}

export default function Solver() {
    const [gridState, setGridState] = useState<CellValue[][]>(initialState);

    const solve = () => console.log("solve it");

    return (
        <div>
            <Grid value={gridState} setValue={setGridState}/>
            <button type="button" onClick={solve} className="mx-3 mt-12 px-8 py-4 bg-gray-800 hover:bg-white-900">Solve</button>
        </div>
    );
}