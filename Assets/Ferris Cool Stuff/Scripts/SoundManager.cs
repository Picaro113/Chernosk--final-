using System;
using UnityEngine;
using static Gun;
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; set; }

    // 1911 Sounds
    public AudioSource gun1911;
    public AudioSource reloading1911;
    public AudioSource emptyMag1911;

    // AKM Sounds
    public AudioSource gunAKM;
    public AudioSource reloadingAKM;
    public AudioSource emptyMagAKM;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void PlayShootingSound(GunModel gunModel)
    {
        switch (gunModel)
        {
            case GunModel.Pistol1911:
                gun1911.Play();
                break;
            case GunModel.AKM:
                gunAKM.Play();
                break;
        }
    }

    public void PlayReloadingSound(GunModel gunModel)
    {
        switch (gunModel)
        {
            case GunModel.Pistol1911:
                reloading1911.Play();
                break;
            case GunModel.AKM:
                reloadingAKM.Play();
                break;
        }
    }

    public void PlayEmptyMagSound(GunModel gunModel)
    {
        switch (gunModel)
        {
            case GunModel.Pistol1911:
                emptyMag1911.Play();
                break;
            case GunModel.AKM:
                emptyMagAKM.Play();
                break;
        }
    }
}


