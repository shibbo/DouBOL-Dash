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
        List<uint> counts = new List<uint>();

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

            counts.Add(this.sec1Count);
            counts.Add(this.sec2Count);
            counts.Add(this.sec5Count);
            counts.Add(this.sec7Count);
            counts.Add(this.sec8Count);
            counts.Add(this.sec3Count);
            counts.Add(this.sec9Count);

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

        public uint returnCount(int id)
        {
            uint[] countArry = counts.ToArray();
            return countArry[id];
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

        public void Parse(EndianBinaryReader reader, uint count)
        {
            // failsafe part 2
            if (count == 0)
                Console.WriteLine("EnemyCount is 0, skipping...");
            else
            {
                for (int i = 0; i < count; i++)
                {
                    this.xPos = reader.ReadSingle();
                    this.yPos = reader.ReadSingle();
                    this.zPos = reader.ReadSingle();

                    this.pointSetting = reader.ReadInt16();
                    this.link = reader.ReadInt16();
                    this.scale = reader.ReadSingle();
                    this.groupSetting = reader.ReadUInt16();

                    this.group = reader.ReadByte();
                    this.pointSetting2 = reader.ReadByte();

                    reader.ReadBytes(8);
                }
            }
        }
    }

    class CheckpointGroup : LevelObj
    {
        uint pointLength, groupLink, index;
        int prev1, prev2, prev3, prev4;
        int next1, next2, next3, next4;

        Dictionary<uint, uint> dict1 = new Dictionary<uint, uint>();

        public CheckpointGroup()
        {
            this.pointLength = 0;
            this.groupLink = 0;
            this.prev1 = 0;
            this.prev2 = 0;
            this.prev3 = 0;
            this.prev4 = 0;

            this.next1 = 0;
            this.next2 = 0;
            this.next3 = 0;
            this.next4 = 0;

            this.index = 0;
        }

        public void Parse(EndianBinaryReader reader, uint count)
        {
            if (count == 0)
                Console.WriteLine("CheckpointGroup is 0, skipping...");
            else
            {
                for (uint i = 0; i < count; i++)
                {
                    this.pointLength = reader.ReadUInt16();
                    this.groupLink = reader.ReadUInt16();

                    this.prev1 = reader.ReadInt16();
                    this.prev2 = reader.ReadInt16();
                    this.prev3 = reader.ReadInt16();
                    this.prev4 = reader.ReadInt16();

                    this.next1 = reader.ReadInt16();
                    this.next2 = reader.ReadInt16();
                    this.next3 = reader.ReadInt16();
                    this.next4 = reader.ReadInt16();

                    this.index = i;

                    dict1.Add(this.index, this.pointLength);
                }
            }
        }

        public Dictionary<uint, uint> returnDictionary()
        {
            return dict1;
        }
    }

    class Checkpoint : LevelObj
    {
        float xPosStart, yPosStart, zPosStart;
        float xPosEnd, yPosEnd, zPosEnd;
        uint groupID;

        public Checkpoint()
        {
            this.xPosStart = 0;
            this.yPosStart = 0;
            this.zPosStart = 0;

            this.xPosEnd = 0;
            this.yPosEnd = 0;
            this.zPosEnd = 0;

            this.groupID = 0;
        }

        public void Parse(EndianBinaryReader reader, Dictionary<uint, uint> dictionary, uint count)
        {
            /* 
             * I haven't seen a multi-grouped checkpoint track
             * this should allow support for these just in case
            */
            if (count == 0)
                Console.WriteLine("CheckpointCount is 0, skipping...");
            else
            {
                for (uint i = 0; i < count; i++)
                {
                    uint countInSec = dictionary[i];
                    for (uint j = 0; j < countInSec; j++)
                    {
                        this.groupID = i;

                        this.xPosStart = reader.ReadSingle();
                        this.yPosStart = reader.ReadSingle();
                        this.zPosStart = reader.ReadSingle();

                        this.xPosEnd = reader.ReadSingle();
                        this.yPosEnd = reader.ReadSingle();
                        this.zPosEnd = reader.ReadSingle();

                        reader.ReadBytes(4);
                    }

                    Console.WriteLine("Finished Group " + i);
                }
            }
        }
    }

    class RouteGroup : LevelObj
    {
        Dictionary<uint, GroupStruct> groupDict = new Dictionary<uint, GroupStruct>();
        GroupStruct group = new GroupStruct();
        uint pointLength, pointStart, groupID;

        public RouteGroup()
        {
            this.pointLength = 0;
            this.pointStart = 0;
            this.groupID = 0; // program generated, not part of BOL
        }

        public void Parse(EndianBinaryReader reader, uint count)
        {
            for (uint i = 0; i < count; i++)
            {
                this.pointLength = reader.ReadUInt16();
                this.pointStart = reader.ReadUInt16();
                this.groupID = i;

                reader.ReadBytes(12);

                group.pointLength = this.pointLength;
                group.pointStart = this.pointStart;

                groupDict.Add(i, group);
                
            }
        }

        public Dictionary<uint, GroupStruct> returnDictionary()
        {
            return groupDict;
        }
    }

    class RoutePoint : LevelObj
    {
        float xPos, yPos, zPos;

        public RoutePoint()
        {
            this.xPos = 0;
            this.yPos = 0;
            this.zPos = 0;
        }
        
        public void Parse(EndianBinaryReader reader, Dictionary<uint, GroupStruct> groupDict)
        {
            uint count = (uint)groupDict.Count();

            for (uint i = 0; i < count; i++)
            {
                GroupStruct group = groupDict[i];
                for (uint j = 0; j < group.pointLength; j++)
                {
                    this.xPos = reader.ReadSingle();
                    this.yPos = reader.ReadSingle();
                    this.zPos = reader.ReadSingle();

                    reader.ReadBytes(20);
                }
            }
        }
    }

    class Object : LevelObj
    {
        float xPos, yPos, zPos;
        float xScale, yScale, zScale;
        int xRot, yRot, zRot;
        double rotation;
        uint objID;
        int routeID;
        long unk1, unk2, unk3;

        public Object()
        {
            this.xPos = 0;
            this.yPos = 0;
            this.zPos = 0;
            this.xScale = 0;
            this.yScale = 0;
            this.zScale = 0;

            this.xRot = 0;
            this.yRot = 0;
            this.zRot = 0;

            this.objID = 0;
            this.routeID = 0;
            this.unk1 = 0;
            this.unk2 = 0;
            this.unk3 = 0;
        }

        public void Parse(EndianBinaryReader reader, uint count)
        {
            for (uint i = 0; i < count; i++)
            {
                this.xPos = reader.ReadSingle();
                this.yPos = reader.ReadSingle();
                this.zPos = reader.ReadSingle();

                this.xScale = reader.ReadSingle();
                this.yScale = reader.ReadSingle();
                this.zScale = reader.ReadSingle();

                this.xRot = reader.ReadInt32();
                this.yRot = reader.ReadInt32();
                this.zRot = reader.ReadInt32();

                this.objID = reader.ReadUInt16();
                this.routeID = reader.ReadInt16();

                this.unk1 = reader.ReadInt64();
                this.unk2 = reader.ReadInt64();
                this.unk3 = reader.ReadInt64();

                rotation = MiscHacks.returnRotations(this.xRot, this.yRot);
            }
        }
    }

    class KartPoint : LevelObj
    {
        float xPos, yPos, zPos;
        float xScale, yScale, zScale;
        int xRot, yRot, zRot;
        byte polePos, playerID;

        public KartPoint()
        {
            this.xPos = 0;
            this.yPos = 0;
            this.zPos = 0;
            this.xScale = 0;
            this.yScale = 0;
            this.zScale = 0;

            this.xRot = 0;
            this.yRot = 0;
            this.zRot = 0;

            this.polePos = 0;
            this.playerID = 0;
        }

        public void Parse(EndianBinaryReader reader, uint count)
        {
            for (uint i = 0; i < count; i++)
            {
                this.xPos = reader.ReadSingle();
                this.yPos = reader.ReadSingle();
                this.zPos = reader.ReadSingle();

                this.xScale = reader.ReadSingle();
                this.yScale = reader.ReadSingle();
                this.zScale = reader.ReadSingle();

                this.xRot = reader.ReadInt32();
                this.yRot = reader.ReadInt32();
                this.zRot = reader.ReadInt32();

                this.polePos = reader.ReadByte();
                this.playerID = reader.ReadByte();

                reader.ReadBytes(2);
            }
        }
    }

    class Area : LevelObj
    {
        public Area()
        {

        }
    }

    class Camera : LevelObj
    {
        public Camera()
        {

        }
    }

    class Respawn : LevelObj
    {
        float xPos, yPos, zPos;
        int xRot, yRot, zRot, respawnID, unk1, unk2, unk3;

        public Respawn()
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

    struct GroupStruct
    {
        public uint pointLength;
        public uint pointStart;
    }
}
