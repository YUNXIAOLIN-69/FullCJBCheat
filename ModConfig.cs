using System.Collections.Generic;

namespace FullCJBCheat
{
    public class ModConfig
    {
        #region 玩家&工具
        public bool InfiniteHealth { get; set; } = false;
        public bool InfiniteStamina { get; set; } = false;
        public bool WeaponNoCooldown { get; set; } = false;
        public bool OneHitMonster { get; set; } = false;
        public bool OneHitBreak { get; set; } = false;
        public bool MaxDailyLuck { get; set; } = false;
        public float MoveSpeedMulti { get; set; } = 1f;
        public int BackpackSize { get; set; } = 36;
        public bool InfiniteWaterCan { get; set; } = false;
        public bool ScytheAllCrop { get; set; } = false;
        #endregion

        #region 农场&钓鱼
        public bool AutoWater { get; set; } = false;
        public bool NoFenceDecay { get; set; } = false;
        public bool InstantBuild { get; set; } = false;
        public bool AutoPetAnimal { get; set; } = false;
        public bool InfiniteHay { get; set; } = false;
        public bool InstantCatchFish { get; set; } = false;
        public bool FishAlwaysTreasure { get; set; } = false;
        public bool ToolNoBreak { get; set; } = false;
        public bool InstantMachine { get; set; } = false;
        public bool InstantFruitTree { get; set; } = false;
        public bool InstantCropGrow { get; set; } = false;
        #endregion

        #region 技能
        public int FarmingLevel { get; set; } = 0;
        public int MiningLevel { get; set; } = 0;
        public int ForagingLevel { get; set; } = 0;
        public int FishingLevel { get; set; } = 0;
        public int CombatLevel { get; set; } = 0;
        #endregion

        #region 天气
        public string TomorrowWeather { get; set; } = "晴天";
        public string CurrentWeather { get; set; } = "晴天";
        #endregion

        #region 人际关系
        public bool GiftNoLimit { get; set; } = false;
        public bool FriendshipNoDecay { get; set; } = false;
        public int TargetFriendship { get; set; } = 2500;
        #endregion

        #region 时间
        public bool FreezeGlobalTime { get; set; } = false;
        public bool FreezeMineTime { get; set; } = false;
        public bool FreezeBuildingTime { get; set; } = false;
        public int TargetDayTime { get; set; } = 600;
        public int TargetDay { get; set; } = 1;
        public string TargetSeason { get; set; } = "春季";
        public int TargetYear { get; set; } = 1;
        #endregion

        #region 高级解锁
        public bool CompleteAllQuests { get; set; } = false;
        public bool UnlockAllWalletItems { get; set; } = false;
        public bool UnlockAllAreas { get; set; } = false;
        public bool CompleteCommunityCenter { get; set; } = false;
        public bool CompleteJoja { get; set; } = false;
        #endregion

        #region 货币数值
        public int AddMoneyAmount { get; set; } = 100000;
        public int AddQiCoins { get; set; } = 1000;
        public int AddGoldenWalnut { get; set; } = 10;
        #endregion

        #region 菜单设置
        public string OpenMenuHotkey { get; set; } = "P";
        public int ScrollSpeed { get; set; } = 10;
        #endregion

        public List<string> SavedWarps { get; set; } = new();
    }
}
