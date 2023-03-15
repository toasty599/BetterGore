using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace BetterGore.Core.Particles
{
    public static class BloodManager
    {
        internal static List<Blood> BloodParticles;

        public static Texture2D BloodTexture
        { 
            get;
            private set;
        }

        internal static void Load()
        {
            BloodParticles = new();
            BloodTexture = ModContent.Request<Texture2D>("BetterGore/Assets/Textures/Blood", AssetRequestMode.ImmediateLoad).Value;
            On.Terraria.Main.SortDrawCacheWorms += DrawParticles;
        }


        internal static void Unload()
        {
            BloodParticles = null;
            BloodTexture = null;
            On.Terraria.Main.SortDrawCacheWorms -= DrawParticles;
        }

        internal static void UpdateParticles()
        {
            foreach (var blood in BloodParticles)
                blood.Update();

            BloodParticles.RemoveAll(b => b.Timer > b.MaxTime);
        }

        public static void SpawnParticle(Vector2 center, Vector2 velocity, Vector2 size) => BloodParticles.Add(new(center, velocity, size));

        private static void DrawParticles(On.Terraria.Main.orig_SortDrawCacheWorms orig, Main self)
        {
            orig(self);
            
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);

            foreach (var ball in BloodParticles)
                ball.Draw(Main.spriteBatch);
            
            Main.spriteBatch.End();
        }
    }
}
