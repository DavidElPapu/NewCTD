using UnityEngine;

public class TowerProjectileScript : MonoBehaviour
{
    protected ProjectileShootAbilityDataSO data;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private Rigidbody rb;
    private SphereCollider hitbox;
    private float activeTimer;

    private void Awake()
    {
        //Get all components on awake
        TryGetComponent(out meshFilter);
        TryGetComponent(out meshRenderer);
        TryGetComponent(out rb);
        TryGetComponent(out hitbox);
    }

    public virtual void Initialize(ProjectileShootAbilityDataSO data, Vector3 startPos, Quaternion startRot)
    {
        //Starting values for the projectile when called
        this.data = data;
        meshFilter.sharedMesh = this.data.projectileMesh;
        meshRenderer.sharedMaterial = this.data.projectileMaterial;
        //Since we get a layer we want to detect, we flip it to exclude all other layers (since by default it collides with everything)
        LayerMask exludedLayer = ~this.data.detectionLayers;
        hitbox.excludeLayers = exludedLayer;
        hitbox.radius = this.data.projectileSizeRadius;
        activeTimer = this.data.projectileActiveTime;
        transform.position = startPos;
        transform.rotation = startRot;
        //We assume the object came inactive from the pool
        gameObject.SetActive(true);
    }

    public void LaunchProjectile(Vector3 direction)
    {
        //Resets any velocity it might had before
        rb.linearVelocity = Vector3.zero;
        rb.AddForce(direction, ForceMode.Impulse);
    }

    private void Update()
    {
        if (activeTimer > 0)
        {
            activeTimer -= Time.deltaTime;
            if (activeTimer <= 0)
                DisableProjectile();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Deals damage to enemy on contact and then disables
        if (other.gameObject.TryGetComponent(out BaseEnemy enemy))
        {
            OnImpact(enemy);
        }
    }

    protected virtual void OnImpact(BaseEnemy enemy)
    {
        enemy.TakeDamage(data.damage);
        enemy.ApplyStatusEffect(data.applyEffect, data.effectDuration);
        DisableProjectile();
    }

    protected void DisableProjectile()
    {
        //Here could be any visuals related to the projectile's ending
        //Returns to the pool
        TowerProjectilePoolManager.singleton.ReturnBaseProjectile(this, data.projectileType);
    }
}
