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
		//Todo: make the picture customisable
		//Todo: change the picture to be my original artwork
		
		RenderWindow app;
		Texture t;
		Sprite[] sprite;
		
		int w;
		int[,] grid;

		void app_Closed(object sender, EventArgs e)
		{
			app.Close();
		}

		void app_MouseButtonPressed(object sender, MouseButtonEventArgs e)
		{
			//if(e.Button == Mouse.Button.Left)
				
		}

		public Fifteen()
		{
			app = new RenderWindow(new VideoMode(256, 256), "15-Puzzle!");
			app.SetFramerateLimit(60);
			
			t = new Texture("images/06/15.png");
			
			w = 64;
			grid = new int[6,6];
			
			sprite = new Sprite[20];
			
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
				
				
			}
			
		}
	}
}
