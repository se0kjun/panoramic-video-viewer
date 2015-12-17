using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour {
	public string XMLFileName;

	[HideInInspector]
	public VideoXMLParser parser;

	public List<GameObject> cube_plane;
	private List<MovieTexture> cube_movie;
    private List<DataWrapper> cube_data;
    private List<VideoXMLWrapper> video_list;

	[HideInInspector]
	public static MovieTimer GlobalMovieTimer;

	void Start () {
        cube_movie = new List<MovieTexture>();
        GlobalMovieTimer = new MovieTimer(0.0f);

		if(XMLFileName != null) {
			parser = new VideoXMLParser(XMLFileName);
			cube_data = parser.ParseXML();
            video_list = parser.ParseVideoXML();
		}

        //foreach(VideoXMLWrapper video in video_list)
        //{
        //    MovieTexture tmp = new MovieTexture();
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

        int curr_frame = GlobalMovieTimer.GetFrame(10);
        foreach(DataWrapper data in cube_data)
        {
            if(data.MarkerWrapper.StartFrame < curr_frame && data.MarkerWrapper.EndFrame > curr_frame)
            {
                data.TrackingState = DataWrapper.TrackState.START;
                MovieHelper.tracking_object.Add(data);
            }
        }

        MovieHelper.tracking_object.RemoveAll(item => (item.MarkerWrapper.EndFrame < curr_frame));
        Debug.Log(curr_frame);
        //Debug.Log(GlobalMovieTimer.Seconds.ToString() + ":" + GlobalMovieTimer.Fraction.ToString() + "-----" + curr_frame.ToString());
        //Debug.Log(string.Format("{0:00} : {1:00} : {2:} - {3:}", GlobalMovieTimer.Minutes, (int)GlobalMovieTimer.Seconds, (int)GlobalMovieTimer.Fraction, curr_frame));
    }
}
