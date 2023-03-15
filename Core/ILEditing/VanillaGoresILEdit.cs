using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterGore.Core.ILEditing
{
    public class VanillaGoresILEdit : IHookEdit
    {
        public void Load() => On.Terraria.Gore.NewGore_IEntitySource_Vector2_Vector2_int_float += ReplaceGores;

        public void Unload() => On.Terraria.Gore.NewGore_IEntitySource_Vector2_Vector2_int_float += ReplaceGores;

        private int ReplaceGores(On.Terraria.Gore.orig_NewGore_IEntitySource_Vector2_Vector2_int_float orig, Terraria.DataStructures.IEntitySource source, Microsoft.Xna.Framework.Vector2 Position, Microsoft.Xna.Framework.Vector2 Velocity, int Type, float Scale)
        {
            return orig(source, Position, Velocity, Type, Scale);
        }
    }
}
