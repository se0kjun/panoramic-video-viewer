using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataWrapper {
    public enum TrackState
    {
        NOTRACK,
        START,
        TRACKING,
        END
    };

    private MarkerXMLWrapper _MarkerWrapper;
    private List<VideoXMLWrapper> _VideoWrapper;
    private GameObject _VideoPlane;
    private TrackState _trackingState;

    public GameObject VideoPlane
    {
        get
        {
            return _VideoPlane;
        }
        set
        {
            _VideoPlane = value;
        }
    }

    public MarkerXMLWrapper MarkerWrapper
    {
        get
        {
            return _MarkerWrapper;
        }
    }

    public List<VideoXMLWrapper> VideoWrapper
    {
        get
        {
            return _VideoWrapper;
        }
        set
        {
            _VideoWrapper = value;
        }
    }

    public TrackState TrackingState
    {
        get
        {
            return _trackingState;
        }
        set
        {
            _trackingState = value;
        }
    }

    public DataWrapper(int id)
    {
        _trackingState = TrackState.NOTRACK;
        _MarkerWrapper = new MarkerXMLWrapper(id);
        _VideoWrapper = new List<VideoXMLWrapper>();
    }
}
