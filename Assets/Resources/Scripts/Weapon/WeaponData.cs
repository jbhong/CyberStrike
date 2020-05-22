using UnityEngine;

public class WeaponData : MonoBehaviour
{ 
    public string gunName = "M4";

    public int damage = 30;
    public float range = 50f;

    public float fireRate = 5f;

    public int clipSize = 30;

    public int maxAmmo = 1000;

    public int ammoLeft = 60;

    [HideInInspector]
    public int bullets = -1;

    public float reloadTime = 1f;

    private void Start()
    {
        bullets = clipSize;
    }
}
