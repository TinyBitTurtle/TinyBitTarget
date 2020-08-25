﻿using UnityEngine;
using UnityEditor;

public class GameFlowEditor : EditorWindow
{
    int selected = 0;
    string[] options = new string[]
    {
     "Option1", "Option2", "Option3",
    };

    Rect window1;
    Rect window2;

    [MenuItem("Window/Node editor")]
    static void ShowEditor()
    {
        GameFlowEditor editor = EditorWindow.GetWindow<GameFlowEditor>();
        editor.Init();
    }

    public void Init()
    {
        window1 = new Rect(10, 10, 100, 100);
        window2 = new Rect(210, 210, 100, 100);
    }

    void OnGUI()
    {
        DrawNodeCurve(window1, window2); // Here the curve is drawn under the windows

        BeginWindows();
        window1 = GUI.Window(1, window1, DrawNodeWindow, "Splash");   // Updates the Rect's when these are dragged
        window2 = GUI.Window(2, window2, DrawNodeWindow, "Window 2");

        selected = EditorGUILayout.Popup("Label", selected, options);
        EndWindows();
    }

    void DrawNodeWindow(int id)
    {
        GUI.DragWindow();
    }

    void DrawNodeCurve(Rect start, Rect end)
    {
        Vector3 startPos = new Vector3(start.x + start.width, start.y + start.height / 2, 0);
        Vector3 endPos = new Vector3(end.x, end.y + end.height / 2, 0);
        Vector3 startTan = startPos + Vector3.right * 50;
        Vector3 endTan = endPos + Vector3.left * 50;
        Color shadowCol = new Color(0, 0, 0, 0.06f);
        for (int i = 0; i < 3; i++) // Draw a shadow
            Handles.DrawBezier(startPos, endPos, startTan, endTan, shadowCol, null, (i + 1) * 5);
        Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.black, null, 1);
    }
}