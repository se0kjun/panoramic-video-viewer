﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour {
	public string XMLFileName;

	[HideInInspector]
	public VideoXMLParser parser;

	public List<GameObject> cube_plane;
	private List<MovieTexture> cube_movie;

	[HideInInspector]
	public static MovieTimer GlobalMovieTimer;

	void Start () {
        cube_movie = new List<MovieTexture>();
        GlobalMovieTimer = new MovieTimer(0.0f);

		if(XMLFileName != null) {
			parser = new VideoXMLParser(XMLFileName);
			parser.ParseXML();
		}
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

        foreach (MarkerXMLWrapper a in parser.MarkerList)
        {
            foreach (MarkerWrapper b in a.TrackList)
            {
                
                if (b.M_Timer == GlobalMovieTimer)
                {
                }
            }
        }
        Debug.Log (string.Format("{0:00} : {1:00} : {2:000}", GlobalMovieTimer.Minutes, GlobalMovieTimer.Seconds, GlobalMovieTimer.Fraction));
	}
}
