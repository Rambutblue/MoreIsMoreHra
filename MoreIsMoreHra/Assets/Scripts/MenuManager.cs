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
    private GameObject[] characters;
    [SerializeField]
    private int currentCharacter;
    [SerializeField]
    private GameObject charName;
    [SerializeField]
    private GameObject charStats;
    private string[] charNamesArr = { "Basic Farmer", "Advanced Farmer", "Construction Worker" };
    private string[] charStatsArr = { "Has 3 HP", "Has 5 HP", "Has 10 HP" };


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
        DataManager.instance.charType = currentCharacter;
        selectCharacter.SetActive(false);
        defaultMenu.SetActive(true);
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
}
