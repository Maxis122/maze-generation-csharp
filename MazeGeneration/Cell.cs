using System.Collections.Generic;
using MazeGeneration.Pathfinding;

public enum DIRECTION { NORTH, EAST, SOUTH, WEST };

namespace MazeGeneration {
    class Cell {

        ///Constructor
        public Cell(int _row, int _col) {
            row = _row;
            col = _col;
            links = new Dictionary<Cell, bool>();
            neighbours = new List<Cell>(new Cell[] { null, null, null, null });
        }

        ///Public Variables
        public int row, col;
        public Dictionary<Cell, bool> links;
        public List<Cell> neighbours;

        ///Public Methods
        //Link method will link cell with provided cell, with bidirectional option.
        public void Link(Cell _cellLink, bool bidi) {
            if (_cellLink == null) return;
            links[_cellLink] = true;
            if (bidi) _cellLink.Link(this, false);
        }

        //Unlink method will unlink cell with provided cell, with bidirectional option.
        public void Unlink(Cell _cellLink, bool bidi) {
            if (_cellLink == null) return;
            links.Remove(_cellLink);
            if (bidi) _cellLink.Unlink(this, false);
        }

        //GetLinks method will return list of cells linked to current cell.
        public List<Cell> GetLinks() {
            return new List<Cell>(links.Keys);
        }

        //IsLinked method returns bool of whether provided cell is already linked to current cell.
        public bool IsLinked(Cell _cellLink) {
            if (_cellLink == null) return false;
            return links.ContainsKey(_cellLink);
        }

        //GetNeighbours method returns list of all valid neighbours of current cell.
        public List<Cell> GetNeighbours() {
            List<Cell> validNeighbours = new List<Cell>();
            foreach (Cell cell in neighbours) if (cell != null) validNeighbours.Add(cell);
            return validNeighbours;
        }

        //GetDistance returns a distance object will the distance of all cells from its location.
        public Distance GetDistance() {

            //Create the distances object and frontier list.
            Distance distances = new Distance(this);
            List<Cell> frontier = new List<Cell>();
            frontier.Add(this);

            //Pathfinding loop.
            while(frontier.Count != 0) {

                //Create a new frontier.
                List<Cell> newFrontier = new List<Cell>();

                //Add each unvisited cell in frontier to the new frontier.
                foreach (Cell front in frontier) {
                    foreach(Cell link in front.GetLinks()) {
                        if (!distances.cells.ContainsKey(link)) {
                            distances[link] = distances[front] + 1;
                            newFrontier.Add(link);
                        }
                    }
                }

                //Replace frontier with new frontier.
                frontier = newFrontier;
            }

            //Return distances object.
            return distances;
        }
    }
}
