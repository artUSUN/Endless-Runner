using UnityEngine;

public class PlayerPrefsHandler : MonoBehaviour
{
    private int coins = 0;
    private int recordScore = 0;

    public int Coins => coins;
    public int RecordScore => recordScore;


    private void Awake()
    {
        StateBus.Prefs = this;
        Load();
    }


    public void AddNewValues(int coins, int score, out bool isItNewRecord)
    {
        this.coins += coins;
        if (score > recordScore)
        {
            isItNewRecord = true;
            recordScore = score;
        }
        else isItNewRecord = false;
        Save();
    }

    private void Load()
    {
        recordScore = PlayerPrefs.GetInt("RecordScore", 0);
        coins = PlayerPrefs.GetInt("CoinsCount", 0);
    }

    private void Save()
    {
        PlayerPrefs.SetInt("RecordScore", recordScore);
        PlayerPrefs.SetInt("CoinsCount", coins);
        PlayerPrefs.Save();
    }   
}
