using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    private GameBoard _gameBoard;
    private EntityFactory _entityFactory;

    private Player _player;
    private List<Enemy> _enemies;


    public void Initialize(GameBoard gameBoard, EntityFactory entityFactory)
    {
        _gameBoard = gameBoard;
        _entityFactory = entityFactory;
    }


    public void GameUpdate(Direction direction)
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            if (_enemies[i].TryHit() == false)
            {
                _enemies[i].MoveToRandomDirection();
            }
        }

        _player.Move(direction);

        for (int i = 0; i < _enemies.Count; i++)
        {
            _enemies[i].TryHit();
        }
    }


    public Player SpawnPlayer()
    {
        if(_player != null)
        {
            _player.OriginFactory.Reclaim(_player);
        }

        GameTile spawnTile = _gameBoard.GetRandomTile();

        _player = (Player)_entityFactory.Get(EntityType.Player);
        _player.transform.SetParent(transform, false);

        _player.Initialize(_gameBoard);
        _player.Teleport(spawnTile);

        return _player;
    }

    public Enemy SpawnEnemy()
    {
        if(_enemies == null)
        {
            _enemies = new List<Enemy>();
        }


        List<GameTile> exclusionCells = new List<GameTile>() { _player.CurrentTile };
        exclusionCells.AddRange(_player.CurrentTile.GetNeighbors());
        GameTile spawnTile = _gameBoard.GetRandomTile(exclusionCells);

        Enemy enemy = (Enemy)_entityFactory.Get(EntityType.DefaultEnemy);
        enemy.transform.SetParent(transform, false);

        enemy.Initialize();
        enemy.Teleport(spawnTile);

        _enemies.Add(enemy);
        enemy.OnEnemyDied += DestroyEnemy;

        void DestroyEnemy(Enemy enemy)
        {
            _enemies.Remove(enemy);
            enemy.OnEnemyDied -= DestroyEnemy;

            SpawnEnemy();
        }

        return enemy;
    }


    public void Clear()
    {
        _player.OriginFactory.Reclaim(_player);
        for (int i = 0; i < _enemies.Count; i++)
        {
            _enemies[i].OriginFactory.Reclaim(_enemies[i]);
        }

        _player = null;
        _enemies.Clear();
    }
}
