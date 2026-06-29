using System.Collections.Generic;
using UnityEngine;

public class TowerProjectilePoolManager : MonoBehaviour
{
    [SerializeField] private GameObject baseProjectilePrefab;
    public static TowerProjectilePoolManager singleton;
    private Stack<TowerProjectileScript> baseProjectilePool;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            baseProjectilePool = new Stack<TowerProjectileScript>();
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
            GameObject newProjectile = Instantiate(baseProjectilePrefab, transform);
            newProjectile.SetActive(false);
            baseProjectilePool.Push(newProjectile.GetComponent<TowerProjectileScript>());
        }
    }

    public TowerProjectileScript GetBaseProjectileScript()
    {
        //If pool is not empty, returns the top element, else creates a new one
        TowerProjectileScript projectileScript = null;
        if (baseProjectilePool.Count > 0)
        {
            projectileScript = baseProjectilePool.Pop();
        }
        else
        {
            GameObject newProjectile = Instantiate(baseProjectilePrefab,transform);
            newProjectile.SetActive(false);
            projectileScript = newProjectile.GetComponent<TowerProjectileScript>();
        }
        return projectileScript;
    }

    public void ReturnBaseProjectile(TowerProjectileScript projectileScript)
    {
        projectileScript.transform.parent = transform;
        projectileScript.gameObject.SetActive(false);
        baseProjectilePool.Push(projectileScript);
    }
}
