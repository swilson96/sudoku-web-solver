import { GridValue } from "../types";

const initialiseArray = (length: number, width: number, depth?: number) => {
    const ret = new Array(length);
    for (let i = 0; i < length; ++i) {
        ret[i] = new Array(width);
        if (depth) {
            for (let j = 0; j < width; ++j) {
                ret[i][j] = new Array(depth);
            }
        }
    }
    return ret;
};

export default class Solver {
    private sudokuGrid: number[][] = initialiseArray(9, 9);
    private possibilities: number[][][]  = initialiseArray(9, 9, 9);
    private exit: boolean = false;
    private change: boolean = false;
    private contradiction: boolean = false;
    
    private log: (message: string) => void;

    public constructor(log?: (message: string) => void) {
        this.log = log || console.log;
    }

    public solve(input: GridValue): GridValue {
        for (let i = 0; i < 9; i++)
        {
            for (let j = 0; j < 9; j++)
            {
                this.sudokuGrid[i][j] = input[i][j] || 0;
            }
        }
        return this.findSolution(); // shouldn't have to convert zeros to nulls
    }

    private findSolution()
    {
        if (!this.isValid(this.sudokuGrid))
        {
            throw new Error("Invalid Sudoku");
        }
        for (let i = 0; i < 9; i++)          /* anything is possible                  */
            for (let j = 0; j < 9; j++)
                for (let k = 0; k < 9; k++)
                {
                    this.possibilities[i][j][k] = 1;
                }
        while (!this.finished())        /* the actual program: alternate between */
        {         
            this.basic();               /* basic techniques...  */
            if (this.exit)
                break;
            if (!this.change)
                if (!this.makeGuess())  /* ...and guessing                       */
                {
                    try
                    {
                        this.guess2();
                    }
                    catch (Exception)
                    {
                        throw new Error("Failed to solve this one!");
                    }
                    break;
                }
        }
        return this.sudokuGrid;
    }

    public isValid(input: number[][])  /* checks the current grid is valid */
    {     
        let list1:number[] = new Array(10);
        let list2:number[] = new Array(10);
        let list3:number[] = new Array(10);
        for(let i=0 ; i<9 ; i++) 
        {     /* i is a row, columnm or box */
            for (let j = 0; j < 10; j++)
            {
                list1[j] = 0;
                list2[j] = 0;
                list3[j] = 0;            /* lists mark off each number as it's found */
            }
            for(let j=0 ; j<9 ; j++) 
            {   /* j are the 9 elements of i */
                list1[input[i][j]]++;                           /* rows */
                if(list1[input[i][j]]>1 && input[i][j]>0)
                    return false;
                list2[input[j][i]]++;                           /* columns */
                if(list2[input[j][i]]>1 && input[j][i]>0)
                    return false;
                list3[ input[Math.floor(i/3)*3 + j%3][(i*3)%9 + Math.floor(j/3)] ]++; /* boxes */
                if(list3[ input[Math.floor(i/3)*3 + j%3][(i*3)%9 + Math.floor(j/3)] ] > 1 
                    && input[Math.floor(i/3)*3 + j%3][(i*3)%9 + Math.floor(j/3)] > 0)
                    return false;
            }
        }
        return true;
    }

    private updateGrid() /* puts in a number if it is the only possibility */
    {  
        for (let i = 0; i < 9; i++)
        {
            for (let j = 0; j < 9; j++)
                if (this.sudokuGrid[i][j] == 0)
                {
                    let sum = 0;
                    for (let k = 0; k < 9; k++)
                        sum += this.possibilities[i][j][k];
                    if (sum == 1)
                        for (let k = 0; k < 9; k++)
                            if (this.possibilities[i][j][k] == 1)
                            {
                                this.sudokuGrid[i][j] = k + 1;
                                this.change = true;
                                break;
                            }
                    if (sum < 1)
                    {
                        this.log(`Update error: no possibilities found for [${i}, ${j}]`);
                        this.contradiction = true;
                        return;
                    }
                }
        }
    } /* end of update */

