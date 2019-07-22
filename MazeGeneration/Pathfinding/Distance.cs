using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGeneration.Pathfinding {
    class Distance {

        ///Constructor
        public Distance(Cell _root) {
            root = _root;
            cells = new Dictionary<Cell, int>();
            cells[root] = 0;
        }

        ///Public Variables
        public Cell root;
        public Dictionary<Cell, int> cells;
        public int longestDistance = 0;

        ///Public Methods
        //GetCells method returns the list of cells that are listed within cells.
        public List<Cell> GetCells() {
            return new List<Cell>(cells.Keys);
        }

        //SetDistance method sets the distance of the cell from the root cell.
        public void SetDistance(Cell _cell, int _distance) {
            cells[_cell] = _distance;
        }

        //PathTo method returns a distance object with the shortest path from the provided cell to the root.
        public Distance PathTo(Cell _cell) {
            //Set the starting cell.
            Cell currentCell = _cell;

            //Create path distance object and set provided cell as first part of path.
            Distance path = new Distance(root);
            path[currentCell] = cells[currentCell];

            //Loop until we have found the root cell.
            while(currentCell != root) {

                //Get the neighbours of this cell.
                List<Cell> paths = currentCell.GetLinks();
                foreach(Cell p in paths) {

                    //Find neighbour that is closest to the goal, add to path and set as new current cell.
                    if (cells[p] < cells[currentCell]) {
                        path[p] = cells[p];
                        currentCell = p;
                        break;
                    }
                }
            }

            //Return the path.
            return path;
        }

        //MaxDistance method returns the distance to the furthest cell and outs the cell found in this way.
        public int MaxDistance(out Cell resultCell) {
            Cell maxCell = new Cell(0, 0);
            int maxDistance = 0;

            foreach (Cell cell in GetCells()) {
                if (cells[cell] > maxDistance) {
                    maxCell = cell;
                    maxDistance = cells[cell];
                }
            }

            resultCell = maxCell;
            return maxDistance;
        }

        ///Accessors
        public int this[Cell index] {
            get {
                return cells[index];
            }
            set {
                cells[index] = value;
            }
        }
    }
}
