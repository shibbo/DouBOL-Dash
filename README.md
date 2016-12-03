# DouBOL Dash
DouBOL Dash was a editor I started back during the summer of 2016 and ended up trashing because I didn't know a thing or two how to code a proper editor. But now it's back!

It is written in C# and uses OpenTK for rendering. Big shoutout to StapleButter for the BMD rendering code, and miluaces for his few patches that I will be using.

# How to use
First, clone or download the repo. To run from source, simply click the solution and set it up (OpenTK references may be missing, it's normal). I used Visual Studio 2015 for this project, and .NET Framework 4.5.2.

If you want to run the EXE, it's located in DouBOLDash/bin/Debug. Note: You have to have the game extracted, and be sure that the BOL you're opening is in the same directory as the bmd course model! If you just extracted the arc and did nothing else, it should be structured fine to work with the editor.

# What's supported
DouBOL Dash can parse every single section in the file except for the last one (that's unknown). It can also render objects, and checkpoints.

# Credits
Thanks to StapleButter for the main BMD rendering code, and miluaces for minor changes. Icon was made by Kaio.