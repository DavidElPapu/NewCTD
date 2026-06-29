using UnityEngine;

public class TowerProjectileScript : MonoBehaviour
{
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private Rigidbody rb;
    private SphereCollider hitbox;
    private float damage;

    private void Awake()
    {
        TryGetComponent(out meshFilter);
        TryGetComponent(out meshRenderer);
        TryGetComponent(out rb);
        TryGetComponent(out hitbox);
    }

    public void Initialize(Mesh newMesh, Material newMat, LayerMask detectionMask, float newSizeRadius, float newDamage, Vector3 startPos, Quaternion startRot)
    {
        //Starting values for the projectile when called
        meshFilter.mesh = newMesh;
        meshRenderer.material = newMat;
        //Since we get a layer we want to detect, we flip it to exclude all other layers (since by default it collides with everything)
        LayerMask exludedLayer = ~detectionMask;
        hitbox.excludeLayers = exludedLayer;
        hitbox.radius = newSizeRadius;
        damage = newDamage;
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

    private void OnTriggerEnter(Collider other)
    {
        //Deals damage to enemy on contact and then disables
        if (other.gameObject.TryGetComponent(out BaseEnemy enemy))
        {
            enemy.TakeDamage(damage);
            DisableProjectile();
        }
    }

    private void DisableProjectile()
    {
        //Here could be any visuals related to the projectile's ending
        //Returns to the pool
        TowerProjectilePoolManager.singleton.ReturnBaseProjectile(this);
    }
}
