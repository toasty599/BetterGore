using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterGore.Core.ILEditing
{
    public interface IHookEdit
    {
        public void Load();

        public void Unload();
    }
}
