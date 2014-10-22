using System;
using System.Linq;
using System.Net.Sockets;
using LeagueSharp;
using LeagueSharp.Common;
using LX_Orbwalker;
using SharpDX;
 
namespace Autocombo
{
 
    internal class Autocombo2
    {
        public static Obj_AI_Hero Player;
        public static Spell Q;
        public static Spell W;
        public static Spell E;
        public static Spell R;
        public static Menu Config;
 
        private static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += OnGameLoad;
        }
        private static void Game_OnGameUpdate(EventArgs args)
        {
            if (LXOrbwalker.CurrentMode.ToString() == "LaneClear" && !UnitUnderCursor().IsMe)
            {
 
                if (Config.Item("UseQ").GetValue<bool>() )
                {
                    Q.CastOnUnit(UnitUnderCursor(), true);
 
                }
                if (Config.Item("UseW").GetValue<bool>())
                {
                    W.CastOnUnit(UnitUnderCursor(), true);
                }
                if (Config.Item("UseE").GetValue<bool>())
                {
                    //E.CastOnUnit(UnitUnderCursor(), true);
                }
                if (Config.Item("UseR").GetValue<bool>())
                {
                    R.CastOnUnit(UnitUnderCursor(), true);
                }
            }
        }
 
        private static void OnGameLoad(EventArgs args)
        {
 
            Game.OnGameUpdate += Game_OnGameUpdate;
            Config = new Menu("XcxooxL Sion", "Sion", true);
            var tsMenu = new Menu("Target Selector", "Target Selector");
            var orbwalkerMenu = new Menu("My Orbwalker", "my_Orbwalker");
            LXOrbwalker.AddToMenu(orbwalkerMenu);
            Config.AddSubMenu(orbwalkerMenu);
 
            SimpleTs.AddToMenu(tsMenu);
            Config.AddSubMenu(tsMenu);
            Config.AddToMainMenu();
            Config.AddSubMenu(new Menu("Combo Settings", "Combo"));
            Config.SubMenu("Combo").AddItem(new MenuItem("UseQ", "Use Q").SetValue(true));
            Config.SubMenu("Combo").AddItem(new MenuItem("UseW", "Use W").SetValue(true));
            Config.SubMenu("Combo").AddItem(new MenuItem("UseE", "Use E").SetValue(true));
            Config.SubMenu("Combo").AddItem(new MenuItem("UseR", "Use R").SetValue(true));
            Player = ObjectManager.Player;
 
            Q = new Spell(SpellSlot.Q, 900);
            W = new Spell(SpellSlot.W, 900);
            E = new Spell(SpellSlot.E, 900);
            R = new Spell(SpellSlot.R, 900);
 
            Game.PrintChat("<font color='#F7A100'>Exploit .</font>");
        }
       
 
 
        private static Obj_AI_Base UnitUnderCursor()
        {
            return ObjectManager
                .Get<Obj_AI_Base>()
                .Where(x => x.IsAlly || x.IsEnemy)
                .FirstOrDefault(unit => Vector2.Distance(Game.CursorPos.To2D(), unit.ServerPosition.To2D()) < 200);
        }
 
    }
}
