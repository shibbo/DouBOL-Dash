using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DouBOLDash
{
    class BOL
    {
        /* class that represents a BOL header */
        // this will store the offsets we use in Form1.cs
        // we call the other parse methods with these variables
        List<uint> offsets = new List<uint>();
        List<uint> counts = new List<uint>();

        List<BOLInformation> bolList = new List<BOLInformation>();
        BOLInformation bolInfo = new BOLInformation();

        string type;
        float unk3, unk4, unk5;
        uint magic, unk1, unk2;
        byte numLaps, musicID;
        ushort sec1Count, sec2Count, sec3Count, sec5Count, sec7Count, sec8Count, sec9Count;
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
            bolInfo.magic = reader.ReadUInt32();
            bolInfo.unk1 = reader.ReadUInt32();
            bolInfo.unk2 = reader.ReadUInt32();

            bolInfo.unk3 = reader.ReadSingle();
            bolInfo.unk4 = reader.ReadSingle();
            bolInfo.unk5 = reader.ReadSingle();

            bolInfo.numLaps = reader.ReadByte();
            bolInfo.musicID = reader.ReadByte();

            bolInfo.sec1Count = reader.ReadUInt16();
            bolInfo.sec2Count = reader.ReadUInt16();
            bolInfo.sec5Count = reader.ReadUInt16();
            bolInfo.sec7Count = reader.ReadUInt16();
            bolInfo.sec8Count = reader.ReadUInt16();
            bolInfo.sec3Count = reader.ReadUInt16();
            bolInfo.sec9Count = reader.ReadUInt16();

            counts.Add(bolInfo.sec1Count);
            counts.Add(bolInfo.sec2Count);
            counts.Add(bolInfo.sec5Count);
            counts.Add(bolInfo.sec7Count);
            counts.Add(bolInfo.sec8Count);
            counts.Add(bolInfo.sec3Count);
            counts.Add(bolInfo.sec9Count);

            bolInfo.unk6 = reader.ReadUInt32();
            bolInfo.unk7 = reader.ReadUInt32();
            bolInfo.unk8 = reader.ReadUInt32();
            bolInfo.unk9 = reader.ReadUInt32();
            bolInfo.unk10 = reader.ReadUInt32();
            bolInfo.unk11 = reader.ReadUInt32();
            bolInfo.fileStart = reader.ReadUInt32();

            bolInfo.sec1Offs = reader.ReadUInt32();
            bolInfo.sec2Offs = reader.ReadUInt32();
            bolInfo.sec3Offs = reader.ReadUInt32();
            bolInfo.sec4Offs = reader.ReadUInt32();
            bolInfo.sec5Offs = reader.ReadUInt32();
            bolInfo.sec6Offs = reader.ReadUInt32();
            bolInfo.sec7Offs = reader.ReadUInt32();
            bolInfo.sec8Offs = reader.ReadUInt32();
            bolInfo.sec9Offs = reader.ReadUInt32();
            bolInfo.sec10Offs = reader.ReadUInt32();
            bolInfo.sec11Offs = reader.ReadUInt32();

            offsets.Add(bolInfo.sec1Offs);
            offsets.Add(bolInfo.sec2Offs);
            offsets.Add(bolInfo.sec3Offs);
            offsets.Add(bolInfo.sec4Offs);
            offsets.Add(bolInfo.sec5Offs);
            offsets.Add(bolInfo.sec6Offs);
            offsets.Add(bolInfo.sec7Offs);
            offsets.Add(bolInfo.sec8Offs);
            offsets.Add(bolInfo.sec9Offs);
            offsets.Add(bolInfo.sec10Offs);
            offsets.Add(bolInfo.sec11Offs);

            reader.ReadBytes(12); // padding

            bolList.Add(bolInfo);
        }

        public void Write(EndianBinaryWriter writer, List<BOLInformation> listBOL)
        {
            byte pad = 0;
            foreach (BOLInformation bolInfo in listBOL)
            {
                writer.Write(0x30303135);
                writer.Write(bolInfo.unk1);
                writer.Write(bolInfo.unk2);

                writer.Write(bolInfo.unk3);
                writer.Write(bolInfo.unk4);
                writer.Write(bolInfo.unk5);

                writer.Write(bolInfo.numLaps);
                writer.Write(bolInfo.musicID);

                writer.Write(bolInfo.sec1Count);
                writer.Write(bolInfo.sec2Count);
                writer.Write(bolInfo.sec5Count);
                writer.Write(bolInfo.sec7Count);
                writer.Write(bolInfo.sec8Count);
                writer.Write(bolInfo.sec3Count);
                writer.Write(bolInfo.sec9Count);

                writer.Write(bolInfo.unk6);
                writer.Write(bolInfo.unk7);
                writer.Write(bolInfo.unk8);
                writer.Write(bolInfo.unk9);
                writer.Write(bolInfo.unk10);
                writer.Write(bolInfo.unk11);
                writer.Write(bolInfo.fileStart); // if this isn't 0, rip

                writer.Write(bolInfo.sec1Offs);
                writer.Write(bolInfo.sec2Offs);
                writer.Write(bolInfo.sec3Offs);
                writer.Write(bolInfo.sec4Offs);
                writer.Write(bolInfo.sec5Offs);
                writer.Write(bolInfo.sec6Offs);
                writer.Write(bolInfo.sec7Offs);
                writer.Write(bolInfo.sec8Offs);
                writer.Write(bolInfo.sec9Offs);
                writer.Write(bolInfo.fileSize); // just write the filesize here (section 10)
                writer.Write(bolInfo.fileSize); // just write the filesize here (section 11)

                for (int i = 0; i < 12; i++)
                    writer.Write(pad);
            }
        }

        public List<BOLInformation> returnList()
        {
            return bolList;
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

    class EnemyRoutes
    {
        EnemyRoute enmObj = new EnemyRoute();
        List<EnemyRoute> enmdict = new List<EnemyRoute>();

        float xPos, yPos, zPos;
        short pointSetting, link;
        float scale;
        ushort groupSetting;
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

        public void Write(EndianBinaryWriter writer, List<EnemyRoute> enmList)
        {
            byte pad = 0;
            foreach(EnemyRoute enmObj in enmList)
            {
                writer.Write(enmObj.xPos);
                writer.Write(enmObj.yPos);
                writer.Write(enmObj.zPos);

                writer.Write(enmObj.pointSetting);
                writer.Write(enmObj.link);
                writer.Write(enmObj.scale);
                writer.Write(enmObj.groupSetting);

                writer.Write(enmObj.group);
                writer.Write(enmObj.pointSetting2);

                for (int i = 0; i < 8; i++)
                    writer.Write(pad);

            }
        }

        public List<EnemyRoute> returnList()
        {
            return enmdict;
        }

    }

    class CheckpointGroup
    {
        ushort pointLength, groupLink, index;
        short prev1, prev2, prev3, prev4;
        short next1, next2, next3, next4;

        CheckpointGroupObject chckObj = new CheckpointGroupObject();
        List<CheckpointGroupObject> chckGrp = new List<CheckpointGroupObject>();
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
                for (ushort i = 0; i < count; i++)
                {
                    chckObj.pointLength = reader.ReadUInt16();
                    chckObj.groupLink = reader.ReadUInt16();

                    chckObj.prev1 = reader.ReadInt16();
                    chckObj.prev2 = reader.ReadInt16();
                    chckObj.prev3 = reader.ReadInt16();
                    chckObj.prev4 = reader.ReadInt16();

                    chckObj.next1 = reader.ReadInt16();
                    chckObj.next2 = reader.ReadInt16();
                    chckObj.next3 = reader.ReadInt16();
                    chckObj.next4 = reader.ReadInt16();

                    chckObj.index = i;

                    dict1.Add(chckObj.index, chckObj.pointLength);
                    chckGrp.Add(chckObj);
                }
            }
        }

        public void Write(EndianBinaryWriter writer, List<CheckpointGroupObject> grpObj)
        {
            foreach(CheckpointGroupObject chckObj in grpObj)
            {
                writer.Write(chckObj.pointLength);
                writer.Write(chckObj.groupLink);

                writer.Write(chckObj.prev1);
                writer.Write(chckObj.prev2);
                writer.Write(chckObj.prev3);
                writer.Write(chckObj.prev4);

                writer.Write(chckObj.next1);
                writer.Write(chckObj.next2);
                writer.Write(chckObj.next3);
                writer.Write(chckObj.next4);
            }
        }

        public List<CheckpointGroupObject> returnList()
        {
            return chckGrp;
        }

        public Dictionary<uint, uint> returnDictionary()
        {
            return dict1;
        }
    }

    class Checkpoint
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
                }
            }
        }

        public void Write(EndianBinaryWriter writer, List<CheckpointObject> chkObj)
        {
            byte shit = 0;

            foreach (CheckpointObject checkObj in chkObj)
            {
                writer.Write(checkObj.xPosStart);
                writer.Write(checkObj.yPosStart);
                writer.Write(checkObj.zPosStart);

                writer.Write(checkObj.xPosEnd);
                writer.Write(checkObj.yPosEnd);
                writer.Write(checkObj.zPosEnd);

                for (int i = 0; i < 4; i++)
                    writer.Write(shit);
            }
        }

        public List<CheckpointObject> returnList()
        {
            return chkdict;
        }
    }


    class RouteGroup
    {
        Dictionary<uint, RouteGroupSetup> groupDict = new Dictionary<uint, RouteGroupSetup>();
        List<RouteGroupSetup> grpSetup = new List<RouteGroupSetup>();
        ushort pointLength, pointStart;
        uint groupID;

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
                RouteGroupSetup group = new RouteGroupSetup();
                group.pointLength = reader.ReadUInt16();
                group.pointStart = reader.ReadUInt16();
                this.groupID = i;

                reader.ReadBytes(12);

                grpSetup.Add(group);
                groupDict.Add(i, group);
                
            }
        }

        public void Write(EndianBinaryWriter writer, List<RouteGroupSetup> grp)
        {
            byte pad = 0;
            foreach (RouteGroupSetup routeObj in grp)
            {
                writer.Write(routeObj.pointLength);
                writer.Write(routeObj.pointStart);

                for (int i = 0; i < 12; i++)
                    writer.Write(pad);
            }
        }

        public Dictionary<uint, RouteGroupSetup> returnDictionary()
        {
            return groupDict;
        }

        public List<RouteGroupSetup> returnList()
        {
            return grpSetup;
        }
    }

    class RoutePoint
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
        
        public void Parse(EndianBinaryReader reader, Dictionary<uint, RouteGroupSetup> groupDict)
        {
            uint count = (uint)groupDict.Count();

            for (uint i = 0; i < count; i++)
            {
                RouteGroupSetup group = groupDict[i];
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

        public void Write(EndianBinaryWriter writer, List<RoutePointObject> rpObj)
        {
            byte pad = 0;
            foreach(RoutePointObject rObj in rpObj)
            {
                writer.Write(rObj.xPos);
                writer.Write(rObj.yPos);
                writer.Write(rObj.zPos);

                for (int i = 0; i < 20; i++)
                    writer.Write(pad);
            }
        }

        public List<RoutePointObject> returnList()
        {
            return rpdict;
        }
    }

    class TrackObject
    {
        LevelObject lvlobj = new LevelObject();
        List<LevelObject> lvldict = new List<LevelObject>();

        public float xPos, yPos, zPos;
        public float xScale, yScale, zScale;
        public double rotation;
        public int xRot, yRot, zRot;
        public ushort objID;
        public short routeID;
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

            this.rotation = 0;

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

                MiscHacks misc = new MiscHacks();
                lvlobj.modelName = misc.returnModel(lvlobj.objID);

                lvlobj.unk1 = reader.ReadInt64();
                lvlobj.unk2 = reader.ReadInt64();
                lvlobj.unk3 = reader.ReadInt64();

                lvlobj.ID = i;

                lvlobj.rotation = MiscHacks.returnRotations(lvlobj.xRot, lvlobj.yRot);

                lvldict.Add(lvlobj);
            }
        }

        public void Write(EndianBinaryWriter writer, List<LevelObject> objList)
        {
            foreach (LevelObject lvlobj in objList)
            {
                writer.Write(lvlobj.xPos);
                writer.Write(lvlobj.yPos);
                writer.Write(lvlobj.zPos);

                writer.Write(lvlobj.xScale);
                writer.Write(lvlobj.yScale);
                writer.Write(lvlobj.zScale);

                writer.Write(655360000);
                writer.Write(655360000);
                writer.Write(655360000);

                writer.Write(lvlobj.objID);
                writer.Write(lvlobj.routeID);
                writer.Write(lvlobj.unk1);
                writer.Write(lvlobj.unk2);
                writer.Write(lvlobj.unk3);
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

        public void Write(EndianBinaryWriter writer, List<KartPointObject> kartObj)
        {
            byte pad = 0;
            foreach(KartPointObject kpobj in kartObj)
            {
                writer.Write(kpobj.xPos);
                writer.Write(kpobj.yPos);
                writer.Write(kpobj.zPos);

                writer.Write(kpobj.xScale);
                writer.Write(kpobj.yScale);
                writer.Write(kpobj.zScale);

                writer.Write(655360000);
                writer.Write(655360000);
                writer.Write(655360000);

                writer.Write(kpobj.polePos);
                writer.Write(kpobj.playerID);

                for (int i = 0; i < 2; i++)
                    writer.Write(pad);
            }
        }

        public List<KartPointObject> returnList()
        {
            return kpdict;
        }
    }

    class Area
    {
        AreaObject areaObj = new AreaObject();
        List<AreaObject> areaDict = new List<AreaObject>();

        float xPos, yPos, zPos;
        float xScale, yScale, zScale;
        int xRot, yRot, zRot;
        ushort unk1, unk2;
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
                areaObj.xPos = reader.ReadSingle();
                areaObj.yPos = reader.ReadSingle();
                areaObj.zPos = reader.ReadSingle();

                areaObj.xScale = reader.ReadSingle();
                areaObj.yScale = reader.ReadSingle();
                areaObj.zScale = reader.ReadSingle();

                areaObj.xRot = reader.ReadInt32();
                areaObj.yRot = reader.ReadInt32();
                areaObj.zRot = reader.ReadInt32();

                rotation = MiscHacks.returnRotations(areaObj.xRot, areaObj.yRot);

                areaObj.unk1 = reader.ReadUInt16();
                areaObj.unk2 = reader.ReadUInt16();

                areaObj.unk3 = reader.ReadInt64();
                areaObj.unk4 = reader.ReadInt64();

                areaDict.Add(areaObj);
            }
        }

        public void Write(EndianBinaryWriter writer, List<AreaObject> areaObj)
        {
            foreach (AreaObject aObj in areaObj)
            {
                writer.Write(aObj.xPos);
                writer.Write(aObj.yPos);
                writer.Write(aObj.zPos);

                writer.Write(aObj.xScale);
                writer.Write(aObj.yScale);
                writer.Write(aObj.zScale);

                writer.Write(655360000);
                writer.Write(655360000);
                writer.Write(655360000);

                writer.Write(aObj.unk1);
                writer.Write(aObj.unk2);
                writer.Write(aObj.unk3);
                writer.Write(aObj.unk4);
            }
        }

        public List<AreaObject> returnList()
        {
            return areaDict;
        }
    }

    class Camera
    {
        List<CameraObject> camDict = new List<CameraObject>();

        float xView1, yView1, zView1;
        int xRot, yRot, zRot;
        float xView2, yView2, zView2;
        float xView3, yView3, zView3;
        byte unk1, type;
        ushort startZoom, duration, unk2, unk3, unk4;
        short routeID;
        ushort routeSpeed, endZoom;
        short nextCamera;
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
            for (uint i = 0; i < count; i++)
            {
                CameraObject camObj = new CameraObject();

                camObj.xView1 = reader.ReadSingle();
                camObj.yView1 = reader.ReadSingle();
                camObj.zView1 = reader.ReadSingle();

                camObj.xRot = reader.ReadInt32();
                camObj.yRot = reader.ReadInt32();
                camObj.zRot = reader.ReadInt32();

                rotation = MiscHacks.returnRotations(camObj.xRot, camObj.yRot);

                camObj.xView2 = reader.ReadSingle();
                camObj.yView2 = reader.ReadSingle();
                camObj.zView2 = reader.ReadSingle();

                camObj.xView3 = reader.ReadSingle();
                camObj.yView3 = reader.ReadSingle();
                camObj.zView3 = reader.ReadSingle();

                camObj.unk1 = reader.ReadByte();
                camObj.type = reader.ReadByte();

                camObj.startZoom = reader.ReadUInt16();
                camObj.duration = reader.ReadUInt16();
                camObj.unk2 = reader.ReadUInt16();
                camObj.unk3 = reader.ReadUInt16();
                camObj.unk4 = reader.ReadUInt16();

                camObj.routeID = reader.ReadInt16();
                camObj.routeSpeed = reader.ReadUInt16();
                camObj.endZoom = reader.ReadUInt16();

                camObj.nextCamera = reader.ReadInt16();
                camObj.name = Encoding.ASCII.GetString(reader.ReadBytes(4));

                camDict.Add(camObj);
            }
        }

        public void Write(EndianBinaryWriter writer, List<CameraObject> camList)
        {
            foreach (CameraObject camObj in camList)
            {
                writer.Write(camObj.xView1);
                writer.Write(camObj.yView1);
                writer.Write(camObj.zView1);

                writer.Write(655360000);
                writer.Write(655360000);
                writer.Write(655360000);

                writer.Write(camObj.xView2);
                writer.Write(camObj.yView2);
                writer.Write(camObj.zView2);

                writer.Write(camObj.xView3);
                writer.Write(camObj.yView3);
                writer.Write(camObj.zView3);

                writer.Write(camObj.unk1);
                writer.Write(camObj.type);

                writer.Write(camObj.startZoom);
                writer.Write(camObj.duration);
                writer.Write(camObj.unk2);
                writer.Write(camObj.unk3);
                writer.Write(camObj.unk4);

                writer.Write(camObj.routeID);
                writer.Write(camObj.routeSpeed);
                writer.Write(camObj.endZoom);
                writer.Write(camObj.nextCamera);

                byte[] b = Encoding.ASCII.GetBytes(camObj.name);
                for (int i = 0; i < 4; i++)
                    writer.Write(b[i]);
            }
        }

        public List<CameraObject> returnList()
        {
            return camDict;
        }
    }

    class Respawn : RespawnObject
    {
        List<RespawnObject> resLis = new List<RespawnObject>();

        float xPos, yPos, zPos;
        int xRot, yRot, zRot;
        ushort respawnID, unk1, unk2, unk3;
        double rotation;

        public Respawn()
        {
            xPos = 0;
            yPos = 0;
            zPos = 0;
            xRot = 0;
            yRot = 0;
            zRot = 0;
            respawnID = 0;
            unk1 = 0;
            unk2 = 0;
            unk3 = 0;
            rotation = 0;
        }

        public void Parse(EndianBinaryReader reader, uint count)
        {
            for (uint i = 0; i < count; i++)
            {
                RespawnObject resObj = new RespawnObject();
                resObj.xPos = reader.ReadSingle();
                resObj.yPos = reader.ReadSingle();
                resObj.zPos = reader.ReadSingle();

                resObj.xRot = reader.ReadInt32();
                resObj.yRot = reader.ReadInt32();
                resObj.zRot = reader.ReadInt32();

                resObj.rotation = MiscHacks.returnRotations(resObj.xRot, resObj.yRot);

                resObj.respawnID = reader.ReadUInt16();
                resObj.unk1 = reader.ReadUInt16();
                resObj.unk2 = reader.ReadUInt16();
                resObj.unk3 = reader.ReadUInt16();

                resLis.Add(resObj);
            }
        }

        public void Write(EndianBinaryWriter writer, List<RespawnObject> respawnList)
        {
            foreach (RespawnObject resObj in respawnList)
            {
                writer.Write(resObj.xPos);
                writer.Write(resObj.yPos);
                writer.Write(resObj.zPos);

                double derp;
                double derp2;

                MiscHacks.inverseRotations((int)resObj.rotation, out derp, out derp2);

                writer.Write(655360000);
                writer.Write(655360000);
                writer.Write(655360000);

                writer.Write(resObj.respawnID);
                writer.Write(resObj.unk1);
                writer.Write(resObj.unk2);
                writer.Write(resObj.unk3);
            }
        }

        public List<RespawnObject> returnList()
        {
            return resLis;
        }
    }

    class BOLInformation
    {
        public string type;
        public float unk3, unk4, unk5;
        public uint magic, unk1, unk2;
        public byte numLaps, musicID;
        public ushort sec1Count, sec2Count, sec3Count, sec4Count, sec5Count, sec6Count, sec7Count, sec8Count, sec9Count;
        public uint unk6, unk7, unk8, unk9, unk10, unk11, fileStart;
        public uint sec1Offs, sec2Offs, sec3Offs, sec4Offs, sec5Offs, sec6Offs, sec7Offs, sec8Offs, sec9Offs, sec10Offs, sec11Offs;
        public int fileSize;
        
    }

    class PositionObject
    {
        public float xPos, yPos, zPos;
    }

    struct EnemyRoute
    {
        public float xPos, yPos, zPos;
        public short pointSetting, link;
        public float scale;
        public ushort groupSetting;
        public byte group, pointSetting2;
    }

    public class RouteGroupSetup
    {
        public ushort pointLength, pointStart;
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
        public double rotation;
        public int xRot, yRot, zRot;
        public ushort objID;
        public short routeID;
        public long unk1, unk2, unk3;
        public string modelName;
        public uint ID;
    }

    struct CheckpointObject
    {
        public float xPosStart, yPosStart, zPosStart;
        public float xPosEnd, yPosEnd, zPosEnd;
        public uint groupID;
    }

    struct CheckpointGroupObject
    {
        public ushort pointLength, groupLink, index;
        public short prev1, prev2, prev3, prev4;
        public short next1, next2, next3, next4;
    }

    struct KartPointObject
    {
        public float xPos, yPos, zPos;
        public float xScale, yScale, zScale;
        public int xRot, yRot, zRot;
        public byte polePos, playerID;
        public double rotation;
    }

    struct AreaObject
    {
        public float xPos, yPos, zPos;
        public float xScale, yScale, zScale;
        public int xRot, yRot, zRot;
        public ushort unk1, unk2;
        public long unk3, unk4;
        public double rotation;
    }

    public class CameraObject
    {
        public float xView1, yView1, zView1;
        public int xRot, yRot, zRot;
        public float xView2, yView2, zView2;
        public float xView3, yView3, zView3;
        public byte unk1, type;
        public ushort startZoom, duration, unk2, unk3, unk4;
        public short routeID;
        public ushort routeSpeed, endZoom;
        public short nextCamera;
        public string name;
        public double rotation;
    }

    public class RespawnObject
    {
        public float xPos, yPos, zPos;
        public int xRot, yRot, zRot;
        public ushort unk1, unk2, unk3, respawnID;
        public double rotation;
    }
}
