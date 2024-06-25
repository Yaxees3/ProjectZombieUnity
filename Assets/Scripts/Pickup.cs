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

    [Header("Moving")]
    public float rotateSpeed; 
    public float bobSpeed;
    public float bobHeight;

    private Vector3 startPosition;
    private bool bobbingUp;


    private void Start()
    {
        startPosition = transform.position; 
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);

        Vector3 offset = bobbingUp == true ? new Vector3(0, bobHeight / 2, 0) : new Vector3(0, -bobHeight / 2, 0);

        transform.position = Vector3.MoveTowards(transform.position,startPosition+offset,bobSpeed*Time.deltaTime);  

        if (transform.position ==startPosition+offset )
            bobbingUp = !bobbingUp;

    }
    private void OnTriggerEnter(Collider other)
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

            Destroy(gameObject);
        }
    }
}
