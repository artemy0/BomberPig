using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private BoardData _boardData;
    [SerializeField] private GameTileContentFactory _contentFactory;
    [Space(10)]
    [SerializeField] private GameBoard _gameBoard;


    private void Start()
    {
        _gameBoard.Initialize(_boardData, _contentFactory);
    }
}
