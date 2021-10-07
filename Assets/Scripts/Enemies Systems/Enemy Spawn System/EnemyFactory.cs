using Object = UnityEngine.Object;

/// <summary>
/// This is the Enemy Factory Class, it uses the Factory Method Design Pattern.
/// The objetive of this class is to create the asked Enemy, no matter what type it is.
/// It uses an Scriptable Object named Enemy Configuration to keep control of all types of enemies in the project. 
/// Since the Enemy Factory don´t need to use Monobehaviour, you can call this class from the Service Locator.
/// </summary>

public class EnemyFactory
{
    private EnemyConfiguration enemyConfiguration;

    public EnemyFactory(EnemyConfiguration _enemyConfiguration)
    {
        enemyConfiguration = _enemyConfiguration;
    }

    public Enemy Create(string ID)
    {
        var enemy = enemyConfiguration.GetEnemyPrefabByID(ID);        
        return Object.Instantiate(enemy);
    }

}