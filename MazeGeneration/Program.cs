using MazeGeneration.Generators;
using MazeGeneration.Pathfinding;
using SixLabors.ImageSharp.Formats.Png;

namespace MazeGeneration {
    class Program {
        static void Main(string[] args) {

            //Generate a maze and output the result into the grid object.
            DistanceGrid gridD;
            MazeGenerator.GenerateColourImage(new Sidewinder(), 40, 40, "image-color.png", new PngEncoder(), out gridD);

        }
    }
}
