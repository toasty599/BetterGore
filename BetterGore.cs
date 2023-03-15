using BetterGore.Core.Particles;
using Terraria.ModLoader;

namespace BetterGore
{
	public class BetterGore : Mod
	{
		public static BetterGore Instance
        {
			get;
			private set;
        }

        public override void Load()
        {
            Instance = this;
            BloodManager.Load();
        }

        public override void Unload()
        {
            Instance = null;
            BloodManager.Unload();
        }
    }
}