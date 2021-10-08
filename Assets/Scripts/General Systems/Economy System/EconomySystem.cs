using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EconomySystem : MonoBehaviour
{
    [SerializeField] private int initialMoney = 10;
    [SerializeField] private Text txtMoney;
    [SerializeField] private Text changeMessage;
    [SerializeField] private Image economyBar;
    [SerializeField] private Vector2 timeRate;
    [SerializeField] private Vector2 moneyRate;

    public void Start()
    {
        EventBus.Subscribe(GameEvent.STARTGAME, Init);
        EventBus.Subscribe(GameEvent.BASEDESTROYED, Stop);
        EventBus.Subscribe(GameEvent.GAMEWINNED, Stop);

        CreateBank();
    }

    void CreateBank()
    {
        Bank bank = new Bank(initialMoney, this);
        ServiceLocator.RegisterService(bank);
    }

    void Init()
    {
        txtMoney.text = initialMoney.ToString();
        StartCoroutine("GenerateMoney");
    }

    void Stop()
    {
        StopAllCoroutines();
    }

    IEnumerator GenerateMoney()
    {
        while (true)
        {
            yield return StartCoroutine(FillEconomyBar());
            AddMoneyToBank();
        }

    }

    IEnumerator FillEconomyBar()
    {
        float timeToWait = GetWaitingTime();
        float time = 0;

        while (time < timeToWait)
        {
            economyBar.fillAmount = Mathf.Lerp(0,1, time/ timeToWait);
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }



    float GetWaitingTime()
    {
        return Random.Range(timeRate.x, timeRate.y);
    }

    void AddMoneyToBank()
    {
        int amount = (int) Random.Range(moneyRate.x, moneyRate.y);
        ServiceLocator.GetService<Bank>().AddMoney(amount);
    }



    public void UpdateUI(int _money)
    {
        txtMoney.text = ServiceLocator.GetService<Bank>().GetMoney().ToString();
        Color color = _money > 0 ? Color.green : Color.red;

        StartCoroutine(ShowChange(_money, color));
    }

    IEnumerator ShowChange(int _money, Color _color)
    {
        changeMessage.text = _money > 0 ? "+" +_money.ToString(): _money.ToString();
        changeMessage.color = _color;

        yield return new WaitForSeconds(1.0f);

        changeMessage.text = "";
    }


}