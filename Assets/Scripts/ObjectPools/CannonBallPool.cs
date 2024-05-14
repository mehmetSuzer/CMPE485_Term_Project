using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPools
{
    public class CannonBallPool : BaseObjectPool
    {
        public static CannonBallPool Instance;

        protected override void Awake()
        {
            base.Awake();
            if (Instance == null)
            {
                Instance = this;
            }
            amountToPool = GameManager.instance.cannonNumber * 5;

        }
        
    }
}
