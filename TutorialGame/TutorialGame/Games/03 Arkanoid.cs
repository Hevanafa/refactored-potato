/*
 * Erstellt mit SharpDevelop.
 * Benutzer: Hevanafa
 * Datum: 18.07.2018
 * Zeit: 15:15
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
using System;
using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace TutorialGame
{
	//Todo: limit paddle movement
	//Todo: add scoring system
	//Todo: add lives
	//Todo: add win animation
	//Todo: add bonus
	//Todo: add different colours
	//Optional todo: add particles
	
	public class Arkanoid
	{
		bool isCollide(Sprite s1, Sprite s2) {
			return s1.GetGlobalBounds().Intersects(s2.GetGlobalBounds());
		}
		
		Random rand;
		
		RenderWindow app;
		
		Texture t1, t2, t3, t4;
		Sprite sBackground, sBall, sPaddle;
		Sprite[] block;
		
		float dx, dy;

		void app_Closed(object sender, EventArgs e)
		{
			app.Close();
		}
		
		public Arkanoid()
		{
			rand = new Random(0);
			
			app = new RenderWindow(new VideoMode(520,450), "Arkanoid Game!");
			app.SetFramerateLimit(60);
			t1 = new Texture("images/03/block01.png");
			t2 = new Texture("images/03/background.jpg");
			t3 = new Texture("images/03/ball.png");
			t4 = new Texture("images/03/paddle.png");
			
			sBackground = new Sprite(t2);
			sBall = new Sprite(t3);
			sPaddle = new Sprite(t4);
			
			sPaddle.Position = new Vector2f(300,440);
			sBall.Position = new Vector2f(300,300);
			
			block = new Sprite[1000];
			for(var a=0; a<block.Length; a++)
				block[a] = new Sprite(t1);
			
			int n=0;
			for (int i=1; i<=10; i++)
				for(int j=1; j<=10; j++) {
				//block[n].Texture = t1;
				block[n].Position = new Vector2f(i*43, j*20);
				n++;
			}
			
			app.Closed += app_Closed;
			
			dx = 6;
			dy = 5;
			
			while (app.IsOpen) {
				app.DispatchEvents();
				
				sBall.Position += new Vector2f(dx, 0);
				for(int i=0; i<n; i++)
					if(isCollide(sBall, block[i])) {
						block[i].Position = new Vector2f(-100, 0);
						dx = -dx;
					}
				
				sBall.Position += new Vector2f(0, dy);
				for(int i=0; i<n; i++)
					if(isCollide(sBall, block[i])) {
						block[i].Position = new Vector2f(-100, 0);
						dy = -dy;
					}
						
				if(Keyboard.IsKeyPressed(Keyboard.Key.Right)) sPaddle.Position += new Vector2f(6, 0);
				if(Keyboard.IsKeyPressed(Keyboard.Key.Left)) sPaddle.Position += new Vector2f(-6, 0);
				
				if(isCollide(sPaddle,sBall))
					dy = -(rand.Next() % 5 + 2);
				
				
				Vector2f b = sBall.Position;
				if (b.X < 0 || b.X > 520) dx = -dx;
				if (b.Y < 0 || b.Y > 450) dy = -dy;
				
				app.Clear();
				app.Draw(sBackground);
				app.Draw(sBall);
				app.Draw(sPaddle);
				
				for(int i =0; i<n; i++)
					app.Draw(block[i]);
				
				app.Display();
			}
			
		}
	}
}
