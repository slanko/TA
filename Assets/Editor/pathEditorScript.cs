using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(pathCreatorScript))]
public class pathEditorScript : Editor
{
    pathCreatorScript creator;
    Path path;

    void OnSceneGUI()
    {
        Draw();
        Input();
    }

    void Input()
    {
        Event guiEvent = Event.current;
        Vector2 mousePos = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition).origin;

        if(guiEvent.type == EventType.MouseDown && guiEvent.button == 0 && guiEvent.shift)
        {
            Undo.RecordObject(creator, "Add Segment");
            path.addSegment(mousePos);
        }
    }

    void Draw()
    {

        for (int i = 0; i < path.numSegments; i++)
        {
            Vector2[] points = path.getPointsInSegment(i);
            Handles.color = Color.black;
            Handles.DrawLine(points[1], points[0]);
            Handles.DrawLine(points[2], points[3]);
            Handles.DrawBezier(points[0], points[3], points[1], points[2], Color.blue, null, 2);
        }

        Handles.color = Color.red;
        for (int i = 0; i < path.numPoints; i++)
        {
            Vector2 newPos = Handles.FreeMoveHandle(path[i], Quaternion.identity, .1f, Vector2.zero, Handles.CylinderHandleCap);
            if(path[i] != newPos)
            {
                Undo.RecordObject(creator, "Move Point");
                path.movePoint(i, newPos);
            }
        }
    }

    private void OnEnable()
    {
        creator = (pathCreatorScript)target;
        if(creator.path == null)
        {
            creator.createPath();
        }
        path = creator.path;
    }

}
