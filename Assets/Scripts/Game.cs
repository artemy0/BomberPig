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
    [Space(10)]
    [SerializeField] private MainView _mainView;
    [SerializeField] private LoseView _loseView;


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
        _entitySpawner.SpawnEntities(SubHandleInput);
        _entitySpawner.OnPlayerDie += EndGame;

        _mainView.CloseView();
    }

    public void EndGame()
    {
        _entitySpawner.ReleaseEntities();
        UnsubHandleInput();
        _entitySpawner.OnPlayerDie -= EndGame;

        _loseView.OpenView();
    }

    public void ClearGame()
    {
        _gameBoard.Clear();
        _entitySpawner.Clear();

        _mainView.OpenView();
        _loseView.CloseView();
    }


    private void SubHandleInput()
    {
        _input.OnInputHandled += HandleInput;
    }
    private void UnsubHandleInput()
    {
        _input.OnInputHandled -= HandleInput;
    }

    private void HandleInput(Direction direction)
    {
        _gameBoard.GameUpdate();

        _entitySpawner.GameUpdate(direction);
    }
}
