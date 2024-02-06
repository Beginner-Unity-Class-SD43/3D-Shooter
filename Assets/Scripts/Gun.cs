using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] float damage = 10f; // Gun damage
    [SerializeField] float range = 100f; // Shooting range
    [SerializeField] float fireRate = 4f; // How fast you can fire

    [SerializeField] Camera playerCam; // player camera

    float nextTimeToFire;

    [SerializeField] ParticleSystem muzzleFlash; // Muzzle flash particle system
    [SerializeField] GameObject impactEffect; // Impact effect

    public bool canShoot;

    private void Start()
    {
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && canShoot)
        {
            nextTimeToFire = Time.time + 1 / fireRate; // Fires at every 1/firerate intervals
            Shoot();
        }
    }

    void Shoot()
    {
        muzzleFlash.Play(); // Play the muzzle flash particle

        RaycastHit hit; // Draw a line from the camera and detect what it hits
        if(Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, range))
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.TakeDamage(damage);
                Debug.Log("Enemy hit");
            }

            GameObject impactObject = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactObject, 1f);

        }

    }
}
