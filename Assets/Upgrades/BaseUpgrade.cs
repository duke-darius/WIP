using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Upgrades
{
    public abstract class BaseUpgrade
    {
        public BaseUpgrade(ShipLogic ship)
        {
            this.Ship = ship;
        }
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract decimal BasePrice { get; }
        public abstract decimal NextPrice { get; }
        public long Level { get; set; } = 0;
        public decimal CurrentPrice { get; set; }
        public ShipLogic Ship { get; }

        public abstract void OnProjectileShoot();
    }
}
