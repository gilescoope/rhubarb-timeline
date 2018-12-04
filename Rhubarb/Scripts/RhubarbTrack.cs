using System;
using System.Collections.Generic;
using UnityEngine;

namespace FriendlyMonster.RhubarbTimeline
{
    public class RhubarbTrack : ScriptableObject
    {
        public List<RhubarbKeyframe> keyframes;

        public void Deserialize(string text)
        {
            keyframes = new List<RhubarbKeyframe>();
            string[] lines = text.Split(new string[] {"\r\n", "\n"}, StringSplitOptions.None);
            for (int i = 0; i < lines.Length; i++)
            {
                RhubarbKeyframe keyframe = new RhubarbKeyframe();
                if (keyframe.Deserialize(lines[i]))
                {
                    keyframes.Add(keyframe);
                }
            }
        }

        public MouthShape GetActiveKeyframe(float time)
        {
            float frame = time / Rhubarb.frameTime;
            if (frame < keyframes[0].frame)
            {
                return keyframes[0].phoneme;
            }

            int lower = 0;
            int upper = keyframes.Count - 1;

            if (frame > keyframes[upper].frame)
            {
                return keyframes[upper].phoneme;
            }

            while (upper - lower > 1)
            {
                int next = lower + (upper - lower) / 2;
                if (keyframes[next].frame < frame)
                {
                    lower = next;
                } else
                {
                    upper = next;
                }
            }

            return keyframes[lower].phoneme;
        }
    }
}