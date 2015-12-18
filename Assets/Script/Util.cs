using UnityEngine;
using System.Collections;

public class Util {
    public static Vector3 GetVideoPoint(int pos_x, int pos_y)
    {
        //video_plane.transform.
        return new Vector3(pos_x, pos_y, 0f);
        //return Vector3.zero;
    }
}
