using KitchenLib;
using KitchenLib.Logging;
using KitchenLib.Logging.Exceptions;
using KitchenMods;
using System.Linq;
using System.Reflection;
using UnityEngine;
using KitchenLogger = KitchenLib.Logging.KitchenLogger;
using KitchenLib.Interfaces;
using KitchenLib.Event;
using KitchenLib.Utils;
using KitchenLib.References;
using KitchenData;
using KitchenMysteryMeat.Components;
using TMPro;
using KitchenMysteryMeat.Customs.Processes;
using System.Collections.Generic;
using System;
using Kitchen;
using KitchenMysteryMeat.Views;

namespace KitchenMysteryMeat
{
    public class Mod : BaseMod, IModSystem, IAutoRegisterAll
    {
        public const string MOD_GUID = "com.quackandcheese.mysterymeat";
        public const string MOD_NAME = "Mystery Meat";
        public const string MOD_VERSION = "0.1.1";
        public const string MOD_AUTHOR = "QuackAndCheese";
        public const string MOD_GAMEVERSION = ">=1.1.9";

        internal static Mod Instance;
        internal static AssetBundle Bundle;
        internal static KitchenLogger Logger;
        internal static AssetDirectory VanillaAssetDirectory => Instance.AssetDirectory;

        public Mod() : base(MOD_GUID, MOD_NAME, MOD_AUTHOR, MOD_VERSION, MOD_GAMEVERSION, Assembly.GetExecutingAssembly()) 
        {
            Instance = this;
        }

        public static SoundEvent StabSoundEvent;
        public static SoundEvent PoisonSoundEvent;
        public static SoundEvent AlertSoundEvent;
        public static GameObject WantedDisplayPrefab;

        protected override void OnInitialise()
        {
            Logger.LogWarning($"{MOD_GUID} v{MOD_VERSION} in use!");
        }

        protected override void OnUpdate()
        {
        }

        protected override void OnPostActivate(KitchenMods.Mod mod)
        {
            Bundle = mod.GetPacks<AssetBundleModPack>().SelectMany(e => e.AssetBundles).FirstOrDefault() ?? throw new MissingAssetBundleException(MOD_GUID);
            Logger = InitLogger();

            Bundle.LoadAllAssets<Texture2D>();
            Bundle.LoadAllAssets<Sprite>();
            var spriteAsset = Bundle.LoadAsset<TMP_SpriteAsset>("GrindMeat");
            TMP_Settings.defaultSpriteAsset.fallbackSpriteAssets.Add(spriteAsset);
            spriteAsset.material = UnityEngine.Object.Instantiate(TMP_Settings.defaultSpriteAsset.material);
            spriteAsset.material.mainTexture = Bundle.LoadAsset<Texture2D>("GrindMeatTex");

            Events.BuildGameDataEvent += delegate (object s, BuildGameDataEventArgs args)
            {
                //((Item)GDOUtils.GetExistingGDO(ItemReferences.SharpKnife)).Properties.Add(new CKillsCustomer());
                ((Item)GDOUtils.GetExistingGDO(ItemReferences.Mince)).DerivedProcesses.Add(new Item.ItemProcess()
                {
                    Process = (Process)GDOUtils.GetExistingGDO(ProcessReferences.Knead),
                    Result = (Item)GDOUtils.GetExistingGDO(ItemReferences.BurgerPattyRaw),
                    Duration = 0.75f
                });

                SetupSFX(args.gamedata);
            };
        }

        private void SetupSFX(GameData gameData)
        {
            #region Stab
            StabSoundEvent = (SoundEvent)VariousUtils.GetID(MOD_GUID + "-STAB");

            if (!gameData.ReferableObjects.Clips.ContainsKey(StabSoundEvent))
                gameData.ReferableObjects.Clips.Add(StabSoundEvent, new AudioAssetRandom());

            var stab1 = Bundle.LoadAsset<AudioClip>("stab-01"); stab1.LoadAudioData();
            var stab2 = Bundle.LoadAsset<AudioClip>("stab-02"); stab2.LoadAudioData();
            var stab3 = Bundle.LoadAsset<AudioClip>("stab-03"); stab3.LoadAudioData();

            typeof(AudioAssetRandom)
                .GetField("Clips", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(gameData.ReferableObjects.Clips[StabSoundEvent], new List<AudioClip>() { stab1, stab2, stab3 });
            #endregion

            #region Poison
            PoisonSoundEvent = (SoundEvent)VariousUtils.GetID(MOD_GUID + "-POISON");

            if (!gameData.ReferableObjects.Clips.ContainsKey(PoisonSoundEvent))
                gameData.ReferableObjects.Clips.Add(PoisonSoundEvent, new AudioAsset());

            var poison1 = Bundle.LoadAsset<AudioClip>("blub"); poison1.LoadAudioData();

            typeof(AudioAsset)
                .GetField("Clip", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(gameData.ReferableObjects.Clips[PoisonSoundEvent], poison1);
            #endregion

            #region Alert
            AlertSoundEvent = (SoundEvent)VariousUtils.GetID(MOD_GUID + "-ALERT");

            if (!gameData.ReferableObjects.Clips.ContainsKey(AlertSoundEvent))
                gameData.ReferableObjects.Clips.Add(AlertSoundEvent, new AudioAsset());

            var alert1 = Bundle.LoadAsset<AudioClip>("alert"); alert1.LoadAudioData();

            typeof(AudioAsset)
                .GetField("Clip", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(gameData.ReferableObjects.Clips[AlertSoundEvent], alert1);
            #endregion
        }
    }
}