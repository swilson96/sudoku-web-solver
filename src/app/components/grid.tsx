import { useMemo } from 'react';
import Cell, { CellValue } from './cell';

type GridProps = {
    value: CellValue[][];
    setValue: (v: CellValue[][]) => void;
};

export default function Grid({ value, setValue }: GridProps) {
    let setSingleValue = useMemo(() => (rowTarget: number, colTarget: number) => (newVal: CellValue) => {
        const newGrid = [...value.map((row, rowIndex) => rowIndex === rowTarget ? [...row.map((colVal, colIndex) => colIndex === colTarget ? newVal : colVal)] : [...row])];
        setValue(newGrid);
    }, [setValue, value]);

    return (
        <div className="grid lg:grid-cols-9">
            {Array.from({ length: 9 }, (_, i) => (
                <div key ={i} className="">
                    {Array.from({ length: 9 }, (_, j) => (
                        <Cell key={j} rowIndex={j} colIndex={i} value={value[i][j]} setValue={setSingleValue(i, j)} />
                    ))}
                </div>
            ))}
        </div>
    );
}