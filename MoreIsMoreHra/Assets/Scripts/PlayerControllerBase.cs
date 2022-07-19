using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerBase : MonoBehaviour
{
    [SerializeField]
    private bool isSwappingLines = false;
    private float lineSwapSpeed = 75;
    private float[] linesX = { -9, 0, 9 };
    private int currentLine = 1;
    public int playerHp = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow) && !isSwappingLines && currentLine > 0)
        {
            isSwappingLines = true;
            currentLine -= 1;
            StartCoroutine(MoveLeft());
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && !isSwappingLines && currentLine < 2)
        {
            isSwappingLines = true;
            currentLine += 1;
            StartCoroutine(MoveRight());
        }
    }
    IEnumerator MoveLeft()
    {
        while(transform.position.x > linesX[currentLine])
        {
            transform.Translate(new Vector3(-1, 0, 0) * Time.deltaTime * lineSwapSpeed);
            yield return null;  
        }
        isSwappingLines = false;  
    }
    IEnumerator MoveRight()
    {
        while (transform.position.x < linesX[currentLine])
        {
            transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * lineSwapSpeed);
            yield return null;
        }
        isSwappingLines = false;
    }
    
}
