using GameLogic;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class Map
{

    private Cell[,] _grid;

	public int Width { get; private set; }
    public int Height { get; private set; }

    public Cell? this[Vector2Int index]
	{
		get
		{
			var (x, y) = (index.x, index.y);
			if (x < 0 || x >= Width)
				return null;
			if (y < 0 || y >= Height)
				return null;

			return _grid[x, y];
		}
		private set => _grid[index.x, index.y] = value;
	}

	public Map(int width, int height)
	{
        Width = width;
        Height = height;
        _grid = new Cell[Width, Height];

		for (int x = 0; x < Width; x++)
            for (int y = 0; y < Height; y++)
				_grid[x, y] = new Cell(this, new Vector2Int(x, y));

        for (int x = 0; x < Width; x++)
            for (int y = 0; y < Height; y++)
				_grid[x, y].InitNeighbours();
    }

	public Map Clone()
	{
		Map newMap = new Map(Width, Height);
		for (int x = 0; x < Width; x++)
			for (int y = 0; y < Height; y++)
				newMap[new Vector2Int(x, y)] = _grid[x, y].Clone(newMap);

		return newMap;
    }

    public override string ToString()
    {
		StringBuilder builder = new StringBuilder();
		for (int x = 0; x < Width; x++)
		{
			for (int y = 0; y < Height; y++)
				builder.Append(_grid[x, y].IsAlive ? "A" : "D");
			builder.AppendLine();
		}
        return builder.ToString();
    }
}
