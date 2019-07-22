using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGeneration.Generators {
    class BinaryTree :IGenerator {

        ///Public Methods
        //Generate function will apply the BinaryTree algorithm to the grid object.
        public void Generate(Grid _grid) {

            //Create random object.
            Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);

            //Loop through all cells.
            foreach (List<Cell> row in _grid.grid) {
                foreach(Cell cell in row) {

                    //Check for edges.
                    Cell neighbour;
                    if (cell.row == 0) neighbour = cell.neighbours[(int)DIRECTION.EAST];
                    else if (cell.col == _grid.cols - 1) neighbour = cell.neighbours[(int)DIRECTION.NORTH];
                    else {
                        //Choose either north or east.
                        int index = rnd.Next(2);
                        neighbour = cell.neighbours[index];
                    }
                    
                    //If valid neighbour, create link.
                    if (neighbour != null) cell.Link(neighbour, true);
                }
            }
        }
    }
}
