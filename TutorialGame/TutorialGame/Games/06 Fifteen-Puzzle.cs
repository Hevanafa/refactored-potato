/*
 * Erstellt mit SharpDevelop.
 * Benutzer: root
 * Datum: 18.07.2018
 * Zeit: 21:51
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
using System;
using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace TutorialGame
{
	public class Fifteen
	{
		//Gameplay
		//Todo: add time elapsed
		//Todo: add reset game
		//Todo: add shuffler
		
		//Customisation
		//Todo: make the picture customisable
		
		RenderWindow app;
		Texture t;
		Sprite[] sprite;
		
		int w;
		int[,] grid;

		Vector2i pos;
		int x, y; //for mouse position
		int dx, dy; //displacement
		int n;
		
		float speed;
		
		void app_Closed(object sender, EventArgs e)
		{
			app.Close();
		}

		void app_MouseButtonPressed(object sender, MouseButtonEventArgs e)
		{
			if(e.Button == Mouse.Button.Left) {
				pos = Mouse.GetPosition(app);
				x = pos.X / w + 1;
				y = pos.Y / w + 1;
				
				dx = 0;
				dy = 0;
				if (grid[x + 1, y] == 16) { dx = 1; dy = 0; }
				if (grid[x, y + 1] == 16) { dx = 0; dy = 1; }
				if (grid[x, y - 1] == 16) { dx = 0; dy = -1; }
				if (grid[x - 1, y] == 16) { dx = -1; dy = 0; }
				
				n = grid[x, y];
				grid[x, y] = 16;
				grid[x + dx, y + dy] = n;
				
				//animation
				sprite[16].Position += new Vector2f(-dx * w, -dy * w);
				speed = 3;
				for(float i=0; i<w; i+= speed) {
					sprite[n].Position += new Vector2f(speed * dx, speed * dy);
					app.Draw(sprite[16]);
					app.Draw(sprite[n]);
					app.Display();
				}
			}
		}

		public Fifteen()
		{
			app = new RenderWindow(new VideoMode(256, 256), "15-Puzzle!");
			app.SetFramerateLimit(60);
			
			t = new Texture("images/06/15.png");
			
			w = 64;
			grid = new int[6,6];
			
			sprite = new Sprite[20];
			for(var a=0; a<sprite.Length; a++)
				sprite[a] = new Sprite();
			
			int n= 0;
			
			for (int i=0; i<4; i++)
				for(int j=0; j<4; j++)
				{
				n++;
				sprite[n].Texture = t;
				sprite[n].TextureRect = new IntRect(i*w, j*w, w, w);
				grid[i+1, j+1] = n;
				}
			
			app.Closed += app_Closed;
			app.MouseButtonPressed += app_MouseButtonPressed;
			
			while(app.IsOpen) {
				app.DispatchEvents();
				
				app.Clear(Color.White);
				for (int i=0; i<4; i++)
					for (int j=0; j<4; j++)
					{
						n = grid[i+1, j+1];
						sprite[n].Position = new Vector2f(i*w, j*w);
						app.Draw(sprite[n]);
					}
				
				app.Display();
			}
			
		}
	}
}
