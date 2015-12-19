using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovieHelper : MonoBehaviour {
    private int mode = 0;
    private bool moving_state = true;
    private GameObject tracking_space;
    public static List<DataWrapper> tracking_object = new List<DataWrapper>();

	// Use this for initialization
	void Start () {
        tracking_space = GameObject.Find("TrackingSpace");
	}
	
	// Update is called once per frame
	void Update () {
        if (tracking_object.Count > 0)
            moving_state = false;
        if (mode == 0)
        {
            foreach (Transform a in transform)
            {
                Destroy(a.gameObject);
            }

            foreach (DataWrapper a in tracking_object)
            {
                int current_frame = GameManager.GlobalMovieTimer.GetFrame(Util.MOVIE_FRAME);
                int frame_idx = 0;
                int prev_frame = int.MaxValue;
                foreach (MarkerWrapper b in a.MarkerWrapper.TrackList)
                {
                    if(prev_frame > (current_frame - b.FrameID))
                    {
                        prev_frame = current_frame - b.FrameID;
                        frame_idx = a.MarkerWrapper.TrackList.IndexOf(b);
                    }
                }
                GameObject tmp = new GameObject();
                tmp.tag = "Finish";
                tmp.name = "test";
                tmp.transform.localScale = new Vector3(.1f, .1f, .1f);
                tmp.transform.parent = a.MarkerWrapper.TrackList[frame_idx].VideoInstance.ObjectPlane.transform;
                tmp.transform.localPosition = Util.GetVideoPoint(
                    a.MarkerWrapper.TrackList[frame_idx].VideoInstance,
                    a.MarkerWrapper.TrackList[frame_idx].PositionX,
                    a.MarkerWrapper.TrackList[frame_idx].PositionY);
            }
        }
        else if (mode == 1 && !moving_state)
        {
            //if(tracking_object.Count == 0)
            //{
            //    moving_state = true;
            //    return;
            //}

            //if(tracking_space.transform.localEulerAngles.y != (360 - (int.Parse(tracking_object[0].MarkerWrapper.TrackList[0].VideoID) * 90)))
            //{
            //    tracking_space.transform.Rotate(Vector3.up * 120 * Time.deltaTime);
            //}
        }
	}
}
