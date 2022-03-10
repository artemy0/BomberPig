using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTile : MonoBehaviour
{
    private GameTileContent _content;
    private Entity _entity;

    private GameTile _right, _left, _down, _up; 


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
        List<GameTile> neighbors = new List<GameTile>() { _up, _down, _left, _right };

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
                return _up;
            case Direction.Down:
                return _down;
            case Direction.Left:
                return _left;
            case Direction.Right:
                return _right;
        }

        return null;
    }


    public static void MakeLeftRightNeighbors(GameTile right, GameTile left)
    {
        left._right = right;
        right._left = left;
    }

    public static void MakeUpDownNeighbors(GameTile up, GameTile down)
    {
        up._down = down;
        down._up = up;
    }
}
