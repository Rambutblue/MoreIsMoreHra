using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CubeMovement : MonoBehaviour
{
    protected int cubeDmg = 1;
    private GameManager gameManager;
    private bool isGameManager;
    
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (gameObject.activeInHierarchy)
        {
            transform.Translate(Vector3.back * gameManager.gameSpeed * Time.deltaTime);
        }
        
        if (transform.position.z < -55)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameManager.playerDmg = cubeDmg;
            gameManager.PlayerHit();
        }
    }

}
