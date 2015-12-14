using UnityEngine;
using System.Collections;

public class VideoXMLWrapper {
	private string _file;
	private int _fileSequence;
    private int _height;
    private int _width;
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

    public int Height
    {
        get
        {
            return _height;
        }
    }

    public int Width
    {
        get
        {
            return _width;
        }
    }

	public VideoXMLWrapper(string _f, int _s, int _fs, int _h, int _w) {
		_file = _f;
		_fileSequence = _s;
        _fps = _fs;
        _height = _h;
        _width = _w;
	}
}
