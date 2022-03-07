using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    private GameBoard _gameBoard;
    private EntityFactory _entityFactory;


    public void Initialize(GameBoard gameBoard, EntityFactory entityFactory)
    {
        _gameBoard = gameBoard;
        _entityFactory = entityFactory;
    }


    public Player SpawnPlayer()
    {
        GameTile spawnTile = _gameBoard.GetRandomTile();

        Player player = (Player)_entityFactory.Get(EntityType.Player);
        player.transform.SetParent(transform, false);

        player.Initialize(_gameBoard);
        player.Teleport(spawnTile);

        return player;
    }


    public Enemy SpawnEnemy()
    {
        GameTile spawnTile = _gameBoard.GetRandomTile();

        Enemy enemy = (Enemy)_entityFactory.Get(EntityType.DefaultEnemy);
        enemy.transform.SetParent(transform, false);

        enemy.Initialize();
        enemy.Teleport(spawnTile);

        return enemy;
    }
}
