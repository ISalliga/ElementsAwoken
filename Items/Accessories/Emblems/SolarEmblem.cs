﻿using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace ElementsAwoken.Items.Accessories.Emblems
{
    public class SolarEmblem : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 22;
            item.rare = 10;
            item.value = Item.buyPrice(0, 10, 0, 0);
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Solar Emblem");
            Tooltip.SetDefault("30% increased melee damage\n30% decreased non-melee damage");
        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (slot != i && player.armor[i].type == ItemID.WarriorEmblem)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.meleeDamage += 0.3f;
            player.rangedDamage -= 0.3f;
            player.magicDamage -= 0.3f;
            player.minionDamage -= 0.3f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FragmentSolar, 6);
            recipe.AddIngredient(ItemID.WarriorEmblem, 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
