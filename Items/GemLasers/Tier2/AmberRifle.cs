﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Audio;

namespace ElementsAwoken.Items.GemLasers.Tier2
{
    public class AmberRifle : ModItem
    {
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.SpaceGun);
            item.ranged = true;
            item.damage = 33;
            item.useTime = 13;
            item.useAnimation = 13;
            item.knockBack = 4;
            item.scale *= 1.2f;
            item.value = Item.buyPrice(0, 10, 0, 0);
            item.rare = 4;
            item.UseSound = SoundID.Item12;
            item.shoot = mod.ProjectileType("AmberLaser");
            item.shootSpeed = 24f;
            item.autoReuse = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Amber Rifle");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HallowedBar, 8);
            recipe.AddIngredient(null, "AmberPistol", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
