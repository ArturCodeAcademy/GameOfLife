using GameLogic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class Grid : MonoBehaviour
{
	public float LeftBorder { get; private set; }
	public float RightBorder { get; private set; }
	public float UpBorder { get; private set; }
	public float DownBorder { get; private set; }

    [SerializeField] private Tilemap _tilemap;
	[SerializeField] private Tile _aliveTile; 
    [SerializeField] private Tile _diedTile; 
    [SerializeField] private int _defaultWidth; 
    [SerializeField] private int _defaultHeight; 

    private LogicBase _logic;

	private const float CELL_SIZE = 1.1f;

    private void Awake()
    {
		ResetGrid(_defaultWidth, _defaultHeight);
	}

#region Rules

	private bool StayAliveRule(Cell cell)
    {
        int count = cell.GetAliveNeighboursCount();
        return count == 2 || count == 3;
    }

    private bool ChangeToAlivePredictorRule(Cell cell)
    {
        int count = cell.GetAliveNeighboursCount();
        return count == 3;
    }

	#endregion

	public void ResetGrid(int width, int height)
	{
		_tilemap.ClearAllTiles();
		_logic = new LogicBase
		(
			width,
			height,
			StayAliveRule,
			ChangeToAlivePredictorRule
		);
		Init(_logic.Map);
		Draw(_logic.Map);

        UpBorder = (height * CELL_SIZE / 2);
		DownBorder = -(height * CELL_SIZE / 2);
		LeftBorder = -(width * CELL_SIZE / 2);
		RightBorder = (width * CELL_SIZE / 2);

		_tilemap.transform.position = new Vector3(LeftBorder, DownBorder);
	}

	public Vector3Int GetCellPosition(Vector3 position)
	{
		return _tilemap.WorldToCell(position);
	}

	public void ChangeStateByPosition(Vector3 position)
	{
		Vector3Int coord3d = _tilemap.WorldToCell(position);
		Cell cell = _logic.Map[new Vector2Int(coord3d.x, coord3d.y)];
		if (cell == null)
			return;
		cell.IsAlive = !cell.IsAlive;
		_tilemap.SetTile(coord3d, cell.IsAlive ? _aliveTile : _diedTile);
	}

	public void TakeStep()
	{		
		Draw(_logic.GetNextMap());
	}

	private void Init(Map map)
    {
        for (int x = 0; x < map.Width; x++)
            for (int y = 0; y < map.Height; y++)
                _tilemap.SetTile(new Vector3Int(x, y), _diedTile);
    }

    private void Draw(Map map)
    {
        for (int x = 0; x < map.Width; x++)
            for (int y = 0; y < map.Height; y++)
            {
                Vector2Int pos2d = new Vector2Int(x, y);
                Vector3Int pos3d = new Vector3Int(x, y);
                _tilemap.SetTile(pos3d, map[pos2d].IsAlive? _aliveTile : _diedTile);
            }
    }
}
