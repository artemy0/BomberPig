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
    [Space(5)]
    [SerializeField] private int _enemyCount;
    [SerializeField] private float _delayBtwSpawn;
    [Space(10)]
    [SerializeField] private MainView _mainView;
    [SerializeField] private LoseView _loseView;

    private Player _player;
    private LinkedList<Enemy> _enemies;


    private void Start()
    {
        _gameBoard.Initialize(_boardData, _contentFactory);
        _entitySpawner.Initialize(_gameBoard, _entityFactory);


        _mainView.OpenView();

        _mainView.OnStartButtonClicked += StartGame;
        _loseView.OnMenuButtonClicked += ClearGame;
    }

    private void OnDestroy()
    {
        _mainView.OnStartButtonClicked -= StartGame;
        _loseView.OnMenuButtonClicked -= ClearGame;
    }


    public void StartGame()
    {
        SpawnEntities();

        _mainView.CloseView();
    }

    public void EndGame()
    {
        ReleaseEntities();

        _loseView.OpenView();
    }

    public void ClearGame()
    {
        _gameBoard.Clear();
        _entitySpawner.Clear();

        _mainView.OpenView();
        _loseView.CloseView();
    }


    private void SpawnEntities()
    {
        StartCoroutine(SpawnEntitiesSmoothly());
    }
    private IEnumerator SpawnEntitiesSmoothly()
    {
        WaitForSeconds waitBtwSpawn = new WaitForSeconds(_delayBtwSpawn);

        _player = _entitySpawner.SpawnPlayer();
        _player.OnPlayerDied += EndGame;

        yield return waitBtwSpawn;

        _enemies = new LinkedList<Enemy>();
        for (int i = 0; i < _enemyCount; i++)
        {
            _enemies.AddFirst(_entitySpawner.SpawnEnemy());

            yield return waitBtwSpawn;
        }

        _input.OnInputHandled += HandleInput;
    }

    private void ReleaseEntities()
    {
        _player.OnPlayerDied -= EndGame;

        _input.OnInputHandled -= HandleInput;
    }


    private void HandleInput(Direction direction)
    {
        _gameBoard.GameUpdate();

        _entitySpawner.GameUpdate(direction);
    }
}
