using System;
using System.Collections.Generic;
using System.Linq;
using Modding;
using MonoMod.Cil;
using UnityEngine;

namespace CarefreeLuck
{
    public class CarefreeLuck : Mod
    {
        internal static CarefreeLuck instance;
        
        public CarefreeLuck() : base(null)
        {
            instance = this;
        }
        
        public override string GetVersion()
        {
            return GetType().Assembly.GetName().Version.ToString();
        }
        
        public override void Initialize()
        {
            Log("Initializing Mod...");

            IL.HeroController.TakeDamage += HeroController_TakeDamage;
        }

        private void HeroController_TakeDamage(ILContext il)
        {
            ILCursor cursor = new(il);

            while (cursor.TryGotoNext(MoveType.After, i => i.MatchCallOrCallvirt(typeof(UnityEngine.Random), "Range")))
            {
                cursor.EmitDelegate<Func<int, int>>(x => 1);
            }
        }
    }
}