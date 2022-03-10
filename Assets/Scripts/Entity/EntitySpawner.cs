using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    public event Action OnPlayerDie;

    [SerializeField] private int _enemyCount;
    [SerializeField] private float _delayBtwSpawn;

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
                _enemies[i].Move(direction);
            }
        }

        _player.Move(direction);

        for (int i = 0; i < _enemies.Count; i++)
        {
            _enemies[i].TryHit();
        }
    }


    public void SpawnEntities(Action OnEntitiesSpawned)
    {
        StartCoroutine(SpawnEntitiesSmoothly(OnEntitiesSpawned));
    }
    private IEnumerator SpawnEntitiesSmoothly(Action OnEntitiesSpawned)
    {
        WaitForSeconds waitBtwSpawn = new WaitForSeconds(_delayBtwSpawn);

        _player = SpawnPlayer();
        _player.OnPlayerDied += PlayerDied;

        yield return waitBtwSpawn;

        _enemies = new List<Enemy>();
        for (int i = 0; i < _enemyCount; i++)
        {
            _enemies.Add(SpawnEnemy());

            yield return waitBtwSpawn;
        }

        OnEntitiesSpawned?.Invoke();
    }

    public void ReleaseEntities()
    {
        _player.OnPlayerDied -= PlayerDied;
    }


    private Player SpawnPlayer()
    {
        if(_player != null)
        {
            _player.Recycle();
        }

        GameTile spawnTile = _gameBoard.GetRandomTile();

        Player player = (Player)_entityFactory.Get(EntityType.Player);
        player.transform.SetParent(transform, false);

        player.Initialize(_gameBoard);
        player.Teleport(spawnTile);

        return player;
    }
    private void PlayerDied()
    {
        OnPlayerDie?.Invoke();
    }

    private Enemy SpawnEnemy()
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
        _player.Recycle();
        for (int i = 0; i < _enemies.Count; i++)
        {
            _enemies[i].Recycle();
        }

        _player = null;
        _enemies.Clear();
    }
}
