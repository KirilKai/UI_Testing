using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class MainMenuPresenter 
{
    private Button startButton;
    private Button settingsButton;
    private Button quitButton; 
    public Action OpenSettings { set => settingsButton.clicked += value; }
    public Action OpenCreateView { set => startButton.clicked += value; }

  public MainMenuPresenter(VisualElement root)
    {
       startButton = root.Q<Button>("Start");
       settingsButton = root.Q<Button>("Settings");
       quitButton = root.Q<Button>("Quit");
       
       AddLogsToButtons();
    }
    private void AddLogsToButtons()
    {
        startButton.clicked += () => Debug.Log("Start button clicked");
        settingsButton.clicked += () => Debug.Log("Settings button clicked");
        quitButton.clicked += () => 
        {
          Debug.Log("Quit button clicked");
          Application.Quit();
        };
    }
}
