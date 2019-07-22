namespace MazeGeneration.Pathfinding {
    class DistanceGrid : Grid, IGrid {

        ///Constructor
        public DistanceGrid(int _rows, int _cols) : base(_rows, _cols) { }

        ///Public Variables
        public Distance distances;

        ///Public Methods
        //CellContents returns the distance this cell is from the root cell as string.
        public new string CellContents(Cell cell) {
            if (distances.cells.ContainsKey(cell)) {
                string output = distances.cells[cell].ToString();
                output = output.PadRight(3);
                return output;
            }

            return "   ";
        }

        //CellDistance returns the distance this cell is from the root cell as int.
        public new int CellDistance(Cell cell) {

            if (distances.cells.ContainsKey(cell)) {
                return distances.cells[cell];
            }

            return 0;
        }

        //FindDistances method will use the root cell to create a distances object.
        public void FindDistances(Cell _rootCell) {
            distances = _rootCell.GetDistance();
        }

        //FindLongestPath method will set the distance root to the furthest cell away from the return cell.
        public Cell FindLongestPath() {

            //Find the furthest cell away from the original root.
            Cell startingCell;
            distances.MaxDistance(out startingCell);

            //Use furthest cell to find new distances.
            distances = startingCell.GetDistance();

            //Find the furthest cell away from the new root.
            distances.longestDistance = distances.MaxDistance(out startingCell);
            return startingCell;

        }

        //LongestDistance returns the longest distance, but only creates accurate result after running MaxDistance.
        public new int LongestDistance() {
            return distances.longestDistance;
        }

    }
}
