using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class MainMenuPresenter
{
  private Button startButton;
  private Button settingsButton;
  private Button quitButton; 
  private Button loadButton;
  private MonoBehaviour monoBehaviour; // **********SUS********
  // Public properties to set the click actions for the buttons
  public Action OpenSettings { set => settingsButton.clicked += value; }
  public Action OpenCreateView { set => startButton.clicked += value; }
  public Action LoadFile { set => loadButton.clicked += value; } 
  public Action QuitGame { set => quitButton.clicked += value; } 

  public MainMenuPresenter(VisualElement root, MonoBehaviour monoBehaviour) // **********SUS********
  { // Constructor that takes a root VisualElement as a parameter 
    startButton = root.Q<Button>("Start");
    loadButton = root.Q<Button>("Load");
    settingsButton = root.Q<Button>("Settings");
    quitButton = root.Q<Button>("Quit");
    this.monoBehaviour = monoBehaviour; // **********SUS********

       
    AddLogsAndActionsToButtons();
  }

  private void AddLogsAndActionsToButtons()
  {
    startButton.clicked += () => Debug.Log("Start button clicked");
    LoadFile = () => LoadFilePressed();
    settingsButton.clicked += () => Debug.Log("Settings button clicked");
    QuitGame = () => QuitGamePressed();
  }

  public void QuitGamePressed()
  {
    Debug.Log("Quit button clicked");
    Application.Quit();
  }

  public void LoadFilePressed()
  {
    string path = UnityEditor.EditorUtility.OpenFilePanel("Select JSON file", "", "json");
    if (!string.IsNullOrEmpty(path))
    {
      Debug.Log("Selected file: " + path);

      // Load the file text
      string fileText = System.IO.File.ReadAllText(path);

      // Try to parse the JSON data
      try
      {
        object jsonData = JsonUtility.FromJson(fileText, typeof(object));
        Debug.Log("JSON file is valid");
        Debug.Log("GOOD FILE");
              
        // Load the data into your game
        // ...

        // Switch to the next scene
        monoBehaviour.StartCoroutine(LoadNextSceneAfterDelay(3f)); // **********SUS********
      }
      catch (Exception ex)
      {
        Debug.LogError("Error parsing JSON file: " + ex.Message);
        Debug.Log("BAD FILE");
      }
    }
  }
  private IEnumerator LoadNextSceneAfterDelay(float delay)
  {
    yield return new WaitForSeconds(delay);
    SceneManager.LoadScene(1);
  }
}
