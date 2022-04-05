

using UnityEngine;

namespace Assets.Upgrades
{
    public class DoubleTapUpgrade : BaseUpgrade
    {
        public DoubleTapUpgrade(ShipLogic ship) : base(ship) { }

        public override string Name => "Double Tap";

        public override string Description => "Fires an extra (+1 per level) projectile at no extra cost!";

        public override decimal BasePrice => 50;

        public override decimal NextPrice => CurrentPrice * 1.12m;

        public override void OnProjectileShoot()
        {
            for(var i = 0; i< Level;i++)
            {
                Ship.CreateProjectile(Random.Range(-20, 20));
            }
        }
    }
}
