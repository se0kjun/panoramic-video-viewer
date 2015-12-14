using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class VideoXMLParser {
	private XmlDocument _XMLDocument;
	private List<VideoXMLWrapper> _VideoWrapper;
	private List<MarkerXMLWrapper> _MarkerWrapper;
	private int _count;

	public List<VideoXMLWrapper> VideoList {
		get {
			return _VideoWrapper;
		}
	}

	public List<MarkerXMLWrapper> MarkerList {
		get {
			return _MarkerWrapper;
		}
	}

	public VideoXMLParser(string xml_file) {
		_VideoWrapper = new List<VideoXMLWrapper>();
		_MarkerWrapper = new List<MarkerXMLWrapper>();
		_XMLDocument = new XmlDocument();
		_XMLDocument.Load(xml_file);
	}

	public void ParseXML() {
		XmlNodeList video_node = _XMLDocument.SelectNodes("/data/videos/video");
		XmlNodeList marker_node = _XMLDocument.SelectNodes("/data/markers/marker");

		foreach(XmlNode video in video_node) {
            VideoXMLWrapper tmp = new VideoXMLWrapper(
                video.InnerText,
                int.Parse(video.Attributes.GetNamedItem("seq").Value),
                int.Parse(video.Attributes.GetNamedItem("frame").Value),
                int.Parse(video.Attributes.GetNamedItem("height").Value),
                int.Parse(video.Attributes.GetNamedItem("width").Value)
				);
			_VideoWrapper.Add (tmp);
		}

		foreach(XmlNode marker in marker_node) {
			int markerId = int.Parse(marker.Attributes.GetNamedItem("id").Value);
			XmlNodeList track_list = marker.ChildNodes;
			foreach(XmlNode track in track_list) {
				MarkerXMLWrapper tmp = new MarkerXMLWrapper(markerId);
				string video_id = track.Attributes.GetNamedItem("video").Value;
				int pos_x = (int)float.Parse(track.Attributes.GetNamedItem("position_x").Value.ToString());
				int pos_y = (int)float.Parse(track.Attributes.GetNamedItem("position_y").Value.ToString());
				int frame_id = int.Parse(track.Attributes.GetNamedItem("frame").Value.ToString());
				tmp.TrackList.Add (new MarkerWrapper(
					video_id, frame_id, pos_x, pos_y, _VideoWrapper
					));
			}
		}
	}
}
