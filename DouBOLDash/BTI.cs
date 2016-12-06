using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing.Imaging;

namespace DouBOLDash
{
    public class BTIFile
    {
        /* Reads BTI file and loads texture into OpenGL */
        public static int ReadBTI(FileBase fb, out int owidth, out int oheight)
        {
            fb.BigEndian = true;

            /* Header */
            fb.Reader.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
            byte format = fb.Reader.ReadByte();
            byte unknown1 = fb.Reader.ReadByte();
            UInt16 width = fb.Reader.ReadUInt16();
            UInt16 height = fb.Reader.ReadUInt16();
            UInt16 unknown2 = fb.Reader.ReadUInt16();
            byte unknown3 = fb.Reader.ReadByte();
            byte paletteFormat = fb.Reader.ReadByte();
            UInt16 paletteCount = fb.Reader.ReadUInt16();
            UInt32 paletteOffset = fb.Reader.ReadUInt32();
            UInt32 unknown4 = fb.Reader.ReadUInt32();
            UInt16 unknown5 = fb.Reader.ReadUInt16();
            UInt16 unknown6 = fb.Reader.ReadUInt16();
            byte mipmapCount = fb.Reader.ReadByte();
            byte unknown7 = fb.Reader.ReadByte();
            UInt16 unknown8 = fb.Reader.ReadUInt16();
            UInt32 dataOffset = fb.Reader.ReadUInt32();

            owidth = width;
            oheight = height;

            UInt16[] palette = new UInt16[paletteCount];
            if (paletteCount != 0)
            {
                if (paletteFormat != 0 && paletteFormat != 1 && paletteFormat != 2)
                    throw new NotImplementedException();

                fb.Stream.Seek(paletteOffset, System.IO.SeekOrigin.Begin);
                for (int i = 0; i < paletteCount; i++)
                    palette[i] = fb.Reader.ReadUInt16();
            }

            TextureWrapMode[] wrapmodes = { TextureWrapMode.ClampToEdge, TextureWrapMode.Repeat, TextureWrapMode.MirroredRepeat };
            TextureMinFilter[] minfilters = { TextureMinFilter.Nearest, TextureMinFilter.Linear,
                                                TextureMinFilter.NearestMipmapNearest, TextureMinFilter.LinearMipmapNearest,
                                                TextureMinFilter.NearestMipmapLinear, TextureMinFilter.LinearMipmapLinear };
            TextureMagFilter[] magfilters = { TextureMagFilter.Nearest, TextureMagFilter.Linear,
                                                TextureMagFilter.Nearest, TextureMagFilter.Linear,
                                                TextureMagFilter.Nearest, TextureMagFilter.Linear };

            int texture = GL.GenTexture();

            GL.BindTexture(TextureTarget.Texture2D, texture);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMaxLevel, mipmapCount - 1);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, 0);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, 0);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, 0.0f);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, 0.0f);

            PixelInternalFormat ifmt;
            OpenTK.Graphics.OpenGL.PixelFormat fmt;
            switch (format)
            {
                case 0:
                case 1: ifmt = PixelInternalFormat.Intensity; fmt = OpenTK.Graphics.OpenGL.PixelFormat.Luminance; break;

                case 2:
                case 3: ifmt = PixelInternalFormat.Luminance8Alpha8; fmt = OpenTK.Graphics.OpenGL.PixelFormat.LuminanceAlpha; break;

                default: ifmt = PixelInternalFormat.Four; fmt = OpenTK.Graphics.OpenGL.PixelFormat.Bgra; break;
            }

            byte[][] images = new byte[mipmapCount][];
            for (int mip = 0; mip < mipmapCount; mip++)
            {
                byte[] image = null;

                switch (format)
                {
                    case 0: // I4
                        {
                            image = new byte[width * height];

                            for (int by = 0; by < height; by += 8)
                            {
                                for (int bx = 0; bx < width; bx += 8)
                                {
                                    for (int y = 0; y < 8; y++)
                                    {
                                        for (int x = 0; x < 8; x += 2)
                                        {
                                            byte b = fb.Reader.ReadByte();

                                            int outp = (((by + y) * width) + (bx + x));
                                            image[outp++] = (byte)((b & 0xF0) | (b >> 4));
                                            image[outp] = (byte)((b << 4) | (b & 0x0F));
                                        }
                                    }
                                }
                            }
                        }
                        break;

                    case 1: // I8
                        {
                            image = new byte[width * height];

                            for (int by = 0; by < height; by += 4)
                            {
                                for (int bx = 0; bx < width; bx += 8)
                                {
                                    for (int y = 0; y < 4; y++)
                                    {
                                        for (int x = 0; x < 8; x++)
                                        {
                                            byte b = fb.Reader.ReadByte();

                                            int outp = (((by + y) * width) + (bx + x));
                                            image[outp] = b;
                                        }
                                    }
                                }
                            }
                        }
                        break;

                    case 2: // I4A4
                        {
                            image = new byte[width * height * 2];

                            for (int by = 0; by < height; by += 4)
                            {
                                for (int bx = 0; bx < width; bx += 8)
                                {
                                    for (int y = 0; y < 4; y++)
                                    {
                                        for (int x = 0; x < 8; x++)
                                        {
                                            byte b = fb.Reader.ReadByte();

                                            int outp = (((by + y) * width) + (bx + x)) * 2;
                                            image[outp++] = (byte)((b << 4) | (b & 0x0F));
                                            image[outp] = (byte)((b & 0xF0) | (b >> 4));
                                        }
                                    }
                                }
                            }
                        }
                        break;

                    case 3: // I8A8
                        {
                            image = new byte[width * height * 2];

                            for (int by = 0; by < height; by += 4)
                            {
                                for (int bx = 0; bx < width; bx += 4)
                                {
                                    for (int y = 0; y < 4; y++)
                                    {
                                        for (int x = 0; x < 4; x++)
                                        {
                                            byte a = fb.Reader.ReadByte();
                                            byte l = fb.Reader.ReadByte();

                                            int outp = (((by + y) * width) + (bx + x)) * 2;
                                            image[outp++] = l;
                                            image[outp] = a;
                                        }
                                    }
                                }
                            }
                        }
                        break;

                    case 4: // RGB565
                        {
                            image = new byte[width * height * 4];

                            for (int by = 0; by < height; by += 4)
                            {
                                for (int bx = 0; bx < width; bx += 4)
                                {
                                    for (int y = 0; y < 4; y++)
                                    {
                                        for (int x = 0; x < 4; x++)
                                        {
                                            ushort col = fb.Reader.ReadUInt16();

                                            int outp = (((by + y) * width) + (bx + x)) * 4;
                                            image[outp++] = (byte)(((col & 0x001F) << 3) | ((col & 0x001F) >> 2));
                                            image[outp++] = (byte)(((col & 0x07E0) >> 3) | ((col & 0x07E0) >> 8));
                                            image[outp++] = (byte)(((col & 0xF800) >> 8) | ((col & 0xF800) >> 13));
                                            image[outp] = 255;
                                        }
                                    }
                                }
                            }
                        }
                        break;

                    //RGB5A3 Added 1/20/15
                    case 5:
                        {
                            image = new byte[width * height * 4];

                            for (int by = 0; by < height; by += 4)
                            {
                                for (int bx = 0; bx < width; bx += 4)
                                {
                                    for (int y = 0; y < 4; y++)
                                    {
                                        for (int x = 0; x < 4; x++)
                                        {
                                            byte r, g, b, a;
                                            ushort srcPixel = fb.Reader.ReadUInt16();
                                            int outp = (((by + y) * width) + (bx + x)) * 4;
                                            if ((srcPixel & 0x8000) == 0x8000)
                                            {
                                                r = (byte)((srcPixel & 0x7c00) >> 10);
                                                r = (byte)((r << (8 - 5)) | (r >> (10 - 8)));

                                                g = (byte)((srcPixel & 0x3e0) >> 5);
                                                g = (byte)((g << (8 - 5)) | (g >> (10 - 8)));

                                                b = (byte)(srcPixel & 0x1f);
                                                b = (byte)((b << (8 - 5)) | (b >> (10 - 8)));

                                                a = 0xff;
                                            }
                                            else //a3rgb4
                                            {
                                                r = (byte)((srcPixel & 0x7000) >> 12);
                                                r = (byte)((r << (8 - 3)) | (r << (8 - 6)) | (r >> (9 - 8)));

                                                g = (byte)((srcPixel & 0xf00) >> 8);
                                                g = (byte)((g << (8 - 4)) | g);

                                                b = (byte)((srcPixel & 0xf0) >> 4);
                                                b = (byte)((b << (8 - 4)) | b);

                                                a = (byte)(srcPixel & 0xf);
                                                a = (byte)((a << (8 - 4)) | a);
                                            }

                                            image[outp++] = r;
                                            image[outp++] = g;
                                            image[outp++] = b;
                                            image[outp] = a;
                                        }
                                    }
                                }
                            }
                        }
                        break;

                    //ARGB8 Added 1/20/15
                    case 6:
                        {
                            image = new byte[width * height * 4];

                            for (int by = 0; by < height; by += 4)
                            {
                                for (int bx = 0; bx < width; bx += 4)
                                {
                                    for (int y = 0; y < 4; y++)
                                    {
                                        for (int x = 0; x < 4; x++)
                                        {
                                            if (x + bx < width && y + by < height)
                                            {
                                                byte a = fb.Reader.ReadByte();
                                                byte r = fb.Reader.ReadByte();
                                                int outp = (((by + y) * width) + (bx + x)) * 4;
                                                image[outp + 0] = r;
                                                image[outp + 3] = a;
                                            }
                                        }
                                    }
                                    for (int y = 0; y < 4; y++)
                                    {
                                        for (int x = 0; x < 4; x++)
                                        {
                                            if (x + bx < width && y + by < height)
                                            {
                                                byte g = fb.Reader.ReadByte();
                                                byte b = fb.Reader.ReadByte();
                                                int outp = (((by + y) * width) + (bx + x)) * 4;
                                                image[outp + 1] = g;
                                                image[outp + 2] = b;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;

                    case 14: // DXT1
                        {
                            image = new byte[width * height * 4];

                            for (int by = 0; by < height; by += 8)
                            {
                                for (int bx = 0; bx < width; bx += 8)
                                {
                                    for (int sby = 0; sby < 8; sby += 4)
                                    {
                                        for (int sbx = 0; sbx < 8; sbx += 4)
                                        {
                                            ushort c1 = fb.Reader.ReadUInt16();
                                            ushort c2 = fb.Reader.ReadUInt16();
                                            uint block = fb.Reader.ReadUInt32();

                                            byte r1 = (byte)((c1 & 0xF800) >> 8);
                                            byte g1 = (byte)((c1 & 0x07E0) >> 3);
                                            byte b1 = (byte)((c1 & 0x001F) << 3);
                                            byte r2 = (byte)((c2 & 0xF800) >> 8);
                                            byte g2 = (byte)((c2 & 0x07E0) >> 3);
                                            byte b2 = (byte)((c2 & 0x001F) << 3);

                                            byte[,] colors = new byte[4, 4];
                                            colors[0, 0] = 255; colors[0, 1] = r1; colors[0, 2] = g1; colors[0, 3] = b1;
                                            colors[1, 0] = 255; colors[1, 1] = r2; colors[1, 2] = g2; colors[1, 3] = b2;
                                            if (c1 > c2)
                                            {
                                                int r3 = ((r1 << 1) + r2) / 3;
                                                int g3 = ((g1 << 1) + g2) / 3;
                                                int b3 = ((b1 << 1) + b2) / 3;

                                                int r4 = (r1 + (r2 << 1)) / 3;
                                                int g4 = (g1 + (g2 << 1)) / 3;
                                                int b4 = (b1 + (b2 << 1)) / 3;

                                                colors[2, 0] = 255; colors[2, 1] = (byte)r3; colors[2, 2] = (byte)g3; colors[2, 3] = (byte)b3;
                                                colors[3, 0] = 255; colors[3, 1] = (byte)r4; colors[3, 2] = (byte)g4; colors[3, 3] = (byte)b4;
                                            }
                                            else
                                            {
                                                colors[2, 0] = 255;
                                                colors[2, 1] = (byte)((r1 + r2) / 2);
                                                colors[2, 2] = (byte)((g1 + g2) / 2);
                                                colors[2, 3] = (byte)((b1 + b2) / 2);
                                                colors[3, 0] = 0; colors[3, 1] = r2; colors[3, 2] = g2; colors[3, 3] = b2;
                                            }

                                            for (int y = 0; y < 4; y++)
                                            {
                                                for (int x = 0; x < 4; x++)
                                                {
                                                    int c = (int)(block >> 30);
                                                    int outp = (((by + sby + y) * width) + (bx + sbx + x)) * 4;
                                                    image[outp++] = (byte)(colors[c, 3] | (colors[c, 3] >> 5));
                                                    image[outp++] = (byte)(colors[c, 2] | (colors[c, 2] >> 5));
                                                    image[outp++] = (byte)(colors[c, 1] | (colors[c, 1] >> 5));
                                                    image[outp] = colors[c, 0];
                                                    block <<= 2;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;

                    default: throw new NotImplementedException("Bmd: unsupported texture format " + format.ToString());
                }
                GL.TexImage2D(TextureTarget.Texture2D, mip, ifmt, width, height, 0, fmt, PixelType.UnsignedByte, image);
                width /= 2; height /= 2;
            }

            return texture;
        }

        /* Reads BTI file and loads texture into OpenGL */
        public static Bitmap ReadBTIToBitmap(FileBase fb)
        {
            fb.BigEndian = true;

            /* Header */
            fb.Reader.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
            byte format = fb.Reader.ReadByte();
            byte unknown1 = fb.Reader.ReadByte();
            UInt16 width = fb.Reader.ReadUInt16();
            UInt16 height = fb.Reader.ReadUInt16();
            UInt16 unknown2 = fb.Reader.ReadUInt16();
            byte unknown3 = fb.Reader.ReadByte();
            byte paletteFormat = fb.Reader.ReadByte();
            UInt16 paletteCount = fb.Reader.ReadUInt16();
            UInt32 paletteOffset = fb.Reader.ReadUInt32();
            UInt32 unknown4 = fb.Reader.ReadUInt32();
            UInt16 unknown5 = fb.Reader.ReadUInt16();
            UInt16 unknown6 = fb.Reader.ReadUInt16();
            byte mipmapCount = fb.Reader.ReadByte();
            byte unknown7 = fb.Reader.ReadByte();
            UInt16 unknown8 = fb.Reader.ReadUInt16();
            UInt32 dataOffset = fb.Reader.ReadUInt32();

            UInt16[] palette = new UInt16[paletteCount];
            if (paletteCount != 0)
            {
                if (paletteFormat != 0 && paletteFormat != 1 && paletteFormat != 2)
                    throw new NotImplementedException();

                fb.Stream.Seek(paletteOffset, System.IO.SeekOrigin.Begin);
                for (int i = 0; i < paletteCount; i++)
                    palette[i] = fb.Reader.ReadUInt16();
            }

            TextureWrapMode[] wrapmodes = { TextureWrapMode.ClampToEdge, TextureWrapMode.Repeat, TextureWrapMode.MirroredRepeat };
            TextureMinFilter[] minfilters = { TextureMinFilter.Nearest, TextureMinFilter.Linear,
                                                TextureMinFilter.NearestMipmapNearest, TextureMinFilter.LinearMipmapNearest,
                                                TextureMinFilter.NearestMipmapLinear, TextureMinFilter.LinearMipmapLinear };
            TextureMagFilter[] magfilters = { TextureMagFilter.Nearest, TextureMagFilter.Linear,
                                                TextureMagFilter.Nearest, TextureMagFilter.Linear,
                                                TextureMagFilter.Nearest, TextureMagFilter.Linear };

            int texture = GL.GenTexture();

            Bitmap bmimg = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            BitmapData bmd = bmimg.LockBits(new Rectangle(0, 0, bmimg.Width, bmimg.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, bmimg.PixelFormat);
            const int FORMATSIZE = 4;

            unsafe
            {
                switch (format)
                {
                    case 0: // I4
                        {
                            for (int by = 0; by < height; by += 8)
                            {
                                for (int bx = 0; bx < width; bx += 8)
                                {
                                    for (int y = 0; y < 8; y++)
                                    {
                                        for (int x = 0; x < 8; x += 2)
                                        {
                                            byte b = fb.Reader.ReadByte();

                                            byte* cur = (byte*)bmd.Scan0 + ((by + y) * bmd.Stride) + ((bx + x) * FORMATSIZE);

                                            byte i1 = (byte)((b & 0xF0) | (b >> 4));
                                            byte i2 = (byte)((b << 4) | (b & 0x0F));
                                            *(cur++) = i1;
                                            *(cur++) = i1;
                                            *(cur++) = i1;
                                            *(cur++) = i1;

                                            *(cur++) = i2;
                                            *(cur++) = i2;
                                            *(cur++) = i2;
                                            *(cur++) = i2;
                                        }
                                    }
                                }
                            }
                        }
                        break;

                    case 1: // I8
                        {
                            for (int by = 0; by < height; by += 4)
                            {
                                for (int bx = 0; bx < width; bx += 8)
                                {
                                    for (int y = 0; y < 4; y++)
                                    {
                                        for (int x = 0; x < 8; x++)
                                        {
                                            byte b = fb.Reader.ReadByte();
                                            byte* cur = (byte*)bmd.Scan0 + ((by + y) * bmd.Stride) + ((bx + x) * FORMATSIZE);

                                            *(cur++) = b;
                                            *(cur++) = b;
                                            *(cur++) = b;
                                            *(cur++) = b;
                                        }
                                    }
                                }
                            }
                        }
                        break;

                    case 2: // I4A4
                        {
                            for (int by = 0; by < height; by += 4)
                            {
                                for (int bx = 0; bx < width; bx += 8)
                                {
                                    for (int y = 0; y < 4; y++)
                                    {
                                        for (int x = 0; x < 8; x++)
                                        {
                                            byte b = fb.Reader.ReadByte();
                                            byte* cur = (byte*)bmd.Scan0 + ((by + y) * bmd.Stride) + ((bx + x) * FORMATSIZE);

                                            byte i = (byte)((b << 4) | (b & 0x0F));
                                            byte a = (byte)((b & 0xF0) | (b >> 4));

                                            *(cur++) = i;
                                            *(cur++) = i;
                                            *(cur++) = i;
                                            *(cur++) = a;
                                        }
                                    }
                                }
                            }
                        }
                        break;

                    case 3: // I8A8
                        {
                            for (int by = 0; by < height; by += 4)
                            {
                                for (int bx = 0; bx < width; bx += 4)
                                {
                                    for (int y = 0; y < 4; y++)
                                    {
                                        for (int x = 0; x < 4; x++)
                                        {
                                            byte a = fb.Reader.ReadByte();
                                            byte i = fb.Reader.ReadByte();

                                            byte* cur = (byte*)bmd.Scan0 + ((by + y) * bmd.Stride) + ((bx + x) * FORMATSIZE);

                                            *(cur++) = i;
                                            *(cur++) = i;
                                            *(cur++) = i;
                                            *(cur++) = a;
                                        }
                                    }
                                }
                            }
                        }
                        break;

                    case 4: // RGB565
                        {
                            for (int by = 0; by < height; by += 4)
                            {
                                for (int bx = 0; bx < width; bx += 4)
                                {
                                    for (int y = 0; y < 4; y++)
                                    {
                                        for (int x = 0; x < 4; x++)
                                        {
                                            ushort col = fb.Reader.ReadUInt16();

                                            byte* cur = (byte*)bmd.Scan0 + ((by + y) * bmd.Stride) + ((bx + x) * FORMATSIZE);

                                            byte b = (byte)(((col & 0x001F) << 3) | ((col & 0x001F) >> 2));
                                            byte g = (byte)(((col & 0x07E0) >> 3) | ((col & 0x07E0) >> 8));
                                            byte r = (byte)(((col & 0xF800) >> 8) | ((col & 0xF800) >> 13));

                                            *(cur++) = b;
                                            *(cur++) = g;
                                            *(cur++) = r;
                                            *(cur++) = 255;
                                        }
                                    }
                                }
                            }
                        }
                        break;

                    //RGB5A3 Added 1/20/15
                    case 5:
                        {
                            for (int by = 0; by < height; by += 4)
                            {
                                for (int bx = 0; bx < width; bx += 4)
                                {
                                    for (int y = 0; y < 4; y++)
                                    {
                                        for (int x = 0; x < 4; x++)
                                        {
                                            byte r, g, b, a;
                                            ushort srcPixel = fb.Reader.ReadUInt16();
                                            byte* cur = (byte*)bmd.Scan0 + ((by + y) * bmd.Stride) + ((bx + x) * FORMATSIZE);
                                            if ((srcPixel & 0x8000) == 0x8000)
                                            {
                                                r = (byte)((srcPixel & 0x7c00) >> 10);
                                                r = (byte)((r << (8 - 5)) | (r >> (10 - 8)));

                                                g = (byte)((srcPixel & 0x3e0) >> 5);
                                                g = (byte)((g << (8 - 5)) | (g >> (10 - 8)));

                                                b = (byte)(srcPixel & 0x1f);
                                                b = (byte)((b << (8 - 5)) | (b >> (10 - 8)));

                                                a = 0xff;
                                            }
                                            else //a3rgb4
                                            {
                                                r = (byte)((srcPixel & 0x7000) >> 12);
                                                r = (byte)((r << (8 - 3)) | (r << (8 - 6)) | (r >> (9 - 8)));

                                                g = (byte)((srcPixel & 0xf00) >> 8);
                                                g = (byte)((g << (8 - 4)) | g);

                                                b = (byte)((srcPixel & 0xf0) >> 4);
                                                b = (byte)((b << (8 - 4)) | b);

                                                a = (byte)(srcPixel & 0xf);
                                                a = (byte)((a << (8 - 4)) | a);
                                            }

                                            *(cur++) = b;
                                            *(cur++) = g;
                                            *(cur++) = r;
                                            *(cur++) = a;
                                        }
                                    }
                                }
                            }
                        }
                        break;

                    //ARGB8 Added 1/20/15
                    case 6:
                        {
                            for (int by = 0; by < height; by += 4)
                            {
                                for (int bx = 0; bx < width; bx += 4)
                                {
                                    for (int y = 0; y < 4; y++)
                                    {
                                        for (int x = 0; x < 4; x++)
                                        {
                                            if (x + bx < width && y + by < height)
                                            {
                                                byte* cur = (byte*)bmd.Scan0 + ((by + y) * bmd.Stride) + ((bx + x) * FORMATSIZE) + 2;

                                                byte a = fb.Reader.ReadByte();
                                                byte r = fb.Reader.ReadByte();

                                                *(cur++) = r;
                                                *(cur++) = a;
                                            }
                                        }
                                    }
                                    for (int y = 0; y < 4; y++)
                                    {
                                        for (int x = 0; x < 4; x++)
                                        {
                                            if (x + bx < width && y + by < height)
                                            {
                                                byte* cur = (byte*)bmd.Scan0 + ((by + y) * bmd.Stride) + ((bx + x) * FORMATSIZE);

                                                byte g = fb.Reader.ReadByte();
                                                byte b = fb.Reader.ReadByte();

                                                *(cur++) = b;
                                                *(cur++) = g;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;

                    case 14: // DXT1
                        {
                            for (int by = 0; by < height; by += 8)
                            {
                                for (int bx = 0; bx < width; bx += 8)
                                {
                                    for (int sby = 0; sby < 8; sby += 4)
                                    {
                                        for (int sbx = 0; sbx < 8; sbx += 4)
                                        {
                                            ushort c1 = fb.Reader.ReadUInt16();
                                            ushort c2 = fb.Reader.ReadUInt16();
                                            uint block = fb.Reader.ReadUInt32();

                                            byte r1 = (byte)((c1 & 0xF800) >> 8);
                                            byte g1 = (byte)((c1 & 0x07E0) >> 3);
                                            byte b1 = (byte)((c1 & 0x001F) << 3);
                                            byte r2 = (byte)((c2 & 0xF800) >> 8);
                                            byte g2 = (byte)((c2 & 0x07E0) >> 3);
                                            byte b2 = (byte)((c2 & 0x001F) << 3);

                                            byte[,] colors = new byte[4, 4];
                                            colors[0, 0] = 255; colors[0, 1] = r1; colors[0, 2] = g1; colors[0, 3] = b1;
                                            colors[1, 0] = 255; colors[1, 1] = r2; colors[1, 2] = g2; colors[1, 3] = b2;
                                            if (c1 > c2)
                                            {
                                                int r3 = ((r1 << 1) + r2) / 3;
                                                int g3 = ((g1 << 1) + g2) / 3;
                                                int b3 = ((b1 << 1) + b2) / 3;

                                                int r4 = (r1 + (r2 << 1)) / 3;
                                                int g4 = (g1 + (g2 << 1)) / 3;
                                                int b4 = (b1 + (b2 << 1)) / 3;

                                                colors[2, 0] = 255; colors[2, 1] = (byte)r3; colors[2, 2] = (byte)g3; colors[2, 3] = (byte)b3;
                                                colors[3, 0] = 255; colors[3, 1] = (byte)r4; colors[3, 2] = (byte)g4; colors[3, 3] = (byte)b4;
                                            }
                                            else
                                            {
                                                colors[2, 0] = 255;
                                                colors[2, 1] = (byte)((r1 + r2) / 2);
                                                colors[2, 2] = (byte)((g1 + g2) / 2);
                                                colors[2, 3] = (byte)((b1 + b2) / 2);
                                                colors[3, 0] = 0; colors[3, 1] = r2; colors[3, 2] = g2; colors[3, 3] = b2;
                                            }

                                            for (int y = 0; y < 4; y++)
                                            {
                                                for (int x = 0; x < 4; x++)
                                                {
                                                    int c = (int)(block >> 30);
                                                    byte* cur = (byte*)bmd.Scan0 + ((by + sby + y) * bmd.Stride) + ((bx + sbx + x) * FORMATSIZE);

                                                    *(cur++) = (byte)(colors[c, 3] | (colors[c, 3] >> 5));
                                                    *(cur++) = (byte)(colors[c, 2] | (colors[c, 2] >> 5));
                                                    *(cur++) = (byte)(colors[c, 1] | (colors[c, 1] >> 5));
                                                    *(cur++) = colors[c, 0];
                                                    block <<= 2;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;

                    default: throw new NotImplementedException("Bmd: unsupported texture format " + format.ToString());
                }
            }

            bmimg.UnlockBits(bmd);

            return bmimg;
        }
    }
}
