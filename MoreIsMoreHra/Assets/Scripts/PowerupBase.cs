using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerupBase : MonoBehaviour
{
    private float rotSpeed = 20;
    protected GameManager gameManager;
    private Transform poweruptransform;
    private bool isInBounds;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        poweruptransform = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        isInBounds = transform.position.z > -55;

        if (gameManager.isPowerupActive && isInBounds)
        {
            transform.Translate(Vector3.back * gameManager.gameSpeed * Time.deltaTime);
            //transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime);
        }

        if (!isInBounds)
        {
            gameManager.PowerupRendererControl(false, poweruptransform);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(Powerup());
            gameManager.PowerupRendererControl(false, poweruptransform);
        }
    }
    protected abstract IEnumerator Powerup();

}
