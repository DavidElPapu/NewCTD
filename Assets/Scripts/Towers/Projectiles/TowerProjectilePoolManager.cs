using System.Collections.Generic;
using UnityEngine;

public class TowerProjectilePoolManager : MonoBehaviour
{
    [SerializeField] private GameObject[] projectilePrefabs;
    public static TowerProjectilePoolManager singleton;
    private Stack<TowerProjectileScript>[] projectilePools;
    private Stack<TowerProjectileScript> baseProjectilePool, explosiveProjectilePool;

    private void Awake()
    {
        //Only 1 instance of this manager, initializes the pool on awake
        if (singleton == null)
        {
            singleton = this;
            projectilePools = new Stack<TowerProjectileScript>[projectilePrefabs.Length];
            baseProjectilePool = new Stack<TowerProjectileScript>();
            explosiveProjectilePool = new Stack<TowerProjectileScript>();
            Setup();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Setup()
    {
        //For now the pool starts with 10 elements
        for (int i = 0; i < 10; i++)
        {
            //Creates the starting basic projectiles
            GameObject newProjectile = Instantiate(projectilePrefabs[0], transform);
            newProjectile.SetActive(false);
            baseProjectilePool.Push(newProjectile.GetComponent<TowerProjectileScript>());

            //Creates the staring explosive projectiles
            GameObject newExplosiveProjectile = Instantiate(projectilePrefabs[1], transform);
            newExplosiveProjectile.SetActive(false);
            explosiveProjectilePool.Push(newExplosiveProjectile.GetComponent<TowerProjectileScript>());
        }
        //Adds the pools to the pool list
        projectilePools[0] = baseProjectilePool;
        projectilePools[1] = explosiveProjectilePool;
    }

    public TowerProjectileScript GetProjectileScript(TowerProjectileType projectileType)
    {
        //If pool is not empty, returns the top element, else creates a new one
        TowerProjectileScript projectileScript = null;
        if (projectilePools[(int)projectileType].Count > 0)
        {
            projectileScript = projectilePools[(int)projectileType].Pop();
        }
        else
        {
            GameObject newProjectile = Instantiate(projectilePrefabs[(int)projectileType], transform);
            newProjectile.SetActive(false);
            projectileScript = newProjectile.GetComponent<TowerProjectileScript>();
        }
        return projectileScript;
    }

    public void ReturnBaseProjectile(TowerProjectileScript projectileScript, TowerProjectileType projectileType)
    {
        projectileScript.transform.parent = transform;
        projectileScript.gameObject.SetActive(false);
        projectilePools[(int)projectileType].Push(projectileScript);
    }
}

public enum TowerProjectileType
{
    Basic,
    Explosive
}
