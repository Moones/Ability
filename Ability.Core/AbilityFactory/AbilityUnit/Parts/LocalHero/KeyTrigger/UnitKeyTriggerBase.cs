using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ability.Core.AbilityFactory.AbilityUnit.Parts.LocalHero.KeyTrigger
{
    using Ability.Core.AbilityFactory.Utilities;
    using Ability.Core.MenuManager.Menus.AbilityMenu.Items;

    using Ensage.Common.Menu;

    public class UnitKeyTriggerBase : IUnitKeyTrigger
    {
        private DataObserver<KeyBind> keyBindObserver;
        public UnitKeyTriggerBase(IAbilityUnit unit)
        {
            this.Unit = unit;
        }

        public virtual void Dispose()
        {
        }

        public IAbilityUnit Unit { get; set; }

        public virtual void Initialize()
        {
            this.keyBindObserver = new DataObserver<KeyBind>(
                bind =>
                    {
                        if (bind.Active)
                        {
                            this.Activate();
                        }
                        else
                        {
                            this.Deactivate();
                        }
                    });
        }

        public bool Activated { get; private set; }

        public virtual void Activate()
        {
            this.Activated = true;
        }

        public virtual void Deactivate()
        {
            this.Activated = false;
        }

        private Dictionary<AbilityMenuItem<KeyBind>, IDisposable> keyBinds = new Dictionary<AbilityMenuItem<KeyBind>, IDisposable>();

        public void AddKeyBind(AbilityMenuItem<KeyBind> keyBind)
        {
            this.keyBinds.Add(keyBind, keyBind.NewValueProvider.Subscribe(this.keyBindObserver));
            if (keyBind.Value.Active)
            {
                this.Activate();
            }
        }

        public void RemoveKeyBind(AbilityMenuItem<KeyBind> keyBind)
        {
            this.keyBinds[keyBind].Dispose();
            this.keyBinds.Remove(keyBind);
            if (keyBind.Value.Active)
            {
                this.Deactivate();
            }
        }
    }
}
