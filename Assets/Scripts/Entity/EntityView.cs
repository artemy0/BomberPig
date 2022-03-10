using UnityEngine;
using System;

[RequireComponent(typeof(SpriteRenderer))]
public class EntityView : MonoBehaviour
{
    [SerializeField] private Direction _defaultDirection = Direction.Right;
    [Space(10)]
    [SerializeField] private SpriteState _defaultSpriteState;
    [SerializeField] private SpriteState _attackSpriteState;

    private SpriteRenderer _renderer;

    private Direction _currentDirection;
    private SpriteState _currentSpriteState;


    public void Initialize()
    {
        _renderer = GetComponent<SpriteRenderer>();

        SetDefault();
        SetDirection(_defaultDirection);
    }


    public void SetDirection(Direction direction)
    {
        Sprite targetSprite = _renderer.sprite;
        switch (direction)
        {
            case Direction.Up:
                targetSprite = _currentSpriteState.UpDirectionSprite;
                break;
            case Direction.Down:
                targetSprite = _currentSpriteState.DownDirectionSprite;
                break;
            case Direction.Left:
                targetSprite = _currentSpriteState.LeftDirectionSprite;
                break;
            case Direction.Right:
                targetSprite = _currentSpriteState.RightDirectionSprite;
                break;
        }
        _renderer.sprite = targetSprite;

        _currentDirection = direction;
    }


    public void SetDefault()
    {
        SetState(_defaultSpriteState);
    }

    public void SetAttack()
    {
        SetState(_attackSpriteState);
    }


    private void SetState(SpriteState spriteState)
    {
        //у entity нет данного SpriteState-а
        if (spriteState.UpDirectionSprite == null)
        {
            return;
        }

        _currentSpriteState = spriteState;
        SetDirection(_currentDirection);
    }


    [Serializable]
    private class SpriteState
    {
        public Sprite UpDirectionSprite;
        public Sprite DownDirectionSprite;
        public Sprite LeftDirectionSprite;
        public Sprite RightDirectionSprite;
    }
}

