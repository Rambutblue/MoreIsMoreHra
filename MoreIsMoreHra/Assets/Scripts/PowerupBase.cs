using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerupBase : MonoBehaviour
{
    private float rotSpeed = 20;
    protected GameManager gameManager;
    private Transform poweruptransform;
    private Renderer powerupsRenderer;
    private bool isInBounds;
    protected bool isPowerupBoostActive = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        poweruptransform = gameObject.GetComponent<Transform>();
        //powerupsRenderer = poweruptransform.GetChild(0).GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        isInBounds = transform.position.z > -55;

        if (isInBounds)
        {
            transform.Translate(Vector3.back * gameManager.gameSpeed * Time.deltaTime, Space.World);
            transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime);
        }

        if (!isInBounds)
        {
            gameManager.PowerupRendererControl(false, poweruptransform);
            gameManager.isPowerupActive = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !isPowerupBoostActive)
        {
            gameManager.PowerupRendererControl(false, poweruptransform);
            isPowerupBoostActive = true;
            Powerup();
        }
    }
    protected abstract void Powerup();

}
