using System;
using System.Collections.Generic;
using System.Linq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;
using System.IO;
using SixLabors.Primitives;
using MazeGeneration.Pathfinding;

namespace MazeGeneration.Rendering {

    enum RENDER_MODE { COLOUR, OUTLINE, END }

    static class MazeDisplay {

        ///Public Static Methods
        //ASCIIDisplay method will convert grid data into a Console ASCII visualisation.
        public static void ASCIIDisplay(IGrid _grid) {
            //Create the header.
            string output = "+" + new string(Enumerable.Range(0, _grid.cols).SelectMany(x => "---+").ToArray()) + "\n";

            //Loop through grid and add values.
            foreach (List<Cell> row in _grid.grid) {

                //Begin the display row.
                string top = "|", bottom = "+";

                //Add each cell to the display row.
                foreach (Cell cell in row) {
                    //Top of cell.
                    string body = _grid.CellContents(cell);
                    if (cell.IsLinked(cell.neighbours[(int)DIRECTION.EAST])) top += body + " "; else top += body + "|";

                    //Bottom of cell.
                    if (cell.IsLinked(cell.neighbours[(int)DIRECTION.SOUTH])) bottom += "   +"; else bottom += "---+";
                }

                //Add row to the output.
                output += top + "\n" + bottom + "\n";
            }

            //Output the result.
            Console.WriteLine(output);

        }

        //ToPNG method will save the grid data in a png image format.
        public static void ToImage(int _cellSize, IGrid _grid, RENDER_MODE _render, string _path, IImageEncoder imageEncoder) {

            //Setup image size.
            int imageWidth = _cellSize * _grid.cols;
            int imageHeight = _cellSize * _grid.rows;

            //Create the image object.
            Image<Rgb24> image = new Image<Rgb24>(new Configuration(), imageWidth + 1, imageHeight + 1, new Rgb24(255, 255, 255));
            Rgb24 drawColour = new Rgb24(0, 0, 0);

            //Create the image.
            for(RENDER_MODE rendering = _render; rendering < RENDER_MODE.END; rendering++) {
                foreach (List<Cell> row in _grid.grid) {
                    foreach (Cell cell in row) {
                        float x1 = cell.col * _cellSize;
                        float y1 = cell.row * _cellSize;
                        float x2 = (cell.col + 1) * _cellSize;
                        float y2 = (cell.row + 1) * _cellSize;

                        //Switch mode.
                        switch (rendering) {
                            case RENDER_MODE.OUTLINE:
                                //North Wall.
                                if (cell.neighbours[(int)DIRECTION.NORTH] == null)
                                    image.Mutate(x => x.DrawLines(drawColour, 1.0f, new PointF[] { new PointF(x1, y1), new PointF(x2, y1) }));

                                //West Wall.
                                if (cell.neighbours[(int)DIRECTION.WEST] == null)
                                    image.Mutate(x => x.DrawLines(drawColour, 1.0f, new PointF[] { new PointF(x1, y1), new PointF(x1, y2) }));

                                //East Wall.
                                if (!cell.IsLinked(cell.neighbours[(int)DIRECTION.EAST]))
                                    image.Mutate(x => x.DrawLines(drawColour, 1.0f, new PointF[] { new PointF(x2, y1), new PointF(x2, y2) }));

                                //South Wall.
                                if (!cell.IsLinked(cell.neighbours[(int)DIRECTION.SOUTH]))
                                    image.Mutate(x => x.DrawLines(drawColour, 1.0f, new PointF[] { new PointF(x1, y2), new PointF(x2, y2) }));

                                break;

                            case RENDER_MODE.COLOUR:

                                //Find the intensity of the colour.
                                float cellDistance = _grid.CellDistance(cell);
                                float cellLongestDistance = _grid.LongestDistance();

                                float percent = cellDistance / cellLongestDistance;
                                byte darkFill = (byte)(255.0f * percent);
                                byte brightFill = (byte)(128.0f + (127.0f * percent));
                                Rgb24 fillColour = new Rgb24(darkFill, brightFill, darkFill);

                                //Draw the rectangle
                                image.Mutate(x => x.Fill(fillColour, new RectangleF(x1, y1, _cellSize, _cellSize)));
                                break;
                        }
                    }
                }
            }
            
            //Create file and save image to it.
            FileStream fileStream = new FileStream(_path, FileMode.Create, FileAccess.Write);
            image.Save(fileStream, imageEncoder);

            //Close the file.
            fileStream.Close();

            //Log result.
            Console.WriteLine("File created: " + _path);
        }
        
    }
}
