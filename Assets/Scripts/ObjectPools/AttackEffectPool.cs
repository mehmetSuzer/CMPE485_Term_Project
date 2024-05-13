using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace ObjectPools
{
    public class AttackEffectPool : MonoBehaviour
    {
        public static AttackEffectPool Instance;
        private List<GameObject> bloodPool = new List<GameObject>();
        private List<GameObject> magicPool = new List<GameObject>();
        private int amountToPool;

        [SerializeField] private GameObject bloodEffectPrefab;
        [SerializeField] private GameObject magicEffectPrefab;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        private void Start()
        {
            amountToPool = GameManager.instance.soldierPerLine * 6;

            for (int i = 0; i < amountToPool; i++)
            {
                GameObject gameObj = Instantiate(bloodEffectPrefab, transform);
                gameObj.SetActive(false);
                bloodPool.Add(gameObj);
            }

            for (int i = 0; i < amountToPool; i++)
            {
                GameObject gameObj = Instantiate(magicEffectPrefab, transform);
                gameObj.SetActive(false);
                magicPool.Add(gameObj);
            }
        }

        public GameObject GetPooledObject(AttackEffectType type, float lifetime)
        {
            switch (type)
            {
                case AttackEffectType.Blood:
                    for (int i = 0; i < bloodPool.Count; i++)
                    {
                        if (!bloodPool[i].activeInHierarchy)
                        {
                            var pooledObject = bloodPool[i];
                            pooledObject.transform.SetParent(transform);
                            pooledObject.SetActive(true);
                            StartCoroutine(DestroyBlood(pooledObject, lifetime));
                            return pooledObject;
                        }
                    }

                    return null;
                case AttackEffectType.Magic:
                    for (int i = 0; i < bloodPool.Count; i++)
                    {
                        if (!magicPool[i].activeInHierarchy)
                        {
                            var pooledObject = magicPool[i];
                            pooledObject.transform.SetParent(transform);
                            pooledObject.SetActive(true);
                            StartCoroutine(DestroyMagic(pooledObject, lifetime));
                            return pooledObject;
                        }
                    }

                    return null;
            }


            return null;
        }

        private IEnumerator DestroyBlood(GameObject gameObj, float lifetime)
        {
            yield return new WaitForSeconds(lifetime);
            gameObj.SetActive(false);
        }
        private IEnumerator DestroyMagic(GameObject gameObj, float lifetime)
        {
            yield return new WaitForSeconds(lifetime);
            gameObj.SetActive(false);
        }
    }
}