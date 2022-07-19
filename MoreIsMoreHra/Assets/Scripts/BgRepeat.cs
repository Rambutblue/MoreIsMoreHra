using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgRepeat : MonoBehaviour
{
    private Vector3 startPos;
    private float repeatLength;
    private float backgroundspeed = 20;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        repeatLength = GetComponent<Renderer>().bounds.size.z / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < startPos.z - repeatLength)
        {
            transform.position = startPos;
        }
        transform.Translate(Vector3.back * backgroundspeed * Time.deltaTime);
    }
}
