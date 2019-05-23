﻿using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ElementsAwoken.Encounters.Fog
{
    public class EncounterFogBgStyle : ModSurfaceBgStyle
    {
		ScreenFog fog = new ScreenFog(true);
        public override bool ChooseBgStyle()
        {
            return !Main.gameMenu && MyWorld.encounter2;
        }
        
        public override void ModifyFarFades(float[] fades, float transitionSpeed)
        {
            for (int i = 0; i < fades.Length; i++)
            {
                if (i == Slot)
                {
                    fades[i] += transitionSpeed;
                    if (fades[i] > 1f)
                    {
                        fades[i] = 1f;
                    }
                }
                else
                {
                    fades[i] -= transitionSpeed;
                    if (fades[i] < 0f)
                    {
                        fades[i] = 0f;
                    }
                }
            }
        }
		
		public override bool PreDrawCloseBackground(SpriteBatch spriteBatch)
		{
            fog.Update(mod.GetTexture("Encounters/Fog/Fog"));
            fog.Draw(mod.GetTexture("Encounters/Fog/Fog"), true, new Color(120, 120, 200));
			return true;
		}
    }
}