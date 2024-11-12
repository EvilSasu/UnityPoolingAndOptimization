using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;
    public int initialPoolSize = 10;
    public int maxActiveObjects = 50;
    private List<GameObject> pool = new List<GameObject>();
    [SerializeField] private int activeCount = 0;

    private void Start()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.transform.SetParent(transform, false);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject GetObject()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                UpdateColliderState(obj);
                return obj;
            }
        }

        GameObject newObj = Instantiate(prefab);
        newObj.transform.SetParent(transform, false);
        newObj.SetActive(true);
        pool.Add(newObj);
        UpdateColliderState(newObj);
        return newObj;
    }

    public void ReturnObject(GameObject obj)
    {
        TryGetHealthController(obj);

        obj.SetActive(false);

        UpdateColliderState(obj, reset: true);
    }

    private void UpdateColliderState(GameObject obj, bool reset = false)
    {
        Collider2D collider = obj.GetComponent<Collider2D>();
        
        if (collider != null)
        {
            if (reset)
            {
                collider.enabled = true;
                if(activeCount > 0) 
                    activeCount--;
            }
            else
            {
                collider.enabled = activeCount < maxActiveObjects;
                if (collider.enabled)
                {
                    activeCount++;
                }
            }
        }
    }

    private void TryGetHealthController(GameObject obj)
    {
        HealthController health = obj.GetComponent<HealthController>();
        if (health != null)
        {
            health.Setup();
        }
    }
}
