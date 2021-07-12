using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public enum PickableItem { Weapon }
    [SerializeField] PickableItem itemType;
    [SerializeField] GameObject item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch (itemType)
            {
                case PickableItem.Weapon:
                    EquipWeapon();
                    Destroy(gameObject);
                    break;
            }
        }      
    }

    private void EquipWeapon()
    {
        Player.Instance.EquipWeapon(item.GetComponent<Weapon>());
    }
}
