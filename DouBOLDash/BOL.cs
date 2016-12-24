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
using System.IO;
using System.ComponentModel;

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
        float unk3, unk4, unk5, unk7, unk8;
        uint magic, unk1, unk2;
        byte numLaps, musicID;
        ushort sec1Count, sec2Count, sec3Count, sec5Count, sec7Count, sec8Count, sec9Count;
        uint unk6, unk9, unk10, unk11, fileStart;
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
            bolInfo.unk7 = reader.ReadSingle();
            bolInfo.unk8 = reader.ReadSingle();
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
            this.prev1 = -1;
            this.prev2 = -1;
            this.prev3 = -1;
            this.prev4 = -1;

            this.next1 = -1;
            this.next2 = -1;
            this.next3 = -1;
            this.next4 = -1;

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
                group.groupID = i;

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
                    RoutePointObject rpobj = new RoutePointObject();
                    rpobj.xPos = reader.ReadSingle();
                    rpobj.yPos = reader.ReadSingle();
                    rpobj.zPos = reader.ReadSingle();

                    rpobj.groupID = i;

                    rpobj.groupNum = j;

                    rpobj.pointLength = group.pointLength;

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
        List<LevelObject> lvldict = new List<LevelObject>();

        public float xPos, yPos, zPos;
        public float xScale, yScale, zScale;
        public double rotation;
        public int xRot, yRot, zRot;
        public ushort objID;
        public short routeID;
        public byte settings0, settings1, settings2, settings3, settings4, settings5, settings6, settings7, settings8;
        public byte settings9, settings10, settings11, settings12, settings13, settings14, settings15, settings16;
        public byte settings17, settings18, settings19, settings20, settings21, settings22, settings23;
        public string modelName, friendlyName;

        public TrackObject()
        {
            this.xPos = 0;
            this.yPos = 0;
            this.zPos = 0;
            this.xScale = 1;
            this.yScale = 1;
            this.zScale = 1;

            this.xRot = 0;
            this.yRot = 0;
            this.zRot = 0;

            this.rotation = 0;

            this.objID = 0;
            this.routeID = -1;

            this.settings0 = 0;
            this.settings1 = 0;
            this.settings2 = 0;
            this.settings3 = 0;
            this.settings4 = 0;
            this.settings5 = 0;
            this.settings6 = 0;
            this.settings7 = 0;
            this.settings8 = 0;
            this.settings9 = 0;
            this.settings10 = 0;
            this.settings11 = 0;
            this.settings12 = 0;
            this.settings13 = 0;
            this.settings14 = 0;
            this.settings15 = 0;
            this.settings16 = 0;
            this.settings17 = 0;
            this.settings18 = 0;
            this.settings19 = 0;
            this.settings20 = 0;
            this.settings21 = 0;
            this.settings22 = 0;
            this.settings23 = 0;

            this.modelName = "null";
        }

        public void Parse(EndianBinaryReader reader, uint count)
        {
            for (uint i = 0; i < count; i++)
            {
                LevelObject lvlobj = new LevelObject();
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
                lvlobj.friendlyName = misc.returnName(lvlobj.objID);

                lvlobj.settings0 = reader.ReadByte();
                lvlobj.settings1 = reader.ReadByte();
                lvlobj.settings2 = reader.ReadByte();
                lvlobj.settings3 = reader.ReadByte();
                lvlobj.settings4 = reader.ReadByte();
                lvlobj.settings5 = reader.ReadByte();
                lvlobj.settings6 = reader.ReadByte();
                lvlobj.settings7 = reader.ReadByte();
                lvlobj.settings8 = reader.ReadByte();
                lvlobj.settings9 = reader.ReadByte();
                lvlobj.settings10 = reader.ReadByte();
                lvlobj.settings11 = reader.ReadByte();
                lvlobj.settings12 = reader.ReadByte();
                lvlobj.settings13 = reader.ReadByte();
                lvlobj.settings14 = reader.ReadByte();
                lvlobj.settings15 = reader.ReadByte();
                lvlobj.settings16 = reader.ReadByte();
                lvlobj.settings17 = reader.ReadByte();
                lvlobj.settings18 = reader.ReadByte();
                lvlobj.settings19 = reader.ReadByte();
                lvlobj.settings20 = reader.ReadByte();
                lvlobj.settings21 = reader.ReadByte();
                lvlobj.settings22 = reader.ReadByte();
                lvlobj.settings23 = reader.ReadByte();

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
                writer.Write(lvlobj.settings0);
                writer.Write(lvlobj.settings1);
                writer.Write(lvlobj.settings2);
                writer.Write(lvlobj.settings3);
                writer.Write(lvlobj.settings4);
                writer.Write(lvlobj.settings5);
                writer.Write(lvlobj.settings6);
                writer.Write(lvlobj.settings7);
                writer.Write(lvlobj.settings8);
                writer.Write(lvlobj.settings9);
                writer.Write(lvlobj.settings10);
                writer.Write(lvlobj.settings11);
                writer.Write(lvlobj.settings12);
                writer.Write(lvlobj.settings13);
                writer.Write(lvlobj.settings14);
                writer.Write(lvlobj.settings15);
                writer.Write(lvlobj.settings16);
                writer.Write(lvlobj.settings17);
                writer.Write(lvlobj.settings18);
                writer.Write(lvlobj.settings19);
                writer.Write(lvlobj.settings20);
                writer.Write(lvlobj.settings21);
                writer.Write(lvlobj.settings22);
                writer.Write(lvlobj.settings23);
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
        public float unk3, unk4, unk5, unk7, unk8;
        public uint magic, unk1, unk2;
        public byte numLaps, musicID;
        public ushort sec1Count, sec2Count, sec3Count, sec4Count, sec5Count, sec6Count, sec7Count, sec8Count, sec9Count;
        public uint unk6, unk9, unk10, unk11, fileStart;
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

        [DisplayName("X Position"), Category("Position"), Description("The X position of the route point.")]
        public float XPosition
        {
            get { return xPos; }
            set { xPos = value; }
        }

        [DisplayName("Y Position"), Category("Position"), Description("The Y position of the route point.")]
        public float YPosition
        {
            get { return yPos; }
            set { yPos = value; }
        }

        [DisplayName("Z Position"), Category("Position"), Description("The Z position of the route point.")]
        public float ZPosition
        {
            get { return zPos; }
            set { zPos = value; }
        }
        [DisplayName("Scale"), Category("Position"), Description("The scale of the route point. This determines how far off from the placed point the CPUs go.")]
        public float Scale
        {
            get { return zPos; }
            set { zPos = value; }
        }
        [DisplayName("Point Setting 1"), Category("Settings"), Description("loldunno.")]
        public short PointSetting
        {
            get { return pointSetting; }
            set { pointSetting = value; }
        }
        [DisplayName("Point Setting 2"), Category("Settings"), Description("loldunno.")]
        public byte PointSetting2
        {
            get { return pointSetting2; }
            set { pointSetting2 = value; }
        }
        [DisplayName("Route Link"), Category("Settings"), Description("-1 if it continues with the route, and if it's not -1, it will go to the group ID that is specified. If it's the same as the current group ID, it will loop.")]
        public short Link
        {
            get { return link; }
            set { link = value; }
        }
        [DisplayName("Group Setting"), Category("Settings"), Description("loldunno.")]
        public ushort GroupSetting
        {
            get { return groupSetting; }
            set { groupSetting = value; }
        }
    }

    public class RouteGroupSetup
    {
        public ushort pointLength, pointStart;
        public uint groupID;

        [DisplayName("Length"), Category("Settings"), Description("The amount of points this group has.")]
        public ushort PointLength
        {
            get { return pointLength; }
            set { pointLength = value; }
        }

        [DisplayName("Start"), Category("Settings"), Description("The start of the group, like an index.")]
        public ushort PointStart
        {
            get { return pointStart; }
            set { pointStart = value; }
        }

        [DisplayName("Group ID"), Category("Settings"), Description("The group id."), ReadOnly(true)]
        public uint GroupID
        {
            get { return groupID; }
            set { groupID = value; }
        }
    }

    public class RoutePointObject
    {
        public float xPos, yPos, zPos;
        public uint groupID, groupNum, pointLength;

        [DisplayName("X Position"), Category("Position"), Description("The X position of the point.")]
        public float XPosition
        {
            get { return xPos; }
            set { xPos = value; }
        }

        [DisplayName("Y Position"), Category("Position"), Description("The Y position of the point.")]
        public float YPosition
        {
            get { return yPos; }
            set { yPos = value; }
        }

        [DisplayName("Z Position"), Category("Position"), Description("The Z position of the point.")]
        public float ZPosition
        {
            get { return zPos; }
            set { zPos = value; }
        }

        [DisplayName("Group ID"), Category("Settings"), Description("The Group ID the route point is set to.")]
        public uint GroupID
        {
            get { return groupID; }
            set { groupID = value; }
        }
    }

    public class LevelObject
    {
        public float xPos, yPos, zPos;
        public float xScale, yScale, zScale;
        public double rotation;
        public int xRot, yRot, zRot;
        public ushort objID;
        public short routeID;
        public byte settings0, settings1, settings2, settings3, settings4, settings5, settings6, settings7, settings8;
        public byte settings9, settings10, settings11, settings12, settings13, settings14, settings15, settings16;
        public byte settings17, settings18, settings19, settings20, settings21, settings22, settings23;
        public string set1, set2, set3; // string versions for hex converting
        public string modelName, friendlyName;
        public uint ID;

        [DisplayName("X Position"), Category("Position"), Description("The X position of the object.")]
        public float XPosition
        {
            get { return xPos; }
            set { xPos = value; }
        }

        [DisplayName("Y Position"), Category("Position"), Description("The Y position of the object.")]
        public float YPosition
        {
            get { return yPos; }
            set { yPos = value; }
        }

        [DisplayName("Z Position"), Category("Position"), Description("The Z position of the object.")]
        public float ZPosition
        {
            get { return zPos; }
            set { zPos = value; }
        }
        [DisplayName("X Scale"), Category("Position"), Description("The X scale of the object.")]
        public float XScale
        {
            get { return xScale; }
            set { xScale = value; }
        }

        [DisplayName("Y Scale"), Category("Position"), Description("The Y scale of the object.")]
        public float YScale
        {
            get { return yScale; }
            set { yScale = value; }
        }

        [DisplayName("Z Scale"), Category("Position"), Description("The Z scale of the object.")]
        public float ZScale
        {
            get { return zScale; }
            set { zScale = value; }
        }
        [DisplayName("Rotation"), Category("Position"), Description("The rotation of the object.")]
        public double Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }
        [DisplayName("Object ID"), Category("Settings"), Description("The ID of the object, to determine what it is.")]
        public ushort ObjectID
        {
            get { return objID; }
            set { objID = value; }
        }
        [DisplayName("Route ID"), Category("Settings"), Description("The route ID of the object. The object will follow the path of the cooresponding ID. -1 is there is no route.")]
        public short RouteID
        {
            get { return routeID; }
            set { routeID = value; }
        }
        [DisplayName("Model"), Category("Settings"), Description("The model the object loads."), ReadOnly(true)]
        public string Model
        {
            get { return modelName; }
            set { modelName = value; }
        }
        [DisplayName("Setting 1"), Category("Settings"), Description("The settings that coorespond to the object.")]
        public byte Setting1
        {
            get { return settings0; }
            set { settings0 = value; }
        }
        [DisplayName("Setting 2"), Category("Settings"), Description("The settings that coorespond to the object.")]
        public byte Setting2
        {
            get { return settings1; }
            set { settings1 = value; }
        }
        [DisplayName("Setting 3"), Category("Settings"), Description("The settings that coorespond to the object.")]
        public byte Setting3
        {
            get { return settings2; }
            set { settings2 = value; }
        }
        [DisplayName("Setting 4"), Category("Settings"), Description("The settings that coorespond to the object.")]
        public byte Setting4
        {
            get { return settings3; }
            set { settings3 = value; }
        }
        [DisplayName("Setting 5"), Category("Settings"), Description("The settings that coorespond to the object.")]
        public byte Setting5
        {
            get { return settings4; }
            set { settings4 = value; }
        }
        [DisplayName("Setting 6"), Category("Settings"), Description("The settings that coorespond to the object.")]
        public byte Setting6
        {
            get { return settings5; }
            set { settings5 = value; }
        }
        [DisplayName("Setting 7"), Category("Settings"), Description("The settings that coorespond to the object.")]
        public byte Setting7
        {
            get { return settings6; }
            set { settings6 = value; }
        }
        [DisplayName("Setting 8"), Category("Settings"), Description("The settings that coorespond to the object.")]
        public byte Setting8
        {
            get { return settings7; }
            set { settings7 = value; }
        }
        [DisplayName("Setting 9"), Category("Settings"), Description("The settings that coorespond to the object.")]
        public byte Setting9
        {
            get { return settings8; }
            set { settings8 = value; }
        }
        [DisplayName("Setting 10"), Category("Settings"), Description("The settings that coorespond to the object.")]
        public byte Setting10
        {
            get { return settings9; }
            set { settings9 = value; }
        }
        [DisplayName("Setting 11"), Category("Settings"), Description("The settings that coorespond to the object.")]
        public byte Setting11
        {
            get { return settings10; }
            set { settings10 = value; }
        }
        [DisplayName("Setting 12"), Category("Settings"), Description("The settings that coorespond to the object.")]
        public byte Setting12
        {
            get { return settings11; }
            set { settings11 = value; }
        }
        [DisplayName("Setting 13"), Category("Settings"), Description("The settings that coorespond to the object.")]
        public byte Setting13
        {
            get { return settings12; }
            set { settings12 = value; }
        }
        [DisplayName("Setting 14"), Category("Settings"), Description("The settings that coorespond to the object.")]
        public byte Setting14
        {
            get { return settings13; }
            set { settings13 = value; }
        }
        [DisplayName("Setting 15"), Category("Settings"), Description("The settings that coorespond to the object.")]
        public byte Setting15
        {
            get { return settings14; }
            set { settings14 = value; }
        }
        [DisplayName("Setting 16"), Category("Settings"), Description("The settings that coorespond to the object.")]
        public byte Setting16
        {
            get { return settings15; }
            set { settings15 = value; }
        }
        [DisplayName("Setting 17"), Category("Settings"), Description("The settings that coorespond to the object.")]
        public byte Setting17
        {
            get { return settings16; }
            set { settings16 = value; }
        }
        [DisplayName("Setting 18"), Category("Settings"), Description("The settings that coorespond to the object.")]
        public byte Setting18
        {
            get { return settings17; }
            set { settings17 = value; }
        }
        [DisplayName("Setting 19"), Category("Settings"), Description("The settings that coorespond to the object.")]
        public byte Setting19
        {
            get { return settings18; }
            set { settings18 = value; }
        }
        [DisplayName("Setting 20"), Category("Settings"), Description("The settings that coorespond to the object.")]
        public byte Setting20
        {
            get { return settings19; }
            set { settings19 = value; }
        }
        [DisplayName("Setting 21"), Category("Settings"), Description("The settings that coorespond to the object.")]
        public byte Setting21
        {
            get { return settings20; }
            set { settings20 = value; }
        }
        [DisplayName("Setting 22"), Category("Settings"), Description("The settings that coorespond to the object.")]
        public byte Setting22
        {
            get { return settings21; }
            set { settings21 = value; }
        }
        [DisplayName("Setting 23"), Category("Settings"), Description("The settings that coorespond to the object.")]
        public byte Setting23
        {
            get { return settings22; }
            set { settings22 = value; }
        }
        [DisplayName("Setting 24"), Category("Settings"), Description("The settings that coorespond to the object.")]
        public byte Setting24
        {
            get { return settings23; }
            set { settings23 = value; }
        }
    }

    struct CheckpointObject
    {
        public float xPosStart, yPosStart, zPosStart;
        public float xPosEnd, yPosEnd, zPosEnd;
        public uint groupID;

        [DisplayName("X Position"), Category("Checkpoint Side 1"), Description("The X position of one side of the checkpoint.")]
        public float XPosition
        {
            get { return xPosStart; }
            set { xPosStart = value; }
        }

        [DisplayName("Y Position"), Category("Checkpoint Side 1"), Description("The Y position of one side of the checkpoint.")]
        public float YPosition
        {
            get { return yPosStart; }
            set { yPosStart = value; }
        }

        [DisplayName("Z Position"), Category("Checkpoint Side 1"), Description("The Z position of one side of the checkpoint.")]
        public float ZPosition
        {
            get { return zPosStart; }
            set { zPosStart = value; }
        }
        [DisplayName("X Position"), Category("Checkpoint Side 2"), Description("The X position of one side of the checkpoint.")]
        public float XPositionEnd
        {
            get { return xPosEnd; }
            set { xPosEnd = value; }
        }

        [DisplayName("Y Position"), Category("Checkpoint Side 2"), Description("The Y position of one side of the checkpoint.")]
        public float YPositionEnd
        {
            get { return yPosEnd; }
            set { yPosEnd = value; }
        }

        [DisplayName("Z Position"), Category("Checkpoint Side 2"), Description("The Z position of one side of the checkpoint.")]
        public float ZPositionEnd
        {
            get { return zPosEnd; }
            set { zPosEnd = value; }
        }
    }

    struct CheckpointGroupObject
    {
        public ushort pointLength, groupLink, index;
        public short prev1, prev2, prev3, prev4;
        public short next1, next2, next3, next4;

        [DisplayName("Length"), Category("Settings"), Description("The amount of points in this group.")]
        public ushort PointLength
        {
            get { return pointLength; }
            set { pointLength = value; }
        }

        [DisplayName("Group Link"), Category("Settings"), Description("The group link.")]
        public ushort GroupLink
        {
            get { return groupLink; }
            set { groupLink = value; }
        }

        [DisplayName("Next Group 1"), Category("Settings"), Description("The next group that connects to this one.")]
        public short Next1
        {
            get { return next1; }
            set { next1 = value; }
        }

        [DisplayName("Next Group 2"), Category("Settings"), Description("The next group that connects to this one.")]
        public short Next2
        {
            get { return next2; }
            set { next2 = value; }
        }

        [DisplayName("Next Group 1"), Category("Settings"), Description("The next group that connects to this one.")]
        public short Next3
        {
            get { return next3; }
            set { next3 = value; }
        }

        [DisplayName("Previous Group 1"), Category("Settings"), Description("The previous group that connects to this one.")]
        public short Previous1
        {
            get { return prev1; }
            set { prev1 = value; }
        }

        [DisplayName("Previous Group 2"), Category("Settings"), Description("The previous group that connects to this one.")]
        public short Previous2
        {
            get { return prev2; }
            set { prev2 = value; }
        }

        [DisplayName("Previous Group 3"), Category("Settings"), Description("The previous group that connects to this one.")]
        public short Previous3
        {
            get { return prev3; }
            set { prev3 = value; }
        }
    }

    struct KartPointObject
    {
        public float xPos, yPos, zPos;
        public float xScale, yScale, zScale;
        public int xRot, yRot, zRot;
        public byte polePos, playerID;
        public double rotation;

        [DisplayName("X Position"), Category("Position"), Description("The X position of the starting point.")]
        public float XPosition
        {
            get { return xPos; }
            set { xPos = value; }
        }

        [DisplayName("Y Position"), Category("Position"), Description("The Y position of the starting point.")]
        public float YPosition
        {
            get { return yPos; }
            set { yPos = value; }
        }

        [DisplayName("Z Position"), Category("Position"), Description("The Z position of the starting point.")]
        public float ZPosition
        {
            get { return zPos; }
            set { zPos = value; }
        }
        [DisplayName("X Scale"), Category("Position"), Description("The X scale of the starting point.")]
        public float XScale
        {
            get { return xScale; }
            set { xScale = value; }
        }

        [DisplayName("Y Scale"), Category("Position"), Description("The Y scale of the starting point.")]
        public float YScale
        {
            get { return yScale; }
            set { yScale = value; }
        }

        [DisplayName("Z Scale"), Category("Position"), Description("The Z scale of the starting point.")]
        public float ZScale
        {
            get { return zScale; }
            set { zScale = value; }
        }
        [DisplayName("Rotation"), Category("Position"), Description("The rotation of the starting point.")]
        public double Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }
        [DisplayName("Player ID"), Category("Settings"), Description("0xFF if this is a single player map. 0x00 to 0x03 for multiplayer maps.")]
        public byte PlayerID
        {
            get { return playerID; }
            set { playerID = value; }
        }
        [DisplayName("Pole Position"), Category("Settings"), Description("0 = Left, 1= Right")]
        public byte PolePos
        {
            get { return polePos; }
            set { polePos = value; }
        }
    }

    struct AreaObject
    {
        public float xPos, yPos, zPos;
        public float xScale, yScale, zScale;
        public int xRot, yRot, zRot;
        public ushort unk1, unk2;
        public long unk3, unk4;
        public double rotation;

        [DisplayName("X Position"), Category("Position"), Description("The X position of the area.")]
        public float XPosition
        {
            get { return xPos; }
            set { xPos = value; }
        }

        [DisplayName("Y Position"), Category("Position"), Description("The Y position of the area.")]
        public float YPosition
        {
            get { return yPos; }
            set { yPos = value; }
        }

        [DisplayName("Z Position"), Category("Position"), Description("The Z position of the area.")]
        public float ZPosition
        {
            get { return zPos; }
            set { zPos = value; }
        }
        [DisplayName("X Scale"), Category("Position"), Description("The X scale of the area.")]
        public float XScale
        {
            get { return xScale; }
            set { xScale = value; }
        }

        [DisplayName("Y Scale"), Category("Position"), Description("The Y scale of the area.")]
        public float YScale
        {
            get { return yScale; }
            set { yScale = value; }
        }

        [DisplayName("Z Scale"), Category("Position"), Description("The Z scale of the area.")]
        public float ZScale
        {
            get { return zScale; }
            set { zScale = value; }
        }
        [DisplayName("Rotation"), Category("Position"), Description("The rotation of the area.")]
        public double Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }
        [DisplayName("Unknown 1"), Category("Settings"), Description("Unknown.")]
        public ushort Unknown1
        {
            get { return unk1; }
            set { unk1 = value; }
        }
        [DisplayName("Unknown 2"), Category("Settings"), Description("Unknown.")]
        public ushort Unknown2
        {
            get { return unk2; }
            set { unk2 = value; }
        }
        [DisplayName("Unknown 3"), Category("Settings"), Description("Unknown.")]
        public long Unknown3
        {
            get { return unk3; }
            set { unk3 = value; }
        }
        [DisplayName("Unknown 4"), Category("Settings"), Description("Unknown.")]
        public long Unknown4
        {
            get { return unk4; }
            set { unk4 = value; }
        }
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

        [DisplayName("X View 1"), Category("Position View 1"), Description("The X View 1 position of the camera.")]
        public float XView1
        {
            get { return xView1; }
            set { xView1 = value; }
        }
        [DisplayName("Y View 1"), Category("Position View 1"), Description("The Y View 1 position of the camera.")]
        public float YView1
        {
            get { return yView1; }
            set { yView1 = value; }
        }
        [DisplayName("Z View 1"), Category("Position View 1"), Description("The Z View 1 position of the camera.")]
        public float ZView1
        {
            get { return zView1; }
            set { zView1 = value; }
        }
        [DisplayName("X View 2"), Category("Position View 2"), Description("The X View 2 position of the camera.")]
        public float XView2
        {
            get { return xView2; }
            set { xView1 = value; }
        }
        [DisplayName("Y View 2"), Category("Position View 2"), Description("The Y View 2 position of the camera.")]
        public float YView2
        {
            get { return yView2; }
            set { yView1 = value; }
        }
        [DisplayName("Z View 2"), Category("Position View 2"), Description("The Z View 2 position of the camera.")]
        public float ZView2
        {
            get { return zView2; }
            set { zView2 = value; }
        }
        [DisplayName("X View 3"), Category("Position View 3"), Description("The X View 3 position of the camera.")]
        public float XView3
        {
            get { return xView1; }
            set { xView1 = value; }
        }
        [DisplayName("Y View 3"), Category("Position View 3"), Description("The Y View 3 position of the camera.")]
        public float YView3
        {
            get { return yView3; }
            set { yView3 = value; }
        }
        [DisplayName("Z View 3"), Category("Position View 3"), Description("The Z View 3 position of the camera.")]
        public float ZView3
        {
            get { return zView3; }
            set { zView3 = value; }
        }
        [DisplayName("Rotation"), Category("Settings"), Description("The rotation of the camera.")]
        public double Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }
        [DisplayName("Unknown 1"), Category("Settings"), Description("Unknown.")]
        public byte Unknown1
        {
            get { return unk1; }
            set { unk1 = value; }
        }
        [DisplayName("Type"), Category("Settings"), Description("The type of camera.")]
        public byte Type
        {
            get { return type; }
            set { type = value; }
        }
        [DisplayName("Starting Zoom"), Category("Settings"), Description("The zoom that the camera starts at.")]
        public ushort StartZoom
        {
            get { return startZoom; }
            set { startZoom = value; }
        }
        [DisplayName("Duration"), Category("Settings"), Description("How long the camera lasts.")]
        public ushort Duration
        {
            get { return duration; }
            set { duration = value; }
        }
        [DisplayName("Unknown 2"), Category("Settings"), Description("Unknown.")]
        public ushort Unknown2
        {
            get { return unk2; }
            set { unk2 = value; }
        }
        [DisplayName("Unknown 3"), Category("Settings"), Description("Unknown.")]
        public ushort Unknown3
        {
            get { return unk3; }
            set { unk3 = value; }
        }
        [DisplayName("Unknown 4"), Category("Settings"), Description("Unknown.")]
        public ushort Unknown4
        {
            get { return unk4; }
            set { unk4 = value; }
        }
        [DisplayName("Route ID"), Category("Settings"), Description("The Route ID.")]
        public short RouteID
        {
            get { return routeID; }
            set { routeID = value; }
        }
        [DisplayName("Route Speed"), Category("Settings"), Description("The speed of the route.")]
        public ushort RouteSpeed
        {
            get { return routeSpeed; }
            set { routeSpeed = value; }
        }
        [DisplayName("End Zoom"), Category("Settings"), Description("The zoom level when the camera ends.")]
        public ushort EndZoom
        {
            get { return endZoom; }
            set { endZoom = value; }
        }
        [DisplayName("Next Camera ID"), Category("Settings"), Description("The next camera's ID.")]
        public short NextCamera
        {
            get { return nextCamera; }
            set { nextCamera = value; }
        }
        [DisplayName("Camera Name"), Category("Settings"), Description("The camera name.")]
        public string CameraName
        {
            get { return name; }
            set { name = value; }
        }
    }

    public class RespawnObject
    {
        public float xPos, yPos, zPos;
        public int xRot, yRot, zRot;
        public ushort unk1, unk2, unk3, respawnID;
        public double rotation;

        [DisplayName("X Position"), Category("Position"), Description("The X position of the object.")]
        public float XPosition
        {
            get { return xPos; }
            set { xPos = value; }
        }

        [DisplayName("Y Position"), Category("Position"), Description("The Y position of the object.")]
        public float YPosition
        {
            get { return yPos; }
            set { yPos = value; }
        }

        [DisplayName("Z Position"), Category("Position"), Description("The Z position of the object.")]
        public float ZPosition
        {
            get { return zPos; }
            set { zPos = value; }
        }
        [DisplayName("Rotation"), Category("Position"), Description("The rotation of the object.")]
        public double Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }
        [DisplayName("Unknown 1"), Category("Settings"), Description("Unknown.")]
        public ushort Unknown1
        {
            get { return unk1; }
            set { unk1 = value; }
        }
        [DisplayName("Unknown 2"), Category("Settings"), Description("Unknown.")]
        public ushort Unknown2
        {
            get { return unk2; }
            set { unk2 = value; }
        }
        [DisplayName("Unknown 3"), Category("Settings"), Description("Unknown.")]
        public ushort Unknown3
        {
            get { return unk3; }
            set { unk3 = value; }
        }
        [DisplayName("Respawn ID"), Category("Settings"), Description("The respawn id. This respawn id links to the id in a checkpoint(?) that when the player has to be picked up by lakitu, they spawn here.")]
        public ushort RespawnID
        {
            get { return respawnID; }
            set { respawnID = value; }
        }
    }
}
