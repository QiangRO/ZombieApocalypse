using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    //Gun stats
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime;
    public int magazineSize;
    int bulletsLeft;

    //some bools
    bool shooting, readyToShoot, reloading;

    public Transform enemy;
    public Transform attackPos;
    public GameObject muzzleFlash;
    public RaycastHit rayHit;
    public LayerMask whatIsPlayer;

    private void Start()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Update()
    {
        shooting = enemy.GetComponent<ShootingAi>().isAttacking;
        if (shooting && readyToShoot && bulletsLeft > 0 && !reloading) Shoot();
    }

    public void Shoot()
    {
        readyToShoot = false;

        //RayCast
        if (Physics.Raycast(transform.position, enemy.forward, out rayHit, range, whatIsPlayer))
        {
            if (rayHit.collider.gameObject.GetComponent<playerMovement>())
                //rayHit.collider.GetComponent<playerMovement>().TakeDamage(damage);

            Debug.Log(rayHit.collider.gameObject.name);
        }

        bulletsLeft--;

        Instantiate(muzzleFlash, attackPos.position, Quaternion.identity);

        Invoke("ShotReset", timeBetweenShooting);
    }
    private void ShotReset()
    {
        readyToShoot = true;
    }
    private void Reload()
    {
        reloading = true;

        Invoke("ReloadingFinished", reloadTime);
    }
    private void ReloadingFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
