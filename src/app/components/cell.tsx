import { useMemo, useState } from "react";

export type CellValue = number | null;

type CellProps = { 
    rowIndex: number;
    colIndex: number;
    value: CellValue;
    setValue: (v: CellValue) => void 
};

export default function Cell({ rowIndex, colIndex, value, setValue }: CellProps) {
    const [edited, setEdited] = useState(false);

    const classes = useMemo(() => {
        let c = "p-3";
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
            setEdited(false);
        } else {
            setValue(Number(e.currentTarget.value));
            setEdited(true);
        }
    }, [setValue]);

    const inputClasses = "mh-12 mw-12 p-3 " + (edited ? "bg-gray-600" : "bg-gray-800");
    
    return (
        <div className={classes}>
            <input value={value || ""} onChange={onChange} className={inputClasses} type="number" min={1} max={9} />
        </div>
    );
}