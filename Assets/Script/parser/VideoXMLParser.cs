using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class VideoXMLParser {
    private static readonly VideoXMLParser _instance = new VideoXMLParser();
	private XmlDocument _XMLDocument;
    public Dictionary<string, VideoXMLWrapper> _video_wrapper;

    public static VideoXMLParser Instance
    {
        get
        {
            if (_instance != null)
                return _instance;
            else
                return null;
        }
    }

	private VideoXMLParser() {
        _video_wrapper = new Dictionary<string, VideoXMLWrapper>();
        _XMLDocument = new XmlDocument();
	}

    public void LoadXML(string fileName)
    {
        _XMLDocument.Load(fileName);
    }

    public List<DataWrapper> ParseXML() {
        ParseVideoXML();
        List<DataWrapper> result = new List<DataWrapper>();
		XmlNodeList marker_node = _XMLDocument.SelectNodes("/data/markers/marker");

		foreach(XmlNode marker in marker_node) {
			int markerId = int.Parse(marker.Attributes.GetNamedItem("id").Value);
			XmlNodeList track_list = marker.ChildNodes;
            DataWrapper tmp = new DataWrapper(markerId);
            foreach (XmlNode track in track_list) {
				string video_id = track.Attributes.GetNamedItem("video").Value;
				int pos_x = (int)float.Parse(track.Attributes.GetNamedItem("position_x").Value.ToString());
				int pos_y = (int)float.Parse(track.Attributes.GetNamedItem("position_y").Value.ToString());
				int frame_id = int.Parse(track.Attributes.GetNamedItem("frame").Value.ToString());
                VideoXMLWrapper vwrap;
                _video_wrapper.TryGetValue(video_id, out vwrap);
				tmp.MarkerWrapper.TrackList.Add (new MarkerWrapper(
					frame_id, pos_x, pos_y, vwrap
					));
			}
            result.Add(tmp);
        }

        return result;
	}

    private void ParseVideoXML()
    {
        XmlNodeList video_node = _XMLDocument.SelectNodes("/data/videos/video");

        foreach (XmlNode video in video_node)
        {
            VideoXMLWrapper tmp = new VideoXMLWrapper(
                video.InnerText,
                int.Parse(video.Attributes.GetNamedItem("seq").Value),
                int.Parse(video.Attributes.GetNamedItem("frame").Value),
                int.Parse(video.Attributes.GetNamedItem("height").Value),
                int.Parse(video.Attributes.GetNamedItem("width").Value)
                );
            tmp.ObjectPlane = GameObject.Find("Plane (" + video.Attributes.GetNamedItem("seq").Value + ")");
            _video_wrapper.Add(video.Attributes.GetNamedItem("seq").Value, tmp);
        }
    }
}
