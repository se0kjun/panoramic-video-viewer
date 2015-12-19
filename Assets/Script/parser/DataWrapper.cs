using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataWrapper {
    private MarkerXMLWrapper _MarkerWrapper;
    public MarkerXMLWrapper MarkerWrapper
    {
        get
        {
            return _MarkerWrapper;
        }
    }

    public DataWrapper(int id)
    {
        _MarkerWrapper = new MarkerXMLWrapper(id);
    }
}
