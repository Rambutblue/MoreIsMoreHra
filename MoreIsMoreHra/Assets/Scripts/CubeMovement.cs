using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    private float vehicleSpeed = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            transform.Translate(Vector3.back * vehicleSpeed * Time.deltaTime);
        }
        
        if (transform.position.z < -55)
        {
            gameObject.SetActive(false);
        }
    }
}
