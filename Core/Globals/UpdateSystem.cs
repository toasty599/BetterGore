using BetterGore.Core.Particles;
using Terraria.ModLoader;

namespace BetterGore.Core.Globals
{
    public class UpdateSystem : ModSystem
    {
        public override void PostUpdateProjectiles() => BloodManager.UpdateParticles();
    }
}