using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script for the Dragon Tower Type
/// This is mplementing the State Desing Pattern, each behaviour State is declared in other class that implemente the ITowerState
/// interface. In runtime this components are atached to the tower & are used depending of the context.
/// </summary>
public class DragonTower : AtackTower
{
    private AtackTowerState AtackState;
    private SearchTowerState SearchState;
    public override void CreateStateComponents()
    {
        IAtackState = AtackState;
        ISearchState = SearchState;
        IAtackState = gameObject.AddComponent<AtackTowerState>();
        ISearchState = gameObject.AddComponent<SearchTowerState>();
    }
}