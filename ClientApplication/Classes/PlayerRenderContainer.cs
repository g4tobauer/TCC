using CommonData;
using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApplication.Classes
{
    public class PlayerRenderContainer
    {
        private Player Player;
        private Vector3 InitVector;

        public VBO<Vector3> VBO { get; set; }
        public VBO<int> VBE { get; set; }

        public PlayerRenderContainer(Player player)
        {
            Player = player;
            InitPlayerAttributes();
        }
        private void InitPlayerAttributes()
        {
            switch (Player.meshType)
            {
                case MeshType.Triangle:
                    VBO = new VBO<Vector3>(new Vector3[] { new Vector3(0, 1, 0), new Vector3(-1, -1, 0), new Vector3(1, -1, 0) });
                    VBE = new VBO<int>(new int[] { 0, 1, 2 }, BufferTarget.ElementArrayBuffer);
                    InitVector = new Vector3(1.5f, 0, 0);
                    break;

                case MeshType.Quad:
                    VBO = new VBO<Vector3>(new Vector3[] { new Vector3(-1, 1, 0), new Vector3(1, 1, 0), new Vector3(1, -1, 0), new Vector3(-1, -1, 0) });
                    VBE = new VBO<int>(new int[] { 0, 1, 2, 2, 3, 0 }, BufferTarget.ElementArrayBuffer);
                    InitVector = new Vector3(-1.5f, 0, 0);
                    break;
            }
            MovePlayer(Player.Position);
        }

        private void MovePlayer(Vector3 MoveVector)
        {
            Player.Position = InitVector + MoveVector;
        }

        public void Render(ShaderProgram program)
        {
            // transform the Player
            program["model_matrix"].SetValue(Matrix4.CreateTranslation(Player.Position));

            // bind the vertex attribute arrays for the Player (the easy way)
            Gl.BindBufferToShaderAttribute(VBO, program, "vertexPosition");
            Gl.BindBuffer(VBE);

            // draw the triangle
            Gl.DrawElements(BeginMode.Triangles, VBE.Count, DrawElementsType.UnsignedInt, IntPtr.Zero);
        }

        public void Dispose()
        {
            VBO.Dispose();
            VBE.Dispose();
        }
    }
}
