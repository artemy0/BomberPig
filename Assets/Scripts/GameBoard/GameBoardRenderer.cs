using UnityEngine;

public class GameBoardRenderer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _boardRenderer;


    public void Initialize(Sprite boardSprite)
    {
        _boardRenderer.sprite = boardSprite;
    }
}
