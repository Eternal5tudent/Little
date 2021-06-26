using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected D_Weapon weaponData;
    protected int attackNum = 0;
    bool timeToReset = false;
    protected LayerMask whatIsDamageable;

    public virtual void Initialize(LayerMask whatIsDamageable)
    {
        this.whatIsDamageable = whatIsDamageable;
    }

    protected virtual void Start()
    {
        attackNum = 0;
        timeToReset = false;
    }
    public virtual void Attack()
    {
        StopCoroutine(StartReseting_Cor());
        StartCoroutine(StartReseting_Cor());
    }

    public virtual void Update()
    {
        if(timeToReset)
        {
            Unequip();
        }
    }

    public virtual void Unequip()
    {
        attackNum = 0;
        timeToReset = false;
    }

    IEnumerator StartReseting_Cor()
    {
        timeToReset = false;
        yield return new WaitForSeconds(weaponData.resetAfterTime);
        timeToReset = true;
    }
}
