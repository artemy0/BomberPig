using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private BoardData _boardData;
    [SerializeField] private GameTileContentFactory _contentFactory;
    [Space(10)]
    [SerializeField] private InputHandler _input;
    [SerializeField] private GameBoard _gameBoard;
    [SerializeField] private EntitySpawner _entitySpawner;


    private void Start()
    {
        _gameBoard.Initialize(_boardData, _contentFactory);
    }
}
