using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OpenTK;
using System.ComponentModel;

namespace DouBOLDash
{
    // should i make this a struct
    class BCO
    {
        List<BCOInformation> bcoList = new List<BCOInformation>();

        public void Parse(EndianBinaryReader reader)
        {
            BCOInformation bcoInfo = new BCOInformation();
            bcoInfo.magic = Encoding.ASCII.GetString(reader.ReadBytes(4));
            bcoInfo.unk1 = reader.ReadUInt16();
            bcoInfo.unk2 = reader.ReadUInt16();
            bcoInfo.unk3 = reader.ReadUInt16();
            bcoInfo.unk4 = reader.ReadUInt16();
            bcoInfo.unk5 = reader.ReadUInt16();
            bcoInfo.unk6 = reader.ReadUInt16();

            bcoInfo.unkOffs1 = reader.ReadUInt32();
            bcoInfo.unkOffs2 = reader.ReadUInt32();

            bcoInfo.unk7 = reader.ReadUInt16();
            bcoInfo.unk8 = reader.ReadUInt16();
            bcoInfo.unk9 = reader.ReadUInt16();
            bcoInfo.unk10 = reader.ReadUInt16();

            bcoInfo.triOffs = reader.ReadUInt32();
            bcoInfo.vertOffs = reader.ReadUInt32();
            bcoInfo.unkOffs3 = reader.ReadUInt32();

            bcoList.Add(bcoInfo);
        }

        public List<BCOInformation> returnBCOInf()
        {
            return bcoList;
        }
    }

    class BCOVerts
    {
        List<BCOVert> bcoVerts = new List<BCOVert>();
        List<Vector3> VertList = new List<Vector3>();
        Vector3 vec1, vec2, vec3;

        public void Parse(EndianBinaryReader reader, uint count)
        {
            for (uint i = 0; i < count + 1; i++)
            {
                BCOVert bcoVert = new BCOVert();
                bcoVert.xPos1 = reader.ReadSingle();
                bcoVert.yPos1 = reader.ReadSingle();
                bcoVert.zPos1 = reader.ReadSingle();

                vec1 = new Vector3(bcoVert.xPos1, bcoVert.yPos1,bcoVert.zPos1);
                VertList.Add(vec1);

                bcoVert.xPos2 = reader.ReadSingle();
                bcoVert.yPos2 = reader.ReadSingle();
                bcoVert.zPos2 = reader.ReadSingle();

                vec2 = new Vector3(bcoVert.xPos2, bcoVert.yPos2, bcoVert.zPos2);
                VertList.Add(vec2);

                bcoVert.xPos3 = reader.ReadSingle();
                bcoVert.yPos3 = reader.ReadSingle();
                bcoVert.zPos3 = reader.ReadSingle();

                vec3 = new Vector3(bcoVert.xPos3, bcoVert.yPos3, bcoVert.zPos3);
                VertList.Add(vec3);

                bcoVerts.Add(bcoVert);
            }
        }

        public List<BCOVert> returnBCOVerts()
        {
            return bcoVerts;
        }

        public List<Vector3> returnVectors()
        {
            return VertList;
        }
    }

    class BCOTris
    {
        List<BCOTri> tris = new List<BCOTri>();

        public void Parse(EndianBinaryReader reader, uint count)
        {
            for (uint i = 0; i < count; i++)
            {
                BCOTri tri = new BCOTri();
                tri.index1 = reader.ReadInt32();
                tri.index2 = reader.ReadInt32();
                tri.index3 = reader.ReadInt32();

                tri.unk1 = reader.ReadSingle();

                reader.ReadBytes(6);
                tri.collisionFlag = reader.ReadInt16();
                reader.ReadBytes(0xC);

                tri.colStr = tri.collisionFlag.ToString("X4");

                tris.Add(tri);
            }
        }

        public List<BCOTri> returnTris()
        {
            return tris;
        }
    }


    public class BCOInformation
    {
        public string magic;
        public ushort unk1, unk2, unk3, unk4, unk5, unk6;
        public uint unkOffs1, unkOffs2;
        public ushort unk7, unk8, unk9, unk10, unk11;
        public uint triOffs, vertOffs, unkOffs3;
    }

    public class BCOVert
    {
        // class that represents a group of 3 verts
        public float xPos1, yPos1, zPos1;
        public float xPos2, yPos2, zPos2;
        public float xPos3, yPos3, zPos3;
    }

    public class BCOTri
    {
        // class that represents a single BCO triangle.
        public int index1, index2, index3;
        public float unk1;
        public short collisionFlag;
        public string colStr;

        [DisplayName("Collision Flag"), Category("Settings"), Description("The type of collision this triangle has.")]
        public string CollisionFlag
        {
            get { return colStr; }
            set { colStr = value; }
        }
    }
}
