using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGeneration.Generators {
    class Sidewinder : IGenerator {

        ///Public Methods
        //Generate method will apply the Sidewinder algorithm to the grid object.
        public void Generate(Grid _grid) {

            //Create random object.
            Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);

            //Loop through all cells.
            foreach (List<Cell> row in _grid.grid) {

                //Create the run.
                List<Cell> run = new List<Cell>();

                foreach (Cell cell in row) {

                    //Check for edges.
                    if (cell.row == 0) cell.Link(cell.neighbours[(int)DIRECTION.EAST], true);
                    else if (cell.col == _grid.cols - 1) cell.Link(cell.neighbours[(int)DIRECTION.NORTH], true);
                    else {

                        //Randomize event.
                        int chance = rnd.Next(2);
                        run.Add(cell);

                        if (chance == 0) {
                            //Carve east and continue.
                            cell.Link(cell.neighbours[(int)DIRECTION.EAST], true);
                        } else {
                            //Choose random cell in run, have it carve north, clear the run.
                            Cell runCell = run[rnd.Next(run.Count)];
                            runCell.Link(runCell.neighbours[(int)DIRECTION.NORTH], true);
                            run.Clear();
                        }
                    }
                }

            }

        }
    }
}
