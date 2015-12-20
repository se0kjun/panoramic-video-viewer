using UnityEngine;
using System.Collections;

public class CheckRenderObject : MonoBehaviour {
    void OnBecameVisible()
    {
        Debug.Log("testsetsetsetset");
    }

    //void OnBecameInvisible()
    //{
    //    Debug.Log("eeeee" + Camera.current.name);
    //}
    void OnWillRenderObject()
    {
        GameManager.outside_camera_object.Remove(gameObject.name);

        //if (Camera.current.name == "CenterEyeAnchor")
        //{
        //    GameManager.outside_camera_object.Remove(gameObject.name);
        //}
        //else
        //{
        //    //if(GameManager.outside_camera_object.ContainsKey(gameObject.name))
        //    GameManager.outside_camera_object.Add(gameObject.name, gameObject);
        //}
    }
}
