using System;
using System.Collections;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Gun : MonoBehaviour
{

    // Shooting
    public bool isShooting, readyToShoot;
    bool allowReset = true;
    public float shootingDelay = 2f;

    // Burst
    public int bulletsPerBurst = 3;
    public int burstBulletsLeft;

    // Spread
    public float spreadIntensity;
    
    // Bullet
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 30f;
    public float bulletPrefabLifeTime = 3f;

    // Loading 
    public float reloadTime;
    public int magazineSize, bulletsLeft;
    public bool isReloading;


    public bool canShoot;
    public GameObject muzzleEffect;

    public enum GunModel
    {
        Pistol1911,
        AKM,
    }

    public GunModel thisWeaponModel;

    public enum ShootingMode
    {
        Single,
        Burst,
        Auto
    }

    public ShootingMode currentShootingMode;

    private void Awake()
    {
        canShoot = true;
        readyToShoot = true;
        burstBulletsLeft = bulletsPerBurst;

        bulletsLeft = magazineSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (bulletsLeft == 0 && isShooting)
        {
            SoundManager.Instance.emptyMag1911.Play();
        }

        if (currentShootingMode == ShootingMode.Auto && canShoot == true)
        {
            // Holding Left Mouse Down
            isShooting = Input.GetKey(KeyCode.Mouse0);
        }
        else if (currentShootingMode == ShootingMode.Single && canShoot == true || currentShootingMode == ShootingMode.Burst && canShoot == true)
        {
            // Clicking Left Mouse Once
            isShooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if(Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !isReloading)
        {
            Reload();
        }

        if (readyToShoot && isShooting && bulletsLeft > 0)
        {
            burstBulletsLeft = bulletsPerBurst;
            FireWeapon();
        }

        if(AmmoManager.Instance.ammoCount != null)
        {
            AmmoManager.Instance.ammoCount.text =$"{bulletsLeft/bulletsPerBurst}/{magazineSize/bulletsPerBurst}";
        }
    }

    private void FireWeapon()
    {
        bulletsLeft--;

        muzzleEffect.GetComponent<ParticleSystem>().Play();
        // animator will go here eventually 
        SoundManager.Instance.PlayShootingSound(thisWeaponModel);

        readyToShoot = false;

        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;



        // Spawn boolets (I NEED MORE BOOLETS)
       GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

        // point at shooting direction
       bullet.transform.forward = shootingDirection;

        // Shoot the boolet
       bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse);

        // DESTROY BOOLET after a few secs
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifeTime));

        // Checking if done shooting
        if (allowReset)
        {
            Invoke("ResetShot", shootingDelay);
            allowReset = false;
        }

        // Checking if in Burst Mode
        if(currentShootingMode == ShootingMode.Burst && burstBulletsLeft > 1)
        {
            burstBulletsLeft--;
            Invoke("FireWeapon", shootingDelay);
        }
    }

    private void Reload()
    {
        SoundManager.Instance.PlayReloadingSound(thisWeaponModel);

        isReloading = true;
        Invoke("ReloadCompleted", reloadTime);
    }

    private void ReloadCompleted()
    {
        bulletsLeft = magazineSize;
        isReloading = false;
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowReset = true;
    }

    private Vector3 CalculateDirectionAndSpread()
    {
       Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        // Hitting something 
        if(Physics.Raycast(ray, out hit))
        {
            Debug.DrawRay(ray.origin, hit.point, Color.red, 5f);
            targetPoint = hit.point;
        }
        else
        {
            // shooting air 
            targetPoint = ray.GetPoint(100);
        }

        Vector3 direction = targetPoint - bulletSpawn.position;

        // Spread calculation 
        float x = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        
        // Returning spread and direction 
        return direction + new Vector3(x, y, 0);
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {

        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}
