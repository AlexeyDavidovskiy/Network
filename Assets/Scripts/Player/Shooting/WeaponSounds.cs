using Fusion;
using UnityEngine;

public class WeaponSounds : NetworkBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip pistolSound;
    [SerializeField] private AudioClip rifleSound;

    public void PistolShot()
    {
        audioSource.PlayOneShot(pistolSound);
    }

    public void RifleShot()
    {
        audioSource.PlayOneShot(rifleSound);
    }
}