using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : Entity
{
    public event Action<Enemy> OnEnemyDied;


    public override void Move(Direction direction)
    {
        if (direction != Direction.Tap)
        {
            Direction randomDirection = GetRandomDirection();
            base.Move(randomDirection);
        }
    }

    public bool TryHit()
    {
        List<Direction> directions = null;
        List<GameTile> neigbors = _currentTile.GetNeighbors(out directions);

        for (int i = 0; i < neigbors.Count; i++)
        {
            if (IsAvailableToHit(neigbors[i]))
            {
                _view.SetAttack();
                _view.SetDirection(directions[i]);

                Player player = (Player)neigbors[i].Entity;
                player.Kill();

                return true;
            }
        }

        return false;
    }


    private Direction GetRandomDirection()
    {
        List<Direction> availableDirections = new List<Direction>(4);

        List<Direction> directions = null;
        List<GameTile> neigbors = _currentTile.GetNeighbors(out directions);

        for (int i = 0; i < neigbors.Count; i++)
        {
            if (IsAvailableToMove(neigbors[i]))
            {
                availableDirections.Add(directions[i]);
            }
        }

        if (availableDirections.Count > 0)
        {
            int randomAvailableDirectionsIndex = UnityEngine.Random.Range(0, availableDirections.Count);
            return availableDirections[randomAvailableDirectionsIndex];
        }
        else
        {
            return Direction.Tap;
        }
    }
    private bool IsAvailableToHit(GameTile targetTile)
    {
        return targetTile != null && targetTile.Entity != null && targetTile.Entity is Player;
    }

    public override void Kill()
    {
        OnEnemyDied?.Invoke(this);
        Recycle();
    }
}
