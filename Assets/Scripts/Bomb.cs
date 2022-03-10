using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : GameTileContent
{
    [SerializeField] private int _minTicksToExplode = 4;
    [SerializeField] private int _maxTicksToExplode = 6;
    [Space(10)]
    [SerializeField] private ExplosionEffect _explosionEffect;

    private GameBoard _gameBoard;
    private GameTile _gameTile;

    private int _ticksToExplode;


    public void Initialize(GameBoard gameBoard, GameTile gameTile)
    {
        _gameBoard = gameBoard;
        _gameTile = gameTile;

        _ticksToExplode = Random.Range(_minTicksToExplode, _maxTicksToExplode + 1);
    }


    public override void GameUpdate()
    {
        _ticksToExplode--;

        if(_ticksToExplode <= 0)
        {
            Explode();
        }
    }


    private void Explode()
    {
        ExplosionEffect explosionEffect = Instantiate(_explosionEffect);
        explosionEffect.transform.position = transform.position;

        _gameBoard.ForceDestroy(_gameTile);
        if(_gameTile.Entity != null)
        {
            _gameTile.Entity.Kill();
        }


        GameTile upTile = _gameTile.GetNeighbor(Direction.Up);
        GameTile downTile = _gameTile.GetNeighbor(Direction.Down);
        GameTile leftTile = _gameTile.GetNeighbor(Direction.Left);
        GameTile rightTile = _gameTile.GetNeighbor(Direction.Right);

        if(upTile != null && upTile.Entity != null)
        {
            upTile.Entity.Kill();
        }
        if (downTile != null && downTile.Entity != null)
        {
            downTile.Entity.Kill();
        }
        if (leftTile != null && leftTile.Entity != null)
        {
            leftTile.Entity.Kill();
        }
        if (rightTile != null && rightTile.Entity != null)
        {
            rightTile.Entity.Kill();
        }
    }
}
