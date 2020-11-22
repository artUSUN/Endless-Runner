using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Text scoreCounter;
    [SerializeField] private Text coinsCounter;
    [SerializeField] private PlayerPrefsHandler prefs;


    private void Start()
    {
        coinsCounter.text = prefs.Coins.ToString();
        scoreCounter.text = prefs.RecordScore.ToString("000000");
    }

    public void OnStart()
    {
        SceneManager.LoadScene(1);
    }
}
