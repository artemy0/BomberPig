using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseView : BaseView
{
    public event Action OnMenuButtonClicked;

    [SerializeField] private Button _menuButton;


    private void Awake()
    {
        _menuButton.onClick.AddListener(MenuButtonClick);
    }


    private void MenuButtonClick()
    {
        OnMenuButtonClicked?.Invoke();
    }
}
