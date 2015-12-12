using UnityEngine;
using System.Collections;

public class VideoXMLWrapper {
	private string _file;
	private int _fileSequence;
    private int _fps;

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

    public int FPS
    {
        get
        {
            return _fps;
        }
    }

	public VideoXMLWrapper(string _f, int _s, int _fs) {
		_file = _f;
		_fileSequence = _s;
        _fps = _fs;
	}
}
