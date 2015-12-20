using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovieHelper : MonoBehaviour {
    public GameObject position_anchor_object;

    private int mode = 0;
    private GameObject tracking_space;
    private GameObject spawn_object;
    private List<string> spawn_objectlist;
    public static List<DataWrapper> tracking_object = new List<DataWrapper>();

	void Start () {
        spawn_objectlist = new List<string>();
        spawn_object = GameObject.Find("SpawnObject");
        tracking_space = GameObject.Find("TrackingSpace");
        spawn_object.SetActive(false);
    }

    void Update () {
        if (mode == 0)
        {
            if (tracking_object.Count > 0)
            {
                NotTrackObject();
                foreach (DataWrapper a in tracking_object)
                {
                    int current_frame = GameManager.GlobalMovieTimer.GetFrame(Util.MOVIE_FRAME);
                    int frame_idx = 0;
                    int prev_frame = int.MaxValue;
                    foreach (MarkerWrapper b in a.MarkerWrapper.TrackList)
                    {
                        if (prev_frame > (current_frame - b.FrameID))
                        {
                            prev_frame = current_frame - b.FrameID;
                            frame_idx = a.MarkerWrapper.TrackList.IndexOf(b);
                        }
                    }

                    GameObject prev_object;
                    if ((prev_object = GameObject.Find(a.MarkerWrapper.MarkerID.ToString())) != null)
                    {
                        prev_object.transform.localPosition = Util.GetVideoPoint(
                            a.MarkerWrapper.TrackList[frame_idx].VideoInstance,
                            a.MarkerWrapper.TrackList[frame_idx].PositionX,
                            a.MarkerWrapper.TrackList[frame_idx].PositionY);
                    }
                    else
                    {
                        spawn_objectlist.Add(a.MarkerWrapper.MarkerID.ToString());
                        GameObject tmp = Instantiate(position_anchor_object);
                        tmp.tag = "Finish";
                        //tmp.AddComponent<CheckRenderObject>();
                        tmp.name = a.MarkerWrapper.MarkerID.ToString();
                        tmp.transform.parent = a.MarkerWrapper.TrackList[frame_idx].VideoInstance.ObjectPlane.transform;
                        tmp.transform.localScale = new Vector3(1f, 1f, 1f);
                        tmp.transform.localPosition = Util.GetVideoPoint(
                            a.MarkerWrapper.TrackList[frame_idx].VideoInstance,
                            a.MarkerWrapper.TrackList[frame_idx].PositionX,
                            a.MarkerWrapper.TrackList[frame_idx].PositionY);
                        GameManager.outside_camera_object.Add(a.MarkerWrapper.MarkerID.ToString(), tmp);
                    }
                }
            }
            else
            {
                NotTrackObject();
            }
        }
        else if (mode == 1)
        {
            if (tracking_object.Count > 0)
            {
                spawn_object.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    tracking_space.transform.localEulerAngles = new Vector3(
                        tracking_space.transform.localEulerAngles.x,
                        (180 + (tracking_object[0].MarkerWrapper.TrackList[0].VideoInstance.FileSequence * 90)),
                        tracking_space.transform.localEulerAngles.z
                        );
                }
            }
            else
            {
                spawn_object.SetActive(false);
            }
        }
	}

    void NotTrackObject()
    {
        foreach (string name in spawn_objectlist)
        {
            if (!tracking_object.Exists(item => item.MarkerWrapper.MarkerID.ToString() == name))
            {
                Destroy(GameObject.Find(name));
                GameManager.outside_camera_object.Remove(name);
            }
        }
    }
}
