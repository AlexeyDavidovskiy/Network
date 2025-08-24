using Fusion;
using UnityEngine;

public class PlayerAnimationController : NetworkBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private ArmedLogic armedLogic;

    [Networked, OnChangedRender(nameof(OnIsRunningChanged))]
    public bool IsRunning { get; set; }

    [Networked, OnChangedRender(nameof(OnIsPistolActiveChanged))]
    public bool IsPistolActive { get; set; }

    [Networked, OnChangedRender(nameof(OnRunWithPistolChanged))]
    public bool RunWithPistol { get; set; }

    [Networked, OnChangedRender(nameof(OnIsRifleActiveChanged))]
    public bool IsRifleActive { get; set; }
    [Networked, OnChangedRender(nameof(OnRunWithRifleChanged))]
    public bool RunWithRifle { get; set; }

    public override void FixedUpdateNetwork()
    {
        if (!HasInputAuthority) return;

        Vector3 horizontalMove = new Vector3(playerMovement.MoveDirection.x, 0, playerMovement.MoveDirection.z);

        bool running = horizontalMove.magnitude > 0.01f;

        bool isPistolActive = armedLogic.IsPistolArmed == true;
        bool runWithPistol = armedLogic.IsPistolArmed == true && horizontalMove.magnitude > 0.01f;

        bool isRifleActive = armedLogic.IsRifleArmed == true;
        bool runWithRifle = armedLogic.IsRifleArmed == true && horizontalMove.magnitude > 0.1f;

        IsRunning = running;

        IsPistolActive = isPistolActive;
        RunWithPistol = runWithPistol;

        IsRifleActive = isRifleActive;
        RunWithRifle = runWithRifle;
    }

    private void OnIsRunningChanged()
    {
        anim.SetBool("IsRunning", IsRunning);
    }

    private void OnIsPistolActiveChanged()
    {
        anim.SetBool("IsPistolActive", IsPistolActive);
    }

    private void OnRunWithPistolChanged()
    {
        anim.SetBool("RunWithPistol", RunWithPistol);
    }

    private void OnIsRifleActiveChanged()
    {
        anim.SetBool("IsRifleActive", IsRifleActive);
    }

    private void OnRunWithRifleChanged()
    {
        anim.SetBool("RunWithRifle", RunWithRifle);
    }
}