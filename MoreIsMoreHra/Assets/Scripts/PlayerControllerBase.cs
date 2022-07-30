using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class PlayerControllerBase : MonoBehaviour
{
    [SerializeField]
    private bool isSwappingLines = false;
    private float lineSwapSpeed = 75;
    private float[] linesX = { -9, 0, 9 };
    private int currentLine = 1;
    protected GameManager gameManager;
    private Animator animator;
    private Button leftButton;
    private Button rightButton;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        SetPlayerHp();
        animator = GetComponent<Animator>();
        animator.SetFloat("Speed_f", 1);
#if UNITY_ANDROID
        GameObject.Find("MobileControls").SetActive(true);
        leftButton = GameObject.Find("leftButton").GetComponent<Button>();
        rightButton = GameObject.Find("rightButton").GetComponent<Button>();
        leftButton.onClick.AddListener(MobileMoveLeft);
        rightButton.onClick.AddListener(MobileMoveRight);
#endif
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
    void MobileMoveLeft()
    {
        if (!isSwappingLines && currentLine > 0)
        {
            isSwappingLines = true;
            currentLine -= 1;
            StartCoroutine(MoveLeft());
        }
    }
    void MobileMoveRight()
    {
        if (!isSwappingLines && currentLine < 2)
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
    
    protected abstract void SetPlayerHp();
    
    
}
