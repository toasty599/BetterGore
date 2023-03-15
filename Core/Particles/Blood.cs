using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using static BetterGore.Core.Particles.BloodManager;

namespace BetterGore.Core.Particles
{
    public class Blood
    {
		public int Timer;
		public int MaxTime;

		public const int FadeInTime = 20;
		public const int FadeOutTime = 120;

		public float Rotation;
        public Vector2 Center;
        public Vector2 Velocity;
		public Vector2 MaxSize;
		public Vector2 Size;

		public List<Vector2> OldPositions;

        public static Color Color => Color.DarkRed;

		public Vector2 TopLeft => Center + new Vector2(-1f, -1f) * Size * 0.5f;

		public Vector2 TopRight => Center + new Vector2(1f, -1f) * Size * 0.5f;

		public Vector2 BottomLeft => Center + new Vector2(-1f, 1f) * Size * 0.5f;

		public Vector2 BottomRight => Center + new Vector2(1f, 1f) * Size * 0.5f;

		public Blood(Vector2 center, Vector2 velocity, Vector2 size)
		{
			Center = center;
			Velocity = velocity;
			Timer = 0;
			MaxTime = Main.rand.Next(360, 1000);
			MaxSize = size;
			Size = Vector2.Zero;
			OldPositions = new();
			Rotation = 0f;
		}

		public void Update()
        {
			if (!CollidingWithTiles())
				Center += Velocity;

			float fallSpeed = 0.15f * Utils.GetLerpValue(0f, 35f, Timer, true);
			Velocity.Y += fallSpeed;

			if (Timer <= FadeInTime)
				Size = Vector2.Lerp(Vector2.Zero, MaxSize, (float)Timer / FadeInTime);
			else if (Timer >= MaxTime - FadeOutTime)
				Size = Vector2.Lerp(MaxSize, Vector2.Zero, (float)(Timer - (MaxTime - FadeOutTime)) / FadeOutTime);

			if (OldPositions.Count >= 10)
				OldPositions.RemoveAt(0);
			OldPositions.Add(Center);

			Rotation = Velocity.ToRotation();


			Timer++;
        }

		public void Draw(SpriteBatch spriteBatch)
        {
			//Main.spriteBatch.Draw(BloodTexture, Center - Main.screenPosition, null, Color, 0f, BloodTexture.Size() * 0.5f, Size, SpriteEffects.None, 0f);

			for (int i = 0; i < OldPositions.Count; i++)
            {
				float scale = ((float)i / OldPositions.Count);
				Color drawColor = Color.Lerp(Color.Red, Color, scale);
				Main.spriteBatch.Draw(BloodTexture, Vector2.Lerp(OldPositions[i], Center, 0.65f) - Main.screenPosition, null, drawColor, Rotation, BloodTexture.Size() * 0.5f, Size * scale, SpriteEffects.None, 0f);
			}
		}

		public bool CollidingWithTiles() => Collision.SolidTiles(Center, (int)Size.X, (int)Size.Y) || Collision.tile;
	}
}
