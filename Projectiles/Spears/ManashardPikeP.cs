﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Spears
{
    public class ManashardPikeP : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;
            projectile.scale = 1f;
            projectile.aiStyle = 19;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.ownerHitCheck = true;
            projectile.melee = true;
            projectile.timeLeft = 90;
            projectile.hide = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Manashard Pike");
        }
        public override void AI()
        {
            Main.player[projectile.owner].direction = projectile.direction;
            Main.player[projectile.owner].heldProj = projectile.whoAmI;
            Main.player[projectile.owner].itemTime = Main.player[projectile.owner].itemAnimation;
            projectile.position.X = Main.player[projectile.owner].position.X + (float)(Main.player[projectile.owner].width / 2) - (float)(projectile.width / 2);
            projectile.position.Y = Main.player[projectile.owner].position.Y + (float)(Main.player[projectile.owner].height / 2) - (float)(projectile.height / 2);
            projectile.position += projectile.velocity * projectile.ai[0]; if (projectile.ai[0] == 0f)
            {
                projectile.ai[0] = 3f;
                projectile.netUpdate = true;
            }
            if (Main.player[projectile.owner].itemAnimation < Main.player[projectile.owner].itemAnimationMax / 3)
            {
                projectile.ai[0] -= 1.1f;
            }
            else
            {
                projectile.ai[0] += 0.6f;
            }
            if (Main.player[projectile.owner].itemAnimation == Main.player[projectile.owner].itemAnimationMax / 3)
            {
                Vector2 projPos = projectile.position + Vector2.Normalize(new Vector2(projectile.velocity.X, projectile.velocity.Y)) * 2;
                int type = mod.ProjectileType("ManaPikeBolt");
                Projectile.NewProjectile(projPos.X, projPos.Y, projectile.velocity.X * 0.7f, projectile.velocity.Y * 0.7f, type, projectile.damage, projectile.knockBack, projectile.owner);
            }
            if (Main.player[projectile.owner].itemAnimation == 0)
            {
                projectile.Kill();
            }

            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 2.355f;
            if (projectile.spriteDirection == -1)
            {
                projectile.rotation -= 1.57f;
            }
        }
    }
}