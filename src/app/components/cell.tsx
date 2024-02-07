import { useMemo } from "react";
import { CellValue } from "../types";

type CellProps = { 
    rowIndex: number;
    colIndex: number;
    value: CellValue;
    setValue: (v: CellValue) => void;
    touched: boolean;
    setTouched: (b: boolean) => void;
};

export default function Cell({ rowIndex, colIndex, value, setValue, touched, setTouched }: CellProps) {
    const classes = useMemo(() => {
        let c = "p-0.5 sm:p-3 h-15 w-15";
        if (colIndex === 2 || colIndex === 5) {
            c += " border-r-2";
        }
        if (rowIndex === 2 || rowIndex === 5) {
            c += " border-b-2";
        }
        return c;
    }, [colIndex, rowIndex])

    const onChange = useMemo(() => (e: React.FormEvent<HTMLInputElement>) => {
        if (!e.currentTarget.value || e.currentTarget.value == "") {
            setValue(null);
            setTouched(false);
        } else {
            setValue(Number(e.currentTarget.value));
            setTouched(true);
        }
    }, [setValue, setTouched]);

    const inputClasses = "h-8 w-8 sm:h-12 sm:w-12 p-1 sm:p-3 " + (touched ? "bg-gray-600" : "bg-gray-800");
    
    return (
        <div className={classes}>
            <input value={value || ""} onChange={onChange} className={inputClasses} type="number" min="1" max="9"  />
        </div>
    );
}