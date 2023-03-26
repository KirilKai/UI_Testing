using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CreateView
{
  private Button backButton;
  public Action BackAction { set => backButton.clicked += value; }
  public CreateView(VisualElement root)
  {
    backButton = root.Q<Button>("BackButton");
  }
}
