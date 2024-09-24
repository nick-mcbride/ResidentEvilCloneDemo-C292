using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected int ammoCapacity;
    [SerializeField] protected int currentLoadedAmmo;
    [SerializeField] protected int currentSpareAmmo;
    [SerializeField] protected bool canFire;
    [SerializeField] protected Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        canFire = currentLoadedAmmo > 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected virtual void Reload()
    {
        if (currentLoadedAmmo >= ammoCapacity || currentSpareAmmo <= 0)
        {
            return;
        }

        int ammoNeeded = ammoCapacity - currentLoadedAmmo;
        int ammoToReload = Mathf.Min(ammoNeeded, currentSpareAmmo);

        currentLoadedAmmo += ammoToReload;
        currentSpareAmmo -= ammoToReload;
    }

    protected virtual void Fire()
    {
        if (!canFire || currentLoadedAmmo <= 0)
        {
            Debug.Log("Cannot fire: No ammo loaded.");
            return;
        }

        currentLoadedAmmo--;
        canFire = currentLoadedAmmo > 0;

        RaycastHit hit;
        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, 100))
        {
            Debug.Log(hit.transform.name);
            if (hit.transform.CompareTag("Zombie"))
            {
                hit.transform.GetComponent<Enemy>().TakeDamage(1);
            }
        }
    }

    public void TryFire()
    {
        Fire();
    }
}
