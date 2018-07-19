/*
 * Erstellt mit SharpDevelop.
 * Benutzer: root
 * Datum: 18.07.2018
 * Zeit: 21:12
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
using System;
using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace TutorialGame
{
	//Todo: clicking reveals multiple boxes
	//Todo: implement elapsed time
	//Todo: show how many mines are left
	//Todo: show errors and red mine on losing
	
	//Extras:
	//Todo: change the assets to be furry-related
	//Todo: add a story (?)
	public class Minesweeper
	{
		Random rand;
		
		RenderWindow app;
		
		int w;
		int[,] grid, sgrid;
		
		Texture t;
		Sprite s;
		
		Vector2i pos;
		int x, y;
		
		void app_Closed(object sender, EventArgs e)
		{
			app.Close();
		}

		void app_MouseButtonPressed(object sender, MouseButtonEventArgs e)
		{
			pos = Mouse.GetPosition(app);
			x = pos.X / w;
			y = pos.Y / w;
				
			if(e.Button == Mouse.Button.Left)
				sgrid[x, y] = grid[x, y];
			else if (e.Button == Mouse.Button.Right)
				sgrid[x, y] = 11;
		}
		
		
		public Minesweeper()
		{
			rand = new Random(0);
			app = new RenderWindow(new VideoMode(400,400), "Minesweeper!");
			
			w = 32;
			grid = new int[12,12];
			sgrid = new int[12,12];
			
			x=0;
			y=0;
			
			t = new Texture("images/05/tiles.jpg");
			s = new Sprite(t);
			for(int i=1;i<=10; i++)
				for(int j=1; j<=10; j++)
				{
					sgrid[i,j] = 10;
					if(rand.Next() % 5 == 0)
						grid[i, j] = 9;
					else grid[i, j] = 0;
				}

			for(int i=1;i<=10; i++)
				for(int j=1; j<=10; j++)
				{
					int n = 0;
					if(grid[i, j] == 9) continue;
					if(grid[i+1, j] == 9) n++;
					if(grid[i, j+1] == 9) n++;
					if(grid[i-1, j] == 9) n++;
					if(grid[i, j-1] == 9) n++;
					if(grid[i+1, j+1] == 9) n++;
					if(grid[i-1, j-1] == 9) n++;
					if(grid[i-1, j+1] == 9) n++;
					if(grid[i+1, j-1] == 9) n++;
					grid[i, j] = n;
					
				}
			
			app.MouseButtonPressed += app_MouseButtonPressed;
			app.Closed += app_Closed;
			
			while(app.IsOpen) {
				app.DispatchEvents();
				
				app.Clear(Color.White);
				for(int i=1;i<=10;i++)
					for(int j=1;j<=10;j++)
					{
						//Reveals all boxes on losing 
						if (sgrid[x,y] == 9)
							sgrid[i, j] = grid[i, j];
						s.TextureRect = new IntRect(sgrid[i,j] * w, 0, w, w);
						s.Position = new Vector2f(i*w, j*w);
						app.Draw(s);
					}
				
				app.Display();
			}
				
				
		}
	}
}
