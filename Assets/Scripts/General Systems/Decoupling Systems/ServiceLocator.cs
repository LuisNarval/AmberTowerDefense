using System;
using System.Collections.Generic;
/// <summary>
/// The Service Locator is an Decoupling Design Pattern that is quite useful.
/// The main purpose of this pattern is to serve as a telephone operator that connects all classes that don´t inherit from Monobehaviour. 
/// If there is a service that is required, the clients won't have to refer to other classes, but will simply have to call 
/// the Service Locator to find it.
/// The services have to be registered before they can be used.
/// This service locator is implemented as an Monostate Class.
/// </summary>

public class ServiceLocator
{
    private static readonly IDictionary<Type, object> Services = new Dictionary<Type, Object>();

    public static void RegisterService<T>(T service)
    {
        Services[typeof(T)] = service;
    }

    public static T GetService<T>()
    {
        try
        {
            return (T)Services[typeof(T)];
        }
        catch
        {
            throw new ApplicationException ("Requested service not found.");
        }
    }
}