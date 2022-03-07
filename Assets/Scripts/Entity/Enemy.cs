using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public void MoveToRandomDirection()
    {
        Direction randomDirection = GetRandomDirection();
        Move(randomDirection);
    }

    public bool TryHit()
    {
        Direction targetDirection = Direction.Tap;
        GameTile targetNeighbor = null;

        GameTile upNeighbor = _currentTile.GetNeighbor(Direction.Up);
        GameTile downNeighbor = _currentTile.GetNeighbor(Direction.Down);
        GameTile leftNeighbor = _currentTile.GetNeighbor(Direction.Left);
        GameTile rightNeighbor = _currentTile.GetNeighbor(Direction.Right);
        
        if (IsAvailableToHit(upNeighbor))
        {
            targetDirection = Direction.Up;
            targetNeighbor = upNeighbor;
        }
        else if (IsAvailableToHit(downNeighbor))
        {
            targetDirection = Direction.Down;
            targetNeighbor = downNeighbor;
        }
        else if (IsAvailableToHit(leftNeighbor))
        {
            targetDirection = Direction.Left;
            targetNeighbor = leftNeighbor;
        }
        else if (IsAvailableToHit(rightNeighbor))
        {
            targetDirection = Direction.Right;
            targetNeighbor = rightNeighbor;
        }


        if (targetDirection != Direction.Tap && targetNeighbor != null)
        {
            _view.SetAttack();
            _view.SetDirection(targetDirection);

            Player player = (Player)targetNeighbor.Entity;
            player.Kill();

            return true;
        }

        return false;
    }


    private Direction GetRandomDirection()
    {
        List<Direction> directions = new List<Direction>(4);

        GameTile upNeighbor = _currentTile.GetNeighbor(Direction.Up);
        GameTile downNeighbor = _currentTile.GetNeighbor(Direction.Down);
        GameTile leftNeighbor = _currentTile.GetNeighbor(Direction.Left);
        GameTile rightNeighbor = _currentTile.GetNeighbor(Direction.Right);

        if (IsAvailableToMove(upNeighbor))
        {
            directions.Add(Direction.Up);
        }
        if (IsAvailableToMove(downNeighbor))
        {
            directions.Add(Direction.Down);
        }
        if (IsAvailableToMove(leftNeighbor))
        {
            directions.Add(Direction.Left);
        }
        if (IsAvailableToMove(rightNeighbor))
        {
            directions.Add(Direction.Right);
        }


        if (directions.Count > 0)
        {
            int randomDirectionIndex = Random.Range(0, directions.Count);
            return directions[randomDirectionIndex];
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
}
