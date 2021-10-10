using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmTower : Tower
{
    [SerializeField] public Vector2 moneyRate;
    [SerializeField] public Vector2 farmTimeRate;
    [SerializeField] public float turnSpeed;
    [SerializeField] public Transform chestPivot;
    [SerializeField] public Image farmBar;
    [SerializeField] public Text moneyText;
    [SerializeField] public Transform coin;


    private FarmMoneyState FarmState;

    public override void SetInitialState()
    {
        CreateStateComponents();
        Farm();
    }

    public override void ChangeState(ITowerState _towerState)
    {
        if (currentState != null)
        {
            currentState.DisHandle();
        }

        currentState = _towerState;
        currentState.Handle(this);
    }

    private void CreateStateComponents()
    {
        FarmState = gameObject.AddComponent<FarmMoneyState>();
    }

    private void Farm()
    {
        ChangeState(FarmState);
    }

}