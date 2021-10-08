public class Bank
{
    int money;
    EconomySystem economySystem;

    public Bank(int _initialMoney, EconomySystem _economySystem)
    {
        money = _initialMoney;
        economySystem = _economySystem;
    }

    public int GetMoney()
    {
        return money;
    }

    public void AddMoney(int _amount)
    {
        money += _amount;
        economySystem.UpdateUI(_amount);
    }

    public void RetireMoney(int _amount)
    {
        if (CanRetire(_amount))
        {
            money -= _amount;
            economySystem.UpdateUI(_amount*-1);
        }
    }

    public bool CanRetire(int _amount)
    {
        if (money >= _amount)
            return true;
        else
            return false;
    }
}
