/*
 * Erstellt mit SharpDevelop.
 * Benutzer: root
 * Datum: 18.07.2018
 * Zeit: 17:13
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
	//Todo: add body collision death
	//Optional todo: add bonuses
	//Todo: add walls
	//Todo: add wall death
	
	public class Snake
	{
		int N, M;
		int size;
		int w, h;
		
		int dir, num;
		
		struct Snek { public int x, y; }
		Snek[] s;
		
		struct Fruct {public int x, y; }
		Fruct f;
		
		RenderWindow window;
		Texture t1, t2;
		Sprite sprite1, sprite2;
		Clock clock;

		float timer, delay;
		
		Random rand;

		void Tick() {
			for(int i=num; i>0; --i) {
				s[i].x = s[i-1].x;
				s[i].y = s[i-1].y;
			}
			
			//Direction
			switch(dir)
			{
				case 0: s[0].y += 1; break;
				case 1: s[0].x -= 1; break;
				case 2: s[0].x += 1; break;
				case 3: s[0].y -= 1; break;
			}
			
			//Food collision
			if((s[0].x == f.x) && (s[0].y == f.y)) {
				num++;
				f.x = rand.Next() % N;
				f.y = rand.Next() % M;
			}
			
			//Wrapping
			if (s[0].x > N) s[0].x = 0;
			if (s[0].x < 0) s[0].x = N;
			if (s[0].y > M) s[0].y = 0;
			if (s[0].y < 0) s[0].y = M;
				
			
			for(int i=1; i<num; i++)
				if(s[0].x == s[i].x && s[0].y == s[i].y)
					num = i;
			
		}
		
		void window_Closed(object sender, EventArgs e)
		{
			window.Close();
		}
		
		public Snake()
		{
			rand = new Random(0);
			
			N = 30;
			M = 20;
			size = 16;
			w = size*N;
			h = size*M;
			
			num = 4;
			s = new Snek[100];
			for(var a=0; a<s.Length; a++)
				s[a] = new Snek();
			
			f = new Fruct();
			
			window = new RenderWindow(new VideoMode((uint)w, (uint)h),"Snake Game!");
			
			t1 = new Texture("images/04/white.png");
			t2 = new Texture("images/04/red.png");
			
			sprite1 = new Sprite(t1);
			sprite2 = new Sprite(t2);
			
			clock = new Clock();
			timer = 0;
			delay = 0.1f;
			
			f.x = 10;
			f.y = 10;
			
			window.Closed += window_Closed;
			
			while(window.IsOpen) {
				float time = clock.ElapsedTime.AsSeconds();
				clock.Restart();
				timer += time;
				
				window.DispatchEvents();
				
				if(Keyboard.IsKeyPressed(Keyboard.Key.Left)) dir = 1;
				if(Keyboard.IsKeyPressed(Keyboard.Key.Right)) dir = 2;
				if(Keyboard.IsKeyPressed(Keyboard.Key.Up)) dir = 3;
				if(Keyboard.IsKeyPressed(Keyboard.Key.Down)) dir = 0;
				
				
				if (timer>delay){
					timer = 0;
					Tick();
				}
					
				
				//draw
				window.Clear();
				for(int i=0; i<N; i++)
					for(int j=0; j<M; j++)
					{
						sprite1.Position = new Vector2f(i*size, j*size);
						window.Draw(sprite1);
					}
				
				for(int i=0; i<num; i++) {
					sprite2.Position = new Vector2f(s[i].x * size, s[i].y * size);
					window.Draw(sprite2);
				}
				
				sprite2.Position = new Vector2f(f.x * size, f.y * size);
				window.Draw(sprite2);
				
				window.Display();
			}
		}
	}
}
