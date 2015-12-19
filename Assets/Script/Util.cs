using UnityEngine;
using System.Collections;

public class Util {
    public static readonly int MOVIE_FRAME = 10;
    public static readonly int PLANE_WIDTH = 10;
    public static readonly int PLANE_HEIGHT = 10;

    public static Vector3 GetVideoPoint(VideoXMLWrapper video, int pos_x, int pos_y)
    {
        int width = video.Width;
        int height = video.Height;

        float plane_w_point = ((float)pos_x / (float)width) * Util.PLANE_WIDTH;
        float plane_h_point = ((float)pos_y / (float)height) * Util.PLANE_HEIGHT;

        float plane_x = (PLANE_WIDTH / 2) - plane_w_point;
        float plane_z = -(PLANE_HEIGHT / 2) + plane_h_point;

        return new Vector3(plane_x, 0f, plane_z);
    }
}
