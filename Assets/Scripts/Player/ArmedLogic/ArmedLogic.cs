using Fusion;
using UnityEngine;

public class ArmedLogic : NetworkBehaviour
{
    [SerializeField] private GameObject pistol;
    [SerializeField] private GameObject rifle;

    [Networked] public bool IsArmed { get; set; }
    [Networked] public bool IsPistolArmed { get; set; }
    [Networked] public bool IsRifleArmed { get; set; }

    public override void Spawned()
    {
        IsArmed = false;
        IsPistolArmed = false;
        IsRifleArmed = false;

    }

    public override void Render()
    {
        pistol.SetActive(IsPistolArmed);
        rifle.SetActive(IsRifleArmed);
    }

    public void EquipPistol()
    {
        if (!IsArmed) return;

        IsPistolArmed = true;
        IsRifleArmed = false;

    }

    public void EquipRifle()
    {
        if (!IsArmed) return;

        IsRifleArmed = true;
        IsPistolArmed = false;

    }

    public void Disarm()
    {
        if (!IsArmed) return;

        IsPistolArmed = false;
        IsRifleArmed = false;
    }
}