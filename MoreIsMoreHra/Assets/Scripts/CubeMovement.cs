using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CubeMovement : MonoBehaviour
{
    private float vehicleSpeed = 20;
    public int cubeDmg { get; protected set; }
    private GameManager gameManager;
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");
        other.GetComponent<PlayerControllerBase>().playerHp -= cubeDmg;
        if(other.GetComponent<PlayerControllerBase>().playerHp < 0)
        {
            gameManager.GameOver();
            Debug.Log("GameOver");
        }
    }
}
