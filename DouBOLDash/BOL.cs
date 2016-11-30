using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DouBOLDash
{
    class BOL : LevelObj
    {
        /* class that represents a BOL header */
        // this will store the offsets we use in Form1.cs
        // we call the other parse methods with these variables
        List<uint> offsets = new List<uint>();
        string type;
        float unk3;
        uint magic, unk1, unk2, unk4, unk5;
        byte numLaps, musicID;
        uint sec1Count, sec2Count, sec3Count, sec5Count, sec7Count, sec8Count, sec9Count;
        uint unk6, unk7, unk8, unk9, unk10, unk11, fileStart;
        uint sec1Offs, sec2Offs, sec3Offs, sec4Offs, sec5Offs, sec6Offs, sec7Offs, sec8Offs, sec9Offs, sec10Offs, sec11Offs;

        public BOL()
        {
            this.type = "BOL";
            this.magic = 0;
            this.unk1 = 0;
            this.unk2 = 0;
            this.unk3 = 0;
            this.unk4 = 0;
            this.unk5 = 0;
            this.unk6 = 0;
            this.unk7 = 0;
            this.unk8 = 0;
            this.unk9 = 0;
            this.unk10 = 0;
            this.unk11 = 0;
            this.fileStart = 0;
            this.numLaps = 0;
            this.musicID = 0;

            this.sec1Count = 0;
            this.sec2Count = 0;
            this.sec5Count = 0;
            this.sec7Count = 0;
            this.sec8Count = 0;
            this.sec3Count = 0;
            this.sec9Count = 0;

            this.sec1Offs = 0;
            this.sec2Offs = 0;
            this.sec3Offs = 0;
            this.sec4Offs = 0;
            this.sec5Offs = 0;
            this.sec6Offs = 0;
            this.sec7Offs = 0;
            this.sec8Offs = 0;
            this.sec9Offs = 0;
            this.sec10Offs = 0;
            this.sec11Offs = 0;

        }

        public void Parse(EndianBinaryReader reader)
        {
            this.magic = reader.ReadUInt32();
            this.unk1 = reader.ReadUInt32();
            this.unk2 = reader.ReadUInt32();

            this.unk3 = reader.ReadSingle();

            this.unk4 = reader.ReadUInt32();
            this.unk5 = reader.ReadUInt32();

            this.numLaps = reader.ReadByte();
            this.musicID = reader.ReadByte();

            this.sec1Count = reader.ReadUInt16();
            this.sec2Count = reader.ReadUInt16();
            this.sec5Count = reader.ReadUInt16();
            this.sec7Count = reader.ReadUInt16();
            this.sec8Count = reader.ReadUInt16();
            this.sec3Count = reader.ReadUInt16();
            this.sec9Count = reader.ReadUInt16();

            this.unk6 = reader.ReadUInt32();
            this.unk7 = reader.ReadUInt32();
            this.unk8 = reader.ReadUInt32();
            this.unk9 = reader.ReadUInt32();
            this.unk10 = reader.ReadUInt32();
            this.unk11 = reader.ReadUInt32();
            this.fileStart = reader.ReadUInt32();

            this.sec1Offs = reader.ReadUInt32();
            this.sec2Offs = reader.ReadUInt32();
            this.sec3Offs = reader.ReadUInt32();
            this.sec4Offs = reader.ReadUInt32();
            this.sec5Offs = reader.ReadUInt32();
            this.sec6Offs = reader.ReadUInt32();
            this.sec7Offs = reader.ReadUInt32();
            this.sec8Offs = reader.ReadUInt32();
            this.sec9Offs = reader.ReadUInt32();
            this.sec10Offs = reader.ReadUInt32();
            this.sec11Offs = reader.ReadUInt32();

            offsets.Add(this.sec1Offs);
            offsets.Add(this.sec2Offs);
            offsets.Add(this.sec3Offs);
            offsets.Add(this.sec4Offs);
            offsets.Add(this.sec5Offs);
            offsets.Add(this.sec6Offs);
            offsets.Add(this.sec7Offs);
            offsets.Add(this.sec8Offs);
            offsets.Add(this.sec9Offs);
            offsets.Add(this.sec10Offs);
            offsets.Add(this.sec11Offs);

            reader.ReadBytes(12); // padding
        }

        public uint returnOffset(int id)
        {
            uint[] offsetArry = offsets.ToArray();
            return offsetArry[id];
        }
    }

    class EnemyRoutes : LevelObj
    {
        float xPos, yPos, zPos;
        int pointSetting, link;
        float scale;
        uint groupSetting;
        byte group, pointSetting2;

        public EnemyRoutes()
        {
            this.xPos = 0;
            this.yPos = 0;
            this.zPos = 0;

            this.pointSetting = 0;
            this.link = -1;
            this.scale = 0;
            this.groupSetting = 0;
            this.group = 0;
            this.pointSetting2 = 0;
        }

        public void Parse(EndianBinaryReader reader)
        {
            
        }
    }

    class Checkpoints : LevelObj
    {

    }

    class Respawns : LevelObj
    {
        float xPos, yPos, zPos;
        int xRot, yRot, zRot, respawnID, unk1, unk2, unk3;

        public Respawns()
        {
            this.xPos = 0;
            this.yPos = 0;
            this.zPos = 0;

            this.xRot = 0;
            this.yRot = 0;
            this.zRot = 0;

            this.respawnID = 0;
            this.unk1 = 0;
            this.unk2 = 0;
            this.unk3 = 0;
        }

        public void Parse(EndianBinaryReader reader)
        {

        }
    }

    abstract class LevelObj
    {

    }
}
