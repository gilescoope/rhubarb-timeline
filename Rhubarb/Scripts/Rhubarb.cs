using System;
using System.Globalization;
using UnityEngine;

namespace FriendlyMonster.RhubarbTimeline
{
    public static class Rhubarb
    {
        public const float frameTime = 0.01f;

        public static int TimeToFrame(float time)
        {
            return Mathf.RoundToInt(time / frameTime);
        }

        public static float FrameToTime(int frame)
        {
            return frame * frameTime;
        }

        public static int StringToFrame(string time)
        {
            return (int) (float.Parse(time, CultureInfo.InvariantCulture) / frameTime);
        }

        public static string FrameToString(int frame)
        {
            string time = frame.ToString("000");
            return time.Insert(time.Length - 2, ".");
        }

        public static MouthShape GetPhonemeType(string id)
        {
            MouthShape mouthShape;
            Enum.TryParse(id, out mouthShape);
            return mouthShape;
        }

        public static string GetPhonemeName(MouthShape mouthShape)
        {
            return mouthShape.ToString();
        }

        public static MouthShape GetDefaultPhoneme()
        {
            return MouthShape.X;
        }
    }
}