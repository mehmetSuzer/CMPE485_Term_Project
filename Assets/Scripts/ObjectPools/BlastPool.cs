using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPools
{
    public class BlastPool : BaseObjectPool
    {
        public static BlastPool Instance;

        protected override void Awake()
        {
            base.Awake();
            if (Instance == null)
            {
                Instance = this;
            }
            amountToPool = GameManager.instance.cannonNumber;

        }

    }
}