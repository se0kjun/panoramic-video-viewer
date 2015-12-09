﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovieTimer : System.Object {
	private float time;
	private float minutes;
	private float seconds;
	private float fraction;

	public float TotalTime {
		get {
			return time;
		}
	}

	public float Minutes {
		get {
			return minutes;
		}
	}

	public float Seconds {
		get {
			return seconds;
		}
	}

	public float Fraction {
		get {
			return fraction;
		}
	}

	public MovieTimer(float _t) {
		time = _t;
		minutes = time / 60;
		seconds = time % 60;
		fraction = (time * 100) % 100;
	}

	public override bool Equals (object obj)
	{
		if(obj == null) {
			return false;
		}
		
		MovieTimer o = obj as MovieTimer;

		if(Mathf.Abs(o.TotalTime - this.TotalTime) > 0.0001f) {
			return true;
		} else {
			return false;
		}
	}

	public static MovieTimer operator+(MovieTimer a, float b) {
		return new MovieTimer(a.TotalTime + b);
	}

	public static MovieTimer operator-(MovieTimer a, MovieTimer b) {
		return new MovieTimer(Mathf.Abs (a.TotalTime - b.TotalTime));
	}
}
