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
        if (canFire && currentLoadedAmmo > 0)
        {
            //debug.log("firing weapon");
            currentLoadedAmmo--;
            RaycastHit hit;
            //debug.log("raycasting firing");
            if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, 100))
            {
                Debug.DrawRay(firePoint.position, firePoint.forward * hit.distance, Color.red, 2f);
                if (hit.transform.CompareTag("Zombie"))
                {
                    hit.transform.GetComponent<Enemy>().TakeDamage(1);
                }
            }
            else
            {
                //debug.log("no hit");
                Debug.DrawRay(firePoint.position, firePoint.forward * 100, Color.red, 2f);
            }
        }
    }

    public void TryFire()
    {
        Fire();
    }
}
