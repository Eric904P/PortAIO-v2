using System;
using Activator.Base;
using LeagueSharp.Common;

using EloBuddy; namespace Activator.Spells.Evaders
{
    class judicatorintervention : CoreSpell
    {
        internal override string Name => "judicatorintervention";
        internal override string DisplayName => "Intervention | R";
        internal override float Range => 900f;
        internal override MenuType[] Category => new[] { MenuType.SelfLowHP,  MenuType.Zhonyas };
        internal override int DefaultHP => 10;
        internal override int DefaultMP => 0;

        public override void OnTick(EventArgs args)
        {
            if (!Menu.Item("use" + Name).GetValue<bool>() || !IsReady())
                return;

            foreach (var hero in Activator.Allies())
            {
                if (Parent.Item(Parent.Name + "useon" + hero.Player.NetworkId).GetValue<bool>())
                {
                    if (hero.Player.Distance(Player.ServerPosition) <= Range)
                    {
                        if (hero.Player.Health / hero.Player.MaxHealth * 100 <=
                            Menu.Item("selflowhp" + Name + "pct").GetValue<Slider>().Value)
                            if (hero.IncomeDamage > 0)
                                UseSpellOn(hero.Player);

                        if (Menu.Item("use" + Name + "norm").GetValue<bool>())
                            if (hero.IncomeDamage > 0 && hero.HitTypes.Contains(HitType.Danger))
                                UseSpellOn(hero.Player);

                        if (Menu.Item("use" + Name + "ulti").GetValue<bool>())
                            if (hero.IncomeDamage > 0 && hero.HitTypes.Contains(HitType.Ultimate))
                                UseSpellOn(hero.Player);
                    }
                }
            }
        }
    }
}