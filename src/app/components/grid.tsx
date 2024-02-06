import { Fragment, useMemo } from 'react';
import { CellValue, GridValue } from '../types';
import Cell from './cell';

type GridProps = {
    value: GridValue;
    setValue: (v: GridValue) => void;
    touched: boolean[][];
    setTouched: (v: boolean[][]) => void;
};

export default function Grid({ value, setValue, touched, setTouched }: GridProps) {
    const setSingleValue = useMemo(() => (rowTarget: number, colTarget: number) => (newVal: CellValue) => {
        const newGrid = [...value.map((row, rowIndex) => rowIndex === rowTarget ? [...row.map((colVal, colIndex) => colIndex === colTarget ? newVal : colVal)] : [...row])];
        setValue(newGrid);
    }, [setValue, value]);

    const setSingleTouched = useMemo(() => (rowTarget: number, colTarget: number) => (newVal: boolean) => {
        const newGrid = [...touched.map((row, rowIndex) => rowIndex === rowTarget ? [...row.map((colVal, colIndex) => colIndex === colTarget ? newVal : colVal)] : [...row])];
        setTouched(newGrid);
    }, [setTouched, touched]);

    return (
        <div className="grid grid-cols-9">
            {Array.from({ length: 9 }, (_, i) => (
                <Fragment key ={i}>
                    {Array.from({ length: 9 }, (_, j) => (
                        <Cell key={j} rowIndex={i} colIndex={j} value={value[i][j]} setValue={setSingleValue(i, j)} touched={touched[i][j]} setTouched={setSingleTouched(i, j)} />
                    ))}
                </Fragment>
            ))}
        </div>
    );
}