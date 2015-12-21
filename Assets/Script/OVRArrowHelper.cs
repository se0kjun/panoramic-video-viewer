using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OVRArrowHelper : MonoBehaviour {
    public enum OutsideObjectType
    {
        LEFT,
        RIGHT,
        NONE
    };
    public string TagName;
    public Camera cam;
    public float FloatingDistance;
    public Sprite ArrowSprite;
    public Material ArrowMaterial;

    private GameObject[] TagNameObject;
    private SpriteRenderer LeftArrow;
    private SpriteRenderer RightArrow;

    void Start () {
        TagNameObject = GameObject.FindGameObjectsWithTag(TagName);
        LeftArrow = GameObject.Find("LEFT").GetComponent<SpriteRenderer>();
        RightArrow = GameObject.Find("RIGHT").GetComponent<SpriteRenderer>();
        FloatingDistance = 1f;
        ValidObjectArrow();
    }
	
	void Update () {
        TagNameObject = GameObject.FindGameObjectsWithTag(TagName);
        ValidObjectArrow();
        EdgeObjectArrow();
    }

    private void ValidObjectArrow()
    {
        foreach(GameObject arrow in TagNameObject)
        {
            if (arrow.transform.FindChild("arrow") != null)
                continue;
            GameObject arrow_object = new GameObject();
            arrow_object.name = "arrow";
            arrow_object.AddComponent<SpriteRenderer>().sprite = ArrowSprite;
            arrow_object.GetComponent<SpriteRenderer>().material = ArrowMaterial;
            arrow_object.transform.parent = arrow.transform;
            arrow_object.transform.localScale = new Vector3(.3f, .3f, .3f);
            //hardcoding
            if (arrow.transform.parent.name == "Plane (1)" || arrow.transform.parent.name == "Plane (3)")
                arrow_object.transform.localEulerAngles = new Vector3(0f, 90f, 270f);
            else
                arrow_object.transform.localEulerAngles = new Vector3(0f, 0f, 270f);
            arrow_object.transform.localPosition = new Vector3(0f, FloatingDistance, 0f);
        }
    }

    private void EdgeObjectArrow()
    {
        List<OutsideObjectType> side_object = new List<OutsideObjectType>();

        foreach (string key in GameManager.outside_camera_object.Keys)
        {
            GameObject value;
            if(GameManager.outside_camera_object.TryGetValue(key, out value))
                side_object.Add(CheckObjectDirection(value.transform.position));
        }

        if(side_object.Count == 0)
        {
            LeftArrow.enabled = false;
            RightArrow.enabled = false;
        }
        else
        {
            int left_obj = side_object.FindAll(item => item == OutsideObjectType.LEFT).Count;
            int right_obj = side_object.FindAll(item => item == OutsideObjectType.RIGHT).Count;
            if(left_obj > 0 && left_obj > right_obj)
            {
                RightArrow.enabled = false;
                LeftArrow.enabled = true;
            }
            else if(right_obj > 0 && right_obj > left_obj)
            {
                LeftArrow.enabled = false;
                RightArrow.enabled = true;
            }
        }
    }

    private OutsideObjectType CheckObjectDirection(Vector3 object_pos)
    {
        Vector3 rel_point = Camera.main.transform.InverseTransformPoint(object_pos);

        if (rel_point.x < 0)
            return OutsideObjectType.LEFT;
        else if (rel_point.x > 0)
            return OutsideObjectType.RIGHT;
        else
            return OutsideObjectType.NONE;
    }
}
