using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    [SerializeField]
    BoxCollider2D mapBounds;

    private float xMin, xMax, yMin, yMax;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 offset = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        cam = GetComponent<Camera>();
        xMin = mapBounds.bounds.min.x + offset.x;
        xMax = mapBounds.bounds.max.x - offset.x;
        yMin = mapBounds.bounds.min.y + offset.y;
        yMax = mapBounds.bounds.max.y - offset.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector3 point = cam.ScreenToWorldPoint(new Vector2(mousePos.x, mousePos.y));
        cam.transform.position = new Vector3(Mathf.Lerp(cam.transform.position.x, Mathf.Clamp(point.x, xMin, xMax),Time.deltaTime),Mathf.Lerp(cam.transform.position.y, Mathf.Clamp(point.y, yMin, yMax),Time.deltaTime),cam.transform.position.z);
    }
}
