/*
 * Erstellt mit SharpDevelop.
 * Benutzer: Hevanafa
 * Datum: 18.07.2018
 * Zeit: 14:09
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
using System;

using SFML.System;
using SFML.Graphics;
using SFML.Window;

//Todo: add game over
//Todo: add scoring system
//Todo: add levelling system
//Optional todo: add sounds and music
//Optional todo: add effects

namespace TutorialGame
{
	//01 Tetris
	class Tetris {
		const int M = 20;
		const int N = 10;
		
		Random rand;
		
		int[,] field;
		
		struct Point {
			public int x,y;
			public Point(int x = -1, int y = -1) {
				this.x = x;
				this.y = y;
			}
		}
		Point[] a = new Point[4],
			b = new Point[4];
			
		int[,] figures;
		
		void changePiece() {
			for (int i=0; i<4; i++)
				field[b[i].y,b[i].x] = colorNum;
			colorNum = 1 + rand.Next() % 7;
			var n = rand.Next() % 7;
			for(int i=0; i<4; i++)
			{
				a[i].x = figures[n,i] % 2;
				a[i].y = figures[n,i] / 2;
			}
		}
		
		bool check() {
			for(var i = 0; i<4; i++)
				if(a[i].x < 0 || a[i].x>=N || a[i].y >= M) return false;
				else if(field[a[i].y, a[i].x] > 0) return false;
			
			return true;
		}

		void window_Closed(object sender, EventArgs e)
		{
			window.Close();
		}

		void window_KeyPressed(object sender, KeyEventArgs e)
		{
			if(e.Code == Keyboard.Key.Up) rotate = true;
			else if (e.Code == Keyboard.Key.Left) dx = -1;
			else if (e.Code == Keyboard.Key.Right) dx = 1;
		}

		RenderWindow window;
		Texture t1, t2, t3;

		int dx = 0;
		bool rotate = false;
		int colorNum = 1;
		float timer = 0, delay = 0.3f;
		
		Clock clock;
		
		public Tetris() {
			field = new int[M, N];
			figures = new int[7,4] {{1,3,5,7}, {2,4,5,7}, {3,5,4,6}, {3,5,4,7}, {2,3,5,7}, {3,5,7,6}, {2,3,4,5}};
			
			rand = new Random(0);
			
			window = new RenderWindow(new VideoMode(320,480), "The Game!");
			
			t1 = new Texture("images/01/tiles.png");
			t2 = new Texture("images/01/background.png");
			t3 = new Texture("images/01/frame.png");
			
			Sprite s, background, frame;
			s = new Sprite(t1);
			background = new Sprite(t2);
			frame = new Sprite(t3);
			
			clock = new Clock();
			
			window.Closed += window_Closed;
			window.KeyPressed += window_KeyPressed;
			
			changePiece();
			
			while(window.IsOpen) {
				float time = clock.ElapsedTime.AsSeconds();
				clock.Restart();
				timer += time;
				
				window.DispatchEvents();
				if(Keyboard.IsKeyPressed(Keyboard.Key.Down)) delay = 0.05f;

				// <- Move ->
				for(var i = 0; i<4; i++)
				{
					b[i] = a[i];
					a[i].x += dx;
				}
				if(!check()) 
					for(var i = 0; i<4; i++)
						a[i] = b[i];
				
				// Rotate
				if (rotate)
				{
					Point p = a[1]; //centre of rotation
					for(var i = 0; i<4; i++)
					{
						var x = a[i].y - p.y;
						var y = a[i].x - p.x;
						a[i].x = p.x - x;
						a[i].y = p.y + y;
					}
					if(!check())
						for(var i = 0; i<4; i++)
							a[i] = b[i];
				}
				
				// Tick
				if(timer > delay)
				{
					for(int i=0; i<4; i++)
					{
						b[i] = a[i];
						a[i].y += 1;
					}
					
					if(!check())
						changePiece();
					
					timer = 0;
				}
				
				// check lines
				int k = M - 1;
				for(int i = M-1; i>0; i--)
				{
					int count = 0;
					for (int j=0; j<N; j++)
					{
						if(field[i,j] > 0) count++;
						field[k,j] = field[i,j];
					}
					if(count < N) k--;
				}
				
				dx = 0; rotate = false; delay = 0.3f;
				
				// draw
				window.Clear(Color.White);
				window.Draw(background);
				
				for(int i = 0; i < M; i++)
					for(int j = 0; j < N; j++) {
						if(field[i,j] == 0) continue;
						s.TextureRect = new IntRect(field[i,j] * 18, 0, 18, 18);
						s.Position = new Vector2f(j * 18, i * 18);
						s.Position += new Vector2f(28,31); //offset
						window.Draw(s);
					}
				
				for(int i = 0; i < 4; i++) {
					s.TextureRect = new IntRect(colorNum * 18, 0, 18, 18);
					s.Position = new Vector2f(a[i].x * 18, a[i].y * 18);
					s.Position += new Vector2f(28,31); //offset
					window.Draw(s);
				}
				
				window.Draw(frame);
				window.Display();
			}
		}
	}
}
