using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected int ammoCapacity;
    [SerializeField] protected int currentLoadedAmmo;
    [SerializeField] protected int currentSpareAmmo;
    [SerializeField] protected bool canFire;
    [SerializeField] protected Transform firePoint;

    [SerializeField] protected Magazine magazine;

    [SerializeField] public Enums.MagazineType magazineType;

    private GameObject ammoText;

    // Start is called before the first frame update
    void Start()
    {
        ammoText = GameObject.FindWithTag("AmmoText");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void Reload(Magizine newMage)
    {
        magazine = newMag;

        //if (currentLoadedAmmo >= ammoCapacity || currentSpareAmmo <= 0)
        //{
        //    return;
        //}

        //int ammoNeeded = ammoCapacity - currentLoadedAmmo;
        //int ammoToReload = Mathf.Min(ammoNeeded, currentSpareAmmo);

        //currentLoadedAmmo += ammoToReload;
        //currentSpareAmmo -= ammoToReload;

    }
    public virtual void CheckAmmo()
    {
        if (magazine != null)
        {
            return magazine.GetRounds();
        }
        else
        {
            return 0;
        }
    }
    public virtual void Fire()
    {
        if (magazine != null)
        {
            if (magizine.GetRounds() > 0)
            {
                magazine.RemoveRound();
                ammoText.GameObject<TextMeshObject>().text = "Ammo: " + CheckAmmo();
                RaycastHit hit;
                if (Physics.RayCast(firePoint.position, firePoint.forward, out hit, 100))
                {
                    Debug.DrawRay(firePoint.position, firePoint.forward * hit.distance, Color.red, 2f);
                    if (hit.transform.CompareTag("Zombie"))
                    {
                        hit.transform.GetComponent<Enemy>().TakeDamage(1);
                    }
                }
            }
        }
    }
}
