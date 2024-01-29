using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    public void GetDamaged(int damage, Transform target);
}
