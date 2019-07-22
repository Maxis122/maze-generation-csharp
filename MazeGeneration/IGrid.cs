using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGeneration {
    interface IGrid {
        //Interface Methods
        string CellContents(Cell cell);
        int CellDistance(Cell cell);
        int GridSize();
        Cell RandomCell();
        int LongestDistance();

        //Interface Properties
        int rows { get; set; }
        int cols { get; set; }
        List<List<Cell>> grid { get; set; }
    }
}
