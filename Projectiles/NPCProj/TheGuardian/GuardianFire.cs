﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.NPCProj.TheGuardian
{
    public class GuardianFire : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.GreekFire1);
            aiType = ProjectileID.GreekFire1;
            projectile.scale = 1f;
            projectile.hostile = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire");
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 180, false);
        }
    }
}