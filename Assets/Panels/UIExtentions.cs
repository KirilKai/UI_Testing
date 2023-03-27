using System;
using System.IO;
using UnityEngine.UIElements;

// This class provides an extension method called Display
// `this` - represents the VisualElement object that it is called on
public static class UIExtentions
{
  public static void Display(this VisualElement element, bool enabled)
  {
    if (element == null) return;
    element.style.display = enabled ? DisplayStyle.Flex : DisplayStyle.None;
  }
}