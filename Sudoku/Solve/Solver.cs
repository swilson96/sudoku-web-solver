using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

namespace Sudoku
{
    /// <summary>
    /// Don't judge my by this ancient code. I deliberately haven't tidied it up.
    /// </summary>
    public class Solver
    {
        int[, ] SudokuGrid = new int[9, 9];
        int[, , ] Possibilities = new int[9, 9, 9];
        bool Restart;
        bool Exit = false;
        bool Change;
        bool Contradiction;

        public int[,] Solve(int?[,] Boxes)
        {
            Restart = false;
            int i, j;
            for (i = 0; i < 9; i++)
            {
                for (j = 0; j < 9; j++)
                {
                    if (Boxes[i, j] == null)
                    {
                        SudokuGrid[i, j] = 0;
                    }
                    else
                    {
                        SudokuGrid[i, j] = Boxes[i, j].Value;
                    }
                }
            }
            //if (Restart != true)
            //{
                return FindSolution();
            //}
        }

        private int[,] FindSolution()
        {
            int i, j, k;
            if (!IsValid())
            {
                throw new InvalidSudokuException();
            }
            for (i = 0; i < 9; i++)          /* anything is possible                  */
                for (j = 0; j < 9; j++)
                    for (k = 0; k < 9; k++)
                    {
                        Possibilities[i, j, k] = 1;
                    }
            while (!Finished())          /* the actual program: alternate between */
            {         
                Basic();                     /* basic techniques...  */
                if (Exit)
                    break;
                if (!Change)
                    if (!MakeGuess())  /* ...and guessing                       */
                    {
                        try
                        {
                            Guess2();
                        }
                        catch (Exception)
                        {
                            throw new UnableToSolveException();
                        }
                        break;
                    }
            }
            return SudokuGrid;
        }

        private bool IsValid()  /* checks the current grid is valid */
        {     
            int i,j;
            int[] list1 = new int[10];
            int[] list2 = new int[10];
            int[] list3 = new int[10];
            for(i=0 ; i<9 ; i++) 
            {     /* i is a row, columnm or box */
                for (j = 0; j < 10; j++)
                {
                    list1[j] = 0;
                    list2[j] = 0;
                    list3[j] = 0;            /* lists mark off each number as it's found */
                }
                for(j=0 ; j<9 ; j++) 
                {   /* j are the 9 elements of i */
                    list1[SudokuGrid[i, j]]++;                           /* rows */
                    if(list1[SudokuGrid[i, j]]>1 && SudokuGrid[i, j]>0)
	                    return false;
                    list2[SudokuGrid[j, i]]++;                           /* columns */
                    if(list2[SudokuGrid[j, i]]>1 && SudokuGrid[j, i]>0)
	                    return false;
                    list3[ SudokuGrid[(i/3)*3 + j%3, (i*3)%9 + j/3] ]++; /* boxes */
                    if(list3[ SudokuGrid[(i/3)*3 + j%3, (i*3)%9 + j/3] ] > 1 
	                    && SudokuGrid[(i/3)*3 + j%3, (i*3)%9 + j/3] > 0)
	                    return false;
                }
            }
        return true;
        }

        private void UpdateGrid() /* puts in a number if it is the only possibility */
        {  
            int i, j, k;
            int sum;
            for (i = 0; i < 9; i++)
            {
                for (j = 0; j < 9; j++)
                    if (SudokuGrid[i, j] == 0)
                    {
                        sum = 0;
                        for (k = 0; k < 9; k++)
                            sum += Possibilities[i, j, k];
                        if (sum == 1)
                            for (k = 0; k < 9; k++)
                                if (Possibilities[i, j, k] == 1)
                                {
                                    SudokuGrid[i, j] = k + 1;
                                    Change = true;
                                    break;
                                }
                        if (sum < 1)
                        {
                            //printf("\nUpdate error: no possibilities found for [%d, %d]\n", i, j);
                            Contradiction = true;
                            return;
                        }
                    }
            }
        } /* end of update */

