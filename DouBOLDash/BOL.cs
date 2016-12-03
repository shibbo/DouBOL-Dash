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
        EnemyRoute enmObj = new EnemyRoute();
        List<EnemyRoute> enmdict = new List<EnemyRoute>();

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
                    enmObj.xPos = reader.ReadSingle();
                    enmObj.yPos = reader.ReadSingle();
                    enmObj.zPos = reader.ReadSingle();

                    enmObj.pointSetting = reader.ReadInt16();
                    enmObj.link = reader.ReadInt16();
                    enmObj.scale = reader.ReadSingle();
                    enmObj.groupSetting = reader.ReadUInt16();

                    enmObj.group = reader.ReadByte();
                    enmObj.pointSetting2 = reader.ReadByte();

                    enmdict.Add(enmObj);

                    reader.ReadBytes(8);
                }
            }
        }

        public List<EnemyRoute> returnList()
        {
            return enmdict;
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
        CheckpointObject chkobj = new CheckpointObject();
        List<CheckpointObject> chkdict = new List<CheckpointObject>();
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
                        chkobj.groupID = i;

                        chkobj.xPosStart = reader.ReadSingle();
                        chkobj.yPosStart = reader.ReadSingle();
                        chkobj.zPosStart = reader.ReadSingle();

                        chkobj.xPosEnd = reader.ReadSingle();
                        chkobj.yPosEnd = reader.ReadSingle();
                        chkobj.zPosEnd = reader.ReadSingle();

                        chkdict.Add(chkobj);

                        reader.ReadBytes(4);
                    }

                    Console.WriteLine("Finished Group " + i);
                }
            }
        }

        public List<CheckpointObject> returnList()
        {
            return chkdict;
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
        RoutePointObject rpobj = new RoutePointObject();
        List<RoutePointObject> rpdict = new List<RoutePointObject>();

        float xPos, yPos, zPos;
        uint groupID;

        public RoutePoint()
        {
            this.xPos = 0;
            this.yPos = 0;
            this.zPos = 0;
            this.groupID = 0;
        }
        
        public void Parse(EndianBinaryReader reader, Dictionary<uint, GroupStruct> groupDict)
        {
            uint count = (uint)groupDict.Count();

            for (uint i = 0; i < count; i++)
            {
                GroupStruct group = groupDict[i];
                for (uint j = 0; j < group.pointLength; j++)
                {
                    rpobj.xPos = reader.ReadSingle();
                    rpobj.yPos = reader.ReadSingle();
                    rpobj.zPos = reader.ReadSingle();

                    rpobj.groupID = i;

                    rpdict.Add(rpobj);

                    reader.ReadBytes(20);
                }
            }
        }

        public List<RoutePointObject> returnList()
        {
            return rpdict;
        }
    }

    class TrackObject : PositionObject
    {
        LevelObject lvlobj = new LevelObject();
        List<LevelObject> lvldict = new List<LevelObject>();

        public float xPos, yPos, zPos;
        public float xScale, yScale, zScale;
        public int xRot, yRot, zRot;
        public double rotation;
        public uint objID;
        public int routeID;
        public long unk1, unk2, unk3;

        public TrackObject()
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
                lvlobj.xPos = reader.ReadSingle();
                lvlobj.yPos = reader.ReadSingle();
                lvlobj.zPos = reader.ReadSingle();

                lvlobj.xScale = reader.ReadSingle();
                lvlobj.yScale = reader.ReadSingle();
                lvlobj.zScale = reader.ReadSingle();

                lvlobj.xRot = reader.ReadInt32();
                lvlobj.yRot = reader.ReadInt32();
                lvlobj.zRot = reader.ReadInt32();

                lvlobj.objID = reader.ReadUInt16();
                lvlobj.routeID = reader.ReadInt16();

                lvlobj.unk1 = reader.ReadInt64();
                lvlobj.unk2 = reader.ReadInt64();
                lvlobj.unk3 = reader.ReadInt64();

                lvlobj.ID = i;

                lvlobj.rotation = MiscHacks.returnRotations(this.xRot, this.yRot);

                lvldict.Add(lvlobj);
            }
        }

        public List<LevelObject> returnList()
        {
            return lvldict;
        }
    }

    class KartPoint : PositionObject
    {
        KartPointObject kpobj = new KartPointObject();
        List<KartPointObject> kpdict = new List<KartPointObject>();

        float xScale, yScale, zScale;
        int xRot, yRot, zRot;
        byte polePos, playerID;
        double rotation;

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

        public void Parse(EndianBinaryReader reader)
        {
            kpobj.xPos = reader.ReadSingle();
            kpobj.yPos = reader.ReadSingle();
            kpobj.zPos = reader.ReadSingle();

            xPos = kpobj.xPos;
            yPos = kpobj.yPos;
            zPos = kpobj.zPos;

            kpobj.xScale = reader.ReadSingle();
            kpobj.yScale = reader.ReadSingle();
            kpobj.zScale = reader.ReadSingle();

            kpobj.xRot = reader.ReadInt32();
            kpobj.yRot = reader.ReadInt32();
            kpobj.zRot = reader.ReadInt32();

            kpobj.rotation = MiscHacks.returnRotations(kpobj.xRot, kpobj.yRot);

            kpobj.polePos = reader.ReadByte();
            kpobj.playerID = reader.ReadByte();

            kpdict.Add(kpobj);

            reader.ReadBytes(2);

            // if the player id is 0xFF, that means each player is in a race mode
            // if it's not, that means they're on a battle map
            // if it's a battle map, we read through the entries 3 more times
            // this is something that the old BOL editor lacks
            if (kpobj.playerID != 0xFF)
            {
                for (uint z = 0; z < 3; z++)
                {
                    kpobj.xPos = reader.ReadSingle();
                    kpobj.yPos = reader.ReadSingle();
                    kpobj.zPos = reader.ReadSingle();

                    kpobj.xScale = reader.ReadSingle();
                    kpobj.yScale = reader.ReadSingle();
                    kpobj.zScale = reader.ReadSingle();

                    kpobj.xRot = reader.ReadInt32();
                    kpobj.yRot = reader.ReadInt32();
                    kpobj.zRot = reader.ReadInt32();

                    kpobj.rotation = MiscHacks.returnRotations(kpobj.xRot, kpobj.yRot);

                    kpobj.polePos = reader.ReadByte();
                    kpobj.playerID = reader.ReadByte();

                    kpdict.Add(kpobj);

                    reader.ReadBytes(2);
                }
            }
        }

        public List<KartPointObject> returnList()
        {
            return kpdict;
        }
    }

    class Area : LevelObj
    {
        float xPos, yPos, zPos;
        float xScale, yScale, zScale;
        int xRot, yRot, zRot;
        uint unk1, unk2;
        long unk3, unk4;
        double rotation;

        public Area()
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

            this.unk1 = 0;
            this.unk2 = 0;
            this.unk3 = 0;
            this.unk4 = 0;
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

                rotation = MiscHacks.returnRotations(this.xRot, this.yRot);

                this.unk1 = reader.ReadUInt16();
                this.unk2 = reader.ReadUInt16();

                this.unk3 = reader.ReadInt64();
                this.unk4 = reader.ReadInt64();
            }
        }
    }

    class Camera : LevelObj
    {
        float xView1, yView1, zView1;
        int xRot, yRot, zRot;
        float xView2, yView2, zView2;
        float xView3, yView3, zView3;
        byte unk1, type;
        uint startZoom, duration, unk2, unk3, unk4;
        int routeID;
        uint routeSpeed, endZoom;
        int nextCamera;
        string name;
        double rotation;

        public Camera()
        {
            this.xView1 = 0;
            this.yView1 = 0;
            this.zView1 = 0;

            this.xRot = 0;
            this.yRot = 0;
            this.zRot = 0;

            this.xView2 = 0;
            this.yView2 = 0;
            this.zView2 = 0;

            this.xView3 = 0;
            this.yView3 = 0;
            this.zView3 = 0;

            this.unk1 = 0;
            this.type = 0;

            this.startZoom = 0;
            this.duration = 0;
            this.unk2 = 0;
            this.unk3 = 0;
            this.unk4 = 0;
            this.routeID = 0;

            this.routeSpeed = 0;
            this.endZoom = 0;
            this.nextCamera = 0;
            this.name = "";
        }

        public void Parse(EndianBinaryReader reader, uint count)
        {
            Console.WriteLine(count);
            for (uint i = 0; i < count; i++)
            {
                this.xView1 = reader.ReadSingle();
                this.yView1 = reader.ReadSingle();
                this.zView1 = reader.ReadSingle();

                this.xRot = reader.ReadInt32();
                this.yRot = reader.ReadInt32();
                this.zRot = reader.ReadInt32();

                rotation = MiscHacks.returnRotations(this.xRot, this.yRot);

                this.xView2 = reader.ReadSingle();
                this.yView2 = reader.ReadSingle();
                this.zView2 = reader.ReadSingle();

                this.xView3 = reader.ReadSingle();
                this.yView3 = reader.ReadSingle();
                this.zView3 = reader.ReadSingle();

                this.unk1 = reader.ReadByte();
                this.type = reader.ReadByte();

                this.startZoom = reader.ReadUInt16();
                this.duration = reader.ReadUInt16();
                this.unk2 = reader.ReadUInt16();
                this.unk3 = reader.ReadUInt16();
                this.unk4 = reader.ReadUInt16();

                this.routeID = reader.ReadInt16();
                this.routeSpeed = reader.ReadUInt16();
                this.endZoom = reader.ReadUInt16();

                this.nextCamera = reader.ReadInt16();
                this.name = Encoding.ASCII.GetString(reader.ReadBytes(4));
            }
        }
    }

    class Respawn : LevelObj
    {
        RespawnObject resObj = new RespawnObject();
        List<RespawnObject> resLis = new List<RespawnObject>();

        float xPos, yPos, zPos;
        int xRot, yRot, zRot;
        uint respawnID, unk1, unk2, unk3;
        double rotation;

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

        public void Parse(EndianBinaryReader reader, uint count)
        {
            for (uint i = 0; i < count; i++)
            {
                resObj.xPos = reader.ReadSingle();
                resObj.yPos = reader.ReadSingle();
                resObj.zPos = reader.ReadSingle();

                resObj.xRot = reader.ReadInt32();
                resObj.yRot = reader.ReadInt32();
                resObj.zRot = reader.ReadInt32();

                resObj.rotation = MiscHacks.returnRotations(this.xRot, this.yRot);

                resObj.respawnID = reader.ReadUInt16();
                resObj.unk1 = reader.ReadUInt16();
                resObj.unk2 = reader.ReadUInt16();
                resObj.unk3 = reader.ReadUInt16();

                resLis.Add(resObj);
            }
        }

        public List<RespawnObject> returnList()
        {
            return resLis;
        }
    }

    abstract class LevelObj
    {

    }

    class PositionObject
    {
        public float xPos, yPos, zPos;
    }

    struct GroupStruct
    {
        public uint pointLength;
        public uint pointStart;
    }

    struct EnemyRoute
    {
        public float xPos, yPos, zPos;
        public int pointSetting, link;
        public float scale;
        public uint groupSetting;
        public byte group, pointSetting2;
    }

    struct RoutePointObject
    {
        public float xPos, yPos, zPos;
        public uint groupID;
    }

    struct LevelObject
    {
        public float xPos, yPos, zPos;
        public float xScale, yScale, zScale;
        public int xRot, yRot, zRot;
        public double rotation;
        public uint objID;
        public int routeID;
        public long unk1, unk2, unk3;
        public uint ID;
    }

    struct CheckpointObject
    {
        public float xPosStart, yPosStart, zPosStart;
        public float xPosEnd, yPosEnd, zPosEnd;
        public uint groupID;
    }

    struct KartPointObject
    {
        public float xPos, yPos, zPos;
        public float xScale, yScale, zScale;
        public int xRot, yRot, zRot;
        public byte polePos, playerID;
        public double rotation;
    }

    public struct RespawnObject
    {
        public float xPos, yPos, zPos;
        public int xRot, yRot, zRot;
        public uint respawnID, unk1, unk2, unk3;
        public double rotation;
    }
}
