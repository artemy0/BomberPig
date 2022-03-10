using System;

public class Player : Entity
{
    public event Action OnPlayerDied;
    public event Action<GameTile, GameTileContentType> OnBuild;

    private bool isAlive = false;


    public void Initialize(GameBoard gameBoard)
    {
        isAlive = true;

        base.Initialize();
    }


    public override void Move(Direction direction)
    {
        if (isAlive)
        {
            if (direction == Direction.Tap)
            {
                PlantBomb();
            }
            else
            {
                base.Move(direction);
            }
        }
    }

    private void PlantBomb()
    {
        OnBuild?.Invoke(_currentTile, GameTileContentType.Bomb);
    }


    public override void Kill()
    {
        OnPlayerDied?.Invoke();
        isAlive = false;
    }
}
