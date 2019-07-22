using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGeneration {
    class Grid : IGrid {

        ///Contructor
        public Grid(int _rows, int _cols) {
            mRows = _rows;
            mCols = _cols;
            mGrid = PrepareGrid();
            ConfigureCells();
        }

        ///Member Variables
        public int mRows, mCols;
        public List<List<Cell>> mGrid;

        ///Properties
        public int rows {
            get { return mRows; }
            set {  mRows = value; }
        }

        public int cols {
            get { return mCols; }
            set { mCols = value; }
        }

        public List<List<Cell>> grid {
            get { return mGrid; }
            set { mGrid = value; }
        }

        ///Public Methods
        public Cell RandomCell() {
            Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            return grid[rnd.Next(rows)][rnd.Next(cols)];
        }

        public int GridSize() {
            return rows * cols;
        }

        ///Member Methods
        //PrepareGrid method will initialise the grid with cells at their positions.
        List<List<Cell>> PrepareGrid() {
            List<List<Cell>> _grid = new List<List<Cell>>();

            for (int row = 0; row < rows; row++) {
                _grid.Add(new List<Cell>());
                for (int col = 0; col < cols; col++) {
                    _grid[row].Add(new Cell(row, col));
                }
            }

            return _grid;
        }

        //ConfigureCells method will initialise neighbours for each cell.
        void ConfigureCells() {
            for (int row = 0; row < rows; row++)
                for (int col = 0; col < cols; col++) {

                    //North
                    if (row != 0) grid[row][col].neighbours[(int)DIRECTION.NORTH] = grid[row - 1][col];
                    else grid[row][col].neighbours[(int)DIRECTION.NORTH] = null;

                    //East
                    if (col != cols - 1) grid[row][col].neighbours[(int)DIRECTION.EAST] = grid[row][col + 1];
                    else grid[row][col].neighbours[(int)DIRECTION.EAST] = null;

                    //South
                    if (row != rows - 1) grid[row][col].neighbours[(int)DIRECTION.SOUTH] = grid[row + 1][col];
                    else grid[row][col].neighbours[(int)DIRECTION.SOUTH] = null;

                    //West
                    if (col != 0) grid[row][col].neighbours[(int)DIRECTION.WEST] = grid[row][col - 1];
                    else grid[row][col].neighbours[(int)DIRECTION.WEST] = null;
                }
        }

        //CellContents returns nothing due to having no distance object.
        public string CellContents(Cell cell) {
            return "   ";
        }

        //CellDistance returns nothing due to having no distance object.
        public int CellDistance(Cell cell) {
            return 0;
        }

        //LongestDistance returns nothing due to having no distance object.
        public int LongestDistance() {
            return 0;
        }

        ///Accessors
        //[x,y] grid accessor.
        public Cell this[int _row, int _col] {
            get {
                return grid[_row][_col];
            }
            set {
                grid[_row][_col] = value;
            }
        }


    }
}
