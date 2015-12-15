using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MarkerWrapper {
	private string _videoId;
	private int _frameId;
	private int _position_X;
	private int _position_Y;
	private MovieTimer _timer;

	public MovieTimer M_Timer {
		get {
			return _timer;
		}
	}

	public int FrameID {
		get {
			return _frameId;
		}
	}
	
	public int PositionX {
		get {
			return _position_X;
		}
	}
	
	public int PositionY {
		get {
			return _position_Y;
		}
	}

	public string VideoID {
		get {
			return _videoId;
		}
	}

	public MarkerWrapper(string _v, int _f, int _x, int _y, List<VideoXMLWrapper> a) {
		_videoId = _v;
		_frameId = _f;
		_position_X = _x;
		_position_Y = _y;
        float seconds = Mathf.Floor(_frameId / a[int.Parse(_v)].FPS);
        _timer = new MovieTimer(seconds / 60, seconds % 60, 0);
	}
}

public class MarkerXMLWrapper {
	private int _markerId;
	private List<MarkerWrapper> _markerwrapper;

	public int MarkerID {
		get {
			return _markerId;
		}
	}

	public List<MarkerWrapper> TrackList {
		get {
			return _markerwrapper;
		}
	}

	public MarkerXMLWrapper(int _m) {
		_markerId = _m;
		_markerwrapper = new List<MarkerWrapper>();
	}
}
