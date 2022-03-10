using System.Collections.Generic;
using UnityEngine;

public class GameTile : MonoBehaviour
{
    private GameTileContent _content;
    private Entity _entity;

    private GameTile _rightNeighbor, _leftNeighbor, _downNeighbor, _upNeighbor; 


    public GameTileContent Content
    {
        get
        {
            return _content;
        }
        set
        {
            if(_content != null)
            {
                _content.Recycle();
            }

            _content = value;
            _content.transform.SetParent(transform, false);
            _content.transform.localPosition = Vector2.zero;
        }
    }

    public Entity Entity
    {
        get
        {
            return _entity;
        }
        set
        {
            _entity = value;
        }
    }


    public List<GameTile> GetNeighbors()
    {
        List<GameTile> neighbors = new List<GameTile>() { _upNeighbor, _downNeighbor, _leftNeighbor, _rightNeighbor };

        return neighbors;
    }
    public List<GameTile> GetNeighbors(out List<Direction> directions)
    {
        directions = new List<Direction>() { Direction.Up, Direction.Down, Direction.Left, Direction.Right };

        return GetNeighbors();
    }
    public GameTile GetNeighbor(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return _upNeighbor;
            case Direction.Down:
                return _downNeighbor;
            case Direction.Left:
                return _leftNeighbor;
            case Direction.Right:
                return _rightNeighbor;
        }

        return null;
    }


    public static void MakeLeftRightNeighbors(GameTile rightNeighbor, GameTile leftNeighbor)
    {
        leftNeighbor._rightNeighbor = rightNeighbor;
        rightNeighbor._leftNeighbor = leftNeighbor;
    }

    public static void MakeUpDownNeighbors(GameTile upNeighbor, GameTile downNeighbor)
    {
        upNeighbor._downNeighbor = downNeighbor;
        downNeighbor._upNeighbor = upNeighbor;
    }
}
