using UnityEngine;
using System.Collections;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private GameObject startingWeapon;

    [SerializeField]
    private Transform weaponHolder;

    [SerializeField]
    private string weaponLayer = "Weapon";

    private GameObject currentWeapon;
    private WeaponGraphics currentGraphics;
    private WeaponData currentStats;
    private Animator animator;

    public bool isReloading = false;

    private void Start()
    {
        EquipWeapon(startingWeapon);
    }

    public WeaponData GetCurrentWeapon()
    {
        return currentStats;
    }

    public WeaponGraphics GetCurrentGraphics()
    {
        return currentGraphics;
    }

    private void EquipWeapon(GameObject _weapon)
    {
        currentWeapon = _weapon;

        GameObject _weaponIns = (GameObject)Instantiate(_weapon, weaponHolder.position, weaponHolder.rotation);
        _weaponIns.transform.SetParent(weaponHolder);

        currentStats = _weaponIns.GetComponent<WeaponData>();
        currentGraphics = _weaponIns.GetComponent<WeaponGraphics>();
        animator = currentGraphics.animator;

        _weaponIns.name = currentStats.gunName;

        if (currentGraphics == null)
        {
            Debug.LogError("No WeaponGraphics on the weapon object" + _weaponIns.name);
        }

        Utility.SetLayerRecursively(_weaponIns.gameObject, LayerMask.NameToLayer(weaponLayer));
    }

    public void Reload()
    {
        if (isReloading)
        {
            return;
        }

        StartCoroutine(Reload_Coroutine());
    }

    private IEnumerator Reload_Coroutine()
    {
        if (currentStats.ammoLeft > 0)
        {
            isReloading = true;
            animator.SetTrigger("StartReload");
            yield return new WaitForSeconds(currentStats.reloadTime);
        }
        else
        {
            yield return null;
        }

        if (currentStats.ammoLeft > 0)
        {
            int bulletDifference = currentStats.clipSize - currentStats.bullets;
            if (currentStats.ammoLeft >= currentStats.clipSize)
            {
                currentStats.bullets = currentStats.clipSize;
            }
            else
            {
                if(bulletDifference >= currentStats.ammoLeft)
                {
                    currentStats.bullets += currentStats.ammoLeft;
                    bulletDifference = currentStats.ammoLeft;
                }
                else
                {
                    currentStats.bullets = currentStats.clipSize;

                }
            }
            currentStats.ammoLeft -= bulletDifference;
            currentGraphics.reloadSound.Play();
            animator.SetTrigger("EndReload");
            isReloading = false;
        }
    }

}
