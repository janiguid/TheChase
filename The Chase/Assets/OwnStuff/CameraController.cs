using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float centerpoint;

    // Start is called before the first frame update
    void Start()
    {
        centerpoint = Screen.width / 2;    
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mp = Input.mousePosition;
        float diff = mp.x - centerpoint;

        transform.Rotate(Vector3.up, diff * Time.deltaTime);
    }
}
