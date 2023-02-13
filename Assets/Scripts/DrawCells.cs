using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrawCells : MonoBehaviour
{
    [SerializeField] private Grid _grid;

    private Vector3Int? _lastCellPosition;

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject() && _lastCellPosition == null)
            return;

        if (Input.GetMouseButton(0) && _lastCellPosition != null || Input.GetMouseButtonDown(0))
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector3Int currentCellPosition = _grid.GetCellPosition(position);

			if (_lastCellPosition != currentCellPosition)
            {
                _lastCellPosition = currentCellPosition;
                _grid.ChangeStateByPosition(position);
			}
        }

        if (Input.GetMouseButtonUp(0))
			_lastCellPosition = null;
    }
}
