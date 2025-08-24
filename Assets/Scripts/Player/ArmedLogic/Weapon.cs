using Fusion;
using UnityEngine;

public class Weapon : NetworkBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ArmedLogic armedLogic))
        {
            if (!armedLogic.IsArmed)
            {
                armedLogic.IsArmed = true;
                if (Runner.IsServer)
                {
                    Runner.Despawn(Object);
                }
            }
        }
    }
}