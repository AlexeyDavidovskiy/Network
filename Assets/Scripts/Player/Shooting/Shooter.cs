using Fusion;
using UnityEngine;

public class Shooter : NetworkBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private ArmedLogic armedLogic;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private WeaponSounds weaponSounds;
    [SerializeField] private Transform pistolFirePoint;
    [SerializeField] private Transform rifleFirePoint;
    [SerializeField] private NetworkPrefabRef bulletPredab;
    [SerializeField] private float fireRate;

    private float lastFireTime;
    private Transform firePoint;

    public override void FixedUpdateNetwork()
    {
        if (playerInput.ConsumeFireSingle() && armedLogic.IsPistolArmed)
        {
            Shoot();
        }

        if (playerInput.IsFireAuto && armedLogic.IsRifleArmed)
        {
            if (Runner.SimulationTime - lastFireTime >= fireRate)
            {
                Shoot();
                lastFireTime = Runner.SimulationTime;
            }
        }
    }

    private void Shoot()
    {
        Ray ray = playerMovement.Camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        Vector3 targetPoint;

        if (Physics.Raycast(ray, out RaycastHit hit, 1000f))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(1000f);
        }

        if (armedLogic.IsPistolArmed)
        {
            firePoint = pistolFirePoint;
            weaponSounds.PistolShot();
        }
        else if (armedLogic.IsRifleArmed)
        {
            firePoint = rifleFirePoint;
            weaponSounds.RifleShot();
        }

        Vector3 direction = (targetPoint - firePoint.position).normalized;

        NetworkObject bulletObkect = Runner.Spawn(bulletPredab, firePoint.position, Quaternion.LookRotation(direction));

        if (bulletObkect.TryGetComponent<Bullet>(out var bullet))
        {
            bullet.Init(direction);
        }
    }
}