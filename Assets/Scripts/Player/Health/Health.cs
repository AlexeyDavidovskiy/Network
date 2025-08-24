
using Fusion;

public class Health : NetworkBehaviour
{
    [Networked, OnChangedRender(nameof(OnHealthChanged))]
    public int NetworkedHealth { get; set; }

    private HealthUI healthUI;

    public override void Spawned()
    {
        if (Object.HasInputAuthority) 
        {
            healthUI = FindFirstObjectByType<HealthUI>();

            if (healthUI != null) 
            {
                healthUI.SetPlayer(this);
            }
        }
    }
    private void OnHealthChanged()
    {
        if (Object.HasInputAuthority && healthUI != null)
        {
            healthUI.OnHealthChanged(NetworkedHealth);
        }
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void DealDamageRpc(int damage)
    {
        if (!HasStateAuthority) return;
        NetworkedHealth -= damage;
    }
}