    private updateGrid2() /* adds numbers if there is only one place they can go */
    {
        const sum: number[] = new Array(3);
        for (let n = 1; n < 10; n++)         /* n a number in the grid */
        {
            for (let i = 0; i < 9; i++)
            {      /* i a column, row, or box */
                for (let k = 0; k < 3; k++)      /* 0 column, 1 row, 2 box */
                    sum[k] = 0;
                for (let j = 0; j < 9; j++)
                {    /* j an element of i */
                    sum[0] += this.possibilities[i][j][n - 1];
                    sum[1] += this.possibilities[j][i][n - 1];
                    sum[2] += this.possibilities[Math.floor(i / 3) * 3 + j % 3][(i * 3) % 9 + Math.floor(j/3)][n - 1];
                }
                if (sum[0] == 1)
                    for (let j = 0; j < 9; j++)
                        if (this.possibilities[i][j][n - 1] == 1)
                        {
                            if (this.sudokuGrid[i][j] == 0)
                                this.change = true;
                            this.sudokuGrid[i][j] = n;
                            break;
                        }
                if (sum[0] < 1)
                {
                    this.log(`Update2 error: nowhere to put ${n} in column ${i}`);
                    this.contradiction = true;
                    return;
                }
                if (sum[1] == 1)
                    for (let j = 0; j < 9; j++)
                        if (this.possibilities[j][i][n - 1] == 1 && this.sudokuGrid[j][i] == 0)
                        {
                            this.sudokuGrid[j][i] = n;
                            this.change = true;
                            break;
                        }
                if (sum[1] < 1)
                {
                    this.log(`Update2 error: nowhere to put ${n} in row ${i}`);
                    this.contradiction = true;
                    return;
                }
                if (sum[2] == 1)
                    for (let j = 0; j < 9; j++)
                        if (this.possibilities[Math.floor(i / 3) * 3 + j % 3][(i * 3) % 9 + Math.floor(j/3)][n - 1] == 1)
                        {
                            if (this.sudokuGrid[Math.floor(i / 3) * 3 + j % 3][(i * 3) % 9 + Math.floor(j/3)] == 0)
                                this.change = true;
                            this.sudokuGrid[Math.floor(i / 3) * 3 + j % 3][(i * 3) % 9 + Math.floor(j/3)] = n;
                            break;
                        }
                if (sum[2] < 1)
                {
                    this.log(`Update2 error: nowhere to put ${n} in box ${i}`);
                    this.contradiction = true;
                    return;
                }
            }
        }
    }

    private reduce() 
    {       /* reduces possibilities according to a[, ] */
        for (let i = 0; i < 9; i++)
            for (let j = 0; j < 9; j++)
                if (this.sudokuGrid[i][j] > 0)
                {
                    for (let k = 0; k < 9; k++)
                    {
                        this.possibilities[i][k][this.sudokuGrid[i][j] - 1] = 0;
                        this.possibilities[k][j][this.sudokuGrid[i][j] - 1] = 0;
                        this.possibilities[i][j][k] = 0;
                    }
                    for (let k = 0; k < 3; k++)
                        for (let l = 0; l < 3; l++)
                            /* box coordinate i/3,j/3; top left of box is (i/3)*3,(j/3)*3 */
                            this.possibilities[Math.floor(i / 3) * 3 + k][Math.floor(j/3) * 3 + l][this.sudokuGrid[i][j] - 1] = 0;
                    this.possibilities[i][j][this.sudokuGrid[i][j] - 1] = 1;
                }
    }

    private finished() /* checks if the sudoku is finished yet */
    {    
        for (let i = 0; i < 9; i++)
            for (let j = 0; j < 9; j++)
                if (this.sudokuGrid[i][j] == 0)
                    return false;
        this.log("Solved it! (Whole grid is complete)");
        return true;
    }

    private basic() /* applies the basic methods repeatedly */
    {             
        let pass = 0;                 /* number of passes */
        this.contradiction=false;
        this.change=true;
        while(this.change && !this.finished()) 
        {
            this.reduce();
            this.change = false;
            this.updateGrid();
            this.updateGrid2();
            if(!this.isValid(this.sudokuGrid) || this.contradiction) 
            {
                // We might have screwed it up, but assume there is an inherent contradiction
                throw new Error("No solution exists");
            }
            ++pass;
        } /* end while loop */
        
        if (!this.change)
        {
            this.log(`Basic passes exhausted after ${pass} passes.`);
        }
    }