        private void UpdateGrid2() /* adds numbers if there is only one place they can go */
        {
            int n, i, j, k;
            int[] sum = new int[3];
            for (n = 1; n < 10; n++)         /* n a number in the grid */
            {
                for (i = 0; i < 9; i++)
                {      /* i a column, row, or box */
                    for (k = 0; k < 3; k++)      /* 0 column, 1 row, 2 box */
                        sum[k] = 0;
                    for (j = 0; j < 9; j++)
                    {    /* j an element of i */
                        sum[0] += Possibilities[i, j, n - 1];
                        sum[1] += Possibilities[j, i, n - 1];
                        sum[2] += Possibilities[(i / 3) * 3 + j % 3, (i * 3) % 9 + j / 3, n - 1];
                    }
                    if (sum[0] == 1)
                        for (j = 0; j < 9; j++)
                            if (Possibilities[i, j, n - 1] == 1)
                            {
                                if (SudokuGrid[i, j] == 0)
                                    Change = true;
                                SudokuGrid[i, j] = n;
                                break;
                            }
                    if (sum[0] < 1)
                    {
                        //printf("\nUpdate2 error: nowhere to put %d in column %d\n", n, i);
                        Contradiction = true;
                        return;
                    }
                    if (sum[1] == 1)
                        for (j = 0; j < 9; j++)
                            if (Possibilities[j, i, n - 1] == 1 && SudokuGrid[j, i] == 0)
                            {
                                SudokuGrid[j, i] = n;
                                Change = true;
                                break;
                            }
                    if (sum[1] < 1)
                    {
                        //printf("\nUpdate2 error: nowhere to put %d in row %d\n", n, i);
                        Contradiction = true;
                        return;
                    }
                    if (sum[2] == 1)
                        for (j = 0; j < 9; j++)
                            if (Possibilities[(i / 3) * 3 + j % 3, (i * 3) % 9 + j / 3, n - 1] == 1)
                            {
                                if (SudokuGrid[(i / 3) * 3 + j % 3, (i * 3) % 9 + j / 3] == 0)
                                    Change = true;
                                SudokuGrid[(i / 3) * 3 + j % 3, (i * 3) % 9 + j / 3] = n;
                                break;
                            }
                    if (sum[2] < 1)
                    {
                        //printf("\nUpdate2 error: nowhere to put %d in box %d\n", n, i);
                        Contradiction = true;
                        return;
                    }
                }
            }
        }

        private void Reduce() 
        {       /* reduces possibilities according to a[, ] */
            int i, j, k, l;
            for (i = 0; i < 9; i++)
                for (j = 0; j < 9; j++)
                    if (SudokuGrid[i, j] > 0)
                    {
                        for (k = 0; k < 9; k++)
                        {
                            Possibilities[i, k, SudokuGrid[i, j] - 1] = 0;
                            Possibilities[k, j, SudokuGrid[i, j] - 1] = 0;
                            Possibilities[i, j, k] = 0;
                        }
                        for (k = 0; k < 3; k++)
                            for (l = 0; l < 3; l++)
                                /* box coordinate i/3,j/3; top left of box is (i/3)*3,(j/3)*3 */
                                Possibilities[(i / 3) * 3 + k, (j / 3) * 3 + l, SudokuGrid[i, j] - 1] = 0;
                        Possibilities[i, j, SudokuGrid[i, j] - 1] = 1;
                    }
        }

        private bool Finished() /* checks if the sudoku is finished yet */
        {    
            int i, j;
            for (i = 0; i < 9; i++)
                for (j = 0; j < 9; j++)
                    if (SudokuGrid[i, j] == 0)
                        return false;
            //label2.Text = "Solved it!";
            return true;
        }

        private void Basic() /* applies the basic methods repeatedly */
        {             
            int pass=0;                 /* number of passes */
            Contradiction=false;
            Change=true;
            while(Change && !Finished()) 
            {
                Reduce();
                Change=false;
                UpdateGrid();
                UpdateGrid2();
                if(!IsValid() || Contradiction) 
                {
                    //Display();
                    //outposs();
                    //label2.Text = "I screwed it up!";
                    Exit = true;
                    return;
                }
                pass++;
            } /* end while loop */
            //printf("\nAfter %d basic passes we have:",pass);
            // ??? Display();
            if (!Change)
            {
                //label2.Text = "Basic passes exhausted.";
            }
        }

