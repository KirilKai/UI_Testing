using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StartView : MonoBehaviour
{
        private VisualElement _StartView;
        private VisualElement _SettingsView;
        private VisualElement _CreateView;
        void Start()
        {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;

            _StartView = root.Q("MainMenu");
            _SettingsView = root.Q("SettingsView");
            _CreateView = root.Q("CreateView");
        
            SetupStartMenu();
            SetupSettingsMenu();
            SetupCreateView();
        }
        private void SetupStartMenu()
        {
            MainMenuPresenter mainMenuPresenter = new MainMenuPresenter(_StartView);
            mainMenuPresenter.OpenSettings = () => ToggleSettingsMenu(true);
            mainMenuPresenter.OpenCreateView = () => ToggleCreateView(true);
        }
        private void SetupSettingsMenu()
        {
            SettingsView settingsView = new SettingsView(_SettingsView);
            settingsView.BackAction = () => ToggleSettingsMenu(false);
        }
        private void SetupCreateView()
        {
            CreateView createView = new CreateView(_CreateView);
            createView.BackAction = () => ToggleCreateView(false);
        }
        private void ToggleSettingsMenu(bool enable)
        {
            _StartView.Display(!enable);
            _SettingsView.Display(enable);
        }
        private void ToggleCreateView(bool enable)
        {
            _StartView.Display(!enable);
            _CreateView.Display(enable);
        }
}
