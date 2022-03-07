using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameBoardRenderer))]
public class GameBoard : MonoBehaviour
{
    [SerializeField] private GameTile _tilePrefab;

    private BoardData _boardData;
    private GameTileContentFactory _contentFactory;

    private GameTile[,] _tiles;
    private Transform _tilesParent;

    private int _boardXSize;
    private int _boardYSize;

    private GameBoardRenderer _gameBoardRenderer;

    private List<GameTileContent> _contentToUpdate = new List<GameTileContent>();


    private void Awake()
    {
        _gameBoardRenderer = GetComponent<GameBoardRenderer>();    
    }


    public void Initialize(BoardData boardData, GameTileContentFactory contentFactory)
    {
        _boardData = boardData;
        _gameBoardRenderer.Initialize(boardData.BoardSprite);


        _boardXSize = _boardData.BoardSize.x;
        _boardYSize = _boardData.BoardSize.y;

        float cellXSize = _boardData.CellSize.x;
        float cellYSize = _boardData.CellSize.y;

        float initXOffset = _boardData.BoardOffset.x;
        float initYOffset = _boardData.BoardOffset.y;

        Vector2 initOffset = new Vector2(initXOffset + (_boardXSize - 1) * (cellXSize / 2), initYOffset + (_boardYSize - 1) * (cellYSize / 2));


        _contentFactory = contentFactory;

        _tiles = new GameTile[_boardXSize, _boardYSize];

        _tilesParent = new GameObject("GameTiles").transform;
        _tilesParent.SetParent(transform, false);

        for (int y = 0; y < _boardYSize; y++)
        {
            for (int x = 0; x < _boardXSize; x++)
            {
                GameTile tile = _tiles[x, y] = Instantiate(_tilePrefab);
                tile.transform.SetParent(_tilesParent, false);

                float lineOffset = y * (cellXSize - 1); //с каждой последующей строчкой происходит смещение
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

        Clear();
    }


    public void GameUpdate()
    {
        for (int i = 0; i < _contentToUpdate.Count; i++)
        {
            _contentToUpdate[i].GameUpdate();
        }
    }


    public GameTile GetRandomTile()
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

        int randomAvailableTileIndex = Random.Range(0, availableTiles.Count);
        return availableTiles[randomAvailableTileIndex];
    }

    public void Clear()
    {
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


    public void ForceDestroy(GameTile tile)
    {
        if (tile.Content.Type == GameTileContentType.Empty)
        {
            return;
        }

        tile.Content.Recycle();
        _contentToUpdate.Remove(tile.Content);

        GameTileContent emptyContent = _contentFactory.Get(GameTileContentType.Empty);
        ForceBuild(tile, emptyContent);
    }

    public GameTileContent TryBuild(GameTile tile, GameTileContentType contentType)
    {
        if (tile.Content.Type != GameTileContentType.Empty)
        {
            return null;
        }

        GameTileContent content = _contentFactory.Get(contentType);
        
        tile.Content = content;
        _contentToUpdate.Add(content);

        return content;
    }

    private void ForceBuild(GameTile tile, GameTileContent content)
    {
        tile.Content = content;
        _contentToUpdate.Add(content);
    }
}
