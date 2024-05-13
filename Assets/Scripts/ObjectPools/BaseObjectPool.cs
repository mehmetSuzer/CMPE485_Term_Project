using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPools
{
    public abstract class BaseObjectPool : MonoBehaviour
    {
        protected List<GameObject> pooledObjects = new List<GameObject>();
        protected int amountToPool;

        [SerializeField] protected GameObject pooledObjectPrefab;

        protected virtual void Awake()
        {
            
        }

        protected virtual void Start()
        {
            // Instantiate and add objects to the pool
            for (int i = 0; i < amountToPool; i++)
            {
                GameObject gameObj = Instantiate(pooledObjectPrefab, transform);
                gameObj.SetActive(false);
                pooledObjects.Add(gameObj);
            }
        }

        public virtual GameObject GetPooledObject(float lifetime)
        {
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                if (!pooledObjects[i].activeInHierarchy)
                {
                    var pooledObject = pooledObjects[i];
                    pooledObject.transform.SetParent(transform);
                    pooledObject.SetActive(true);
                    StartCoroutine(Destroy(pooledObject, lifetime));
                    return pooledObject;
                }
            }

            return null;
        }

        protected virtual IEnumerator Destroy(GameObject gameObj, float lifetime)
        {
            yield return new WaitForSeconds(lifetime);
            gameObj.SetActive(false);
        }
    }
}