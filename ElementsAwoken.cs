using ElementsAwoken.NPCs.Bosses.Aqueous;
using ElementsAwoken.NPCs.Bosses.Infernace;
using ElementsAwoken.NPCs.Bosses.Permafrost;
using ElementsAwoken.NPCs.Bosses.Regaroth;
using ElementsAwoken.NPCs.Bosses.TheGuardian;
using ElementsAwoken.NPCs.Bosses.VoidLeviathan;
using ElementsAwoken.NPCs.Bosses.TheCelestial;
using ElementsAwoken.NPCs.VoidEventEnemies;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria.UI;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Shaders;
using Terraria.Graphics;
using Terraria.Localization;
using ReLogic.Graphics;
using System.Reflection;
using Terraria.UI.Chat;
using ElementsAwoken.ScreenEffects;

namespace ElementsAwoken
{
    public class ElementsAwoken : Mod
    {
        public static DynamicSpriteFont encounterFont;

        public static ModHotKey neovirtuo;
        public static ModHotKey specialAbility;
        public static ModHotKey armorAbility;
        public static ModHotKey dash2;

        public static ElementsAwoken instance;

        public static bool calamityEnabled;
        public static bool bossChecklistEnabled;
        public static bool ancientsAwakenedEnabled;
        public static bool eaMusicEnabled;
        public static bool eaRetroMusicEnabled;

        public static float screenshakeAmount = 0; // dont go above 15 unless you want to have a seizure
        private static int screenshakeTimer = 0;

        //public static Dictionary<int, Color> RarityColors = new Dictionary<int, Color>();

        public static Texture2D AADeathBall;
        public static Texture2D insanityTex;

        public static List<int> instakillImmune = new List<int>();

