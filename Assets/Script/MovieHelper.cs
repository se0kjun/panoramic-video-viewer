using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovieHelper : MonoBehaviour {
	private Dictionary<int, MarkerWrapper> data;

	public Dictionary<int, MarkerWrapper> Data {
		get {
			return data;
		}
	}

	// Use this for initialization
	void Start () {
		data = new Dictionary<int, MarkerWrapper>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
