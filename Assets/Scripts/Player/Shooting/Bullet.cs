using Fusion;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    [SerializeField] private int damage;
    [SerializeField] private Rigidbody rb;

    private bool hit;
    private float timer;

    public override void Spawned()
    {
        rb.interpolation = RigidbodyInterpolation.None;
    }
    public void Init(Vector3 dir)
    {
        if (!HasStateAuthority) return;
        rb.linearVelocity = dir.normalized * speed;
        timer = 0;
        hit = false;
    }

    public override void FixedUpdateNetwork()
    {
        if (!HasStateAuthority) return;

        timer += Runner.DeltaTime;

        if (timer >= lifeTime)
        {
            Runner.Despawn(Object);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!HasStateAuthority || hit) return;
        hit = true;

        if (other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamageRpc(damage);
        }

        Runner.Despawn(Object);
    }
}