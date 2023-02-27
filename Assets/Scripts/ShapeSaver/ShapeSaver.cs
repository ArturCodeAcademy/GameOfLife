using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class ShapeSaver
{
    private const string JSON_FILE_NAME = "Shapes.json";

    public static List<Shape> Load()
    {
        string path = Path.Combine(Application.dataPath, "Resources", JSON_FILE_NAME);   
        string json = File.ReadAllText(path);
        List<Shape> result = JsonConvert.DeserializeObject<List<Shape>>(json);
        return result;
    }

    public static void Save()
    {
        Shape[] shapes = new Shape[]
        {
            new Shape()
            {
                ShapeName = "Glider",
                ShapePoints = new List<Point2D>
                { 
                    new Point2D(0, 0),
                    new Point2D(1, 0),
                    new Point2D(2, 0),
                    new Point2D(0, -1),
                    new Point2D(1, -2),
                }
            }
        };
        string json = JsonConvert.SerializeObject(shapes);
        string path = Path.Combine(Application.dataPath, "Resources", JSON_FILE_NAME);
        File.WriteAllText(path, json);
    }
}
