using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Path
{
    [SerializeField, HideInInspector] 
    List<Vector2> points;
    [SerializeField, HideInInspector]
    bool isClosed;
    [SerializeField, HideInInspector]
    bool autoSetControls;

    public Path(Vector2 centre)
    {
        points = new List<Vector2>
        {
            centre + Vector2.left,
            centre + (Vector2.left + Vector2.up) * .5f,
            centre + (Vector2.right + Vector2.down) * .5f,
            centre + Vector2.right
        };
    }
    public Vector2 this[int i]
    {
        get
        {
            return points[i];
        }
    }

    public bool isRealClosed
    {
        get
        {
            return isClosed;
        }
        set
        {
            if(isClosed != value)
            {
                isClosed = value;

                if (isClosed)
                {
                    points.Add(points[points.Count - 1] * 2 - points[points.Count - 2]);
                    points.Add(points[0] * 2 - points[1]);
                    if (autoSetControls)
                    {
                        autoAnchor(0);
                        autoAnchor(points.Count - 3);
                    }
                }
                else
                {
                    points.RemoveRange(points.Count - 2, 2);
                    if (autoSetControls)
                    {
                        autoSetEnds();
                    }
                }
            }
        }
    }

    public bool autoSetControlsAC
    {
        get
        {
            return autoSetControls;
        }
        set
        {
            if(autoSetControls != value)
            {
                autoSetControls = value;
                if (autoSetControls)
                {
                    autoSetAll();
                }
            }
        }
    }
    public int numPoints
    {
        get
        {
            return points.Count;
        }
    }
    public int numSegments
    {
        get
        {
            return points.Count / 3;
        }
    }
    public void addSegment(Vector2 anchorPos)
    {
        points.Add(points[points.Count - 1] * 2 - points[points.Count - 2]);
        points.Add((points[points.Count - 1] + anchorPos) * .5f);
        points.Add(anchorPos);

        if (autoSetControls)
        {
            autoSetEffected(points.Count - 1);
        }
    }

    public void splitSegment(Vector2 anchorPos, int segmentIndex)
    {
        points.InsertRange(segmentIndex * 3 + 2, new Vector2[] { Vector2.zero, anchorPos, Vector2.zero });
        if (autoSetControls)
        {
            autoSetEffected(segmentIndex * 3 + 3);
        }
        else
        {
            autoAnchor(segmentIndex * 3 + 3);
        }
    }

    public void deleteSegment(int anchorIndex)
    {
        if(numSegments > 2 || !isClosed && numSegments > 1)
        {
            if (anchorIndex == 0)
            {
                if (isClosed)
                {
                    points[points.Count - 1] = points[2];
                }
                points.RemoveRange(0, 3);
            }
            else if (anchorIndex == points.Count - 1 && !isClosed)
            {
                points.RemoveRange(anchorIndex - 2, 3);
            }
            else
            {
                points.RemoveRange(anchorIndex - 1, 3);
            }
        }
    }

    public Vector2[] getPointsInSegment(int i)
    {
        return new Vector2[] { points[i * 3], points[i * 3 + 1], points[i * 3 + 2], points[loopIndex(i * 3 + 3)] };
    }

    public void movePoint(int i, Vector2 pos)
    {
        Vector2 deltaMove = pos - points[i];
        points[i] = pos;

        if(i % 3 == 0 || !autoSetControls)
        {
            if (autoSetControls)
            {
                autoSetEffected(i);
            }
            else
            {
                if (i % 3 == 0)
                {
                    if (i + 1 < points.Count || isClosed)
                    {
                        points[loopIndex(i + 1)] += deltaMove;
                    }
                    if (i - 1 >= 0)
                    {
                        points[loopIndex(i - 1)] += deltaMove;
                    }
                }
                else
                {
                    bool nextPointIsAnchor = (i + 1) % 3 == 0;
                    int correspondingControlIndex = (nextPointIsAnchor) ? i + 2 : i - 2;
                    int anchorIndex = (nextPointIsAnchor) ? i + 1 : i - 1;

                    if (correspondingControlIndex >= 0 && correspondingControlIndex < points.Count || isClosed)
                    {
                        float dst = (points[loopIndex(anchorIndex)] - points[loopIndex(correspondingControlIndex)]).magnitude;
                        Vector2 dir = (points[loopIndex(anchorIndex)] - pos).normalized;
                        points[loopIndex(correspondingControlIndex)] = points[loopIndex(anchorIndex)] + dir * dst;
                    }
                }
            }
        }
    }

    void autoSetEffected(int newAnchorIndex)
    {
        for(int i = newAnchorIndex - 3; i <= newAnchorIndex + 3; i += 3)
        {
            if(i >= 0 && i < points.Count || isClosed)
            {
                autoAnchor(loopIndex(i));
            }
        }

        autoSetEnds();
    }

    void autoSetAll()
    {
        for (int i = 0; i < points.Count; i += 3)
        {
            autoAnchor(i);
        }

        autoSetEnds();
    }

    void autoAnchor(int anchorIndex)
    {
        Vector2 anchorPos = points[anchorIndex];
        Vector2 dir = Vector2.zero;
        float[] neighbourDistance = new float[2];

        if(anchorIndex - 3 >= 0 || isClosed)
        {
            Vector2 offset = points[loopIndex(anchorIndex - 3)] - anchorPos;
            dir += offset.normalized;
            neighbourDistance[0] = offset.magnitude;
        }

        if (anchorIndex + 3 >= 0 || isClosed)
        {
            Vector2 offset = points[loopIndex(anchorIndex + 3)] - anchorPos;
            dir -= offset.normalized;
            neighbourDistance[1] = -offset.magnitude;
        }

        dir.Normalize();

        for(int i = 0; i < 2; i++)
        {
            int controlIndex = anchorIndex + i * 2 - 1;
            if(controlIndex >= 0 && controlIndex < points.Count || isClosed)
            {
                points[loopIndex(controlIndex)] = anchorPos + dir * neighbourDistance[i] * .5f;
            }
        }
    }

    void autoSetEnds()
    {
        if (!isClosed)
        {
            points[1] = (points[0] + points[2] * .5f);
            points[points.Count - 2] = (points[points.Count - 1] + points[points.Count - 3] * .5f);
        }
    }

    int loopIndex(int i)
    {
        return (i + points.Count) % points.Count;
    }

}
