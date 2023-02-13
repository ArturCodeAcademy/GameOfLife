using GameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace GameLogic
{
    public class LogicBase
    {
        private readonly Func<Cell, bool> r_stayAlivePredictor;
        private readonly Func<Cell, bool> r_changeToAlivePredictor;

        public Map Map { get; private set; }

        public LogicBase(int width, int height,
            Func<Cell, bool> stayAlivePredictor,
            Func<Cell, bool> changeToAlivePredictor)
        {
            Map = new Map(width, height);
            r_stayAlivePredictor = stayAlivePredictor;
            r_changeToAlivePredictor = changeToAlivePredictor;
        }

        public Map GetNextMap()
        {
            Map newMap = Map.Clone();

            for (int x = 0; x < Map.Width; x++)
                for (int y = 0; y < Map.Height; y++)
                {
                    Cell cell = newMap[new Vector2Int(x, y)];
                    cell.InitNeighbours();

                    if (cell.IsAlive)
                        cell.IsAlive = r_stayAlivePredictor(cell);
                    else
                        cell.IsAlive = r_changeToAlivePredictor(cell);
                }
            Map = newMap;
            return newMap;
        }
    }
}
