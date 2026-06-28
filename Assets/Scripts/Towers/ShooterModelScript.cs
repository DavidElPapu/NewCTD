using UnityEngine;

public class ShooterModelScript : TowerModelScript
{
    public Transform projectileSpawnpoint;
    [SerializeField] private Transform targetFollower;

    public void RotateToTarget(Transform target)
    {
        targetFollower.LookAt(target);
    }
}
