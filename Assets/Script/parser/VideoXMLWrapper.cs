using UnityEngine;
using System.Collections;

public class VideoXMLWrapper {
	private string _file;
	private int _fileSequence;

	public string VideoFile {
		get {
			return _file;
		}
	}

	public int FileSequence {
		get {
			return _fileSequence;
		}
	}

	public VideoXMLWrapper(string _f, int _s) {
		_file = _f;
		_fileSequence = _s;
	}
}
