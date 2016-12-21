/*
    Copyright 2016-2017 shibboleet
    This file is part of DouBOL Dash.
    DouBOL Dash is free software: you can redistribute it and/or modify it under
    the terms of the GNU General Public License as published by the Free
    Software Foundation, either version 3 of the License, or (at your option)
    any later version.
    DouBOL Dash is distributed in the hope that it will be useful, but WITHOUT ANY
    WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS
    FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
    You should have received a copy of the GNU General Public License along
    with DouBOL Dash. If not, see http://www.gnu.org/licenses/.
*/
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

            bcoInfo.unkOffs1 = reader.ReadUInt32(); // always the same value. probs not an offset
            bcoInfo.unkOffs2 = reader.ReadUInt32(); // always the same value. probs not an offset

            bcoInfo.unk7 = reader.ReadUInt16(); // last section count
            bcoInfo.unk8 = reader.ReadUInt16();
            bcoInfo.firstSectionOffs = reader.ReadUInt32(); // unknown1

            bcoInfo.triOffs = reader.ReadUInt32();
            bcoInfo.vertOffs = reader.ReadUInt32();
            bcoInfo.unkOffs3 = reader.ReadUInt32(); // unknown2

            bcoList.Add(bcoInfo);
        }

        public List<BCOInformation> returnBCOInf()
        {
            return bcoList;
        }
    }
    
    class BCOUnknown0
    {
        byte length;
        byte[] values;
        uint index;

        public void Parse(EndianBinaryReader reader, uint count)
        {
            for (uint i = 0; i < count; i++)
            {
                length = reader.ReadByte();
                values = reader.ReadBytes(3);
                index = reader.ReadUInt32();

                // 3 bytes => 24-bit integer
                var value = values[0] + (values[1] << 8) + (values[2] << 16);

                Console.WriteLine("Value: " + value);
            }
        }
    }

    class BCOUnknown1
    {
        int unk1;
        public void Parse(EndianBinaryReader reader, uint count)
        {
            for (uint i = 0; i < count; i++)
            {
                unk1 = reader.ReadInt16();
            }
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

                tri.unk2 = reader.ReadInt16();
                tri.unk3 = reader.ReadInt16();
                tri.unk4 = reader.ReadInt16();

                tri.collisionFlag = reader.ReadInt16();

                tri.unk5 = reader.ReadInt16();
                tri.unk6 = reader.ReadInt16();
                tri.unk7 = reader.ReadInt16();
                tri.unk8 = reader.ReadInt16();

                reader.ReadBytes(0x04); // probably padding

                tri.colStr = tri.collisionFlag.ToString("X4");

                tris.Add(tri);
            }
        }

        public List<BCOTri> returnTris()
        {
            return tris;
        }
    }

    class BCOUnknown2
    {
        byte id1, id2;
        short unk1;
        uint unk2, unk3;

        public void Parse(EndianBinaryReader reader, uint count)
        {
            id1 = reader.ReadByte();
            id2 = reader.ReadByte();
            unk1 = reader.ReadInt16();
            unk2 = reader.ReadUInt32();
            unk3 = reader.ReadUInt32();
        }
    }


    public class BCOInformation
    {
        public string magic;
        public ushort unk1, unk2, unk3, unk4, unk5, unk6;
        public uint unkOffs1, unkOffs2;
        public ushort unk7, unk8, unk9, unk10, unk11;
        public uint triOffs, vertOffs, unkOffs3, firstSectionOffs;
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
        public short unk2, unk3, unk4, unk5, unk6, unk7, unk8;

        [DisplayName("Collision Flags"), Category("Collision Flags"), Description("The type of collision this triangle has.")]
        public string CollisionFlag
        {
            get { return colStr; }
            set { colStr = value; }
        }

        [DisplayName("Unknown Index 1"), Category("Indexes"), Description("An unknown index.")]
        public short UnknownIndex1
        {
            get { return unk6; }
            set { unk6 = value; }
        }

        [DisplayName("Unknown Index 2"), Category("Indexes"), Description("An unknown index.")]
        public short UnknownIndex2
        {
            get { return unk7; }
            set { unk7 = value; }
        }

        [DisplayName("Unknown Index 3"), Category("Indexes"), Description("An unknown index.")]
        public short UnknownIndex3
        {
            get { return unk8; }
            set { unk8 = value; }
        }

        [DisplayName("Unknown Short 1"), Category("Unknown"), Description("An unknown short.")]
        public short UnknownValue1
        {
            get { return unk2; }
            set { unk2 = value; }
        }

        [DisplayName("Unknown Short 2"), Category("Unknown"), Description("An unknown short.")]
        public short UnknownValue2
        {
            get { return unk3; }
            set { unk3 = value; }
        }

        [DisplayName("Unknown Short 3"), Category("Unknown"), Description("An unknown short.")]
        public short UnknownValue3
        {
            get { return unk4; }
            set { unk4 = value; }
        }

        [DisplayName("Unknown Short 4"), Category("Unknown"), Description("An unknown short.")]
        public short UnknownShort4
        {
            get { return unk5; }
            set { unk5 = value; }
        }

        [DisplayName("Unknown Float 1"), Category("Unknown"), Description("An unknown float.")]
        public float UnknownFloat1
        {
            get { return unk1; }
            set { unk1 = value; }
        }
    }
}
