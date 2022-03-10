using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : Entity
{
    public event Action OnPlayerDied;

    private GameBoard _gameBoard;
    private bool isAlive = false;


    public void Initialize(GameBoard gameBoard)
    {
        _gameBoard = gameBoard;
        isAlive = true;

        base.Initialize();
    }


    public override void Move(Direction direction)
    {
        if (isAlive)
        {
            if (direction == Direction.Tap)
            {
                Bomb bomb = (Bomb)_gameBoard.TryBuild(_currentTile, GameTileContentType.Bomb);
                if (bomb != null)
                {
                    bomb.Initialize(_gameBoard, _currentTile);
                }
            }
            else
            {
                base.Move(direction);
            }
        }
    }


    public override void Kill()
    {
        OnPlayerDied?.Invoke();
        isAlive = false;
    }
}
