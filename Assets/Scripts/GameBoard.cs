using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameBoardRenderer))]
public class GameBoard : MonoBehaviour
{
    [SerializeField] private GameTile _tilePrefab;

    private BoardData _boardData;

    private GameTile[,] _tiles;
    private GameTileContentFactory _contentFactory;

    private GameBoardRenderer _gameBoardRenderer;


    private void Awake()
    {
        _gameBoardRenderer = GetComponent<GameBoardRenderer>();    
    }


    public void Initialize(BoardData boardData, GameTileContentFactory contentFactory)
    {
        _boardData = boardData;
        _gameBoardRenderer.Initialize(boardData.BoardSprite);


        int boardXSize = _boardData.BoardSize.x;
        int boardYSize = _boardData.BoardSize.y;

        float cellXSize = _boardData.CellSize.x;
        float cellYSize = _boardData.CellSize.y;

        float initXOffset = _boardData.BoardOffset.x;
        float initYOffset = _boardData.BoardOffset.y;

        Vector2 initOffset = new Vector2(initXOffset + (boardXSize - 1) * (cellXSize / 2), initYOffset + (boardYSize - 1) * (cellYSize / 2));


        _tiles = new GameTile[boardXSize, boardYSize];
        _contentFactory = contentFactory;

        for (int y = 0; y < boardYSize; y++)
        {
            for (int x = 0; x < boardXSize; x++)
            {
                GameTile tile = _tiles[x, y] = Instantiate(_tilePrefab);
                tile.transform.SetParent(transform, false);

                float lineOffset = y * (cellXSize - 1); //с каждой последующей строчкой происходит смещение
                tile.transform.localPosition = new Vector2(x * cellXSize + lineOffset - initOffset.x, y * cellYSize - initOffset.y);
            }
        }

        Clear();
    }

    private void Clear()
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


    private void ForceBuild(GameTile tile, GameTileContent content)
    {
        tile.Content = content;
        //_contentToUpdate.Add(content);
    }
}
