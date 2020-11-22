using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEndMenu : MonoBehaviour
{
    [SerializeField] private float valueLerpDuration = 1;
    [SerializeField] private float timeBetweenTextFlashing = 0.2f;
    [SerializeField] private Text scoreCounter;
    [SerializeField] private Text coinCounter;
    [SerializeField] private Transform buttons;
    [SerializeField] private Transform newRecord;

    private bool isNewRecord; 
    private int coins, score;

    private void OnEnable()
    {
        StateBus.GameMenu.GetValues(out float floatScore, out coins);
        score = Mathf.FloorToInt(floatScore);
        StateBus.Prefs.AddNewValues(coins, score, out isNewRecord);
        StartCoroutine(EndActions());
    }

    public void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnHome()
    {
        SceneManager.LoadScene(0);
    }

    private IEnumerator EndActions()
    {
        StartCoroutine(LerpFromZeroToValues(scoreCounter, score, valueLerpDuration));
        yield return StartCoroutine(LerpFromZeroToValues(coinCounter, coins, valueLerpDuration));
        if (isNewRecord) EnableNewRecordFade();
        yield return new WaitForSeconds(1f);
        buttons.gameObject.SetActive(true);
    }

    private IEnumerator LerpFromZeroToValues(Text text, int maxValue, float duration)
    {
        float timer = 0;
        while (timer < duration)
        {
            float curStepValue = Mathf.Lerp(0, maxValue, timer/duration);
            text.text = Mathf.FloorToInt(curStepValue).ToString();
            timer += Time.deltaTime;
            yield return null;
        }
    }

    private void EnableNewRecordFade()
    {
        newRecord.gameObject.SetActive(true);
        StartCoroutine(BlinkText());
    }

    private IEnumerator BlinkText()
    {
        WaitForSeconds wait = new WaitForSeconds(timeBetweenTextFlashing);

        for (int i = 0; i < 3; i++)
        {
            scoreCounter.color = Color.yellow;
            yield return wait;
            scoreCounter.color = Color.cyan;
            yield return wait;
        }
    }
}
