using UnityEngine;

[CreateAssetMenu(fileName = "Board/BoardData", menuName = "BoardData")]
public class BoardData : ScriptableObject
{
    public Vector2Int BoardSize { get { return _boardSize; } }
    [SerializeField] private Vector2Int _boardSize;
    public Vector2 CellSize { get { return _cellSize; } }
    [SerializeField] private Vector2 _cellSize = new Vector2(1.111f, 1f);
    
    //смещение начатьных каординат (я не хотел изменять значения положения поля или точки отсчёта спавна клеток)
    public Vector2 BoardOffset { get { return _boardOffset; } } 
    [SerializeField] private Vector2 _boardOffset = new Vector2(0f, 0.5f);

    public Sprite BoardSprite { get { return _boardSprite; } }
    [SerializeField] private Sprite _boardSprite;
}
