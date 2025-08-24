using Fusion;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    [SerializeField] private int damage;

    private Vector3 direction;
    private float timer;

    public void Init(Vector3 dir)
    {
        direction = dir.normalized;
        timer = 0;
    }

    public override void FixedUpdateNetwork()
    {
        transform.position += direction * speed * Runner.DeltaTime;

        timer += Runner.DeltaTime;
        if (timer >= lifeTime)
        {
            Runner.Despawn(Object);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!HasStateAuthority) return;

        if (other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamageRpc(damage);
        }

        Runner.Despawn(Object);
    }
}