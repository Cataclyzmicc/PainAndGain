using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PainAndGain.Projectiles
{
	public class HolyBubble : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Holy Bubble");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode
		}

		public override void SetDefaults()
		{
			projectile.width = 32;               //The width of projectile hitbox
			projectile.height = 32;              //The height of the projectile hitbox
			projectile.friendly = true;         //Can the projectile deal damage to enemies?
			projectile.hostile = false;         //Can the projectile deal damage to the player?
			projectile.ranged = false;           //Is the projectile shoot by a ranged weapon?
			projectile.penetrate = 1;           //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
			projectile.timeLeft = 300;          //The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
			projectile.alpha = 255;             //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in)
			projectile.light = 0.75f;            //How much light emit around the projectile
			projectile.ignoreWater = true;          //Does the projectile's speed be influenced by water?
			projectile.tileCollide = true;          //Can the projectile collide with tiles?
			projectile.extraUpdates = 1;            //Set to above 0 if you want the projectile to update multiple time in a frame
        		projectile.rotation += (float)projectile.direction * 0.8f;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			//Redraw the projectile with the color not influenced by light
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}
		
		public override void AI()
		{
		        projectile.velocity.X = 16f;
			projectile.velocity.Y = 16f;
			if (projectile.velocity.X > 0)
			{
			        projectile.velocity.X *= 0.8f;
				projectile.velocity.Y *= 0.8f;
			}
			if (projectile.alpha > 45)
			{
				projectile.alpha -= 15;
				if (projectile.alpha < 45)
				{
					projectile.alpha = 45;
				}
			}
		}
		
		// will use Main.screenPosition or projectile.Center to try and get feathers to fall from sky later
	}
}
