/*
 * Erstellt mit SharpDevelop.
 * Benutzer: root
 * Datum: 19.07.2018
 * Zeit: 12:02
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
using System;
using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace TutorialGame
{
	public class Racing
	{
		RenderWindow app;
		Texture t1, t2;
		Sprite sBackground, sCar;

		float x, y;
		float speed, angle;
		float maxSpeed;
		float acc, dec;
		float turnSpeed;
		
		int offsetX, offsetY;
		
		bool Up, Right, Down, Left;
		
		const int N = 5;
		
		Car[] car;
		Color[] colours;
		
		void app_Closed(object sender, EventArgs e)
		{
			app.Close();
		}
		
		struct Car {
			public float x, y, speed, angle;
			
			public Car(float speed = 2, float angle = 0) {
				x = 0;
				y = 0;
				this.speed = speed;
				this.angle = angle;
			}
			
			
			public void move() {
				x += (float)(Math.Sin(angle) * speed);
				y -= (float)(Math.Cos(angle) * speed);
			}
		}
		
		public Racing()
		{
			app = new RenderWindow(new VideoMode(640, 480), "Car Racing Game!");
			app.SetFramerateLimit(60);
			
			t1 = new Texture("images/07/background.png");
			t2 = new Texture("images/07/car.png");
			
			sBackground = new Sprite(t1);
			sBackground.Scale = new Vector2f(2,2);
			sCar = new Sprite(t2);
			sCar.Position = new Vector2f(320,240);
			sCar.Origin = new Vector2f(22, 22);
			
			x = 300f;
			y = 300f;
			speed = 0;
			angle = 0;
			maxSpeed = 12.0f;
			acc = 0.2f;
			dec = 0.3f;
			turnSpeed = 0.08f;
			
			//Todo: continue at 2:08
			//https://www.youtube.com/watch?v=YzhhVHb0WVY
			car = new Car[N];
			for(int i=0; i<N; i++) {
				car[i] = new Car { 
					x = 300 + i * 50,
					y = 1700 + i * 80,
					speed = 7 + i
				};
			}
			
			colours = new Color[] {Color.Red, Color.Green, Color.Magenta, Color.Blue, Color.White};
			
			app.Closed += app_Closed;
			
			while(app.IsOpen) {
				app.DispatchEvents();
				
				Up = false;
				Right = false;
				Down = false;
				Left = false;
			
				if(Keyboard.IsKeyPressed(Keyboard.Key.Up)) Up = true;
				if(Keyboard.IsKeyPressed(Keyboard.Key.Right)) Right = true;
				if(Keyboard.IsKeyPressed(Keyboard.Key.Down)) Down = true;
				if(Keyboard.IsKeyPressed(Keyboard.Key.Left)) Left = true;
				
				//car movement
				if(Up && speed < maxSpeed)
					if(speed<0) speed += dec;
					else speed += acc;
			
				if(Down && speed > -maxSpeed)
					if(speed>0) speed -= dec;
					else speed -= acc;

				if(!Up && !Down)
					if(speed - dec > 0) speed -= dec;
					else if (speed + dec < 0) speed += dec;
		        	else speed = 0;
		        
	        	if(Right && speed != 0f) angle += turnSpeed * speed / maxSpeed;
		        if(Left && speed != 0f) angle -= turnSpeed * speed / maxSpeed;
				
		        car[0].speed = speed;
		        car[0].angle = angle;
		        
		        for(int i=0; i<N; i++)
		        	car[i].move();
		        
//		        x += (float)(Math.Sin(angle) * speed);
//		        y -= (float)(Math.Cos(angle) * speed);
		        
		        
		        if(car[0].x > 320) offsetX = (int)(car[0].x - 320);
		        if(car[0].y > 240) offsetY = (int)(car[0].y - 240);
							
				//draw
				app.Clear(Color.White);
				sBackground.Position = new Vector2f(car[0].x - offsetX, car[0].y - offsetY); //new Vector2f(-x, -y);
				app.Draw(sBackground);
				
				app.Clear(Color.White);
				app.Draw(sBackground);
				
				for(int i=0; i<N; i++) {
					sCar.Position = new Vector2f(car[i].x - offsetX, car[i].y - offsetY);//new Vector2f(x, y);
					sCar.Rotation = (float)(car[i].angle * 180d / Math.PI);
					sCar.Color = colours[i];
					app.Draw(sCar);
				}
				
				app.Display();
			}
		}
	}
}
