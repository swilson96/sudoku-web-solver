"use client";

import React, { useState } from 'react';
import { GridValue } from '../types';
import Grid from './grid'

const initialState: GridValue = new Array();
for (let i = 0; i < 9; ++i) {
    initialState[i] = new Array();
    for (let j = 0; j < 9; ++j) {
        initialState[i][j] = null;
    }
}

export default function Solver() {
    const [gridState, setGridState] = useState<GridValue>(initialState);

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        console.log("solve it");
    }

    return (
        <form onSubmit={handleSubmit}>
            <Grid value={gridState} setValue={setGridState}/>
            <button type="submit" className="mx-3 mt-12 px-8 py-4 bg-gray-800 hover:bg-white-900">Solve</button>
        </form>
    );
}