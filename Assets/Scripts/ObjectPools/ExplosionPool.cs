using UnityEngine;

namespace ObjectPools
{
    public class ExplosionPool : BaseObjectPool
    {
        public static ExplosionPool Instance;

        protected override void Awake()
        {
            base.Awake();

            if (Instance == null)
                Instance = this;
            amountToPool = GameManager.instance.cannonNumber * 3;
        }
    }
}