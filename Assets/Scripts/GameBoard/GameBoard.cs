using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameBoardRenderer))]
public class GameBoard : MonoBehaviour
{
    [SerializeField] private GameTile _tilePrefab;

    private BoardData _boardData;
    private GameTileContentFactory _contentFactory;
    
    private GameBoardRenderer _gameBoardRenderer;

    private GameTile[,] _tiles;
    private Transform _tilesParent;
    
    private List<GameTileContent> _contentToUpdate = new List<GameTileContent>();

    private int _boardXSize;
    private int _boardYSize;


    private void Awake()
    {
        _gameBoardRenderer = GetComponent<GameBoardRenderer>();    
    }


    public void Initialize(BoardData boardData, GameTileContentFactory contentFactory)
    {
        _boardData = boardData;
        _gameBoardRenderer.Initialize(boardData.BoardSprite);

        _contentFactory = contentFactory;


        InitBoard();

        Clear();
    }

    private void InitBoard()
    {
        _boardXSize = _boardData.BoardSize.x;
        _boardYSize = _boardData.BoardSize.y;

        float cellXSize = _boardData.CellSize.x;
        float cellYSize = _boardData.CellSize.y;

        float initXOffset = _boardData.BoardOffset.x;
        float initYOffset = _boardData.BoardOffset.y;

        Vector2 initOffset = new Vector2(initXOffset + (_boardXSize - 1) * (cellXSize / 2), initYOffset + (_boardYSize - 1) * (cellYSize / 2));


        _tiles = new GameTile[_boardXSize, _boardYSize];

        _tilesParent = new GameObject("GameTiles").transform;
        _tilesParent.SetParent(transform, false);

        for (int y = 0; y < _boardYSize; y++)
        {
            for (int x = 0; x < _boardXSize; x++)
            {
                GameTile tile = _tiles[x, y] = Instantiate(_tilePrefab);
                tile.transform.SetParent(_tilesParent, false);

                //with each subsequent line there is an offset
                float lineOffset = y * (cellXSize - 1);
                tile.transform.localPosition = new Vector2(x * cellXSize + lineOffset - initOffset.x, y * cellYSize - initOffset.y);

                if (x > 0)
                {
                    GameTile.MakeLeftRightNeighbors(tile, _tiles[x - 1, y]);
                }

                if (y > 0)
                {
                    GameTile.MakeUpDownNeighbors(tile, _tiles[x, y - 1]);
                }
            }
        }
    }


    public void GameUpdate()
    {
        for (int i = 0; i < _contentToUpdate.Count; i++)
        {
            _contentToUpdate[i].GameUpdate();
        }
    }


    public GameTile GetRandomTile(List<GameTile> exclusionCells = null)
    {
        List<GameTile> availableTiles = new List<GameTile>(_boardXSize * _boardYSize);

        for (int x = 0; x < _boardXSize; x++)
        {
            for (int y = 0; y < _boardYSize; y++)
            {
                if(_tiles[x, y].Content.Type == GameTileContentType.Empty)
                {
                    availableTiles.Add(_tiles[x, y]);
                }
            }
        }

        if (exclusionCells != null)
        {
            for (int i = 0; i < exclusionCells.Count; i++)
            {
                availableTiles.Remove(exclusionCells[i]);
            }
        }

        int randomAvailableTileIndex = Random.Range(0, availableTiles.Count);
        return availableTiles[randomAvailableTileIndex];
    }


    public void TryBuild(GameTile tile, GameTileContentType contentType)
    {
        if (tile.Content.Type != GameTileContentType.Empty)
        {
            return;
        }

        GameTileContent content = _contentFactory.Get(contentType);

        //violation of the Barbara Liskov substitution principle!
        if (content is Bomb)
        {
            Bomb bomb = (Bomb)content;
            bomb.Initialize(tile);
            bomb.OnExploded += ForceDestroy;
        }
        
        tile.Content = content;
        _contentToUpdate.Add(content);
    }

    private void ForceBuild(GameTile tile, GameTileContent content)
    {
        tile.Content = content;
        tile.Entity = null;

        _contentToUpdate.Add(content);
    }

    public void ForceDestroy(GameTile tile)
    {
        if (tile.Content.Type == GameTileContentType.Empty)
        {
            return;
        }

        //violation of the Barbara Liskov substitution principle!
        if (tile.Content is Bomb)
        {
            Bomb bomb = (Bomb)tile.Content;
            bomb.OnExploded -= ForceDestroy;
        }

        tile.Content.Recycle();
        _contentToUpdate.Remove(tile.Content);

        GameTileContent emptyContent = _contentFactory.Get(GameTileContentType.Empty);
        ForceBuild(tile, emptyContent);
    }


    public void Clear()
    {
        _contentToUpdate.Clear();

        for (int x = 0; x < _tiles.GetLength(0); x++)
        {
            for (int y = 0; y < _tiles.GetLength(1); y++)
            {
                GameTileContentType contentType = GameTileContentType.Empty;
                if(x % 2 != 0 && y % 2 != 0)
                {
                    contentType = GameTileContentType.Stone;
                }

                GameTileContent content = _contentFactory.Get(contentType);
                ForceBuild(_tiles[x, y], content);
            }
        }
    }
}
