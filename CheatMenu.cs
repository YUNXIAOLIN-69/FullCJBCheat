using FullCJBCheat.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FullCJBCheat.UI
{
    public class CheatMenu
    {
        public bool IsOpen;
        private readonly ModConfig _config;
        private int _currentTab;
        private int _selectedControlIndex;
        private int _scrollOffset;
        private const int LineHeight = 36;
        private const int ContentWidth = 520;
        private readonly List<List<IControl>> _tabs = new();

        public CheatMenu(ModConfig config)
        {
            _config = config;
            BuildAllTabs();
        }

        public void HandleControllerInput(GamePadState oldPad, GamePadState newPad)
        {
            // LT 上一页标签
            if (newPad.Triggers.Left > 0.2f && oldPad.Triggers.Left <= 0.2f)
            {
                _currentTab--;
                if (_currentTab < 0) _currentTab = _tabs.Count - 1;
                _selectedControlIndex = 0;
                _scrollOffset = 0;
            }
            // RT 下一页标签
            if (newPad.Triggers.Right > 0.2f && oldPad.Triggers.Right <= 0.2f)
            {
                _currentTab++;
                if (_currentTab >= _tabs.Count) _currentTab = 0;
                _selectedControlIndex = 0;
                _scrollOffset = 0;
            }

            var currentTabList = _tabs[_currentTab];
            // 上下选择吸附
            if (newPad.DPad.Up == ButtonState.Pressed && oldPad.DPad.Up == ButtonState.Released)
            {
                _selectedControlIndex--;
                if (_selectedControlIndex < 0) _selectedControlIndex = currentTabList.Count - 1;
            }
            if (newPad.DPad.Down == ButtonState.Pressed && oldPad.DPad.Down == ButtonState.Released)
            {
                _selectedControlIndex++;
                if (_selectedControlIndex >= currentTabList.Count) _selectedControlIndex = 0;
            }

            var selectedCtrl = currentTabList[_selectedControlIndex];
            if (newPad.DPad.Left == ButtonState.Pressed && oldPad.DPad.Left == ButtonState.Released)
                selectedCtrl.Left();
            if (newPad.DPad.Right == ButtonState.Pressed && oldPad.DPad.Right == ButtonState.Released)
                selectedCtrl.Right();

            if (newPad.Buttons.A == ButtonState.Pressed && oldPad.Buttons.A == ButtonState.Released)
                selectedCtrl.Activate();

            if (newPad.Buttons.B == ButtonState.Pressed && oldPad.Buttons.B == ButtonState.Released)
                IsOpen = false;

            for (int i = 0; i < currentTabList.Count; i++)
                currentTabList[i].Selected = i == _selectedControlIndex;
        }

        private void BuildAllTabs()
        {
            // Tab0 玩家&工具
            List<IControl> tab0 = new()
            {
                new ToggleControl("无限生命", _config.InfiniteHealth, v => _config.InfiniteHealth = v),
                new ToggleControl("无限体力", _config.InfiniteStamina, v => _config.InfiniteStamina = v),
                new ToggleControl("武器无冷却", _config.WeaponNoCooldown, v => _config.WeaponNoCooldown = v),
                new ToggleControl("怪物一击必杀", _config.OneHitMonster, v => _config.OneHitMonster = v),
                new ToggleControl("资源一击破坏", _config.OneHitBreak, v => _config.OneHitBreak = v),
                new ToggleControl("每日最大运气", _config.MaxDailyLuck, v => _config.MaxDailyLuck = v),
                new SliderControl("移动速度倍率", _config.MoveSpeedMulti, 0.5f, 3f, val => _config.MoveSpeedMulti = val),
                new SliderControl("背包格子数量", _config.BackpackSize, 12, 120, val => _config.BackpackSize = (int)val),
                new ToggleControl("喷壶无限水量", _config.InfiniteWaterCan, v => _config.InfiniteWaterCan = v),
                new ToggleControl("镰刀收割全部作物", _config.ScytheAllCrop, v => _config.ScytheAllCrop = v)
            };
            _tabs.Add(tab0);

            // Tab1 农场&钓鱼
            List<IControl> tab1 = new()
            {
                new ToggleControl("自动浇水", _config.AutoWater, v => _config.AutoWater = v),
                new ToggleControl("围栏永不腐烂", _config.NoFenceDecay, v => _config.NoFenceDecay = v),
                new ToggleControl("建筑瞬间完工", _config.InstantBuild, v => _config.InstantBuild = v),
                new ToggleControl("自动抚摸动物", _config.AutoPetAnimal, v => _config.AutoPetAnimal = v),
                new ToggleControl("无限干草", _config.InfiniteHay, v => _config.InfiniteHay = v),
                new ToggleControl("钓鱼立刻上钩", _config.InstantCatchFish, v => _config.InstantCatchFish = v),
                new ToggleControl("钓鱼必出宝箱", _config.FishAlwaysTreasure, v => _config.FishAlwaysTreasure = v),
                new ToggleControl("工具永不损坏", _config.ToolNoBreak, v => _config.ToolNoBreak = v),
                new ToggleControl("机器瞬间产出", _config.InstantMachine, v => _config.InstantMachine = v),
                new ToggleControl("果树立刻结果", _config.InstantFruitTree, v => _config.InstantFruitTree = v),
                new ToggleControl("作物瞬间成熟", _config.InstantCropGrow, v => _config.InstantCropGrow = v)
            };
            _tabs.Add(tab1);

            // Tab2 技能
            List<IControl> tab2 = new()
            {
                new SliderControl("耕种等级", _config.FarmingLevel, 0, 10, val => _config.FarmingLevel = (int)val),
                new SliderControl("采矿等级", _config.MiningLevel, 0, 10, val => _config.MiningLevel = (int)val),
                new SliderControl("采集等级", _config.ForagingLevel, 0, 10, val => _config.ForagingLevel = (int)val),
                new SliderControl("钓鱼等级", _config.FishingLevel, 0, 10, val => _config.FishingLevel = (int)val),
                new SliderControl("战斗等级", _config.CombatLevel, 0, 10, val => _config.CombatLevel = (int)val)
            };
            _tabs.Add(tab2);

            // Tab3 天气
            List<IControl> tab3 = new()
            {
                new DropdownControl("明日天气", new List<string>(){"晴天","雨天","雷雨","下雪","沙漠雨"}, 0, idx =>
                {
                    var opts = new List<string>(){"晴天","雨天","雷雨","下雪","沙漠雨"};
                    _config.TomorrowWeather = opts[idx];
                }),
                new DropdownControl("当前天气", new List<string>(){"晴天","雨天","雷雨","下雪","沙漠雨"},0, idx =>
                {
                    var opts = new List<string>(){"晴天","雨天","雷雨","下雪","沙漠雨"};
                    _config.CurrentWeather = opts[idx];
                })
            };
            _tabs.Add(tab3);

            // Tab4 人际关系
            List<IControl> tab4 = new()
            {
                new ToggleControl("送礼无每日上限", _config.GiftNoLimit, v => _config.GiftNoLimit = v),
                new ToggleControl("好感永不衰减", _config.FriendshipNoDecay, v => _config.FriendshipNoDecay = v),
                new SliderControl("统一NPC好感数值", _config.TargetFriendship, 0, 2500, val => _config.TargetFriendship = (int)val)
            };
            _tabs.Add(tab4);

            // Tab5 时间
            List<IControl> tab5 = new()
            {
                new ToggleControl("全局冻结时间", _config.FreezeGlobalTime, v => _config.FreezeGlobalTime = v),
                new ToggleControl("矿井内冻结时间", _config.FreezeMineTime, v => _config.FreezeMineTime = v),
                new ToggleControl("房屋内冻结时间", _config.FreezeBuildingTime, v => _config.FreezeBuildingTime = v),
                new SliderControl("指定当日时间", _config.TargetDayTime, 600, 2600, val => _config.TargetDayTime = (int)val),
                new SliderControl("修改日期", _config.TargetDay, 1, 28, val => _config.TargetDay = (int)val),
                new DropdownControl("修改季节", new List<string>(){"春季","夏季","秋季","冬季"},0, idx =>
                {
                    var s = new List<string>(){"春季","夏季","秋季","冬季"};
                    _config.TargetSeason = s[idx];
                }),
                new SliderControl("修改年份", _config.TargetYear, 1, 100, val => _config.TargetYear = (int)val)
            };
            _tabs.Add(tab5);

            // Tab6 高级解锁
            List<IControl> tab6 = new()
            {
                new ToggleControl("一键完成全部任务", _config.CompleteAllQuests, v => _config.CompleteAllQuests = v),
                new ToggleControl("解锁全部钱包道具", _config.UnlockAllWalletItems, v => _config.UnlockAllWalletItems = v),
                new ToggleControl("解锁全部地图区域", _config.UnlockAllAreas, v => _config.UnlockAllAreas = v),
                new ToggleControl("完成社区中心全部收集包", _config.CompleteCommunityCenter, v => _config.CompleteCommunityCenter = v),
                new ToggleControl("开启Joja完整改造", _config.CompleteJoja, v => _config.CompleteJoja = v)
            };
            _tabs.Add(tab6);

            // Tab7 货币数值
            List<IControl> tab7 = new()
            {
                new SliderControl("单次添加金币", _config.AddMoneyAmount, 1000, 1000000, val => _config.AddMoneyAmount = (int)val),
                new SliderControl("单次齐币数量", _config.AddQiCoins, 100, 10000, val => _config.AddQiCoins = (int)val),
                new SliderControl("单次金核桃数量", _config.AddGoldenWalnut, 1, 100, val => _config.AddGoldenWalnut = (int)val)
            };
            _tabs.Add(tab7);

            // Tab8 菜单设置
            List<IControl> tab8 = new()
            {
                new DropdownControl("菜单打开快捷键", new List<string>(){"F1","F2","P","O"},0, idx =>
                {
                    var keys = new List<string>(){"F1","F2","P","O"};
                    _config.OpenMenuHotkey = keys[idx];
                }),
                new SliderControl("列表滚动速度", _config.ScrollSpeed, 2, 30, val => _config.ScrollSpeed = (int)val),
                new ButtonControl("重置全部作弊配置", () =>
                {
                    _config = new ModConfig();
                })
            };
            _tabs.Add(tab8);

            // Tab9 传送页【置顶联机玩家传送】
            List<IControl> tab9 = new();
            // 置顶联机玩家区域
            tab9.Add(new ToggleControl("========= 联机玩家传送 =========", false, _ => { }));
            if (Game1.IsMultiplayer && Game1.otherFarmers.Any())
            {
                foreach (var farmer in Game1.otherFarmers.Values)
                {
                    var target = farmer;
                    tab9.Add(new ButtonControl($"传送到 {target.Name}", () =>
                    {
                        Game1.player.currentLocation = target.currentLocation;
                        Game1.player.Position = target.Position;
                    }));
                }
            }
            else
            {
                tab9.Add(new ToggleControl("当前无联机玩家在线", false, _ => { }));
            }
            // 分割线
            tab9.Add(new ToggleControl("========= 地图传送点 =========", false, _ => { }));
            // 原版全部传送点
            foreach (var warp in WarpData.AllWarps)
            {
                string locId = warp.Value;
                tab9.Add(new ButtonControl($"传送至 {warp.Key}", () =>
                {
                    Game1.warpFarmer(locId, 30, 10, false);
                }));
            }
            _tabs.Add(tab9);
        }

        public void Draw(SpriteBatch batch)
        {
            if (!IsOpen) return;
            Rectangle bgRect = new Rectangle(Game1.viewport.Width / 2 - ContentWidth / 2, 80, ContentWidth, 480);
            batch.Draw(Game1.staminaRect, bgRect, Color.Black * 0.85f);

            string tabHint = $"LT切换上标签 | RT切换下标签 | 当前分类：{_currentTab}";
            batch.DrawString(Game1.dialogueFont, tabHint, new Vector2(bgRect.X + 10, bgRect.Y + 8), Color.White);

            int drawY = bgRect.Y + 40 - _scrollOffset;
            var list = _tabs[_currentTab];
            for (int i = 0; i < list.Count; i++)
            {
                var ctrl = list[i];
                ctrl.Bounds = new Rectangle(bgRect.X + 10, drawY, ContentWidth - 20, LineHeight);
                ctrl.Draw(batch);
                drawY += LineHeight;
            }
        }
    }
}
