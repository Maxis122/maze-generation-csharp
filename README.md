<h1 align="center">Maze Generation - C# Console Application</h1>

This is a C# Console Application which can generate mazes and output them to the console or to image files. 
Currently it lacks an interface to choose what kind of maze and result is achieved - but for now the aim is to build the backend.

<h2>Maze Algorithms</h2>
<p><b>Binary Tree:</b> Loops through the grid row by row, randomly choosing whether to carve upwards or to the right.</p>
<p><b>Sidewinder:</b> Loops through the grid row by row, creating runs of cells. Randomly choose to close run and choose a cell within it to carve upwards.</p>
<p><b>Aldous Broder:</b> Randomly walk through the grid, only connecting cells which haven't been connected to yet.</p>

<h2>Pathfinding Algorithms</h2>
<p><b>Dijkstra:</b> Continuously rebuilds it's frontier using the current frontier each loop, to flood the map in every direction and find the goal - or to define every cell's distance from the root.</p>

<h2>API's Used</h2>
<p>SixLabors ImageSharp Graphics API: https://github.com/SixLabors/ImageSharp</p>
