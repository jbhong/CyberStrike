using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    //for finding enemies
    private const string ENEMY_FLAG = "Enemy";

    //weapon details and manager
    private WeaponManager weaponManager;
    private WeaponData currentWeapon;

    //reference to camera
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask mask;

    private bool shooting;

    //on start method
    private void Start()
    {
        //if no camera reference disable object
        if(cam == null)
        {
            Debug.LogError("PlayerShoot: No camera referenced");
            this.enabled = false;
        }

        //gets weapon manager
        weaponManager = GetComponent<WeaponManager>();
    }

    //update methods
    private void Update()
    {
        if (PauseMenu.IsOn)
        {
            if (shooting)
            {
                CancelInvoke("Shoot");
                shooting = false;
            }
            return;
        }
        if (weaponManager != null)
        {
            //checks weapon
            currentWeapon = weaponManager.GetCurrentWeapon();
            if (currentWeapon != null)
            {
                if (Input.GetKey(KeyCode.R))
                {
                    if ((currentWeapon.bullets < currentWeapon.clipSize) && currentWeapon.ammoLeft > 0)
                    {
                        weaponManager.Reload();
                        if (shooting)
                        {
                            CancelInvoke("Shoot");
                            shooting = false;
                        }
                        return;
                    }
                }

                if (currentWeapon.bullets <= 0)
                {
                    if (currentWeapon.ammoLeft > 0)
                    {
                        weaponManager.Reload();
                        if (shooting)
                        {
                            CancelInvoke("Shoot");
                            shooting = false;
                        }
                        return;
                    }
                    else
                    {
                        if (shooting)
                        {
                            CancelInvoke("Shoot");
                            shooting = false;
                        }
                        return;
                    }
                }

                //if semi auto
                if (currentWeapon.fireRate <= 0f)
                {
                    //shoot once on button down
                    if (Input.GetButtonDown("Fire1"))
                    {
                        Shoot();
                    }
                }
                //if full auto
                else
                {
                    //start shooting on button down
                    if (Input.GetButtonDown("Fire1"))
                    {
                        if (!shooting)
                        {
                            InvokeRepeating("Shoot", 0f, 1f / currentWeapon.fireRate);
                            shooting = true;
                        }
                    }
                    //stop shooting on button up
                    else if (!Input.GetButton("Fire1"))
                    {
                        if (shooting)
                        {
                            CancelInvoke("Shoot");
                            shooting = false;
                        }
                    }
                }
            }
        }
    }

    //calling when shooting
    void Shoot()
    {
        if (weaponManager.isReloading)
        {
            return;
        }

        //Shooting graphics
        MuzzleFlash();

        //checking what we hit
        RaycastHit _hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, currentWeapon.range, mask))
        {
            OnHit(_hit.point, _hit.normal, _hit.collider);
        }

        currentWeapon.bullets--;
    }

    //called to show muzzle flash
    void MuzzleFlash()
    {
        WeaponGraphics _graphics = weaponManager.GetCurrentGraphics();
        _graphics.muzzleFlash.Play();
        _graphics.gunSound.Play();
    }

    //called when we hit something
    void OnHit(Vector3 _pos, Vector3 _normal, Collider _objectHit)
    {
        GameObject _hitEffect = (GameObject)Instantiate(weaponManager.GetCurrentGraphics().hitEffectPrefab, _pos, Quaternion.LookRotation(_normal));
        Destroy(_hitEffect, 1f);
        if(_objectHit.tag == ENEMY_FLAG)
        { 
            Enemy _enemyController = _objectHit.gameObject.GetComponent<Enemy>();
            _enemyController.GotShot(currentWeapon.damage);
        }
    }
}
