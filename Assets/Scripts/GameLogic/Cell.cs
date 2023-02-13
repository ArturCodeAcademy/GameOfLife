using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameLogic
{
    public class Cell
    {
        public bool IsAlive { get; set; }
        public Vector2Int Coord { get; private set; }

        protected readonly Map r_map;
        protected Cell[] p_neighbours;

        protected Cell? p_up => r_map[Coord + Vector2Int.up];
        protected Cell? p_upRight => r_map[Coord + new Vector2Int(1, 1)];
        protected Cell? p_upLeft => r_map[Coord + new Vector2Int(-1, 1)];
        protected Cell? p_down => r_map[Coord + Vector2Int.down];
        protected Cell? p_downRight => r_map[Coord + new Vector2Int(1, -1)];
        protected Cell? p_downLeft => r_map[Coord + new Vector2Int(-1, -1)];
        protected Cell? p_right => r_map[Coord + Vector2Int.right];
        protected Cell? p_left => r_map[Coord + Vector2Int.left];

        public Cell(Map map, Vector2Int coord)
        {
            r_map = map;
            IsAlive = false;
            Coord = coord;
        }

        public void InitNeighbours()
        {
            p_neighbours = new List<Cell>()
            {
                p_up,
                p_upRight,
                p_upLeft,
                p_down,
                p_downRight,
                p_downLeft,
                p_left,
                p_right
            }.Where(x => x != null).ToArray();
        }

        public int GetAliveNeighboursCount()
        {
            return p_neighbours.Count(x => x.IsAlive);
        }

        public Cell Clone(Map map)
        {
            return new Cell(map, Coord)
            {
                IsAlive = IsAlive
            };
        }

        public override string ToString()
        {
            return $"{(IsAlive? "A" : "D")}({Coord.x} {Coord.y})";
        }
    }
}
