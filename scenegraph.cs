using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using OpenTK;
using OpenTK.Graphics.OpenGL;


namespace Template
{
    public class scenegraph
    {
        public Mesh m;
        public Matrix4 localT;
        public Texture localTex;
        public Shader localS;
        public List<scenegraph> childnodes;

        public scenegraph(Mesh ab, Matrix4 bc, Texture cd, Shader de)
        {
            childnodes = new List<scenegraph>();
            localT = bc;
            localTex = cd;
            localS = de;
            m = ab;
        }

        public void addNode(Mesh a, Matrix4 b, Texture c, Shader d)
        {
            childnodes.Add(new scenegraph(a, b, c, d));
        }

        public List<scenegraph> getChildren()
        {
            return childnodes;
        }

        public Matrix4 localTrans
        {
            get { return localT; }
            set { localT = value; }
        }

        public Texture localText
        {
            get { return localTex; }
            set { localTex = value; }
        }

        public void Render(Matrix4 camera, Matrix4 toWorld, Matrix4 toMove)
        {
            m.Render(localS, localT * camera,toWorld*localT, toMove, localTex);
            if (childnodes.Count != 0)
            {
                foreach(scenegraph x in childnodes)
                {
                    x.Render(localT*camera, toWorld*localT, toMove);
                }
            }
        }
    }
}
