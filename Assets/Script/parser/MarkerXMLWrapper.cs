using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MarkerWrapper {
	private int _frameId;
	private int _position_X;
	private int _position_Y;
    private VideoXMLWrapper _videoInstance;

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

	public VideoXMLWrapper VideoInstance {
		get {
			return _videoInstance;
		}
	}

	public MarkerWrapper(int _f, int _x, int _y, VideoXMLWrapper _v) {
		_frameId = _f;
		_position_X = _x;
		_position_Y = _y;
        _videoInstance = _v;
	}
}

public class MarkerXMLWrapper {
	private int _markerId;
	private List<MarkerWrapper> _markerwrapper;

    public int MarkerID {
        get {
            return _markerId;
        }
        set
        {
            _markerId = value;
        }
	}

	public List<MarkerWrapper> TrackList {
		get {
			return _markerwrapper;
		}
	}

    public int StartFrame
    {
        get
        {
            return _markerwrapper[0].FrameID;
        }
    }

    public int EndFrame
    {
        get
        {
            if (_markerwrapper.Count != 0)
                return _markerwrapper[_markerwrapper.Count - 1].FrameID;
            else
                return 0;
        }
    }

    public MarkerXMLWrapper()
    {
        _markerwrapper = new List<MarkerWrapper>();
    }

	public MarkerXMLWrapper(int _m) {
		_markerId = _m;
		_markerwrapper = new List<MarkerWrapper>();
	}
}
