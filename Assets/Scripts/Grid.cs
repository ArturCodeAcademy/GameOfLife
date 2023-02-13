using GameLogic;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Grid : MonoBehaviour
{
    [SerializeField] private Tile _tileWhite; 
    [SerializeField] private Tile _tileBlack; 
    [SerializeField] private int _width; 
    [SerializeField] private int _height; 

    private LogicBase _logic;
    private Tilemap _tilemap;

    private void Awake()
    {
        _logic = new LogicBase
        (
            _width,
            _height,
            StayAlive,
            ChangeToAlivePredictor
        );
        _tilemap = GetComponent<Tilemap>();
    }

    private bool StayAlive(Cell cell)
    {
        int count = cell.GetAliveNeighboursCount();
        return count == 2 || count == 3;
    }
    private bool ChangeToAlivePredictor(Cell cell)
    {
        int count = cell.GetAliveNeighboursCount();
        return count == 3;
    }

    private void Start()
    {
        Init(_logic.Map);
        Draw(_logic.Map);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int coord3d = _tilemap.WorldToCell(pos);
            Cell cell = _logic.Map[new Vector2Int(coord3d.x, coord3d.y)];
            if (cell == null)
                return;
            cell.IsAlive = !cell.IsAlive;
            _tilemap.SetTile(coord3d, cell.IsAlive
                    ? _tileWhite
                    : _tileBlack);
        }
    }

    private void Init(Map map)
    {
        for (int x = 0; x < map.Width; x++)
            for (int y = 0; y < map.Height; y++)
                _tilemap.SetTile(new Vector3Int(x, y), _tileBlack);
    }

    private void Draw(Map map)
    {
        for (int x = 0; x < map.Width; x++)
            for (int y = 0; y < map.Height; y++)
            {
                Vector2Int pos = new Vector2Int(x, y);
                _tilemap.SetTile(new Vector3Int(x, y), map[pos].IsAlive
                    ? _tileWhite
                    : _tileBlack);
            }
    }

    public void OnNextClick()
    {
        Map map = _logic.GetNextMap();
        Draw(map);
    }
}