    private makeGuess() /* when other methods fail, makes a conjecture */
    {  
        let end = false;
        let pass = 1, maxpass = 100;
        let storeGrid = initialiseArray(9, 9);
        let storePoss = initialiseArray(9, 9, 9);
        //label2.Text = "\nBeginning this.makeGuess() function.";
        for (let row = 0; row < 9; row++)
        {
            for (let col = 0; col < 9; col++)
            {
                if (this.sudokuGrid[row][col] == 0)        /* conjecture that this cell... */
                {
                    for (let n = 1; n <= 9; n++)   /* ...contains n                */
                    {
                        if (this.possibilities[row][col][n - 1] == 1)
                        {
                            this.log(`Conjecturing that [${row}, ${col}] contains ${n}...`);
                            for (let i = 0; i < 9; i++) /* fill in the storage arrays */
                                for (let j = 0; j < 9; j++)
                                {
                                    storeGrid[i][j] = this.sudokuGrid[i][j];
                                    for (let k = 0; k < 9; k++)
                                        storePoss[i][j][k] = this.possibilities[i][j][k];
                                }
                            this.sudokuGrid[row][col] = n; /* make the conjecture! */
                            this.contradiction = false;
                            while (pass <= maxpass)
                            {
                                this.reduce();
                                this.change = false;
                                this.updateGrid();
                                if (!this.contradiction)
                                    this.updateGrid2();
                                if (this.finished())
                                {
                                    this.log("done, leading to the solution");
                                    // FINISHED Display();
                                    return true;
                                }
                                if (!this.isValid(this.sudokuGrid) || this.contradiction)
                                {
                                    storePoss[row][col][n - 1] = 0;
                                    end = true;
                                    this.log("done, ruled it out.");
                                    break;
                                }
                                if (!this.change)
                                {
                                    this.log("done, got stuck.");
                                    break;
                                }
                                pass++;
                            } /* end while loop */
                            for (let i = 0; i < 9; i++) /* restore a[, ] and poss[, , ] */
                                for (let j = 0; j < 9; j++)
                                {
                                    this.sudokuGrid[i][j] = storeGrid[i][j];
                                    for (let k = 0; k < 9; k++)
                                        this.possibilities[i][j][k] = storePoss[i][j][k];
                                }
                        } /* end of current conjecture "if n is a poss..."; end n loop */
                    }
                    if (end)
                    {
                        return true;
                    }
                }
            } /* end col loop */
        }
        return end;
    } /* end of this.makeGuess() */

    private guess2() /* desperate now - try the long way */
    {
        //const store: number[][] = initialiseArray(9, 9);
        const storePoss: number[][][] = initialiseArray(9, 9, 9);
        this.log("Beginning an exhaustive backtracking search.");
        for (let i = 0; i < 9; i++) /* fill in the storage arrays */
        {
            for (let j = 0; j < 9; j++)
            {
                //store[i][j] = this.sudokuGrid[i][j];
                for (let k = 0; k < 9; k++)
                {
                    storePoss[i][j][k] = this.possibilities[i][j][k];
                }
            }
        }
        let row = 0;
        let col = 0;
        let n = 1;
        while (row < 9)
        {
            while(col<9)
            {
                while(n<10)
                {
                    if (this.possibilities[row][col][n-1] == 1)
                    {
                        this.sudokuGrid[row][col] = n;
                        n = 1;
                        break;
                    }
                    n++;
                }
                if (n >= 10) //Occurs only if we didn't find anything
                {
                    //Backtrack here
                    if (col === 0)
                    {
                        row--;
                        col = 8;
                    }
                    else
                    {
                        col--;
                    }
                    n = this.sudokuGrid[row][col] + 1;
                    this.sudokuGrid[row][col] = 0;
                    for (let i = 0; i < 9; ++i)
                    {
                        for (let j = 0; j < 9; ++j)
                        {
                            for (let k = 0; k < 9; ++k)
                            {
                                this.possibilities[i][j][k] = storePoss[i][j][k];
                            }
                        }
                    }
                    this.reduce();
                }
                else
                {
                    this.reduce();
                    col++;
                    n = 1;
                }
            }
            row++;
            col = 0;
        }
        // FINISHED
        this.log("Finished.");
    }
}