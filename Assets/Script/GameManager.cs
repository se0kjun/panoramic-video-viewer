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
    public static Dictionary<string, GameObject> outside_camera_object = new Dictionary<string, GameObject>();

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
    }

    void Update () {
        GlobalMovieTimer += Time.deltaTime;

        int curr_frame = GlobalMovieTimer.GetFrame(Util.MOVIE_FRAME);
        foreach(DataWrapper data in cube_data)
        {
            if(data.MarkerWrapper.StartFrame < curr_frame && data.MarkerWrapper.EndFrame > curr_frame)
            {
                MovieHelper.tracking_object.Add(data);
            }
        }

        MovieHelper.tracking_object.RemoveAll(item => (item.MarkerWrapper.EndFrame < curr_frame));
    }
}