        private bool MakeGuess()  /* when other methods fail, makes a conjecture */
        {  
            int n,i,j,k;         /* and seeks a contradiction/solution          */
            int row,col;
            bool end=false;
            int pass=1,maxpass=100;
            int[,] StoreGrid = new int[9,9];
            int[,,] StorePoss = new int[9, 9, 9];
            //label2.Text = "\nBeginning makeguess function.";
            for (row = 0; row < 9; row++)
            {
                for (col = 0; col < 9; col++)
                {
                    if (SudokuGrid[row, col] == 0)        /* conjecture that this cell... */
                    {
                        for (n = 1; n <= 9; n++)   /* ...contains n                */
                        {
                            if (Possibilities[row, col, n - 1] == 1)
                            {
                                //printf("\nConjecturing that [%d, %d] contains %d...",row,col,n);
                                for (i = 0; i < 9; i++) /* fill in the storage arrays */
                                    for (j = 0; j < 9; j++)
                                    {
                                        StoreGrid[i, j] = SudokuGrid[i, j];
                                        for (k = 0; k < 9; k++)
                                            StorePoss[i, j, k] = Possibilities[i, j, k];
                                    }
                                SudokuGrid[row, col] = n; /* make the conjecture! */
                                Contradiction = false;
                                while (pass <= maxpass)
                                {
                                    Reduce();
                                    Change = false;
                                    UpdateGrid();
                                    if (!Contradiction)
                                        UpdateGrid2();
                                    if (Finished())
                                    {
                                        //printf("done, leading to the solution:");
                                        // FINISHED Display();
                                        return true;
                                    }
                                    if (!IsValid() || Contradiction)
                                    {
                                        StorePoss[row, col, n - 1] = 0;
                                        end = true;
                                        //printf("done, ruled it out.");
                                        break;
                                    }
                                    if (!Change)
                                    {
                                        //printf("done, got stuck.");
                                        break;
                                    }
                                    pass++;
                                } /* end while loop */
                                for (i = 0; i < 9; i++) /* restore a[, ] and poss[, , ] */
                                    for (j = 0; j < 9; j++)
                                    {
                                        SudokuGrid[i, j] = StoreGrid[i, j];
                                        for (k = 0; k < 9; k++)
                                            Possibilities[i, j, k] = StorePoss[i, j, k];
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
        } /* end of makeguess */

        private void Guess2() /* desperate now - try the long way */
        {
            int i, j, k, n;
            int row, col;
            int[,] Store = new int[9, 9];
            int[, ,] StorePoss = new int[9, 9, 9];
            //label2.Text = "Beginning guess2 function.";
            for (i = 0; i < 9; i++) /* fill in the storage arrays */
            {
                for (j = 0; j < 9; j++)
                {
                    Store[i, j] = SudokuGrid[i, j];
                    for (k = 0; k < 9; k++)
                    {
                        StorePoss[i, j, k] = Possibilities[i, j, k];
                    }
                }
            }
            row = 0;
            col = 0;
            n = 1;
            while (row < 9)
            {
                while(col<9)
                {
                    while(n<10)
                    {
                        if (Possibilities[row, col, n-1] == 1)
                        {
                            SudokuGrid[row, col] = n;
                            n = 1;
                            break;
                        }
                        n++;
                    }
                    if (n >= 10) //Occurs only if we didn't find anything
                    {
                        //Backtrack here
                        if (col == 0)
                        {
                            row--;
                            col = 8;
                        }
                        else
                        {
                            col--;
                        }
                        n = SudokuGrid[row, col] + 1;
                        SudokuGrid[row, col] = 0;
                        for (i = 0; i < 9; ++i)
                        {
                            for (j = 0; j < 9; ++j)
                            {
                                for (k = 0; k < 9; ++k)
                                {
                                    Possibilities[i, j, k] = StorePoss[i, j, k];
                                }
                            }
                        }
                        Reduce();
                    }
                    else
                    {
                        Reduce();
                        //Basic();
                        //Reduce();
                        col++;
                        n = 1;
                    }
                }
                row++;
                col = 0;
            }
            // FINISHED Display();
            //label2.Text = "Solved it!";
        }

    }
}