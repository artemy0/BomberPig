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
    }
}
