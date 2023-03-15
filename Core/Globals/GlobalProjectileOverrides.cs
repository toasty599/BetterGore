using BetterGore.Core.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace BetterGore.Core.Globals
{
    public class GlobalProjectileOverrides : GlobalProjectile
    {
        public Vector2? SpawnPosition;

        public override bool InstancePerEntity => true;

        public override bool PreAI(Projectile projectile)
        {
            if (SpawnPosition is null)
                SpawnPosition = projectile.Center;

            return true;
        }       
    }
}
