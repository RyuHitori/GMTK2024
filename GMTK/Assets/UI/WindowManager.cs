using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WindowManager : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject taskbarButtonPF;
    [Header("Relations")]
    public Transform content;
    public Transform taskbar;

    List<WindowID> windowIDs = new List<WindowID>();

    public void OpenWindow(GameObject prefab)
    {
        Window window = prefab.GetComponent<Window>();

        Window existingWindow = null;

        bool exist = false;

        foreach (WindowID item in windowIDs)
        {
            if(item.id == window.id)
            {
                exist = true;
                existingWindow = item.window;

                TaskbarButton button = existingWindow.button;
                button.target = existingWindow;
            }
        }

        if(exist)
        {
            TaskbarButton button = existingWindow.button;

            if(button != null)
            {
                button.Maximize();
                button.Focus();
            }

        }
        else
        {
            // Create a new window
            CreateNewWindow(prefab);
        }


    }

    public void MinimizeAllWindows()
    {
        foreach (WindowID item in windowIDs)
        {
            item.window.button.Minimize();
        }
    }

    public void RemoveWindow(int id)
    {
        foreach (WindowID item in windowIDs)
        {
            if (item.id == id)
            {
                windowIDs.Remove(item);
                break;
            }
        }
    }

    void CreateNewWindow(GameObject prefab)
    {
        GameObject newWindow = Instantiate(prefab, content);
        newWindow.name = prefab.name;
        Window window = newWindow.GetComponent<Window>();

        GameObject newButton = Instantiate(taskbarButtonPF, taskbar);
        newButton.name = prefab.name;
        TaskbarButton button = newButton.GetComponent<TaskbarButton>();

        button.target = newWindow.GetComponent<Window>();
        button.windowManager = this;

        window.SetControlButton(button);

        Image icon = newButton.transform.Find("Icon").GetComponent<Image>();
        icon.sprite = window.ButtonIcon;

        WindowID item = new WindowID();
        item.id = window.id;
        item.window = newWindow.GetComponent<Window>();

        windowIDs.Add(item);
    }
}




[Serializable]
struct WindowID
{
    public Window window;
    public int id;
}