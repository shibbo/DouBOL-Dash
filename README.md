# DouBOL Dash
DouBOL Dash was a editor I started back during the summer of 2016 and ended up trashing because I didn't know a thing or two how to code a proper editor. But now it's back!

It is written in C# and uses OpenTK for rendering. Big shoutout to StapleButter for the BMD rendering code, and miluaces for his few patches that I will be using.

# How to use
First, clone or download the repo. To run from source, simply click the solution and set it up (OpenTK references may be missing, it's normal). I used Visual Studio 2015 for this project, and .NET Framework 4.5.2.

If you want to run the EXE, it's located in DouBOLDash/bin/Debug. Note: You have to have the game extracted, and be sure that the BOL you're opening is in the same directory as the bmd course model! If you just extracted the arc and did nothing else, it should be structured fine to work with the editor.

# What's supported
Currently DouBOL Dash can read, render, and save everything except for the last 2 sections, which don't crash the game if removed. The editor just defaults their offsets to the filesize (which is what retail does), so it avoids a crash. Also, rotations are not saved, as they are defaulted to 0f, 0f, 0f until reversing the algorithm is fixed. Please report saving bugs to me!

# Credits
Thanks to StapleButter for the main BMD rendering code, and miluaces for minor changes. Icon was made by Kaio.