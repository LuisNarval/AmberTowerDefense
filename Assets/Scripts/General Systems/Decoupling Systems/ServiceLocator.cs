using System;
using System.Collections.Generic;
using UnityEngine.Assertions;
/// <summary>
/// The Service Locator is an Decoupling Design Pattern that is quite useful.
/// The main purpose of this pattern is to serve as a telephone operator that connects all classes. 
/// If there is a service that is required, the clients won't have to refer to other classes, but will simply have to call 
/// the Service Locator to find it.
/// </summary>

public class ServiceLocator : Singleton<ServiceLocator>
{
    private IDictionary<object, object> Services;

    public override void Awake()
    {
        base.Awake();
        FillRegistry();
    }

    private void FillRegistry()
    {
        Services = new Dictionary<object, object>();
        Services.Add(typeof(SpawnSystem), new SpawnSystem());
        Services.Add(typeof(WaveSystem), new WaveSystem());
    }

    /*public void RegisterService<T>(T service)
    {
        var type = typeof(T);
        Assert.IsFalse(Services.ContainsKey(type), "Service already registered");
 
        Services.Add(type, service);
    }*/


    public T GetServices<T>()
    {
        try
        {
            return (T)Services[typeof(T)];
        }
        catch
        {
            throw new ApplicationException("The requested services is not found");
        }
    }

}
