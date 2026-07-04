using FullCJBCheat.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Monsters;
using System.Linq;

namespace FullCJBCheat
{
    public class ModEntry : Mod
    {
        private ModConfig _config = null!;
        private CheatMenu _menu = null!;
        private GamePadState _lastPadState;

        public override void Entry(IModHelper helper)
        {
            _config = Helper.ReadConfig<ModConfig>();
            _menu = new CheatMenu(_config);
            _lastPadState = GamePad.GetState(PlayerIndex.One);

            Helper.Events.GameLoop.UpdateTicked += OnUpdateTicked;
            Helper.Events.Input.ButtonPressed += OnButtonPressed;
            Helper.Events.GameLoop.SaveLoaded += OnSaveLoaded;
            Helper.Events.GameLoop.GameLaunched += OnGameLaunched;

            Monitor.Log("完整CJB手柄作弊菜单加载完成，按P打开菜单", LogLevel.Info);
        }

        private void OnGameLaunched(object sender, GameLaunchedEventArgs e)
        {
            var gmcm = Helper.ModRegistry.GetApi<IGenericModConfigMenuApi>("spacechase0.GenericModConfigMenu");
            if (gmcm == null) return;
            gmcm.Register(ModManifest, reset: () => _config = new ModConfig(), save: () => Helper.WriteConfig(_config));
        }

        private void OnSaveLoaded(object sender, SaveLoadedEventArgs e)
        {
            _config = Helper.ReadConfig<ModConfig>();
            _menu = new CheatMenu(_config);
        }

        private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (!Context.IsWorldReady) return;
            if (e.Button.ToString() == _config.OpenMenuHotkey)
            {
                _menu.IsOpen = !_menu.IsOpen;
                Helper.Input.Suppress(e.Button);
            }
        }

        private void OnUpdateTicked(object sender, UpdateTickedEventArgs e)
        {
            if (!Context.IsWorldReady) return;
            RunAllCheatEffects();

            var pad = GamePad.GetState(PlayerIndex.One);
            if (_menu.IsOpen)
            {
                _menu.HandleControllerInput(_lastPadState, pad);
            }
            _lastPadState = pad;
        }

        private void RunAllCheatEffects()
        {
            var player = Game1.player;
            var loc = Game1.currentLocation;

            // 玩家常驻作弊
            if (_config.InfiniteHealth) player.health = player.maxHealth;
            if (_config.InfiniteStamina) player.Stamina = player.MaxStamina;
            player.movementSpeed = _config.MoveSpeedMulti;

            if (_config.MaxDailyLuck) Game1.player.DailyLuck = 9999;
            player.MaxInventorySpace = _config.BackpackSize;

            // 怪物无敌仇恨/一击杀
            foreach (var m in loc.characters.OfType<Monster>())
            {
                if (_config.OneHitMonster) m.Health = 0;
            }

            // 时间冻结
            if (_config.FreezeGlobalTime)
                Game1.timeOfDay = _config.TargetDayTime;
            if (_config.FreezeMineTime && loc is Mine)
                Game1.timeOfDay = _config.TargetDayTime;
            if (_config.FreezeBuildingTime && loc.IsBuildingInterior)
                Game1.timeOfDay = _config.TargetDayTime;
        }
    }
}
