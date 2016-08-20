using CommonData;
using Newtonsoft.Json;
using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tao.FreeGlut;

namespace ClientApplication.Classes
{
    public class ClientOpenGLScreen
    {
        private float x = 0, y = 0;
        private bool left, right, up, down;
        private const int Width = 1280;
        private const int Height = 720;
        private ShaderProgram program;
        private List<PlayerRenderContainer> lstPlayerRenderContainer;
        private GameInstance GameInstance;
        private static System.Diagnostics.Stopwatch watch;
        private readonly Client ClientConection;
        private int window;
        private bool _UpdateLoop;

        public ClientOpenGLScreen(Client Client)
        {
            ClientConection = Client;
            InitGL();
        }
        
        public void MakeGameInstance(GameInstance GameInstance)
        {
            this.GameInstance = GameInstance;
        }

        public void AddGameInstanceToList(GameInstance GameInstance)
        {
            lstPlayerRenderContainer.Add(new PlayerRenderContainer(this.GameInstance.Player));
        }

        public void MainLoop()
        {
            _UpdateLoop = true;
            Glut.glutMainLoop();
        }

        public void Close()
        {
            Glut.glutDestroyWindow(window);
        }

        private void SenderUpdate()
        {
            GameInstance.opCode = Operation.Update;
            var json = JsonConvert.SerializeObject(GameInstance);
            ClientConection.Send(json);
        }
        public void ReceiveUpdate(string msg)
        {
            Console.WriteLine(msg);
        }

        private void Exit()
        {
            if (_UpdateLoop)
            {
                if (ClientConection.Exit(GameInstance))
                {
                    _UpdateLoop = false;
                }
            }
        }

        #region Initialization
        private void InitWindow()
        {
            // create an OpenGL window
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);
            Glut.glutInitWindowSize(Width, Height);
            window = Glut.glutCreateWindow("OpenGL Tutorial");
        }
        private void InitFunctionEvents()
        {
            // provide the Glut callbacks that are necessary for running this tutorial
            Glut.glutIdleFunc(OnRenderFrame);
            Glut.glutDisplayFunc(OnDisplay);
            Glut.glutKeyboardFunc(OnKeyboardDown);
            Glut.glutKeyboardUpFunc(OnKeyboardUp);
            Glut.glutCloseFunc(OnClose);
        }
        private void InitProgram()
        {
            // compile the shader program
            program = new ShaderProgram(VertexShader, FragmentShader);

            // set the view and projection matrix, which are static throughout this tutorial
            program.Use();
            program["projection_matrix"].SetValue(Matrix4.CreatePerspectiveFieldOfView(0.45f, (float)Width / Height, 0.1f, 1000f));
            program["view_matrix"].SetValue(Matrix4.LookAt(new Vector3(0, 0, 10), Vector3.Zero, Vector3.Up));
            watch = System.Diagnostics.Stopwatch.StartNew();
        }
        private void InitGL()
        {
            InitWindow();
            InitFunctionEvents();
            InitProgram();

            lstPlayerRenderContainer = new List<PlayerRenderContainer>();
        }
        #endregion

        #region FunctionEvents
        private void OnRenderFrame()
        {
            // set up the OpenGL viewport and clear both the color and depth bits
            Gl.Viewport(0, 0, Width, Height);
            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // use our shader program
            Gl.UseProgram(program);

            foreach (var lst in lstPlayerRenderContainer)
            {
                lst.MovePlayer(new Vector3(x, y, 0));
                lst.Render(program);
            }

            watch.Stop();
            float deltaTime = (float)watch.ElapsedTicks / System.Diagnostics.Stopwatch.Frequency;
            watch.Restart();

            if (right) x += deltaTime;
            if (left) x -= deltaTime;
            if (up) y += deltaTime;
            if (down) y -= deltaTime;

            GameInstance.Player.Position = new Vector3(x, y, 0);

            SenderUpdate();

            Glut.glutSwapBuffers();
        }
        private void OnDisplay() { }
        private void OnKeyboardDown(byte key, int x, int y)
        {
            if (key == 'w') up = true;
            else if (key == 's') down = true;
            else if (key == 'd') right = true;
            else if (key == 'a') left = true;
            else if (key == 27) Glut.glutLeaveMainLoop();
        }
        private void OnKeyboardUp(byte key, int x, int y)
        {
            if (key == 'w') up = false;
            else if (key == 's') down = false;
            else if (key == 'd') right = false;
            else if (key == 'a') left = false;
        }
        private void OnClose()
        {
            Exit();
            program.DisposeChildren = true;
            program.Dispose();
            foreach (var lst in lstPlayerRenderContainer)
            {
                lst.Dispose();
            }
        }
        #endregion


        public static readonly string VertexShader = @"
        #version 130

        in vec3 vertexPosition;

        uniform mat4 projection_matrix;
        uniform mat4 view_matrix;
        uniform mat4 model_matrix;

        void main(void)
        {
        gl_Position = projection_matrix * view_matrix * model_matrix * vec4(vertexPosition, 1);
        }
        ";

        public static readonly string FragmentShader = @"
        #version 130

        out vec4 fragment;

        void main(void)
        {
        fragment = vec4(1, 1, 1, 1);
        }
        ";

    }
}
