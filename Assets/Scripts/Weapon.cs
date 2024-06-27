using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public ObjectPool bulletPool;
    public Transform muzzle;

    public int currentAmmo;
    public int maxAmmo;
    public bool infiniteAmmo;

    public float bulletSpeed;
    public float shootRate;
    private float lastShootTime;
    public bool isPlayer;

    public AudioClip shootSFX;
    public GameObject muzzleFlash;
    private AudioSource audioSource;


    void Awake()
    {
        if(GetComponent<Player>())
        {
            isPlayer = true;
        }

        audioSource = GetComponent<AudioSource>();

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
        lastShootTime = Time.time;
        currentAmmo -= 1;

        if(isPlayer ) 
            UIMenager.instance.UpdateAmmoText(currentAmmo, maxAmmo);

        audioSource.PlayOneShot(shootSFX);
        GameObject MF = Instantiate(muzzleFlash,muzzle.transform.position,transform.rotation*Quaternion.Euler(90,0,0));
        Destroy(MF, 0.05f);

        GameObject bullet = bulletPool.GetObject();

        bullet.transform.position = muzzle.position;
        bullet.transform.rotation = muzzle.rotation;

        bullet.GetComponent<Rigidbody>().velocity = muzzle.forward * bulletSpeed;
    }
}
