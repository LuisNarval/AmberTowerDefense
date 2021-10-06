using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This Class is the implementation of the Arrow Bullet
/// </summary>

public class ArrowBullet : Bullet
{
    public override void Move()
    {
        Aim();
        Fly();
    }
    public override void Aim()
    {
        this.transform.LookAt(target);
    }

    public override void Fly()
    {
        this.transform.Translate(transform.forward * Time.deltaTime * speed);
    }

}