using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private BoardData _boardData;
    [SerializeField] private GameTileContentFactory _contentFactory;
    [SerializeField] private EntityFactory _entityFactory;
    [Space(10)]
    [SerializeField] private InputHandler _input;
    [Space(10)]
    [SerializeField] private GameBoard _gameBoard;
    [SerializeField] private EntitySpawner _entitySpawner;

    private Player _player;
    private LinkedList<Enemy> _enemies;


    private void Start()
    {
        _gameBoard.Initialize(_boardData, _contentFactory);
        _entitySpawner.Initialize(_gameBoard, _entityFactory);

        StartGame();
    }


    public void StartGame()
    {
        _player = _entitySpawner.SpawnPlayer();
        _player.OnPlayerDied += EndGame;

        _enemies = new LinkedList<Enemy>();
        for (int i = 0; i < 5; i++)
        {
            _enemies.AddFirst(_entitySpawner.SpawnEnemy());
        }

        _input.OnInputHandled += HandleInput;
    }

    public void EndGame()
    {
        _player.OnPlayerDied -= EndGame;

        _input.OnInputHandled -= HandleInput;
    }


    private void HandleInput(Direction direction)
    {
        foreach (Enemy enemy in _enemies)
        {
            if (enemy.TryHit() == false)
            {
                enemy.MoveToRandomDirection();
            }
        }

        _player.Move(direction);

        _gameBoard.GameUpdate();
    }
}
