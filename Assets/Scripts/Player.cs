using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Stars")]
    public int currentHP;
    public int maxHP;

    [Header("Movement")]
    public float moveSpeed;
    public float jumpForce;

    [Header("Camera")]
    public float lookSensitivity;
    public float maxLookX;
    public float minLookX;
    private float rotX;

    private Camera cam;
    private Rigidbody rig;
    private Weapon weapon;
    public CharacterController controller;

    public Image image;
    public float flashSpeed;
    private Coroutine fadeAway;
    private AudioSource audioSource;
    public AudioClip playerHurtSFX;


    private void Awake()
    {
        cam = Camera.main;
        rig = GetComponent<Rigidbody>();
        weapon = GetComponent<Weapon>();
        audioSource = GetComponent<AudioSource>();


        Cursor.lockState = CursorLockMode.Locked; 
    }

    private void Start()
    {
        UIMenager.instance.UpdateHEalthBar(currentHP, maxHP);
        UIMenager.instance.UpdateScoreText(0);
        UIMenager.instance.UpdateAmmoText(weapon.currentAmmo, weapon.maxAmmo);
    }

    private void Update()
    {
        if (GameManager.instance.gamePaused == true)
            return;


        Move();
        if (Input.GetButtonDown("Jump"))
             TryJump();
        if(Input.GetButton("Fire1"))
        {
            if (weapon.CanShoot())
                weapon.Shoot();
        }
        Cameralook();
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal") * moveSpeed;
        float z = Input.GetAxis("Vertical") * moveSpeed;

        Vector3 dir = transform.right * x + transform.forward * z;
        dir.Normalize();
        dir *= moveSpeed * Time.deltaTime;
        controller.Move(dir);
        dir.y = rig.velocity.y;
        rig.velocity = dir;
        
    }

    void Cameralook()
    {
        float y = Input.GetAxis("Mouse X") * lookSensitivity;
        rotX += Input.GetAxis("Mouse Y") * lookSensitivity;
        rotX = Mathf.Clamp(rotX, minLookX, maxLookX);

        cam.transform.localRotation = Quaternion.Euler(-rotX, 0, 0);
        transform.eulerAngles += Vector3.up * y;
    }


    void TryJump()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        if(Physics.Raycast(ray,1.1f))
        {
            rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

    }
    public void TakeDamage(int damage)
    {
        audioSource.PlayOneShot(playerHurtSFX);
        currentHP -= damage;
        UIMenager.instance.UpdateHEalthBar(currentHP, maxHP);
        FlashDamage();
        if (currentHP <= 0)
            Die();
    }


    void Die()
    {
        GameManager.instance.LoseGame();
    }

    public void Givehealth(int amountToGive)
    {
        currentHP = Mathf.Clamp(currentHP + amountToGive, 0, maxHP);

        UIMenager.instance.UpdateHEalthBar(currentHP, maxHP);

    }

    public void GiveAmmo(int amountToGive)
    {
        weapon.currentAmmo = Mathf.Clamp(weapon.currentAmmo + amountToGive, 0, weapon.maxAmmo);

        UIMenager.instance.UpdateAmmoText(weapon.currentAmmo,weapon.maxAmmo);   
    }



    public void FlashDamage()
    {
        if(fadeAway !=null)
            StopCoroutine(fadeAway);

        image.enabled = true;
        image.color = Color.white;
        fadeAway = StartCoroutine(FadeAway());
    }

    IEnumerator FadeAway()
    {
        float a = 1.0f;

        while(a>0.0f)
        {
            a-=(1.0f/flashSpeed) * Time.deltaTime;
            image.color = new Color(1.0f, 1.0f, 1.0f, a);

            yield return null;
        }

        image.enabled = false;
    }
}



