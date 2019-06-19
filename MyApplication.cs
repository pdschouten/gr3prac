using System.Diagnostics;
using OpenTK;
using OpenTK.Input;
using System;

namespace Template
{
	class MyApplication
	{
		// member variables
		public Surface screen;                  // background surface for printing etc.
		Mesh mesh, floor, eye, moon, earth;                  // a mesh to draw using OpenGL
		const float PI = 3.1415926535f;         // PI
		float a = 0;                            // teapot rotation angle
		Stopwatch timer;                        // timer for measuring frame duration
		Shader shader;                          // shader to use for rendering
		Texture wood, eyeR, crater, land, stars;                     // texture to use for rendering
        scenegraph meshes;
        Matrix4 Tmove = Matrix4.CreateTranslation(0,0,0);

		// initialize
		public void Init()
		{
            screen.Print("Move: UP, DOWN, LEFT, RIGHT", 10, 10, 10);
			// load teapot
			mesh = new Mesh( "../../assets/teapot.obj" );
			floor = new Mesh( "../../assets/floor.obj" );
            eye = new Mesh( "../../assets/eyeball.obj");
            moon = new Mesh("../../assets/moon.obj");
            earth = new Mesh("../../assets/earth.obj");
            // initialize stopwatch
            timer = new Stopwatch();
			timer.Reset();
			timer.Start();
			// create shaders
			shader = new Shader( "../../shaders/vs.glsl", "../../shaders/fs.glsl" );
			// load a texture
			wood = new Texture( "../../assets/wood.jpg" );
            eyeR = new Texture("../../assets/ref1.jpg");
            crater = new Texture("../../assets/crater.jpg");
            land = new Texture("../../assets/land.jpg");
            stars = new Texture("../../assets/stars.jpg");
            meshes = new scenegraph(floor, Matrix4.CreateScale(1.0f) * Matrix4.CreateFromAxisAngle(new Vector3(0, 1, 0), 0), stars, shader);
            meshes.addNode(mesh, Matrix4.CreateScale(1.0f) * Matrix4.CreateFromAxisAngle(new Vector3(0, 1, 0), 0), stars, shader);
            
        }

		// tick for background surface
		public void Tick()
		{
			screen.Clear( 0 );
		}

		// tick for OpenGL rendering code
		public void RenderGL()
		{
            screen.Print("Camera Move: UP, DOWN, LEFT, RIGHT", 2, 2, 0xffff00);
            screen.Print("Rotate: MOUSE LEFT, MOUSE RIGHT", 2, 32, 0xffff00);
            // measure frame duration
            float frameDuration = timer.ElapsedMilliseconds;
			timer.Reset();
			timer.Start();

			// prepare matrix for vertex shader
			float angle90degrees = PI / 2;
			Matrix4 Tpot = Matrix4.CreateScale( 0.2f ) * Matrix4.CreateFromAxisAngle( new Vector3( 1, 0, 0 ), a*3 );
            Matrix4 Tfloor = Matrix4.CreateScale( 6.0f ) * Matrix4.CreateFromAxisAngle( new Vector3( 0, 1, 0 ), a );
			Matrix4 Tcamera = Matrix4.CreateTranslation( new Vector3( 0, -20.5f, 0 ) ) * Matrix4.CreateFromAxisAngle( new Vector3( 1, 0, 0 ), angle90degrees );
			Matrix4 Tview = Matrix4.CreatePerspectiveFieldOfView( 1.2f, 1.3f, .1f, 1000 );

            //change transforms
            meshes.localT = Tfloor;
            meshes.getChildren().localT = Tpot;


            // update rotation
            a += 0.0001f * frameDuration;
			if( a > 2 * PI ) a -= 2 * PI;

            //render via scenegraph
            meshes.Render(Tmove * Tcamera * Tview);

            // render scene
            //mesh.Render(shader, Tpot * Tcamera * Tview, wood);
            //floor.Render(shader, Tfloor * Tcamera * Tview, wood);

            
        }
        public void KeyMove(Matrix4 mov)
        {
            Tmove *= mov;
        }
        
	}
}