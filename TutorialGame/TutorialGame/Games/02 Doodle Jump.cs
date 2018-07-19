/*
 * Erstellt mit SharpDevelop.
 * Benutzer: Hevanafa
 * Datum: 18.07.2018
 * Zeit: 14:19
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
using System;
using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace TutorialGame
{
	//Todo: add scoring system
	//Todo: add more difficulty by removing platforms after stepping
	//Todo: add obstacles
	public class Doodle
	{
		RenderWindow app;
		Texture t1, t2, t3;
		
		Sprite sBackground, sPlat, sPers;
		
		struct Point { public int x, y; }
		
		Random rand;

		void app_Closed(object sender, EventArgs e)
		{
			app.Close();
		}
		
		int x,y,h;
		float dx, dy;
		
		public Doodle()
		{
			rand = new Random(0);

			app = new RenderWindow(new VideoMode(400, 533), "Doodle Game!");
			app.SetFramerateLimit(60);
			
			t1 = new Texture("images/02/background.png");
			t2 = new Texture("images/02/platform.png");
			t3 = new Texture("images/02/doodle.png");
			
			sBackground = new Sprite(t1);
			sPlat = new Sprite(t2);
			sPers = new Sprite(t3);
			
			Point[] plat = new Point[20];
			
			for(int i=0; i<10; i++) {
				plat[i].x = rand.Next() % 400;
				plat[i].y = rand.Next() % 533;
			}
			
			app.Closed += app_Closed;
			
			x = 100;
			y = 100;
			h = 200;
			dx = 0;
			dy = 0;
			
			while(app.IsOpen) {
				app.DispatchEvents();
				
				if(Keyboard.IsKeyPressed(Keyboard.Key.Right)) x += 3;
				if(Keyboard.IsKeyPressed(Keyboard.Key.Left)) x -= 3;
				
				app.Draw(sBackground);
				app.Draw(sPers);
				for(int i=0; i<10; i++) {
					sPlat.Position = new Vector2f(plat[i].x, plat[i].y);
					app.Draw(sPlat);
				}

				dy += 0.2f;
				y += (int)dy;
				
				if (y>500) dy -= 10;
				
				if(y<h)
					for(int i=0; i<10; i++) {
						y=h;
						plat[i].y = (int)(plat[i].y - dy);
						if(plat[i].y > 533) {
							plat[i].y = 0;
							plat[i].x = rand.Next() % 400;
						}
					}
				
				
				for (int i = 0; i<10; i++)
					if((x + 50 > plat[i].x) && (x+20 < plat[i].x + 68)
					   && (y + 70 > plat[i].y) && (y+70 < plat[i].y + 14) && (dy > 0)) dy = -10;
				
				
				sPers.Position = new Vector2f(x,y);
				
				app.Display();
			}
			
		}
	}
}
