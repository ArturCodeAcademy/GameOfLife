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
            Task[] tasks = new Task[Map.Width];
            for (int x = 0; x < Map.Width; x++)
            {
                int xIndex = x;
                tasks[xIndex] = new Task(() =>
                {
                    for (int y = 0; y < Map.Height; y++)
                    {
                        Cell newCell = newMap[new Vector2Int(xIndex, y)];
                        Cell oldCell = Map[new Vector2Int(xIndex, y)];
                        newCell.InitNeighbours();

                        if (newCell.IsAlive)
                            newCell.IsAlive = r_stayAlivePredictor(oldCell);
                        else
                            newCell.IsAlive = r_changeToAlivePredictor(oldCell);
                    }
                });
				tasks[xIndex].Start();

			}
            Task.WaitAll(tasks);
            Map = newMap;
            return newMap;
        }
    }
}
