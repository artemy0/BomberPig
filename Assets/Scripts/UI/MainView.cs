using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainView : BaseView
{
    public event Action OnStartButtonClicked;

    [SerializeField] private Button _startButton;


    private void Awake()
    {
        _startButton.onClick.AddListener(StartButtonClick);
    }


    private void StartButtonClick()
    {
        OnStartButtonClicked?.Invoke();
    }
}
