using Object = UnityEngine.Object;

/// <summary>
/// This is the tOWER Factory Class, it uses the Factory Method Design Pattern.
/// The objetive of this class is to create the asked tOWER, no matter what type it is.
/// It uses an Scriptable Object named Tower Configuration to keep control of all types of towers in the project. 
/// Since the Tower Factory don´t need to use Monobehaviour, you can call this class from the Service Locator.
/// </summary>

public class TowerFactory
{
    private TowerConfiguration towerConfiguration;

    public TowerFactory(TowerConfiguration _towerConfiguration)
    {
        towerConfiguration = _towerConfiguration;
    }

    public Tower Create(string ID)
    {
        var tower = towerConfiguration.GetTowerPrefabByID(ID);
        return Object.Instantiate(tower);
    }

}