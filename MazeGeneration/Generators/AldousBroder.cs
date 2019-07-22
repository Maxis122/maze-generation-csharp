using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGeneration.Generators {
    class AldousBroder : IGenerator {

        ///Public Methods
        //Generate method will apply the Aldous Broder algorithm to the grid object.
        public void Generate(Grid _grid) {

            //Get a random starting cell.
            Cell cell = _grid.RandomCell();
            int unvisited = _grid.GridSize() - 1;
            Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);


            //Run loop until all cells are visited.
            while (unvisited > 0) {
                //Choose random valid neighbour
                List<Cell> neighbourList = cell.GetNeighbours();
                Cell randomNeighbour = neighbourList[rnd.Next(neighbourList.Count)];

                //Check if neighbour has no links.
                if (randomNeighbour.links.Count == 0) {
                    cell.Link(randomNeighbour, true);
                    unvisited--;
                }

                //Move to the neighbour.
                cell = randomNeighbour;
            }
        }
    }
}
