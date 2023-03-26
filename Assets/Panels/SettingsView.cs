using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingsView
{
  private Button backButton;
  public Action BackAction { set => backButton.clicked += value; }

  public SettingsView(VisualElement root)
  {
    backButton = root.Q<Button>("BackButton");
  }
}
