using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmMoneyState : MonoBehaviour, ITowerState
{
    FarmTower tower;
    public void Handle(FarmTower _tower)
    {
        tower = _tower;
        StartCoroutine(Turn());
        StartCoroutine(GenerateMoney());
    }
    public void Handle(AtackTower _tower){}

    public void DisHandle()
    {
        StopAllCoroutines();
    }

    IEnumerator Turn()
    {
        while (true)
        {
            tower.chestPivot.Rotate(Vector3.up * Time.deltaTime * tower.turnSpeed);
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator GenerateMoney()
    {
        while (true)
        {
            yield return StartCoroutine(Farming());
            AddMoneyToBank();
        }

    }

    IEnumerator Farming()
    {
        float timeToWait = Random.Range(tower.farmTimeRate.x, tower.farmTimeRate.y);
        float time = 0;

        while (time < timeToWait)
        {
            tower.farmBar.fillAmount = Mathf.Lerp(0, 1, time / timeToWait);
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    void AddMoneyToBank()
    {
        int money = (int)Random.Range(tower.moneyRate.x, tower.moneyRate.y);
        ServiceLocator.GetService<Bank>().AddMoney(money);
        StartCoroutine(ShowHint(money));
    }

    IEnumerator ShowHint(int _money)
    {
        tower.moneyText.text ="+"+_money.ToString();
        float time = 0.0f;

        while (time <1.0f)
        {
            tower.coin.transform.Rotate(Vector3.down*Time.deltaTime*150.0f);
            tower.coin.transform.localPosition = Vector3.Lerp(Vector3.zero, Vector3.up*2f, time);
            time += Time.deltaTime*5;
            yield return new WaitForEndOfFrame();
        }

        time = 0.0f;

        while (time < 2.0f)
        {
            tower.coin.transform.Rotate(Vector3.down*Time.deltaTime *150.0f);
            time += Time.deltaTime;
                        yield return new WaitForEndOfFrame();
        }

        tower.moneyText.text = "";
        tower.coin.transform.localPosition = Vector3.zero;
    }

}