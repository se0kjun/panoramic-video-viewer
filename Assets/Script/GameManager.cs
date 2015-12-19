using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	public string XMLFileName;
    public Camera myCam;

	[HideInInspector]
	public VideoXMLParser parser = VideoXMLParser.Instance;

	public List<GameObject> cube_plane;
	public List<MovieTexture> cube_movie;
    private List<DataWrapper> cube_data;

	[HideInInspector]
	public static MovieTimer GlobalMovieTimer;

	void Start () {
        cube_movie = new List<MovieTexture>();
        GlobalMovieTimer = new MovieTimer(0.0f);

		if(XMLFileName != null) {
            parser.LoadXML(XMLFileName);
			cube_data = parser.ParseXML();
		}

        //for(int i =0; i<cube_plane.Count && cube_movie.Count > i ; i++)
        //{
        //    cube_plane[i].GetComponent<Renderer>().material.mainTexture = cube_movie[i];
        //    MovieTexture tmp = cube_plane[i].GetComponent<Renderer>().material.mainTexture as MovieTexture;
        //    if (tmp != null)
        //        tmp.Play();
        //}

        foreach (GameObject plane in cube_plane)
        {
            MovieTexture tmp = plane.GetComponent<Renderer>().material.mainTexture as MovieTexture;
            if (tmp != null)
            {
                tmp.Play();
                cube_movie.Add(tmp);
            }
        }
        Debug.Log(myCam.ScreenToWorldPoint(Vector3.zero));
        Debug.Log(myCam.ScreenToWorldPoint(new Vector3(10.0f, 10.0f, -20.0f)));
    }

    void Update () {
        Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, myCam.farClipPlane));
        float distance;
        xy.Raycast(ray, out distance);
        ray.GetPoint(distance);
        Debug.DrawLine(myCam.transform.position, ray.GetPoint(distance));
        Vector3 m = Input.mousePosition;
        Debug.Log(myCam.nearClipPlane);
        m.z = 5.0f;// myCam.nearClipPlane;

        Debug.Log(myCam.ScreenToWorldPoint(m));

        GlobalMovieTimer += Time.deltaTime;

        int curr_frame = GlobalMovieTimer.GetFrame(Util.MOVIE_FRAME);
        foreach(DataWrapper data in cube_data)
        {
            if(data.MarkerWrapper.StartFrame < curr_frame && data.MarkerWrapper.EndFrame > curr_frame)
            {
                //data.TrackingState = DataWrapper.TrackState.START;
                MovieHelper.tracking_object.Add(data);
            }
        }

        MovieHelper.tracking_object.RemoveAll(item => (item.MarkerWrapper.EndFrame < curr_frame));
        //Debug.Log(curr_frame);
        //Debug.Log(GlobalMovieTimer.Seconds.ToString() + ":" + GlobalMovieTimer.Fraction.ToString() + "-----" + curr_frame.ToString());
        //Debug.Log(string.Format("{0:00} : {1:00} : {2:} - {3:}", GlobalMovieTimer.Minutes, (int)GlobalMovieTimer.Seconds, (int)GlobalMovieTimer.Fraction, curr_frame));
    }
}
