using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(pathCreatorScript))]
public class pathEditorScript : Editor
{
    pathCreatorScript creator;
    Path path;

    const float segmentSelectThresh = .1f;
    int selectedSegmentIndex = -1;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUI.BeginChangeCheck();
        if (GUILayout.Button("Create New"))
        {
            Undo.RecordObject(creator, "Create New Path");
            creator.createPath();
            path = creator.path;
        }

        bool isClosed = GUILayout.Toggle(path.isRealClosed, "Closed");

        if (isClosed != path.isRealClosed)
        {
            Undo.RecordObject(creator, "Toggle Closed");
            path.isRealClosed = isClosed;
        }

        bool autoSetControls = GUILayout.Toggle(path.autoSetControlsAC, "Auto Set Control Points");
        if(autoSetControls != path.autoSetControlsAC)
        {
            Undo.RecordObject(creator, "Toggle Auto Set Controls");
            path.autoSetControlsAC = autoSetControls;
        }

        if (EditorGUI.EndChangeCheck())
        {
            SceneView.RepaintAll();
        }
    }

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
            if(selectedSegmentIndex != -1)
            {
                Undo.RecordObject(creator, "Split Segment");
                path.splitSegment(mousePos, selectedSegmentIndex);
            }
            else if (!path.isRealClosed)
            {
                Undo.RecordObject(creator, "Add Segment");
                path.addSegment(mousePos);
            }
        }

        if(guiEvent.type == EventType.MouseDown && guiEvent.button == 1)
        {
            float minAnchorDist = creator.anchorDiameter * .5f;
            int closestAnchorIndex = -1;

            for(int i = 0; i < path.numPoints; i += 3)
            {
                float dist = Vector2.Distance(mousePos, path[i]);
                if(dist< minAnchorDist)
                {
                    minAnchorDist = dist;
                    closestAnchorIndex = i;
                }
            }

            if(closestAnchorIndex != -1)
            {
                Undo.RecordObject(creator, "Delete Segment");
                path.deleteSegment(closestAnchorIndex);
            }

        }


        if(guiEvent.type == EventType.MouseMove)
        {
            float minDistToSeg = segmentSelectThresh;
            int newSelectedSegIndex = -1;

            for (int i = 0; i < path.numSegments; i++)
            {
                Vector2[] points = path.getPointsInSegment(i);
                float dst = HandleUtility.DistancePointBezier(mousePos, points[0], points[3], points[1], points[2]);
                if (dst < minDistToSeg)
                {
                    minDistToSeg = dst;
                    newSelectedSegIndex = i;
                }
            }
            if(newSelectedSegIndex != selectedSegmentIndex)
            {
                selectedSegmentIndex = newSelectedSegIndex;
                HandleUtility.Repaint();
            }

        }


    }

    void Draw()
    {

        for (int i = 0; i < path.numSegments; i++)
        {
            Vector2[] points = path.getPointsInSegment(i);
            if (creator.displayControlPoints)
            {
                Handles.color = Color.black;
                Handles.DrawLine(points[1], points[0]);
                Handles.DrawLine(points[2], points[3]);
            }
            Color segColor = (i == selectedSegmentIndex && Event.current.shift) ? creator.selectedSegColor : creator.segmentColor;
            Handles.DrawBezier(points[0], points[3], points[1], points[2], segColor, null, 2);
        }

        Handles.color = Color.red;
        for (int i = 0; i < path.numPoints; i++)
        {
            Handles.color = (i % 3 == 0) ? creator.anchorColor : creator.controlColor;
            float handleSize = (i % 3 == 0 || creator.displayControlPoints) ? creator.anchorDiameter : creator.controlDiameter;
            Vector2 newPos = Handles.FreeMoveHandle(path[i], Quaternion.identity, handleSize, Vector2.zero, Handles.CylinderHandleCap);
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
