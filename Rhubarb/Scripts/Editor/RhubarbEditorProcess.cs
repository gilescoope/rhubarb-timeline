using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

namespace FriendlyMonster.RhubarbTimeline
{
    public static class RhubarbEditorProcess
    {
        public static bool IsValid(string rhubarbPath)
        {
#if UNITY_EDITOR_WIN
            return rhubarbPath.EndsWith("rhubarb.exe");
#endif

#if UNITY_EDITOR_OSX
        return rhubarbPath.EndsWith("rhubarb");
#endif
        }

        public static RhubarbTrack Auto(string rhubarbPath, string audioPath, string dialog, bool _isG = true, bool _isH = true, bool _isX = true)
        {
            rhubarbPath = rhubarbPath.Replace("/", "\\");
            audioPath = audioPath.Replace("/", "\\");
            string dialogPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets/dialog.txt").Replace("/", "\\");

            RhubarbTrack rhubarbTrack = ScriptableObject.CreateInstance<RhubarbTrack>();
            rhubarbTrack.keyframes = new List<RhubarbKeyframe>();

            bool isDialog = !string.IsNullOrEmpty(dialog);
            string extendedMouthShapesArgument = ExtendedMouthShapeArgument(_isG, _isH, _isX);

            if (isDialog)
            {
                File.WriteAllText(dialogPath, dialog);
            }

            Process process = new Process();

#if UNITY_EDITOR_WIN
            process.StartInfo.FileName = rhubarbPath;
            string command = "";
            command += "\"" + audioPath + "\" ";
            command += isDialog ? "--dialogFile \"" + dialogPath + "\" " : "";
            command += "--extendedShapes " + extendedMouthShapesArgument;
            process.StartInfo.Arguments = command;
#endif

#if UNITY_EDITOR_OSX
        process.StartInfo.FileName = "/bin/bash";
        string command = rhubarbPath.Replace(Application.dataPath, "Assets") + "/Release/rhubarb ";
        command += isDialog ? "--dialogFile " + dialogPath.Replace(" ", "\\ ") + " " : "";
        command += isOutput ? "--output " + outputPath.Replace(" ", "\\ ") + " " : "";
        command += audioPath.Replace(" ", "\\ ");
        command += "--extendedShapes " + extendedMouthShapesArgument + " ";
        process.StartInfo.Arguments = "-c \" " + command + " \"";
#endif

            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();

            while (!process.StandardOutput.EndOfStream)
            {
                string line = process.StandardOutput.ReadLine();
                RhubarbKeyframe keyframe = new RhubarbKeyframe();
                keyframe.Deserialize(line);
                rhubarbTrack.keyframes.Add(keyframe);
            }

            if (isDialog)
            {
                File.Delete(dialogPath);
            }

            return rhubarbTrack;
        }

        private static string ExtendedMouthShapeArgument(bool _isG, bool _isH, bool _isX)
        {
            string arg = "";
            if (_isG)
            {
                arg += "G";
            }

            if (_isH)
            {
                arg += "H";
            }

            if (_isX)
            {
                arg += "X";
            }

            return arg.Length > 0 ? arg : "\"\"";
        }
    }
}