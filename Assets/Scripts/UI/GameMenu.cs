using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private float multiplier = 10f;
    [SerializeField] private float timeBetweenTextFlashing = 1f;

    [SerializeField] private Text starsCounterText;
    [SerializeField] private Text coinCounterText;
    [SerializeField] private GameObject boostUI;
    [SerializeField] private Image boostImage;
    [SerializeField] private Image progressBar;

    private float starsAmount = 0;
    private int coinAmount = 0;

    private int bonusMultiplier = 1;

    private Color baseColor;

    private void Awake()
    {
        StateBus.GameMenu = this;
    }

    private void Start()
    {
        baseColor = starsCounterText.color;
    }

    private void Update()
    {
        CountStars();
        if (StateBus.Coin_Add) AddCoin(StateBus.Coin_Add);
        if (StateBus.Boost.Value is Star) StartCoroutine(SetBonusMultiplier());
        if (StateBus.Boost.Value != null) StartCoroutine(BoostPanel(StateBus.Boost.Value));
    }

    public void GetValues(out float score, out int coins)
    {
        score = starsAmount;
        coins = coinAmount;
    }

    private void AddCoin(int value)
    {
        coinAmount += value * bonusMultiplier;
        coinCounterText.text = coinAmount.ToString();
    }

    private void CountStars()
    {
        if (StateBus.World_IsGameActive)
        {
            starsAmount += StateBus.World_DifficultyCoefficient * multiplier * Time.deltaTime * bonusMultiplier;
            starsCounterText.text = starsAmount.ToString("000000");
        }
    }

    private IEnumerator SetBonusMultiplier()
    {
        bonusMultiplier = 2;
        Coroutine blink = StartCoroutine(BlinkText());

        yield return new WaitForSeconds(StateBus.Player_Data.DoublePointsDuration);
        bonusMultiplier = 1;
        StopCoroutine(blink);
        starsCounterText.color = baseColor;
        coinCounterText.color = baseColor;
    }

    private IEnumerator BlinkText()
    {
        WaitForSeconds wait = new WaitForSeconds(timeBetweenTextFlashing);

        while (true)
        {
            starsCounterText.color = Color.yellow;
            coinCounterText.color = Color.yellow;
            yield return wait;
            starsCounterText.color = Color.cyan;
            coinCounterText.color = Color.cyan;
            yield return wait;
        }
    }

    private IEnumerator BoostPanel(InteractObject ioType)
    {
        boostImage.sprite = ioType.Icon;
        SetActiveBoostUI(true);

        float targetTime = BoostDuration(ioType);
        float time = 0;

        while (time <= targetTime)
        {
            progressBar.fillAmount = (targetTime - time) / targetTime;

            time += Time.deltaTime;
            yield return null;
        }
         SetActiveBoostUI(false);
    }

    private void SetActiveBoostUI(bool state)
    {
        boostUI.SetActive(state);
    }

    private float BoostDuration(InteractObject io)
    {
        if (io is Magnet) return StateBus.Player_Data.MagnetDuration;
        else if (io is Star) return StateBus.Player_Data.DoublePointsDuration;
        else if (io is JetPack) return StateBus.Player_Data.JetPackDuration;
        else throw new System.Exception("unknown type of IO");
    }
}
