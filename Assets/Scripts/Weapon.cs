using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform muzzle;

    public int currentAmmo;
    public int maxAmmo;
    public bool infiniteAmmo;

    public float bulletSpeed;
    public float shootRate;
    public float lastShootTime;
    public bool isPlayer;


    void Awake()
    {
        if(GetComponent<Player>())
        {
            isPlayer = true;
        }
    }

    public bool CanShoot()
    {
        if(Time.time - lastShootTime >= shootRate)
        {
            if(currentAmmo >0 || infiniteAmmo == true)
            {
                return true;
            }
        }

        return false; 
    }

    public void Shoot()
    {

    }
}
