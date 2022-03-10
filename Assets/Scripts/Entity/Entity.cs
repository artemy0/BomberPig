using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Entity : MonoBehaviour
{
    public EntityFactory OriginFactory { get; set; }
    public GameTile CurrentTile { get { return _currentTile; } }

    [SerializeField] protected EntityView _view;
    [Space(10)]
    [SerializeField] private float _movementDuration = .25f;

    protected GameTile _currentTile;


    public virtual void Initialize()
    {
        _view.Initialize();
    }


    public virtual void Move(Direction direction)
    {
        GameTile targetTile = _currentTile.GetNeighbor(direction);

        if (IsAvailableToMove(targetTile))
        {
            _view.SetDirection(direction);

            transform.DOLocalMove(targetTile.transform.localPosition, _movementDuration);

            _currentTile.Entity = null;
            _currentTile = targetTile;
            _currentTile.Entity = this;
        }
    }
    protected bool IsAvailableToMove(GameTile targetTile)
    {
        return targetTile != null && targetTile.Content.Type == GameTileContentType.Empty && targetTile.Entity == null;
    }

    public void Teleport(GameTile tile)
    {
        transform.localPosition = tile.transform.localPosition;
        
        _currentTile = tile;
        _currentTile.Entity = this;
    }


    public virtual void Kill()
    {

    }
}