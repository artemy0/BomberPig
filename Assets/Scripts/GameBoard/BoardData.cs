using UnityEngine;

[CreateAssetMenu(fileName = "Board/BoardData", menuName = "BoardData")]
public class BoardData : ScriptableObject
{
    public Vector2Int BoardSize => _boardSize;
    [SerializeField] private Vector2Int _boardSize;
    public Vector2 CellSize => _cellSize;
    [SerializeField] private Vector2 _cellSize = new Vector2(1.111f, 1f);

    //offset of the initial coordinates
    //(I did not want to change the values ​​of the position of the field or the starting point for the spawn of cells)
    public Vector2 BoardOffset => _boardOffset; 
    [SerializeField] private Vector2 _boardOffset = new Vector2(0f, 0.5f);

    public Sprite BoardSprite => _boardSprite;
    [SerializeField] private Sprite _boardSprite;
}
