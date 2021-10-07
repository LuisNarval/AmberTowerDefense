using Object = UnityEngine.Object;

/// <summary>
/// This is the Bullet Factory Class, it uses the Factory Method Design Pattern.
/// The objetive of this class is to create the asked Bullet, no matter what type it is.
/// It uses an Scriptable Object named Bullet Configuration to keep control of all types of bullets in the project. 
/// Since the Bullet Factory don´t need to use Monobehaviour, you can call this class from the Service Locator.
/// </summary>

public class BulletFactory
{
    private BulletConfiguration bulletConfiguration;

    public BulletFactory(BulletConfiguration _bulletConfiguration)
    {
        bulletConfiguration = _bulletConfiguration;
    }

    public Bullet Create(string ID)
    {
        var bullet = bulletConfiguration.GetBulletPrefabByID(ID);
        return Object.Instantiate(bullet);
    }

}
