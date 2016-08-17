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
        private int width = 1280, height = 720;
        private ShaderProgram program;
        List<PlayerRenderContainer> lstPlayerRenderContainer;
        private static System.Diagnostics.Stopwatch watch;
        private Client ClientConection;
        private GameInstance GameInstance;
        
        public ClientOpenGLScreen(Client Client)
        {
            ClientConection = Client;
            InitGL();
        }


        private Thread t1;
        private bool _UpdateLoop;
        private bool _ReceiveLoop;


        #region prontos
        private Player MakeQuadPlayer(string playerName)
        {
            return new Player(MeshType.Quad, playerName);
        }
        private Player MakeTrianglePlayer(string playerName)
        {
            return new Player(MeshType.Triangle, playerName);
        }

        public void QuadPlayer(string playerName)
        {
            GameInstance = new GameInstance();
            GameInstance.Player = MakeQuadPlayer(playerName);
            if (ClientConection.Join(GameInstance))
                lstPlayerRenderContainer.Add(new PlayerRenderContainer(GameInstance.Player));                
            //Join();
        }
        public void TrianglePlayer(string playerName)
        {
            GameInstance = new GameInstance();
            GameInstance.Player = MakeTrianglePlayer(playerName);
            if(ClientConection.Join(GameInstance))
                lstPlayerRenderContainer.Add(new PlayerRenderContainer(GameInstance.Player));
            //Join();
        }

        public void Join()
        {
            if (t1 == null)
            {
                GameInstance.opCode = Operation.Join;
                var json = JsonConvert.SerializeObject(GameInstance);
                ClientConection.send(json);
            }
        }

        public void MainLoop()
        {
            _UpdateLoop = true;
            Glut.glutMainLoop();
        }

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

        private void OnDisplay() { }
        #endregion

        #region Nao Pronto

        private int window;

        private void InitGL()
        {
            // create an OpenGL window
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);
            Glut.glutInitWindowSize(width, height);
            window = Glut.glutCreateWindow("OpenGL Tutorial");

            // provide the Glut callbacks that are necessary for running this tutorial
            Glut.glutIdleFunc(OnRenderFrame);
            Glut.glutDisplayFunc(OnDisplay);
            Glut.glutKeyboardFunc(OnKeyboardDown);
            Glut.glutKeyboardUpFunc(OnKeyboardUp);
            Glut.glutCloseFunc(OnClose);

            // compile the shader program
            program = new ShaderProgram(VertexShader, FragmentShader);

            // set the view and projection matrix, which are static throughout this tutorial
            program.Use();
            program["projection_matrix"].SetValue(Matrix4.CreatePerspectiveFieldOfView(0.45f, (float)width / height, 0.1f, 1000f));
            program["view_matrix"].SetValue(Matrix4.LookAt(new Vector3(0, 0, 10), Vector3.Zero, Vector3.Up));
            watch = System.Diagnostics.Stopwatch.StartNew();

            lstPlayerRenderContainer = new List<PlayerRenderContainer>();
        }
        private void SenderUpdate()
        {
            GameInstance.opCode = Operation.Update;
            var json = JsonConvert.SerializeObject(GameInstance);
            ClientConection.send(json);
        }

        private void Exit()
        {
            if (_UpdateLoop || _ReceiveLoop)
            {
                _UpdateLoop = false;
                _ReceiveLoop = false;

                GameInstance.opCode = Operation.Exit;
                var json = JsonConvert.SerializeObject(GameInstance);
                ClientConection.send(json);

                if (t1 != null && t1.IsAlive)
                    t1.Abort();

                t1 = null;

                ClientConection.Close();
            }
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
            ClientConection.Close();
        }
        public void Close()
        {
            OnClose();
            Glut.glutDestroyWindow(window);
        }
        private void OnRenderFrame()
        {
            // set up the OpenGL viewport and clear both the color and depth bits
            Gl.Viewport(0, 0, width, height);
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
