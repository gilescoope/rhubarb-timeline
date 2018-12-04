using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace FriendlyMonster.RhubarbTimeline
{
    [Serializable]
    public class RhubarbPlayableClip : PlayableAsset, ITimelineClipAsset
    {
        public override double duration
        {
            get { return 0.5f; }
        }

        public RhubarbPlayableBehaviour template = new RhubarbPlayableBehaviour();

        public ClipCaps clipCaps
        {
            get { return ClipCaps.Blending; }
        }

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<RhubarbPlayableBehaviour>.Create(graph, template);
            return playable;
        }
    }
}

