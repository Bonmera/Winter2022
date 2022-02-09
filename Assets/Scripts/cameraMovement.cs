using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    [SerializeField]
    BoxCollider2D mapBounds;
    [SerializeField]
    GameObject targetObject;
    [SerializeField]
    float trackingSpeed = 1;

    private float xMin, xMax, yMin, yMax;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        Vector3 offset = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.transform.position.z));
        xMin = mapBounds.bounds.min.x + offset.x;
        xMax = mapBounds.bounds.max.x - offset.x;
        yMin = mapBounds.bounds.min.y + offset.y;
        yMax = mapBounds.bounds.max.y - offset.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 point = targetObject.transform.position;
        //Debug.Log(yMin+"  "+yMax);
        //Debug.Log(cam.transform.position);
        cam.transform.position = new Vector3(Mathf.Lerp(cam.transform.position.x, Mathf.Clamp(point.x, xMin, xMax),Time.deltaTime*trackingSpeed),Mathf.Lerp(cam.transform.position.y, Mathf.Clamp(point.y, yMin, yMax),Time.deltaTime*trackingSpeed),cam.transform.position.z);
    }
}
