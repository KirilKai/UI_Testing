using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;

namespace TaskList.Tasks 
{
 public class TaskListEditor : EditorWindow
    {
        VisualElement container;
        ObjectField savedTasksObjectField;
        Button loadTasksButton;
        TextField taskText;
        Button addTaskButton;
        ScrollView taskListScrollView;
        TaskListSO  taskListSO;
        Button saveProgressButton;
        ProgressBar taskProgressBar;
        ToolbarSearchField searchBox;
        Label notificationLabel;

        public const string path = "Assets/TaskListAssets/TaskList/Editor/EditorWindow/";

        [MenuItem("Task List/Task List")]
        public static void ShowWindow()
        {
            TaskListEditor window = GetWindow<TaskListEditor>();
            window.titleContent = new GUIContent("Task List");
        }

        public void CreateGUI()
        {
            container = rootVisualElement;

            VisualTreeAsset original = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path + "TaskListEditor.uxml");
            container.Add(original.Instantiate());

            StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(path + "TaskListEditor.uss");
            container.styleSheets.Add(styleSheet);
        
            taskText = container.Q<TextField>("taskText");
            taskText.RegisterCallback<KeyDownEvent>(AddTask);

            addTaskButton = container.Q<Button>("addTaskButton");
            addTaskButton.clicked += AddTask;

            taskListScrollView = container.Q<ScrollView>("taskList");

            savedTasksObjectField = container.Q<ObjectField>("savedTasksObjectField");
            savedTasksObjectField.objectType = typeof(TaskListSO); //ScriptableObject

            loadTasksButton = container.Q<Button>("loadTasksButton");
            loadTasksButton.clicked += LoadTasks;

            saveProgressButton = container.Q<Button>("saveProgressButton");
            saveProgressButton.clicked += SaveProgress;

            taskProgressBar = container.Q<ProgressBar>("taskProgressBar");

            searchBox = container.Q<ToolbarSearchField>("searchBox");
            searchBox.RegisterValueChangedCallback(OnSearchTestChange);

            notificationLabel = container.Q<Label>("notificationLabel");

        }

        void AddTask()
        {
            if (!string.IsNullOrEmpty(taskText.value))
            {
                taskListScrollView.Add(CreateTask(taskText.value));
                SaveTask(taskText.value);
                taskText.value = "";
                taskText.Focus();
                notificationLabel.text = "Task Added! Don't forget to save";
                UpdateProgress();
            }
        }

        TaskItem CreateTask(string taskText)
        {
            TaskItem taskItem = new TaskItem(taskText);
            taskItem.GetTaskLabel().text = taskText;
            taskItem.GetTaskToggle().RegisterValueChangedCallback(UpdateProgress);
            return taskItem;
        }

        void AddTask(KeyDownEvent e)
        {
            if (Event.current.Equals(Event.KeyboardEvent("Return")))
            {
                AddTask();
            }
        }

        void LoadTasks()
        {
            taskListSO = savedTasksObjectField.value as TaskListSO;
            if (taskListSO != null)
            {
                taskListScrollView.Clear();
                List<string> tasks = taskListSO.GetTasks();
                foreach (string task in tasks)          
                {
                    taskListScrollView.Add(CreateTask(task));
                }                
                UpdateProgress();
                notificationLabel.text = "Tasks Loaded!";
            }
        }

        void SaveTask(string task)
        {
            taskListSO.AddTask(task);
            EditorUtility.SetDirty(taskListSO);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        void SaveProgress()
        {
            if(taskListSO!=null)
            {
                List<string> tasks = new List<string>();
                
                foreach(TaskItem task in taskListScrollView.Children())
                {
                    if (!task.GetTaskToggle().value) // Toggle off
                    {
                        tasks.Add(task.GetTaskLabel().text); // The text from that toggle
                    }
                }
                
                taskListSO.AddTasks(tasks);
                EditorUtility.SetDirty(taskListSO);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                LoadTasks();
                notificationLabel.text = "Task Saved!";
            }
        }
        void UpdateProgress()
        {
            int count= 0 ;
            int completed = 0;

            foreach (TaskItem task in taskListScrollView.Children())          
            {
                if(task.GetTaskToggle().value)
                {
                    completed++;
                }
                count++;
            }    
            if (count > 0)
            {
                float progress = completed / (float)count;
                taskProgressBar.value = progress;
                taskProgressBar.title = string.Format("{0} %", Mathf.Round(progress * 1000) / 10f);
            }
            else
            {
                taskProgressBar.value = 1;
                taskProgressBar.title = string.Format("{0} %", 100);
            }

        }
        void UpdateProgress(ChangeEvent<bool> e)
        {
            UpdateProgress();
        }
        void OnSearchTestChange(ChangeEvent<string> changeEvent)
        {
            string searchText = changeEvent.newValue.ToUpper();
            foreach(TaskItem task in taskListScrollView.Children())
            {
                string taskText = task.GetTaskLabel().text.ToUpper();
                if (!string.IsNullOrEmpty(searchText) && taskText.Contains(searchText))
                {
                    task.AddToClassList("highlight");
                }
                else
                {
                    task.RemoveFromClassList("highlight");
                }
            }
        }
    }
}
   