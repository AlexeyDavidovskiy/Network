using UnityEngine;
using Fusion;

public class PlayerInput : NetworkBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private ArmedLogic armedLogic;

    private bool isFireSingle;
    private bool isFireAuto;

    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }
    public bool JumpPressed { get; private set; }

    public bool IsFireSingle => isFireSingle;
    public bool IsFireAuto => isFireAuto;

    private void Update()
    {
        if (!HasInputAuthority) return;

        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump"))
        {
            JumpPressed = true;
        }

        EquipPistol();
        EquipRifle();
        Disarm();
        FireSingle();
        FireAuto();
    }

    public void ConsumeJump()
    {
        JumpPressed = false;
    }

    private void EquipPistol()
    {
        if (armedLogic.IsArmed)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                armedLogic.EquipPistol();
            }
        }
    }

    private void EquipRifle()
    {
        if (armedLogic.IsArmed)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                armedLogic.EquipRifle();
            }
        }
    }

    private void Disarm()
    {
        if (armedLogic.IsArmed)
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                armedLogic.Disarm();
            }
        }
    }

    private void FireSingle()
    {
        if (armedLogic.IsPistolArmed)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isFireSingle = true;
            }
        }
    }

    private void FireAuto()
    {
        if (armedLogic.IsRifleArmed)
        {
            if (Input.GetMouseButton(0))
            {
                isFireAuto = true;
            }
            else
            {
                isFireAuto = false;
            }
        }
    }

    public bool ConsumeFireSingle()
    {
        if (!IsFireSingle) return false;
        isFireSingle = false;
        return true;
    }
}