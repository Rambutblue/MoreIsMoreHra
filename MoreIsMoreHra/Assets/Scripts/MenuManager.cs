using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject selectCharacter;
    [SerializeField]
    private GameObject defaultMenu;
    [SerializeField]
    private GameObject charLock;
    [SerializeField]
    private GameObject charLockText;

    [SerializeField]
    private GameObject highscoreObj;

    [SerializeField]
    private GameObject[] characters;
    [SerializeField]
    private int currentCharacter;
    [SerializeField]
    private GameObject charName;
    [SerializeField]
    private GameObject charStats;
    [SerializeField]
    private string[] charNamesArr = { "Basic Farmer", "Advanced Farmer", "Construction Worker" };
    [SerializeField]
    private string[] charStatsArr = { "Has 3 HP", "Has 5 HP", "Has 10 HP" };
    [SerializeField]
    private int[] unlockScores = { 0, 100, 500 };

    private void Start()
    {
        highscoreObj.GetComponent<TextMeshProUGUI>().text = DataManager.instance.bestScore.ToString(); 
    }

    public void LeftArrow()
    {
        characters[currentCharacter].SetActive(false);  
        if (currentCharacter == 0)
        {
            currentCharacter = characters.Length - 1;
        }
        else
        {
            currentCharacter--;
        }
        ActivateCharacter();
    }
    public void RightArrow()
    {
        characters[currentCharacter].SetActive(false);
        if (currentCharacter >= characters.Length - 1)
        {
            currentCharacter = 0;
        }
        else
        {
            currentCharacter++;
        }
        ActivateCharacter();
    }
    private void ActivateCharacter()
    {
        characters[currentCharacter].SetActive(true);
        charName.GetComponent<TextMeshProUGUI>().text = charNamesArr[currentCharacter];
        charStats.GetComponent<TextMeshProUGUI>().text = charStatsArr[currentCharacter];
    }
    public void Select()
    {
        if (DataManager.instance.bestScore > unlockScores[currentCharacter])
        {
            DataManager.instance.charType = currentCharacter;
            selectCharacter.SetActive(false);
            defaultMenu.SetActive(true);
        }
        else
        {
            charLockText.GetComponent<TextMeshProUGUI>().text = "Get atleast " + unlockScores[currentCharacter] + " points to unlock this character";
            charLock.SetActive(true);
        }
    }
    public void ToCharSelect()
    {
        selectCharacter.SetActive(true);
        defaultMenu.SetActive(false);
    }
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void CloseLock()
    {
        charLock.SetActive(false);
    }
}
