using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PickupType
{
    Health, 
    Ammo 
}
public class Pickup : MonoBehaviour
{
    public PickupType type;
    public int value;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            switch(type)
            {
                case PickupType.Health:
                    player.Givehealth(value);
                    break;
                case PickupType.Ammo:
                    player.GiveAmmo(value);
                    break; 

            }
        }
    }
}
