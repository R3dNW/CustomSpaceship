using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class Utilities
{
    public static Vector3 Round(this Vector3 pos)
    {
        return new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), Mathf.Round(pos.z));
    }

    public static Vector2 Round(this Vector2 pos)
    {
        return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
    }

    // https://forum.unity3d.com/threads/left-right-test-function.31420/
    /// <summary>
    /// returns -1 when to the left, 1 to the right, and 0 for forward/backward
    /// </summary>
    /// <returns></returns>
    public static float AngleDirection(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);

        if (dir > 0.0f)
        {
            return 1.0f;
        }
        else if (dir < 0.0f)
        {
            return -1.0f;
        }
        else
        {
            return 0.0f;
        }
    }


    // https://stackoverflow.com/questions/13221873/determining-if-one-2d-vector-is-to-the-right-or-left-of-another
    /// <summary>
    /// returns negative when B is to the left of A, positive when B is to the right of A, and 0 for perfectly aligned
    /// </summary>
    /// <returns></returns>
    public static float AngleDirection(Vector2 A, Vector2 B)
    {
        return -A.x * B.y + A.y * B.x;
    }
}
