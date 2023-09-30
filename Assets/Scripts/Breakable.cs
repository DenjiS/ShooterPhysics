using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : BaseHealth
{
    public override void Die()
    {
        Destroy(gameObject);
    }
}
