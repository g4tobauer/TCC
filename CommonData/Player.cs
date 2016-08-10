using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonData
{
    public enum MeshType
    {
        Triangle = 1,
        Quad = 2
    }
    public class Player
    {
        public string Message { get; set; }

        #region new
        public MeshType meshType { get; set; }
        public string PlayerName { get; set; }
        public Vector3 Position { get; set; }

        public Player() { }
        public Player(MeshType meshType, string playerName)
        {
            this.meshType = meshType;
            PlayerName = playerName;
        }
        #endregion
    }
}
