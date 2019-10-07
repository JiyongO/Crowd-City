using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform targetTF;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - targetTF.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = targetTF.position + offset;
    }
}
