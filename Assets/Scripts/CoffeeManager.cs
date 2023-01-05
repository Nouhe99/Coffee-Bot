using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class CoffeeTypeSGColDictionaryItem
{
    public CoffeeType typeOfCoffee;
    public SugarCollider coffeePrefab;
}
public class CoffeeManager : MonoBehaviour
{
    public UnityEvent OnLoseLife;

    [SerializeField]
    private TMP_Text scoreTxt;
    [SerializeField]
    private TMP_Text HighscoreTxt;
    [SerializeField]
    private GameObject losePanel;

    public bool lose = false;

    public GameObject[] vies;

    [SerializeField]
    private Timer[] timers;

    [SerializeField]
    private SugarDisplay[] sugarDisplays;

    [SerializeField]
    private SpriteRenderer[] pnjs;

    [SerializeField]
    private SugarCollider[] CoffeeList;

    [SerializeField]
    private CoffeeTypeSGColDictionaryItem[] dictionaryItems;

    private Dictionary<CoffeeType,SugarCollider> coffeePrefabs = new Dictionary<CoffeeType, SugarCollider>();

    private int lives = 3;

    private int score = 0;
    private int scoreFromSugar = 10;
    private int scoreFromComplete = 50;

    [HideInInspector]
    public UnityEvent<int> OnLivesUpdate;
    [HideInInspector]
    public UnityEvent<int> OnScoreUpdate;

    private void Start()
    {
        HighscoreTxt.text = "High Score:" + PlayerPrefs.GetInt("HighScore",0);

        foreach (SugarCollider s in CoffeeList)
        {
            s.OnSugarError.AddListener(SugarError);
            s.OnSugarSuccess.AddListener(SugarSuccess);
            s.OnSugarCompleted.AddListener(SugarCompleted);
        }

        foreach(CoffeeTypeSGColDictionaryItem c in dictionaryItems)
        {
            coffeePrefabs.Add(c.typeOfCoffee, c.coffeePrefab);
        }

        InitCoffeeAndClient(0);
        InitCoffeeAndClient(1);
        InitCoffeeAndClient(2);
    }

    private void InitCoffeeAndClient(int id)
    {
        CoffeeType t = (CoffeeType)Random.Range(0, 4);

        SugarCollider newCoffee = Instantiate(coffeePrefabs[t],CoffeeList[id].transform.position, Quaternion.identity);
        newCoffee.id = id;

        newCoffee.sugarNeeded[SugarType.white] = Mathf.Min(Random.Range(1, Mathf.Max(newCoffee.maxSugarWhite, 1)), newCoffee.maxSugarWhite);
        newCoffee.sugarNeeded[SugarType.brown] = Mathf.Min(Random.Range(1, Mathf.Max(newCoffee.maxSugarBrown, 1)), newCoffee.maxSugarBrown);

        newCoffee.OnSugarError.AddListener(SugarError);
        newCoffee.OnSugarSuccess.AddListener(SugarSuccess);
        newCoffee.OnSugarCompleted.AddListener(SugarCompleted);
        //add timer,
        sugarDisplays[id].UpdateDisplay(newCoffee.sugarNeeded[SugarType.white], newCoffee.sugarNeeded[SugarType.brown]);

        timers[id].timerValue = 1;

        CoffeeList[id].KillCoffee();

        CoffeeList[id] = newCoffee;

        pnjs[id].sprite = newCoffee.spritesPerso[Random.Range(0, newCoffee.spritesPerso.Count - 1)];
    }

    public void SugarError(int id)
    {
        //Lose life and reset with new client and coffee, call onlivesupdate and check if dead
        InitCoffeeAndClient(id);
        lives--;
        Destroy(vies[lives]);

        CheckIfDead(lives);
        Debug.Log("lives = " + lives);

        OnLoseLife.Invoke();
    }

    public void SugarSuccess(int id)
    {
        score += scoreFromSugar;

        sugarDisplays[id].UpdateDisplay(CoffeeList[id].sugarNeeded[SugarType.white], CoffeeList[id].sugarNeeded[SugarType.brown]);

        scoreTxt.text = score + "€";
    }

    public void SugarCompleted(int id)
    {
        score += scoreFromComplete;
        InitCoffeeAndClient(id);
        scoreTxt.text = score + "€";

    }

    private void CheckIfDead(int l)
    {
        if(l <= 0 || lose)
        {

            losePanel.SetActive(true);
            //stop coffee and characterss from changing

            if(score > PlayerPrefs.GetInt("HighScore",0))
            {
                PlayerPrefs.SetInt("HighScore", score);
                HighscoreTxt.text = "High Score:" + score.ToString();

            }

        }
        else
        {
            OnLivesUpdate.Invoke(l);
            
        }
    }

   

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }
}
