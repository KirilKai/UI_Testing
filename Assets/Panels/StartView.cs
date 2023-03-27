using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StartView : MonoBehaviour
{
        // Private fields to hold references to the UI elements
        private VisualElement _StartView;
        private VisualElement _SettingsView;
        private VisualElement _CreateView;
        void Start()
        {
            // Get the root visual element of the UI document
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;

            // Get references to the UI elements by their names
            _StartView = root.Q("MainMenu");
            _SettingsView = root.Q("SettingsView");
            _CreateView = root.Q("CreateView");
        
            // Call the setup methods for each menu view
            SetupStartMenu();
            SetupSettingsMenu();
            SetupCreateView();
        }
        private void SetupStartMenu()
        {
            // Create a new MainMenuPresenter instance and pass in the StartView element
            MainMenuPresenter mainMenuPresenter = new MainMenuPresenter(_StartView);

            // Set up the actions to toggle the (create, settings) views on
            mainMenuPresenter.OpenSettings = () => ToggleSettingsMenu(true);
            mainMenuPresenter.OpenCreateView = () => ToggleCreateView(true);
        }
        private void SetupSettingsMenu()
        {   // Set the BackAction to toggle the SettingsView off
            SettingsView settingsView = new SettingsView(_SettingsView);
            settingsView.BackAction = () => ToggleSettingsMenu(false);
        }
        private void SetupCreateView()
        {   // Set the BackAction to toggle the CreateView off
            CreateView createView = new CreateView(_CreateView);
            createView.BackAction = () => ToggleCreateView(false);
        }
        private void ToggleSettingsMenu(bool enable)
        {    // Toggle the settings menu on/off
            _StartView.Display(!enable);
            _SettingsView.Display(enable);
        }
        private void ToggleCreateView(bool enable)
        {    // Toggle the create view on/off
            _StartView.Display(!enable);
            _CreateView.Display(enable);
        }
}
