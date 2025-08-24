using Fusion;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform[] spawnPoints;

    public void PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
            var spawnedPlayer = Runner.Spawn(playerPrefab, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity, player);

            var localHealthUI = FindFirstObjectByType<HealthUI>();
            if (localHealthUI != null)
            {
                var localPlayerHealth = spawnedPlayer.GetComponent<Health>();

                localHealthUI.SetPlayer(localPlayerHealth);
            }
        }
    }
}