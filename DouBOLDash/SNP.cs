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
using System.Windows.Forms;
using System.IO;

namespace DouBOLDash
{
    class SNP
    {
        int value;
        float unk1, unk2, unk3;
        byte unk4, unk5, unk6, unk7;
        uint unk8;
        byte unk9, unk10, unk11, unk12;
        float unk13, unk14, unk15, unk16, unk17, unk18, unk19, unk20, unk21, unk22, unk23, unk24;
        public void Parse(EndianBinaryReader reader)
        {
            // basic check to see if this SNP is correct
            // also unfinished code :D
            for (int i = 0; i < 11; i++)
            {
                value = reader.ReadByte();
                if (value != 0)
                {
                    MessageBox.Show("Invalid SNP file.", "Invalid SNP");
                    return;
                }

                unk1 = reader.ReadSingle();
                unk2 = reader.ReadSingle();
                unk3 = reader.ReadSingle();

                unk4 = reader.ReadByte();
                unk5 = reader.ReadByte();
                unk6 = reader.ReadByte();
                unk7 = reader.ReadByte();

                unk8 = reader.ReadUInt32();


            }
        }
    }
}
