using System.Collections.Generic;
using UnityEngine;

public class ShapesPanel : MonoBehaviour
{
    [SerializeField] private ShapePrefab _viewPrefab;

    private void Start()
    {
        List<Shape> shapes = ShapeSaver.Load();
        foreach (Shape shape in shapes)
        {
            ShapePrefab prefab = Instantiate(_viewPrefab, _viewPrefab.transform.parent);
            prefab.Init(shape);
            prefab.gameObject.SetActive(true);
        }
    }
}
