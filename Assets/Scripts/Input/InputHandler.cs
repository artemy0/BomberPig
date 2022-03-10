using UnityEngine;
using System;

public class InputHandler : MonoBehaviour
{
    public event Action<Direction> OnInputHandled;

    [SerializeField] private float _pixelToSwipe = 10f;

    private const int InputMousButtonIndex = 0;

    private Vector2 _startMousePosition;


    private void Update()
    {
        if (Input.GetMouseButtonDown(InputMousButtonIndex))
        {
            _startMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(InputMousButtonIndex))
        {
            Vector2 currentMousePosition = Input.mousePosition;
            Vector2 swipe = currentMousePosition - _startMousePosition;

            if(swipe.magnitude > _pixelToSwipe)
            {
                if(Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y))
                {
                    if (Mathf.Sign(swipe.x) > 0)
                    {
                        OnInputHandled?.Invoke(Direction.Right);
                    }
                    else
                    {
                        OnInputHandled?.Invoke(Direction.Left);
                    }
                }
                else
                {
                    if(Mathf.Sign(swipe.y) > 0)
                    {
                        OnInputHandled?.Invoke(Direction.Up);
                    }
                    else
                    {
                        OnInputHandled?.Invoke(Direction.Down);
                    }
                }
            }
            else
            {
                OnInputHandled?.Invoke(Direction.Tap);
            }
        }
    }
}
