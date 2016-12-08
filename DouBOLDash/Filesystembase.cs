/*
    Copyright 2016-2017 shibboleet, StapleButter
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

namespace DouBOLDash
{
    public class FilesystemBase
    {
        public virtual void Close() { }

        public virtual string[] GetDirectories(string directory)
        { throw new NotImplementedException("FilesystemBase.GetDirectories()"); }

        public virtual bool DirectoryExists(string directory)
        { throw new NotImplementedException("FilesystemBase.DirectoryExists()"); }


        public virtual string[] GetFiles(string directory)
        { throw new NotImplementedException("FilesystemBase.GetFiles()"); }

        public virtual bool FileExists(string filename)
        { throw new NotImplementedException("FilesystemBase.FileExists()"); }

        public virtual FileBase OpenFile(string filename)
        { throw new NotImplementedException("FilesystemBase.OpenFile()"); }
    }

    public class FileBase
    {
        public Stream Stream
        {
            get { return m_Stream; }
            set
            {
                m_Stream = value;
                InitRW();
            }
        }

        public bool BigEndian
        {
            get { return m_BigEndian; }
            set
            {
                m_BigEndian = value;
                InitRW();
            }
        }

        public Encoding Encoding
        {
            get { return m_Encoding; }
            set
            {
                m_Encoding = value;
                InitRW();
            }
        }

        public BinaryReader Reader;
        public BinaryWriter Writer;

        private Stream m_Stream;
        private bool m_BigEndian;
        private Encoding m_Encoding = Encoding.ASCII;

        private void InitRW()
        {
            Reader = m_BigEndian ? new BinaryReaderBE(m_Stream, m_Encoding) : new BinaryReader(m_Stream, m_Encoding);
            Writer = m_BigEndian ? new BinaryWriterBE(m_Stream, m_Encoding) : new BinaryWriter(m_Stream, m_Encoding);
        }


        public string ReadString()
        {
            string ret = "";
            char c;
            while ((c = Reader.ReadChar()) != '\0')
                ret += c;
            return ret;
        }

        public int WriteString(string str)
        {
            int oldpos = (int)Stream.Position;

            foreach (char c in str)
                Writer.Write(c);
            Writer.Write('\0');

            return (int)(Stream.Position - oldpos);
        }


        public virtual void Flush()
        {
            m_Stream.Flush();
        }

        public virtual void Close()
        {
            m_Stream.Close();
        }
    }
}