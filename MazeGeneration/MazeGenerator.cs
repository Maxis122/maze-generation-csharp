using MazeGeneration.Generators;
using MazeGeneration.Pathfinding;
using MazeGeneration.Rendering;
using SixLabors.ImageSharp.Formats;

namespace MazeGeneration {
    class MazeGenerator {

        ///Public Static Methods
        //GenerateConsole method applys generation to grid and outputs result to console.
        public static void GenerateConsole(IGenerator _generator, int _width, int _height, out Grid _grid) {

            //Create a grid.
            Grid grid = new Grid(_width, _height);

            //Apply maze algorithm.
            _generator.Generate(grid);

            //Output result to console.
            MazeDisplay.ASCIIDisplay(grid);

            //Output the grid.
            _grid = grid;
        }

        //GenerateConsolePath method applys generation to grid, finds longest path between and outputs result to console.
        public static void GenerateConsolePath(IGenerator _generator, int _width, int _height, out DistanceGrid _grid) {

            //Create a grid.
            DistanceGrid grid = new DistanceGrid(_width, _height);

            //Apply maze algorithm.
            _generator.Generate(grid);

            //Find the distances.
            grid.FindDistances(grid[0, 0]);

            //Find the longest path.
            Cell startingCell = grid.FindLongestPath();
            grid.distances = grid.distances.PathTo(startingCell);

            //Output result to console.
            MazeDisplay.ASCIIDisplay(grid);

            //Output the grid.
            _grid = grid;
        }

        //GeneratePNG method applys generation to grid and outputs result to PNG file.
        public static void GenerateImage(IGenerator _generator, int _width, int _height, string _path, IImageEncoder _imageEncoder, out Grid _grid) {

            //Create a grid.
            Grid grid = new Grid(_width, _height);

            //Apply maze algorithm.
            _generator.Generate(grid);

            //Write result to file.
            MazeDisplay.ToImage(40, grid, RENDER_MODE.OUTLINE, _path, _imageEncoder);

            //Output the grid.
            _grid = grid;
        }

        //GenerateColourPNG method applys generation to grid, finds longest path and outputs result to coloured png.
        public static void GenerateColourImage(IGenerator _generator, int _width, int _height, string _path, IImageEncoder _imageEncoder, out DistanceGrid _grid) {

            //Create a grid.
            DistanceGrid grid = new DistanceGrid(_width, _height);

            //Apply maze algorithm.
            _generator.Generate(grid);

            //Find the distances.
            grid.FindDistances(grid[0, 0]);

            //Find the longest path.
            Cell startingCell = grid.FindLongestPath();
            grid.distances = grid.distances.PathTo(startingCell);
            grid.FindLongestPath();

            //Write result to file.
            MazeDisplay.ToImage(40, grid, RENDER_MODE.COLOUR, _path, _imageEncoder);

            //Output the grid.
            _grid = grid;
        }
    }


}
