using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShapePrefab : MonoBehaviour
{
    [SerializeField] private TMP_Text _shapeName;
    [SerializeField] private Image _shapeImage;

    public void Init(Shape shape)
    {
        _shapeName.text = shape.ShapeName;

        int maxX = shape.ShapePoints.Max(x => x.X);
        int maxY = shape.ShapePoints.Max(x => Mathf.Abs(x.Y));

        Texture2D texture = new Texture2D(maxX+1, maxY+1, TextureFormat.ARGB32, false);
        foreach (var point in shape.ShapePoints)
        {
            texture.SetPixel(point.X, maxY + point.Y, Color.red);
        }
        Sprite sprite = Sprite.Create
        (
            texture,
            new Rect(0, 0, texture.width, texture.height),
            Vector2.one / 2            
        );

        _shapeImage.sprite = sprite;
    }
}
