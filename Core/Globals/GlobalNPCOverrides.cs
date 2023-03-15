using BetterGore.Core.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace BetterGore.Core.Globals
{
    public class GlobalNPCOverrides : GlobalNPC
    {
        public int BloodCooldown;

        public override bool InstancePerEntity => true;

        public override void PostAI(NPC npc)
        {
            BloodCooldown = (int)MathHelper.Clamp(BloodCooldown - 1f, 0f, int.MaxValue);
        }

        public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit) => OnHit(npc, projectile, 
            (projectile.Center + projectile.velocity.SafeNormalize(Vector2.UnitY) * 2f).DirectionTo(projectile.Center), npc.lifeMax, damage);

        public void OnHit(NPC npc, Projectile projectile, Vector2 directionToSplat, int maxLife, int damage)
        {
            if (BloodCooldown > 0 && npc.life > 0)
                return;

            BloodCooldown = 10;

            float distanceTravelled = projectile.GetGlobalProjectile<GlobalProjectileOverrides>().SpawnPosition.Value.Distance(projectile.Center);
            float damageRatio = damage / maxLife;
            if (npc.life <= 0)
                Bleed(20, projectile.Center, directionToSplat, MathHelper.TwoPi, projectile.velocity.Length(), distanceTravelled, damageRatio);
            else
                Bleed((int)(5f + 20f * damageRatio), projectile.Center, directionToSplat, MathHelper.PiOver4, projectile.velocity.Length(), distanceTravelled, damageRatio);
        }

        private static void Bleed(int amountToBleed, Vector2 spawnPosition, Vector2 directionToSplat, float spurtAngle, float projectileSpeed, float distanceTravelled, float damageRatio, float bloodSize = 1f)
        {
            float maxDistance = 700f;
            float minDistance = 50f;
            float distanceScalar = 2f - Utils.GetLerpValue(minDistance, maxDistance, distanceTravelled, true);
            int finalBloodAmount = (int)(amountToBleed * distanceScalar);
            float speedScalar = Utils.GetLerpValue(0f, 28f, projectileSpeed, false);
            for (int i = 0; i < finalBloodAmount; i++)
            {
                float rotation = Main.rand.NextFloat(-spurtAngle * 0.5f, spurtAngle * 0.5f);
                float speed = Main.rand.NextFloat(0.3f, 0.5f) * (projectileSpeed * (1f * speedScalar));
                Vector2 velocity = directionToSplat.RotatedBy(rotation) * speed;
                float sizeScalar = (1 + damageRatio) * 3 * Main.rand.NextFloat(0.005f, 0.010f) * bloodSize;
                Vector2 size = new Vector2(Main.rand.NextFloat(0.3f, 1f), Main.rand.NextFloat(0.3f, 1f)) * sizeScalar;
                BloodManager.SpawnParticle(spawnPosition, velocity, size);
            }
        }
    }
}
