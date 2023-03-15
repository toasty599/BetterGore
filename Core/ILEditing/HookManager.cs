using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;

namespace BetterGore.Core.ILEditing
{
    public static class HookManager
    {
        public static List<IHookEdit> Hooks
        {
            get;
            private set;
        } = new List<IHookEdit>();

        internal static void LoadHooks()
        {
            foreach (Type type in BetterGore.Instance.Code.GetTypes())
            {
                if(!type.IsAbstract && type.GetInterfaces().Contains(typeof(IHookEdit)))
                {
                    IHookEdit hook = (IHookEdit)FormatterServices.GetUninitializedObject(type);
                    hook.Load();
                    Hooks.Add(hook);
                }
            }
        }

        internal static void UnloadHooks()
        {
            foreach (IHookEdit hook in Hooks)
                hook.Unload();
        }
    }
}
