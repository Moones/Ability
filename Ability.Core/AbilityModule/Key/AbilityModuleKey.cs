using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityModule.Key
{
    public class AbilityModuleKey : IDisposable
    {
        public AbilityModuleKey(Action keyDown, Action keyUp)
        {
            
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