        public ElementsAwoken()
        {
            Properties = new ModProperties()
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true
            };
        }
        public static void PremultiplyTexture(Texture2D texture)
        {
            Color[] buffer = new Color[texture.Width * texture.Height];
            texture.GetData(buffer);
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = Color.FromNonPremultiplied(buffer[i].R, buffer[i].G, buffer[i].B, buffer[i].A);
            }
            texture.SetData(buffer);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.JungleRose);
            recipe.AddIngredient(ItemID.ManaCrystal, 5);
            recipe.SetResult(ItemID.NaturesGift);
            recipe.AddTile(TileID.WorkBenches);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.ManaCrystal, 5);
            recipe.AddIngredient(ItemID.HellstoneBar, 8);
            recipe.AddIngredient(ItemID.NaturesGift, 1);
            recipe.SetResult(ItemID.CelestialMagnet, 1);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(null, "Stardust", 5);
            recipe.SetResult(ItemID.FallenStar, 1);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.LifeCrystal, 1);
            recipe.AddRecipeGroup("IronBar", 12);
            recipe.AddIngredient(ItemID.Ruby, 1);
            recipe.SetResult(ItemID.BandofRegeneration, 1);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.ManaCrystal, 1);
            recipe.AddRecipeGroup("IronBar", 12);
            recipe.AddIngredient(ItemID.Sapphire, 1);
            recipe.SetResult(ItemID.BandofRegeneration, 1);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.HellstoneBar, 12);
            recipe.AddIngredient(ItemID.SoulofLight, 4);
            recipe.AddIngredient(null, "MagmaCrystal", 5);
            recipe.SetResult(ItemID.LavaCharm, 1);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddRecipeGroup("IronBar", 12);
            recipe.AddIngredient(ItemID.Bone, 16);
            recipe.SetResult(ItemID.WaterWalkingBoots, 1);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(null, "SunFragment", 5);
            recipe.AddIngredient(ItemID.StoneBlock, 100);
            recipe.SetResult(ItemID.LihzahrdBrick, 100);
            recipe.AddRecipe();
        }
        public override void AddRecipeGroups()
        {
            //RecipeGroup group = new RecipeGroup(() => Lang.misc[37] + (" Wings"), new int[]

            RecipeGroup group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + (" Wings"), new int[]
            {
                492,
                493,
                665,
                749,
                761,
                785,
                786,
                821,
                822,
                823,
                948,
                1162,
                1165,
                1515,
                1583,
                1584,
                1585,
                1586,
                1797,
                1830,
                1871,
                2280,
                2494,
                2609,
                2770,
                3468,
                3469,
                3470,
                3471,
                3580,
                3582,
                3588,
                3592,
                ItemType("VoidWings"),
                ItemType("BubblePack"),
                ItemType("SkylineWings")
            });
            RecipeGroup.RegisterGroup("WingGroup", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + (" Lunar Wings"), new int[]
            {
                3468,
                3469,
                3470,
                3471
            });
            RecipeGroup.RegisterGroup("LunarWings", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + (" Silver or Tungsten Sword"), new int[]
            {
                ItemID.SilverBroadsword,
                ItemID.TungstenBroadsword,
            });
            RecipeGroup.RegisterGroup("SilverSword", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + (" Sand"), new int[]
            {
                ItemID.SandBlock,
                ItemID.CrimsandBlock,
                ItemID.EbonsandBlock,
                ItemID.PearlsandBlock,
            });
            RecipeGroup.RegisterGroup("SandGroup", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + (" Sandstone"), new int[]
            {
                ItemID.Sandstone,
                ItemID.CrimsonSandstone,
                ItemID.CorruptSandstone,
                ItemID.HallowSandstone,
            });
            RecipeGroup.RegisterGroup("SandstoneGroup", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + (" Ice Block"), new int[]
            {
                ItemID.IceBlock,
                ItemID.RedIceBlock,
                ItemID.PurpleIceBlock,
                ItemID.PinkIceBlock,
            });
            RecipeGroup.RegisterGroup("IceGroup", group);

            // bars
            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + (" Copper Bar"), new int[]
            {
                ItemID.CopperBar,
                ItemID.TinBar,
            });
            RecipeGroup.RegisterGroup("CopperBar", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + (" Silver Bar"), new int[]
            {
                ItemID.SilverBar,
                ItemID.TungstenBar,
            });
            RecipeGroup.RegisterGroup("SilverBar", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + (" Gold Bar"), new int[]
            {
                ItemID.GoldBar,
                ItemID.PlatinumBar,
            });
            RecipeGroup.RegisterGroup("GoldBar", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + (" Evil Bar"), new int[]
            {
                ItemID.DemoniteBar,
                ItemID.CrimtaneBar,
            });
            RecipeGroup.RegisterGroup("EvilBar", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + (" Cobalt Bar"), new int[]
            {
                ItemID.CobaltBar,
                ItemID.PalladiumBar,
            });
            RecipeGroup.RegisterGroup("CobaltBar", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + (" Mythril Bar"), new int[]
            {
                ItemID.MythrilBar,
                ItemID.OrichalcumBar,
            });
            RecipeGroup.RegisterGroup("MythrilBar", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + (" Adamantite Bar"), new int[]
            {
                ItemID.AdamantiteBar,
                ItemID.TitaniumBar,
            });
            RecipeGroup.RegisterGroup("AdamantiteBar", group);
        }
        // screenshaders, boss heads, config, loaded mod checks and hotkeys
        public override void Unload()
        {
            instance = null;
            AADeathBall = null;
            Main.OnTick += Update;
            if (!Main.dedServ)
            {
                Main.heartTexture = Main.instance.OurLoad<Texture2D>("Images" + Path.DirectorySeparatorChar.ToString() + "Heart");
                Main.heart2Texture = Main.instance.OurLoad<Texture2D>("Images" + Path.DirectorySeparatorChar.ToString() + "Heart2");
                Main.manaTexture = Main.instance.OurLoad<Texture2D>("Images" + Path.DirectorySeparatorChar.ToString() + "Mana");
                Main.rainTexture = Main.instance.OurLoad<Texture2D>("Images" + Path.DirectorySeparatorChar.ToString() + "Rain");

                Main.tileSpelunker[TileID.LunarOre] = false;

                ResetCloudTexture();
            }
        }

        public override void Load()
        {
            Main.OnTick -= Update;

            calamityEnabled = ModLoader.GetMod("CalamityMod") != null;
            bossChecklistEnabled = ModLoader.GetMod("BossChecklist") != null;
            ancientsAwakenedEnabled = ModLoader.GetMod("AncientsAwakened") != null;
            eaMusicEnabled = ModLoader.GetMod("EAMusic") != null;
            eaRetroMusicEnabled = ModLoader.GetMod("EARetroMusic") != null;

            instance = this;

            AddBossHeadTexture("ElementsAwoken/NPCs/Bosses/TheCelestial/TheCelestial_Head_Boss_1");
            AddBossHeadTexture("ElementsAwoken/NPCs/Bosses/TheCelestial/TheCelestial_Head_Boss_2");
            AddBossHeadTexture("ElementsAwoken/NPCs/Bosses/TheCelestial/TheCelestial_Head_Boss_3");
            AddBossHeadTexture("ElementsAwoken/NPCs/Bosses/TheCelestial/TheCelestial_Head_Boss_4");
            //HOTKEYS
            neovirtuo = RegisterHotKey("Neovirtuo", "C");
            specialAbility = RegisterHotKey("Special Ability", "Z");
            armorAbility = RegisterHotKey("Armor Ability", "X");
            dash2 = RegisterHotKey("Secondary Dash", "F");
            if (!Main.dedServ)
            {
                Filters.Scene["ElementsAwoken:VoidLeviathanHead"] = new Filter(new VoidLeviathanScreenShaderData("FilterMiniTower").UseColor(1.0f, 0.2f, 0.65f).UseOpacity(0.7f), EffectPriority.VeryHigh);
                Filters.Scene["ElementsAwoken:Aqueous"] = new Filter(new AqueousScreenShaderData("FilterMiniTower").UseColor(0.4f, 0.7f, 1.0f).UseOpacity(0.5f), EffectPriority.VeryHigh);
                SkyManager.Instance["ElementsAwoken:AqueousSky"] = new AqueousSky();
                Filters.Scene["ElementsAwoken:Infernace"] = new Filter(new InfernaceScreenShaderData("FilterMiniTower").UseColor(1f, 0.4f, 0f).UseOpacity(0.5f), EffectPriority.VeryHigh);
                Filters.Scene["ElementsAwoken:Permafrost"] = new Filter(new PermafrostScreenShaderData("FilterMiniTower").UseColor(0.6f, 0.7f, 1.0f).UseOpacity(0.5f), EffectPriority.VeryHigh);
                Filters.Scene["ElementsAwoken:TheGuardianFly"] = new Filter(new TheGuardianScreenShaderData("FilterMiniTower").UseColor(1f, 0.4f, 0f).UseOpacity(0.5f), EffectPriority.VeryHigh);
                Filters.Scene["ElementsAwoken:Volcanox"] = new Filter(new TheGuardianScreenShaderData("FilterMiniTower").UseColor(1f, 0.4f, 0f).UseOpacity(0.5f), EffectPriority.VeryHigh);
                Filters.Scene["ElementsAwoken:Azana"] = new Filter(new TheGuardianScreenShaderData("FilterMiniTower").UseColor(1f, 0.2f, 0f).UseOpacity(0.5f), EffectPriority.VeryHigh);
                Filters.Scene["ElementsAwoken:Ancients"] = new Filter(new TheGuardianScreenShaderData("FilterMiniTower").UseColor(0f, 0.8f, 0.4f).UseOpacity(0.3f), EffectPriority.VeryHigh);

                Filters.Scene["Blizzard"] = new Filter(new BlizzardShaderData("FilterBlizzardForeground").UseColor(1f, 1f, 1f).UseSecondaryColor(0.7f, 0.7f, 1f).UseImage("Images/Misc/noise", 0, null).UseIntensity(0.4f).UseImageScale(new Vector2(3f, 0.75f), 0), EffectPriority.High);
                Overlays.Scene["Blizzard"] = new SimpleOverlay("Images/Misc/noise", new BlizzardShaderData("FilterBlizzardBackground").UseColor(1f, 1f, 1f).UseSecondaryColor(0.7f, 0.7f, 1f).UseImage("Images/Misc/noise", 0, null).UseIntensity(0.4f).UseImageScale(new Vector2(3f, 0.75f), 0), EffectPriority.High, RenderLayers.Landscape);

                Filters.Scene["ElementsAwoken:VoidEvent"] = new Filter(new VoidEventScreenShaderData("FilterMiniTower").UseColor(0.8f, 0.2f, 0.0f).UseOpacity(0.3f), EffectPriority.VeryHigh);
                Filters.Scene["ElementsAwoken:VoidEventDark"] = new Filter(new VoidEventScreenShaderData("FilterMiniTower").UseColor(0.0f, 0.0f, 0.2f).UseOpacity(0.5f), EffectPriority.VeryHigh); // 0.9 opacity for a very dark spooky effect where u can barely see

                Filters.Scene["ElementsAwoken:Encounter1"] = new Filter(new EncountersScreenShaderData("FilterMiniTower").UseColor(1f, 0.2f, 0.3f).UseOpacity(0.5f), EffectPriority.VeryHigh);
                Filters.Scene["ElementsAwoken:Encounter2"] = new Filter(new EncountersScreenShaderData("FilterMiniTower").UseColor(0.1f, 0.1f, 0.3f).UseOpacity(0.2f), EffectPriority.VeryHigh);
                Filters.Scene["ElementsAwoken:Encounter3"] = new Filter(new EncountersScreenShaderData("FilterMiniTower").UseColor(0.0f, 0.0f, 0.2f).UseOpacity(0.5f), EffectPriority.VeryHigh); // 0.9 opacity for a very dark spooky effect where u can barely see

                Filters.Scene["ElementsAwoken:Despair"] = new Filter(new VoidEventScreenShaderData("FilterMiniTower").UseColor(0.2f, 0.0f, 0.3f).UseOpacity(0.4f), EffectPriority.VeryHigh);

                Filters.Scene["ElementsAwoken:Regaroth"] = new Filter(new RegarothScreenShaderData("FilterMiniTower").UseColor(0.2f, 0.4f, 0.7f).UseOpacity(0.3f), EffectPriority.VeryHigh);
                Filters.Scene["ElementsAwoken:Regaroth2"] = new Filter(new RegarothScreenShaderData("FilterMiniTower").UseColor(0.9f, 0.3f, 0.7f).UseOpacity(0.3f), EffectPriority.VeryHigh);
                Filters.Scene["ElementsAwoken:RegarothIntense"] = new Filter(new RegarothScreenShaderData("FilterMiniTower").UseColor(0.2f, 0.4f, 0.7f).UseOpacity(0.75f), EffectPriority.VeryHigh);
                Filters.Scene["ElementsAwoken:Regaroth2Intense"] = new Filter(new RegarothScreenShaderData("FilterMiniTower").UseColor(0.9f, 0.3f, 0.7f).UseOpacity(0.75f), EffectPriority.VeryHigh);

                SkyManager.Instance["ElementsAwoken:InfernacesWrath"] = new InfernacesWrathSky();

                AADeathBall = instance.GetTexture("Projectiles/NPCProj/Ancients/Gores/LightBall");
                PremultiplyTexture(AADeathBall);
                insanityTex = instance.GetTexture("ScreenEffects/Insanity");
                PremultiplyTexture(insanityTex);
            }
            // config
            Config.Load();

            #region instakill immunities
            instakillImmune.Add(NPCID.EyeofCthulhu);
            instakillImmune.Add(NPCID.EaterofWorldsHead);
            instakillImmune.Add(NPCID.EaterofWorldsBody);
            instakillImmune.Add(NPCID.EaterofWorldsTail);
            instakillImmune.Add(NPCID.BrainofCthulhu);
            instakillImmune.Add(NPCID.Creeper);
            instakillImmune.Add(NPCID.SkeletronHead);
            instakillImmune.Add(NPCID.SkeletronHand);
            instakillImmune.Add(NPCID.QueenBee);
            instakillImmune.Add(NPCID.KingSlime);
            instakillImmune.Add(NPCID.WallofFlesh);
            instakillImmune.Add(NPCID.WallofFleshEye);
            instakillImmune.Add(NPCID.TheDestroyer);
            instakillImmune.Add(NPCID.TheDestroyerBody);
            instakillImmune.Add(NPCID.TheDestroyerTail);
            instakillImmune.Add(NPCID.Retinazer);
            instakillImmune.Add(NPCID.Spazmatism);
            instakillImmune.Add(NPCID.SkeletronPrime);
            instakillImmune.Add(NPCID.PrimeCannon);
            instakillImmune.Add(NPCID.PrimeSaw);
            instakillImmune.Add(NPCID.PrimeVice);
            instakillImmune.Add(NPCID.PrimeLaser);
            instakillImmune.Add(NPCID.Plantera);
            instakillImmune.Add(NPCID.PlanterasTentacle);
            instakillImmune.Add(NPCID.Golem);
            instakillImmune.Add(NPCID.GolemHead);
            instakillImmune.Add(NPCID.GolemFistLeft);
            instakillImmune.Add(NPCID.GolemFistRight);
            instakillImmune.Add(NPCID.GolemHeadFree);
            instakillImmune.Add(NPCID.DukeFishron);
            instakillImmune.Add(NPCID.CultistBoss);
            instakillImmune.Add(NPCID.MoonLordHead);
            instakillImmune.Add(NPCID.MoonLordHand);
            instakillImmune.Add(NPCID.MoonLordCore);
            instakillImmune.Add(NPCID.MoonLordFreeEye);
            instakillImmune.Add(NPCID.DungeonGuardian);
            instakillImmune.Add(NPCID.IceGolem);
            instakillImmune.Add(NPCID.WyvernHead);
            instakillImmune.Add(NPCID.WyvernLegs);
            instakillImmune.Add(NPCID.WyvernTail);
            instakillImmune.Add(NPCID.WyvernBody);
            instakillImmune.Add(NPCID.WyvernBody2);
            instakillImmune.Add(NPCID.WyvernBody3);
            instakillImmune.Add(NPCID.Mothron);
            instakillImmune.Add(NPCID.PlanterasHook);
            instakillImmune.Add(NPCID.PlanterasTentacle);
            instakillImmune.Add(NPCID.Paladin);
            instakillImmune.Add(NPCID.HeadlessHorseman);
            instakillImmune.Add(NPCID.MourningWood);
            instakillImmune.Add(NPCID.Pumpking);
            instakillImmune.Add(NPCID.PumpkingBlade);
            instakillImmune.Add(NPCID.Yeti);
            instakillImmune.Add(NPCID.Everscream);
            instakillImmune.Add(NPCID.IceQueen);
            instakillImmune.Add(NPCID.Krampus);
            instakillImmune.Add(NPCID.MartianSaucer);
            instakillImmune.Add(NPCID.MartianSaucerCannon);
            instakillImmune.Add(NPCID.MartianSaucerCore);
            instakillImmune.Add(NPCID.MartianSaucerTurret);
            instakillImmune.Add(NPCID.MoonLordCore);
            instakillImmune.Add(NPCID.LunarTowerVortex);
            instakillImmune.Add(NPCID.LunarTowerStardust);
            instakillImmune.Add(NPCID.LunarTowerSolar);
            instakillImmune.Add(NPCID.LunarTowerNebula);
            instakillImmune.Add(NPCID.CultistArcherBlue);
            instakillImmune.Add(NPCID.CultistArcherWhite);
            instakillImmune.Add(NPCID.CultistDevote);
            instakillImmune.Add(NPCID.CultistDragonBody1);
            instakillImmune.Add(NPCID.CultistDragonBody2);
            instakillImmune.Add(NPCID.CultistDragonBody3);
            instakillImmune.Add(NPCID.CultistDragonBody4);
            instakillImmune.Add(NPCID.CultistDragonHead);
            instakillImmune.Add(NPCID.CultistDragonTail);
            instakillImmune.Add(NPCID.CultistTablet);
            instakillImmune.Add(NPCID.GoblinSummoner);
            instakillImmune.Add(NPCID.BigMimicJungle);
            instakillImmune.Add(NPCID.BigMimicCorruption);
            instakillImmune.Add(NPCID.BigMimicHallow);
            instakillImmune.Add(NPCID.BigMimicCrimson);
            instakillImmune.Add(NPCID.PirateShip);
            instakillImmune.Add(NPCID.PirateShipCannon);
            instakillImmune.Add(NPCID.SandElemental);
            instakillImmune.Add(NPCID.DD2Betsy);
            instakillImmune.Add(NPCID.DD2DarkMageT1);
            instakillImmune.Add(NPCID.DD2DarkMageT3);
            instakillImmune.Add(NPCID.SolarCrawltipedeBody);
            instakillImmune.Add(NPCID.SolarCrawltipedeHead);
            instakillImmune.Add(NPCID.DD2OgreT2);
            instakillImmune.Add(NPCID.DD2OgreT3);
            instakillImmune.Add(NPCID.RainbowSlime);
            instakillImmune.Add(NPCID.PirateCaptain);
            instakillImmune.Add(NPCID.TargetDummy);
            instakillImmune.Add(NPCType("CosmicObserver"));
            instakillImmune.Add(NPCType("ToySlime"));
            instakillImmune.Add(NPCType("AncientWyrmArms"));
            instakillImmune.Add(NPCType("AncientWyrmBody"));
            instakillImmune.Add(NPCType("AncientWyrmHead"));
            instakillImmune.Add(NPCType("AncientWyrmTail"));
            instakillImmune.Add(NPCType("AndromedaHead"));
            instakillImmune.Add(NPCType("AndromedaBody"));
            instakillImmune.Add(NPCType("AndromedaTail"));
            instakillImmune.Add(NPCType("BarrenSoul"));
            instakillImmune.Add(NPCType("Furosia"));
            instakillImmune.Add(NPCType("ObsidiousHand"));
            instakillImmune.Add(NPCType("RegarothHead"));
            instakillImmune.Add(NPCType("RegarothBody"));
            instakillImmune.Add(NPCType("RegarothTail"));
            instakillImmune.Add(NPCType("ShadeWyrmHead"));
            instakillImmune.Add(NPCType("ShadeWyrmBody"));
            instakillImmune.Add(NPCType("ShadeWyrmTail"));
            instakillImmune.Add(NPCType("SolarFragment"));
            instakillImmune.Add(NPCType("SoulOfInfernace"));
            instakillImmune.Add(NPCType("VoidLeviathanHead"));
            instakillImmune.Add(NPCType("VoidLeviathanBody"));
            instakillImmune.Add(NPCType("VoidLeviathanBodyWeak"));
            instakillImmune.Add(NPCType("VoidLeviathanTail"));
            instakillImmune.Add(NPCType("VolcanoxHook"));
            instakillImmune.Add(NPCType("VolcanoxTentacle"));
            #endregion
        }

        public static bool prevMenuState;

        public static void Update()
        {
            if (Main.gameMenu != prevMenuState)
            {
                if (Main.gameMenu)
                {
                    //Load on menu
                    ResetCloudTexture();
                }
                prevMenuState = Main.gameMenu;
            }
        }

        public static void DebugModeText(object text, int r = 255, int g = 255, int b = 255)
        {
            Color color = new Color(r, g, b);
            if (Config.debugMode)
            {
                Main.NewText(text, color);
            }
        }
        public static void DebugModeNetworkText(string text, int r = 255, int g = 255, int b = 255)
        {
            Color color = new Color(r, g, b);
            if (Config.debugMode)
            {
                NetworkText f = NetworkText.FromLiteral(text);
                NetMessage.BroadcastChatMessage(f, color);
            }
        }
        public static void DebugModeCombatText(string text, Rectangle rect, int r = 255, int g = 255, int b = 255)
        {
            Color color = new Color(r, g, b);
            if (Config.debugMode)
            {
                CombatText.NewText(rect, color, text, true, false);
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            Mod mod = ModLoader.GetMod("ElementsAwoken");
            Player player = Main.player[Main.myPlayer];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>(this);
            AwakenedPlayer awakenedPlayer = player.GetModPlayer<AwakenedPlayer>(this);

            // computer
            if (!Main.player[Main.myPlayer].ghost && modPlayer.inComputer && !Main.gameMenu)
            {
                var computerLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
                var computerState = new LegacyGameInterfaceLayer("ElementsAwoken: UI",
                    delegate
                    {
                        DrawComputer(Main.spriteBatch);
                        return true;
                    },
                    InterfaceScaleType.UI);
                layers.Insert(computerLayer, computerState);
            }
            // encounter text
            if (!Main.player[Main.myPlayer].ghost && !Main.gameMenu && MyWorld.encounterTimer > 0)
            {
                var encounterLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
                var encounterState = new LegacyGameInterfaceLayer("ElementsAwoken: UI",
                    delegate
                    {
                        DrawEncounterText(Main.spriteBatch);
                        return true;
                    },
                    InterfaceScaleType.UI);
                layers.Insert(encounterLayer, encounterState);
            }
            // hearts & mana
            if (!calamityEnabled && !Main.gameMenu)
            {
                var heartLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
                var heartState = new LegacyGameInterfaceLayer("ElementsAwoken: UI2",
                    delegate
                    {
                        DrawHearts();
                        return true;
                    },
                    InterfaceScaleType.UI);
                layers.Insert(heartLayer, heartState);

                // to stop hearts being underneath
                if (modPlayer.voidHeartsUsed == 10)
                {
                    Main.heart2Texture = GetTexture("Extra/Blank");
                }
                else
                {
                    Main.heart2Texture = GetTexture("Extra/Heart2");
                }
                if (modPlayer.lunarStarsUsed == 1)
                {
                    Main.manaTexture = GetTexture("Extra/Mana2");
                }
                else
                {
                    Main.manaTexture = Main.instance.OurLoad<Texture2D>("Images" + Path.DirectorySeparatorChar.ToString() + "Mana");
                }
            }
            // shield hearts
            if (!Main.gameMenu)
            {
                var shieldLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
                var shieldState = new LegacyGameInterfaceLayer("ElementsAwoken: UI2",
                    delegate
                    {
                        DrawShield();
                        return true;
                    },
                    InterfaceScaleType.UI);
                layers.Insert(shieldLayer, shieldState);
            }
            // energy & insanity UI
            if (!Main.player[Main.myPlayer].ghost)
            {
                var BarLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
                var BarState = new LegacyGameInterfaceLayer("ElementsAwoken: UI",
                    delegate
                    {
                        DrawEnergyBar();
                        DrawInsanityBar();
                        return true;
                    },
                    InterfaceScaleType.UI);
                layers.Insert(BarLayer, BarState);

                var insanityOverlayLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
                var insanityOverlayState = new LegacyGameInterfaceLayer("ElementsAwoken: Interface Logic 1",
                    delegate
                    {
                        DrawInsanityOverlay();
                        return true;
                    },
                    InterfaceScaleType.UI);
                layers.Insert(insanityOverlayLayer, insanityOverlayState);
            }
            // sanity book
            if (!Main.player[Main.myPlayer].ghost && awakenedPlayer.openSanityBook && !Main.gameMenu)
            {
                var bookLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
                var bookState = new LegacyGameInterfaceLayer("ElementsAwoken: UI",
                    delegate
                    {
                        DrawSanityBook();
                        return true;
                    },
                    InterfaceScaleType.UI);
                layers.Insert(bookLayer, bookState);
            }
                // make rain black
                if (MyWorld.encounter3)
            {
                Main.rainTexture = GetTexture("Extra/Rain3");
            }
            else
            {
                Main.rainTexture = Main.instance.OurLoad<Texture2D>("Images" + Path.DirectorySeparatorChar.ToString() + "Rain");
            }
            // infernace clouds
            if (!Main.gameMenu)
            {
                if (NPC.downedBoss3 && !MyWorld.downedInfernace)
                {
                    for (int cloud = 0; cloud < 22; cloud++)
                    {
                        Main.cloudTexture[cloud] = GetTexture(string.Concat(new object[]
                        {
                    "Extra/Clouds/Cloud_",
                    cloud
                        }));
                    }
                }
                else
                {
                    ResetCloudTexture();
                }
            }
        }

        private static void ResetCloudTexture()
        {
            for (int num25 = 0; num25 < 22; num25++)
            {
                Main.cloudTexture[num25] = Main.instance.OurLoad<Texture2D>(string.Concat(new object[]
                {
                    "Images",
                    Path.DirectorySeparatorChar.ToString(),
                    "Cloud_",
                    num25
                }));
            }
        }

        private static void DrawHearts()
        {
            Mod mod = ModLoader.GetMod("ElementsAwoken");

            Player player = Main.player[Main.myPlayer];
            var modPlayer = player.GetModPlayer<MyPlayer>(mod);
            float lifePerHeart = 10f;
            if (Main.LocalPlayer.ghost)
            {
                return;
            }

            int lifeForHeart = player.statLifeMax / 20;

            int voidHeartLife = modPlayer.voidHeartsUsed * 2; // multiply by however many hearts you want it to make when you use one
            if (voidHeartLife < 0)
            {
                voidHeartLife = 0;
            }
            if (voidHeartLife > 0)
            {
                lifeForHeart = player.statLifeMax / (20 + voidHeartLife / 4);
                lifePerHeart = player.statLifeMax / 20f;
            }

            int chaosHeartLife = modPlayer.chaosHeartsUsed * 2;
            if (chaosHeartLife < 0)
            {
                chaosHeartLife = 0;
            }
            if (chaosHeartLife > 0)
            {
                lifeForHeart = player.statLifeMax / (20 + chaosHeartLife / 4);
                lifePerHeart = player.statLifeMax / 20f;
            }
            // statLifeMax2 is the actual player life, it equals statLifeMax plus bonuses
            int playerLife = player.statLifeMax2 - player.statLifeMax;
            lifePerHeart += playerLife / lifeForHeart;

            var info = typeof(Main).GetField("UI_ScreenAnchorX", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            int screenAnchorX = (int)info.GetValue(null);

            for (int heartNum = 1; heartNum < (int)(player.statLifeMax2 / lifePerHeart) + 1; heartNum++)
            {
                float scale = 1f;
                bool lastHeart = false;
                int lifeStat;
                if (player.statLife >= heartNum * lifePerHeart)
                {
                    lifeStat = 255;
                    if (player.statLife == heartNum * lifePerHeart)
                    {
                        lastHeart = true;
                    }
                }
                else
                {
                    float num7 = (player.statLife - (heartNum - 1) * lifePerHeart) / lifePerHeart;
                    lifeStat = (int)(30f + 225f * num7);
                    if (lifeStat < 30)
                    {
                        lifeStat = 30;
                    }
                    scale = num7 / 4f + 0.75f;
                    if (scale < 0.75)
                    {
                        scale = 0.75f;
                    }
                    if (num7 > 0f)
                    {
                        lastHeart = true;
                    }
                }
                if (lastHeart)
                {
                    scale += Main.cursorScale - 1f;
                }
                int xPos = 0;
                int yPos = 0;
                if (heartNum > 10)
                {
                    xPos -= 260;
                    yPos += 26;
                }
                if (heartNum > 20)
                {
                    xPos = 0;
                    yPos = 0;
                }
                int a = (int)(lifeStat * 0.9f);
                if (!player.ghost)
                {
                    if (chaosHeartLife > 0)
                    {
                        chaosHeartLife--;
                        Texture2D heart4Texture = mod.GetTexture("Extra/Heart4");
                        Main.spriteBatch.Draw(heart4Texture, new Vector2((float)(500 + 26 * (heartNum - 1) + xPos + screenAnchorX + Main.heartTexture.Width / 2), 32f + ((float)Main.heartTexture.Height - (float)Main.heartTexture.Height * scale) / 2f + (float)yPos + (float)(Main.heartTexture.Height / 2)), new Rectangle?(new Rectangle(0, 0, Main.heartTexture.Width, Main.heartTexture.Height)), new Color(lifeStat, lifeStat, lifeStat, a), 0f, new Vector2((float)(Main.heartTexture.Width / 2), (float)(Main.heartTexture.Height / 2)), scale, SpriteEffects.None, 0f);
                    }
                    else if (voidHeartLife > 0)
                    {
                        voidHeartLife--;
                        Texture2D heart3Texture = mod.GetTexture("Extra/Heart3");
                        if (modPlayer.voidCompressor)
                        {
                            heart3Texture = mod.GetTexture("Extra/Heart3Alt");
                        }
                        else
                        {
                            heart3Texture = mod.GetTexture("Extra/Heart3");
                        }
                        Main.spriteBatch.Draw(heart3Texture, new Vector2((float)(500 + 26 * (heartNum - 1) + xPos + screenAnchorX + Main.heartTexture.Width / 2), 32f + ((float)Main.heartTexture.Height - (float)Main.heartTexture.Height * scale) / 2f + (float)yPos + (float)(Main.heartTexture.Height / 2)), new Rectangle?(new Rectangle(0, 0, Main.heartTexture.Width, Main.heartTexture.Height)), new Color(lifeStat, lifeStat, lifeStat, a), 0f, new Vector2((float)(Main.heartTexture.Width / 2), (float)(Main.heartTexture.Height / 2)), scale, SpriteEffects.None, 0f);
                        //Main.spriteBatch.Draw(heart3Texture, new Vector2((float)(500 + 26 * (heartNum - 1) + xPos + screenAnchorX + heart3Texture.Width / 2), 32f + ((float)Main.heartTexture.Height - (float)Main.heartTexture.Height * scale) / 2f + (float)yPos + (float)(Main.heartTexture.Height / 2)), new Rectangle?(new Rectangle(0, 0, heart3Texture.Width, heart3Texture.Height)), new Color(lifeStat, lifeStat, lifeStat, a), 0.0f, new Vector2((float)(heart3Texture.Width / 2), (float)(heart3Texture.Height / 2)), scale, SpriteEffects.None, 0.0f);

                    }
                }
            }
        }

        private static void DrawShield()
        {
            Mod mod = ModLoader.GetMod("ElementsAwoken");

            Player player = Main.player[Main.myPlayer];
            var modPlayer = player.GetModPlayer<MyPlayer>(mod);
            float lifePerHeart = 5f;
            if (Main.LocalPlayer.ghost)
            {
                return;
            }

            int lifeForHeart = player.statLifeMax / 20;

            int shieldLife = modPlayer.shieldHearts; // multiply by however many hearts you want it to make when you use one
            if (shieldLife < 0)
            {
                shieldLife = 0;
            }
            if (shieldLife > 0)
            {
                lifeForHeart = player.statLifeMax / (20 + shieldLife / 4);
                lifePerHeart = player.statLifeMax / 20f;
            }

            // statLifeMax2 is the actual player life, it equals statLifeMax plus bonuses
            int playerLife = modPlayer.shieldHearts * 5;
            lifePerHeart += playerLife / lifeForHeart;

            var info = typeof(Main).GetField("UI_ScreenAnchorX", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            int screenAnchorX = (int)info.GetValue(null);

            for (int heartNum = 1; heartNum < (int)(player.statLifeMax2 / lifePerHeart) + 1; heartNum++)
            {
                float scale = 1f;
                bool lastHeart = false;
                int lifeStat;
                if (player.statLife >= heartNum * lifePerHeart)
                {
                    lifeStat = 255;
                    if (player.statLife == heartNum * lifePerHeart)
                    {
                        lastHeart = true;
                    }
                }
                else
                {
                    float num7 = (player.statLife - (heartNum - 1) * lifePerHeart) / lifePerHeart;
                    lifeStat = (int)(30f + 225f * num7);
                    if (lifeStat < 30)
                    {
                        lifeStat = 30;
                    }
                    scale = num7 / 4f + 0.75f;
                    if (scale < 0.75)
                    {
                        scale = 0.75f;
                    }
                    if (num7 > 0f)
                    {
                        lastHeart = true;
                    }
                }
                if (lastHeart)
                {
                    scale += Main.cursorScale - 1f;
                }
                int xPos = 0;
                int yPos = 0;
                if (heartNum > 10)
                {
                    xPos -= 260;
                    yPos += 26;
                }
                if (heartNum > 20)
                {
                    xPos = 0;
                    yPos = 0;
                }
                int a = (int)(lifeStat * 0.9f);
                if (!player.ghost)
                {
                    if (shieldLife > 0)
                    {
                        shieldLife--;
                        Texture2D heart4Texture = mod.GetTexture("Extra/ShieldHeart");
                        Main.spriteBatch.Draw(heart4Texture, new Vector2((float)(500 + 26 * (heartNum - 1) + xPos + screenAnchorX + Main.heartTexture.Width / 2), 32f + ((float)Main.heartTexture.Height - (float)Main.heartTexture.Height * scale) / 2f + (float)yPos + (float)(Main.heartTexture.Height / 2)), new Rectangle?(new Rectangle(0, 0, Main.heartTexture.Width, Main.heartTexture.Height)), new Color(lifeStat, lifeStat, lifeStat, a), 0f, new Vector2((float)(Main.heartTexture.Width / 2), (float)(Main.heartTexture.Height / 2)), scale, SpriteEffects.None, 0f);
                    }
                }
            }
        }

        private static void DrawEnergyBar()
        {
            Mod mod = ModLoader.GetMod("ElementsAwoken");
            Player player = Main.player[Main.myPlayer];
            var modPlayer = player.GetModPlayer<PlayerEnergy>(mod);
            if (Main.LocalPlayer.ghost)
            {
                return;
            }
            if (!player.ghost && modPlayer.maxEnergy > 0)
            {
                var info = typeof(Main).GetField("UI_ScreenAnchorX", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
                int screenAnchorX = (int)info.GetValue(null);

                Texture2D backgroundTexture = mod.GetTexture("Extra/EnergyUI");
                int barPosLeft = 415 + screenAnchorX + (MyWorld.awakenedMode ? - 134 : 0);
                Main.spriteBatch.Draw(backgroundTexture, new Rectangle(barPosLeft, 48, backgroundTexture.Width, backgroundTexture.Height), null, Color.White, 0f, new Vector2(backgroundTexture.Width / 2, backgroundTexture.Height / 2), SpriteEffects.None, 0f);

                Texture2D barTexture = mod.GetTexture("Extra/EnergyBar");
                Rectangle barDest = new Rectangle(barPosLeft + 10, 48, (int)(barTexture.Width * ((float)modPlayer.energy / (float)modPlayer.maxEnergy)), barTexture.Height);
                Rectangle barLength = new Rectangle(0, 0, (int)(barTexture.Width * ((float)modPlayer.energy / (float)modPlayer.maxEnergy)), barTexture.Height);
                Main.spriteBatch.Draw(barTexture, barDest, barLength, Color.White, 0f, new Vector2(barTexture.Width / 2, barTexture.Height / 2), SpriteEffects.None, 0f);

                DynamicSpriteFont fontType = Main.fontMouseText;
                string text = "Energy: " + modPlayer.energy + "/" + modPlayer.maxEnergy;
                Vector2 textSize = fontType.MeasureString(text);
                float textPositionLeft = barPosLeft - textSize.X / 2;
                DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, fontType, text, new Vector2(textPositionLeft, 6f), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
            }
        }

        private static void DrawInsanityBar()
        {
            Mod mod = ModLoader.GetMod("ElementsAwoken");
            Player player = Main.player[Main.myPlayer];
            var modPlayer = player.GetModPlayer<AwakenedPlayer>(mod);
            if (Main.LocalPlayer.ghost)
            {
                return;
            }
            if (!player.ghost && MyWorld.awakenedMode)
            {
                var info = typeof(Main).GetField("UI_ScreenAnchorX", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
                int screenAnchorX = (int)info.GetValue(null);

                Texture2D backgroundTexture = mod.GetTexture("Extra/InsanityUI");
                int barPosLeft = 415 + screenAnchorX;
                Main.spriteBatch.Draw(backgroundTexture, new Rectangle(barPosLeft, 48, backgroundTexture.Width, backgroundTexture.Height), null, Color.White, 0f, new Vector2(backgroundTexture.Width / 2, backgroundTexture.Height / 2), SpriteEffects.None, 0f);

                // above 40% sanity
                if (modPlayer.sanity >= modPlayer.sanityMax * 0.4f)
                {
                    Texture2D barTexture = mod.GetTexture("Extra/InsanityBar");
                    Rectangle barDest = new Rectangle(barPosLeft + 11, 48, (int)(barTexture.Width * ((float)modPlayer.sanity / (float)modPlayer.sanityMax)), barTexture.Height);
                    Rectangle barLength = new Rectangle(0, 0, (int)(barTexture.Width * ((float)modPlayer.sanity / (float)modPlayer.sanityMax)), barTexture.Height);
                    Main.spriteBatch.Draw(barTexture, barDest, barLength, Color.White, 0f, new Vector2(barTexture.Width / 2, barTexture.Height / 2), SpriteEffects.None, 0f);
                }
                else
                {
                    Texture2D barTexture = mod.GetTexture("Extra/InsanityBarDistorted");
                    int barHeight = 28;
                    Rectangle barDest = new Rectangle(barPosLeft + 11, 48, (int)(barTexture.Width * ((float)modPlayer.sanity / (float)modPlayer.sanityMax)), barHeight);
                    Rectangle barLength = new Rectangle(0, barHeight * modPlayer.sanityGlitchFrame, (int)(barTexture.Width * ((float)modPlayer.sanity / (float)modPlayer.sanityMax)), barHeight);
                    Main.spriteBatch.Draw(barTexture, barDest, barLength, Color.White, 0f, new Vector2(barTexture.Width / 2, barHeight / 2), SpriteEffects.None, 0f);
                }
                if (modPlayer.sanityRegen != 0)
                {
                    Texture2D arrowTexture = mod.GetTexture("Extra/SanityArrow");
                    int arrowHeight = 26;
                    Rectangle barDest = new Rectangle(barPosLeft + 74, 46, arrowTexture.Width, arrowHeight);
                    Rectangle barLength = new Rectangle(0, arrowHeight * modPlayer.sanityArrowFrame, arrowTexture.Width, arrowHeight);
                    SpriteEffects doFlip = modPlayer.sanityRegen < 0 ? SpriteEffects.None : SpriteEffects.FlipVertically;
                    Main.spriteBatch.Draw(arrowTexture, barDest, barLength, Color.White, 0f, new Vector2(arrowTexture.Width / 2, arrowHeight / 2), doFlip, 0f);
                }
                DynamicSpriteFont fontType = Main.fontMouseText;
                string text = "Sanity: " + modPlayer.sanity + "/" + modPlayer.sanityMax;
                Vector2 textSize = fontType.MeasureString(text);
                float textPositionLeft = barPosLeft - textSize.X / 2;
                DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, fontType, text, new Vector2(textPositionLeft, 6f), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
            }
        }

        private static void DrawInsanityOverlay()
        {
            Mod mod = ModLoader.GetMod("ElementsAwoken");
            Player player = Main.player[Main.myPlayer];
            var modPlayer = player.GetModPlayer<AwakenedPlayer>(mod);

            if (modPlayer.sanity > modPlayer.sanityMax * 0.50f) return;

            Color color = new Color(255, InsanityOverlay.gbValues, InsanityOverlay.gbValues) * InsanityOverlay.transparency;
            int width = insanityTex.Width;
            int num = 10;
            Rectangle rect = Main.player[Main.myPlayer].getRect();
            rect.Inflate((width - rect.Width) / 2, (width - rect.Height) / 2 + num / 2);
            rect.Offset(-(int)Main.screenPosition.X, -(int)Main.screenPosition.Y + (int)Main.player[Main.myPlayer].gfxOffY - num);
            Rectangle destinationRectangle1 = Rectangle.Union(new Rectangle(0, 0, 1, 1), new Rectangle(rect.Right - 1, rect.Top - 1, 1, 1));
            Rectangle destinationRectangle2 = Rectangle.Union(new Rectangle(Main.screenWidth - 1, 0, 1, 1), new Rectangle(rect.Right, rect.Bottom - 1, 1, 1));
            Rectangle destinationRectangle3 = Rectangle.Union(new Rectangle(Main.screenWidth - 1, Main.screenHeight - 1, 1, 1), new Rectangle(rect.Left, rect.Bottom, 1, 1));
            Rectangle destinationRectangle4 = Rectangle.Union(new Rectangle(0, Main.screenHeight - 1, 1, 1), new Rectangle(rect.Left - 1, rect.Top, 1, 1));
            Main.spriteBatch.Draw(Main.magicPixel, destinationRectangle1, new Rectangle?(new Rectangle(0, 0, 1, 1)), color);
            Main.spriteBatch.Draw(Main.magicPixel, destinationRectangle2, new Rectangle?(new Rectangle(0, 0, 1, 1)), color);
            Main.spriteBatch.Draw(Main.magicPixel, destinationRectangle3, new Rectangle?(new Rectangle(0, 0, 1, 1)), color);
            Main.spriteBatch.Draw(Main.magicPixel, destinationRectangle4, new Rectangle?(new Rectangle(0, 0, 1, 1)), color);
            Main.spriteBatch.Draw(insanityTex, rect, color);
        }

        public void DrawComputer(SpriteBatch spriteBatch)
        {
            var mod = ModLoader.GetMod("ElementsAwoken");
            var background = mod.GetTexture("Extra/ComputerText");
            var player = Main.player[Main.myPlayer].GetModPlayer<MyPlayer>(mod);
            string text = player.computerText;
            spriteBatch.Draw(background, new Rectangle(Main.screenWidth / 2, 150, background.Width, background.Height), null, Color.White, 0f, new Vector2(background.Width / 2, background.Height / 2), SpriteEffects.None, 0f);
            Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, text, Main.screenWidth / 2 - 230, 86, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Black, new Vector2());
        }

        public void DrawSanityBook()
        {
            //var background = GetTexture("Extra/ComputerText");
            Player player = Main.player[Main.myPlayer];
            AwakenedPlayer awakenedPlayer = player.GetModPlayer<AwakenedPlayer>(this);
            //Main.spriteBatch.Draw(background, new Rectangle(Main.screenWidth / 2, 150, background.Width, background.Height), null, Color.White, 0f, new Vector2(background.Width / 2, background.Height / 2), SpriteEffects.None, 0f);
            // draw the positive
            Utils.DrawBorderStringFourWay(Main.spriteBatch, Main.fontMouseText, "Regens:", Main.screenWidth - 330, Main.screenHeight - 220, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Black, new Vector2());
            for (int i = 0; i < awakenedPlayer.sanityRegens.Count; i++)
            {
                string text = awakenedPlayer.sanityRegensName[i] + ": " + awakenedPlayer.sanityRegens[i];
                int yPos = Main.screenHeight - 200 + 25 * i;
                Utils.DrawBorderStringFourWay(Main.spriteBatch, Main.fontMouseText, text, Main.screenWidth - 330, yPos, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Black, new Vector2());
            }
            // draw the negative
            Utils.DrawBorderStringFourWay(Main.spriteBatch, Main.fontMouseText, "Drains:", Main.screenWidth - 150, Main.screenHeight - 220, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Black, new Vector2());
            for (int i = 0; i < awakenedPlayer.sanityDrains.Count; i++)
            {
                string text = awakenedPlayer.sanityDrainsName[i] + ": " + awakenedPlayer.sanityDrains[i];
                int yPos = Main.screenHeight - 200 + 25 * i;
                Utils.DrawBorderStringFourWay(Main.spriteBatch, Main.fontMouseText, text, Main.screenWidth - 150, yPos, new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor), Color.Black, new Vector2());
            }
        }

        public void DrawEncounterText(SpriteBatch spriteBatch)
        {
            var mod = ModLoader.GetMod("ElementsAwoken");
            var player = Main.player[Main.myPlayer].GetModPlayer<MyPlayer>(mod);
            string text = player.encounterText;
            if (player.encounterTextTimer > 0)
            {
                Vector2 textSize = Main.fontDeathText.MeasureString(text);
                float textPositionLeft = Main.screenWidth / 2 - textSize.X / 2;

                Vector2 pos = new Vector2(textPositionLeft, Main.screenHeight / 2 - 200);
                float rand = player.finalText ? 3.5f : 2f;
                pos.X += Main.rand.NextFloat(-rand, rand);
                pos.Y += Main.rand.NextFloat(-rand, rand);
                Color color = player.finalText ? new Color(player.encounterTextAlpha, 0, 0, player.encounterTextAlpha) : new Color(player.encounterTextAlpha, player.encounterTextAlpha, player.encounterTextAlpha, player.encounterTextAlpha);
                //Utils.DrawBorderStringBig(spriteBatch, text, pos, color);
                DrawStringOutlined(spriteBatch, text, pos, color);
            }
        }

        /*private static void DrawManaStars()
        {
            
            Mod mod = ModLoader.GetMod("ElementsAwoken");
            Player player = Main.player[Main.myPlayer];
            var modPlayer = player.GetModPlayer<MyPlayer>(mod);
            float manaPerStar = 40f;
            if (Main.LocalPlayer.ghost)
            {
                return;
            }

            int manaForStar = player.statLifeMax / 20;
            int lunarStarMana = modPlayer.lunarStarsUsed; // multiply by however many stars you want it to make when you use one
            if (lunarStarMana < 0)
            {
                lunarStarMana = 0;
            }
            if (lunarStarMana > 0)
            {
                manaForStar = player.statManaMax / (20 + lunarStarMana / 4);
                manaForStar = (int)(player.statManaMax / 20f);
            }


            if (player.statManaMax2 > 0)
            {
                for (int starNum = 1; starNum < player.statManaMax2 / manaPerStar + 1; starNum++)
                {
                    bool lastStar = false;
                    float scale = 1f;
                    int manaStat;
                    if (player.statMana >= starNum * manaPerStar)
                    {
                        manaStat = 255;
                        if (player.statMana == starNum * manaPerStar)
                        {
                            lastStar = true;
                        }
                    }
                    else
                    {
                        float num4 = (float)(player.statMana - (starNum - 1) * manaPerStar) / (float)manaPerStar;
                        manaStat = (int)(30f + 225f * num4);
                        if (manaStat < 30)
                        {
                            manaStat = 30;
                        }
                        scale = num4 / 4f + 0.75f;
                        if ((double)scale < 0.75)
                        {
                            scale = 0.75f;
                        }
                        if (num4 > 0f)
                        {
                            lastStar = true;
                        }
                    }
                    if (lastStar)
                    {
                        scale += Main.cursorScale - 1f;
                    }
                    int a = (int)((double)((float)manaStat) * 0.9);
                    Texture2D starTexture = mod.GetTexture("Extra/Mana2");
                    if (modPlayer.lunarStarsUsed < 10)
                    {
                        if (lunarStarMana > 0)
                        {
                            lunarStarMana--;
                            Main.spriteBatch.Draw(starTexture, new Vector2((float)(775 + UI_ScreenAnchorX), (float)(30 + Main.manaTexture.Height / 2) + ((float)Main.manaTexture.Height - (float)Main.manaTexture.Height * scale) / 2f + (float)(28 * (starNum - 1))), new Rectangle?(new Rectangle(0, 0, Main.manaTexture.Width, Main.manaTexture.Height)), new Color(manaStat, manaStat, manaStat, a), 0f, new Vector2((float)(Main.manaTexture.Width / 2), (float)(Main.manaTexture.Height / 2)), scale, SpriteEffects.None, 0f);
                        }
                    }
                    else
                    {
                        //Main.spriteBatch.Draw(starTexture, new Vector2((float)(775 + UI_ScreenAnchorX), (float)(30 + Main.manaTexture.Height / 2) + ((float)Main.manaTexture.Height - (float)Main.manaTexture.Height * scale) / 2f + (float)(28 * (starNum - 1))), new Rectangle?(new Rectangle(0, 0, Main.manaTexture.Width, Main.manaTexture.Height)), new Color(manaStat, manaStat, manaStat, a), 0f, new Vector2((float)(Main.manaTexture.Width / 2), (float)(Main.manaTexture.Height / 2)), scale, SpriteEffects.None, 0f);
                    }
                }
            }
        }*/


        public override void UpdateMusic(ref int music, ref MusicPriority priority)
        {
            if (Main.myPlayer != -1 && !Main.gameMenu && Main.LocalPlayer.active)
            {
                if (Main.invasionX == Main.spawnTileX && MyWorld.voidInvasionUp)
                {
                    if (!Main.dayTime && Main.time > 16220)
                    {
                        music = MusicID.LunarBoss;
                    }
                    else
                    {
                        music = MusicID.TheTowers;
                    }
                }
                if (MyWorld.encounterTimer > 0)
                {
                    music = GetSoundSlot(SoundType.Music, "Sounds/Blank");
                }
            }
        }

        //boss checklist
        public override void PostSetupContent()
        {
            Mod bossChecklist = ModLoader.GetMod("BossChecklist");
            if (bossChecklist != null)
            {
                /** Vanilla boss numbers
                * SlimeKing = 1f;
                * EyeOfCthulhu = 2f;
                * EaterOfWorlds = 3f;
                * QueenBee = 4f;
                * Skeletron = 5f;
                * WallOfFlesh = 6f;
                * TheTwins = 7f;
                * TheDestroyer = 8f;
                * SkeletronPrime = 9f;
                * Plantera = 10f;
                * Golem = 11f;
                * DukeFishron = 12f;
                * LunaticCultist = 13f;
                * Moonlord = 14f;
                **/
                bossChecklist.Call("AddMiniBossWithInfo", "Toy Slime", 0.1f, (Func<bool>)(() => MyWorld.downedToySlime), "Use a [i:" + ItemType("ToySlimeSummon") + "] to increase the chance of it spawning");
                bossChecklist.Call("AddBossWithInfo", "Wasteland", 2.5f, (Func<bool>)(() => MyWorld.downedWasteland), "Catch and use a [i:" + ItemType("WastelandSummon") + "] in the desert");
                bossChecklist.Call("AddBossWithInfo", "Infernace", 5.5f, (Func<bool>)(() => MyWorld.downedInfernace), "Use a [i:" + ItemType("InfernaceSummon") + "] in the underworld");
                bossChecklist.Call("AddMiniBossWithInfo", "Cosmic Observer", 6.1f, (Func<bool>)(() => MyWorld.downedCosmicObserver), "Use a [i:" + ItemType("CosmicObserverSummon") + "] to increase the chance of it spawning");
                bossChecklist.Call("AddBossWithInfo", "Scourge Fighter", 9.3f, (Func<bool>)(() => MyWorld.downedScourgeFighter), "Use a [i:" + ItemType("ScourgeFighterSummon") + "] at nighttime");
                bossChecklist.Call("AddBossWithInfo", "Regaroth", 9.4f, (Func<bool>)(() => MyWorld.downedRegaroth), "Use a [i:" + ItemType("RegarothSummon") + "] on a sky island");
                bossChecklist.Call("AddBossWithInfo", "The Celestials", 10.99f, (Func<bool>)(() => MyWorld.downedCelestial), "Use an [i:" + ItemType("CelestialSummon") + "] at nighttime");
                bossChecklist.Call("AddBossWithInfo", "Obsidious", 11.2f, (Func<bool>)(() => MyWorld.downedObsidious), "Use an [i:" + ItemType("ObsidiousSummon") + "] at nighttime");
                bossChecklist.Call("AddBossWithInfo", "Permafrost", 11.1f, (Func<bool>)(() => MyWorld.downedPermafrost), "Use an [i:" + ItemType("PermafrostSummon") + "] in the snow");
                bossChecklist.Call("AddBossWithInfo", "Aqueous", 12.1f, (Func<bool>)(() => MyWorld.downedAqueous), "Use a [i:" + ItemType("AqueousSummon") + "] in the ocean");
                bossChecklist.Call("AddBossWithInfo", "Temple Keepers", 14.1f, (Func<bool>)(() => (MyWorld.downedAncientWyrm && MyWorld.downedEye)), "Use the [i:" + ItemType("AncientDragonSummon") + "] at nighttime");
                bossChecklist.Call("AddBossWithInfo", "The Guardian", 14.2f, (Func<bool>)(() => MyWorld.downedGuardian), "Use an [i:" + ItemType("GuardianSummon") + "] at nighttime");
                bossChecklist.Call("AddEventWithInfo", "Dawn of the Void", 14.4f, (Func<bool>)(() => MyWorld.downedVoidEvent), "Use a [i:" + ItemType("VoidEventSummon") + "] at nighttime");
                bossChecklist.Call("AddMiniBossWithInfo", "Shade Wyrm", 14.4f, (Func<bool>)(() => MyWorld.downedShadeWyrm), "Kill a Shade Wyrm during the Dawn of the Void");
                bossChecklist.Call("AddBossWithInfo", "Volcanox", 14.5f, (Func<bool>)(() => MyWorld.downedVolcanox), "Use a [i:" + ItemType("VolcanoxSummon") + "] in the underworld");
                bossChecklist.Call("AddBossWithInfo", "Void Leviathan", 15f, (Func<bool>)(() => MyWorld.downedVoidLeviathan), "Use a [i:" + ItemType("VoidLeviathanSummon") + "] at nighttime");
                bossChecklist.Call("AddBossWithInfo", "Azana", 15.5f, (Func<bool>)(() => MyWorld.downedAzana), "Use a [i:" + ItemType("AzanaSummon") + "] at nighttime");
                bossChecklist.Call("AddBossWithInfo", "The Ancients", 16f, (Func<bool>)(() => MyWorld.downedAncients), "Speak to the storyteller or use an [i:" + ItemType("AncientsSummon") + "]");
            }
            Mod censusMod = ModLoader.GetMod("Census");
            if (censusMod != null)
            {
                //censusMod.Call("TownNPCCondition", NPCType("Example Person"), $"Have [i:{ItemType<Items.ExampleItem>()}] or [i:{ItemType<Items.Placeable.ExampleBlock>()}] in inventory and build a house out of [i:{ItemType<Items.Placeable.ExampleBlock>()}] and [i:{ItemType<Items.Placeable.ExampleWall>()}]");
                if (Config.alchemistEnabled)
                {
                    censusMod.Call("TownNPCCondition", NPCType("Alchemist"), "Defeat Skeletron");
                }
                else
                {
                    censusMod.Call("TownNPCCondition", NPCType("Alchemist"), "Disabled in EA Config");
                }
                censusMod.Call("TownNPCCondition", NPCType("Storyteller"), "Always available");
            }
        }

        internal static void DrawStringOutlined(SpriteBatch spriteBatch, string text, Vector2 position, Color color)
        {
            // outlines
            spriteBatch.DrawString(Main.fontDeathText, text, new Vector2(position.X - 1, position.Y), Color.Black * (color.A / 255f));
            spriteBatch.DrawString(Main.fontDeathText, text, new Vector2(position.X + 1, position.Y), Color.Black * (color.A / 255f));
            spriteBatch.DrawString(Main.fontDeathText, text, new Vector2(position.X, position.Y - 1), Color.Black * (color.A / 255f));
            spriteBatch.DrawString(Main.fontDeathText, text, new Vector2(position.X, position.Y + 1), Color.Black * (color.A / 255f));
            // actual text
            spriteBatch.DrawString(Main.fontDeathText, text, new Vector2(position.X, position.Y), color);
        }
        // shake shake
        public override void ModifyTransformMatrix(ref SpriteViewMatrix Transform)
        {
            if (Config.screenshake)
            {
                // basically simple example mod code?? idk i dont understand the example :lul:
                if (!Main.gameMenu)
                {
                    screenshakeTimer++;
                    if (screenshakeAmount >= 0 && screenshakeTimer >= 5) // so it doesnt immediately decrease
                    {
                        screenshakeAmount -= 0.1f;
                    }
                    if (screenshakeAmount < 0)
                    {
                        screenshakeAmount = 0;
                    }
                    Main.screenPosition += new Vector2(screenshakeAmount * Main.rand.NextFloat(), screenshakeAmount * Main.rand.NextFloat()); //NextFloat creates a random value between 0 and 1, multiply screenshake amount for a bit of variety
                }
                else // dont shake on the menu
                {
                    screenshakeAmount = 0;
                    screenshakeTimer = 0;
                }
            }
            else
            {
                screenshakeAmount = 0;
            }
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            ElementsAwokenMessageType msgType = (ElementsAwokenMessageType)reader.ReadByte();
            switch (msgType)
            {
                case ElementsAwokenMessageType.StarHeartSync:
                    byte playernumber = reader.ReadByte();
                    Player starHeartPlayer = Main.player[playernumber];
                    int voidHeartsUsed = reader.ReadInt32();
                    int chaosHeartsUsed = reader.ReadInt32();
                    int lunarStarsUsed = reader.ReadInt32();
                    starHeartPlayer.GetModPlayer<MyPlayer>().voidHeartsUsed = voidHeartsUsed;
                    starHeartPlayer.GetModPlayer<MyPlayer>().chaosHeartsUsed = chaosHeartsUsed;
                    starHeartPlayer.GetModPlayer<MyPlayer>().lunarStarsUsed = lunarStarsUsed;
                    break;
                default:
                    ErrorLogger.Log("Elements Awoken: Unknown Message type: " + msgType);
                    break;
            }
        }

    }

    enum ElementsAwokenMessageType : byte
    {
        StarHeartSync,
    }
}