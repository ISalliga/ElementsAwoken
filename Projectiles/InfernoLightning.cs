﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles
{
    public class InfernoLightning : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            //projectile.aiStyle = 48;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.penetrate = 1;
            projectile.extraUpdates = 100;
            projectile.timeLeft = 100;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Inferno Storm");
        }
        public override void AI()
        {
            if (projectile.velocity.X != projectile.velocity.X)
            {
                projectile.position.X = projectile.position.X + projectile.velocity.X;
                projectile.velocity.X = -projectile.velocity.X;
            }
            if (projectile.velocity.Y != projectile.velocity.Y)
            {
                projectile.position.Y = projectile.position.Y + projectile.velocity.Y;
                projectile.velocity.Y = -projectile.velocity.Y;
            }
            for (int num447 = 0; num447 < 4; num447++)
            {
                if (Main.rand.Next(3) == 0)
                {
                    Vector2 vector33 = projectile.position;
                    vector33 -= projectile.velocity * ((float)num447 * 0.25f);
                    projectile.alpha = 255;
                    int dust = Dust.NewDust(vector33, 1, 1, 6, 0f, 0f, 0, default(Color), 0.75f);
                    Main.dust[dust].position = vector33;
                    Main.dust[dust].scale = (float)Main.rand.Next(70, 110) * 0.013f;
                    Main.dust[dust].velocity *= 0.05f;
                    Main.dust[dust].noGravity = true;
                }
            }
            return;
        }
    }
}