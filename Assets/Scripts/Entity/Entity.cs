using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Entity : MonoBehaviour
{
    [SerializeField] private EntityView _view;
    [Space(10)]
    [SerializeField] private float _movementDuration = 1f;

    private GameTile _currentTile;


    public void Move(GameTile tile)
    {
        transform.localPosition = tile.transform.localPosition;
        _currentTile = tile;
    }

    public void Move(Direction direction)
    {
        GameTile targetTile = _currentTile.GetNeighbor(direction);

        transform.DOLocalMove(targetTile.transform.localPosition, _movementDuration);
        _currentTile = targetTile;
    }
}

public enum Direction
{
    Tap,

    Up,
    Down,
    Left,
    Right
}
