using FriendlyMonster.RhubarbTimeline;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
    [TrackColor(0.855f, 0.8623f, 0.87f)]
    [TrackClipType(typeof(RhubarbPlayableClip))]
    [TrackBindingType(typeof(RhubarbSprite))]
    public class RhubarbPlayableTrack : TrackAsset
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            foreach (var c in GetClips())
            {
                RhubarbPlayableClip clip = (RhubarbPlayableClip) c.asset;
                c.displayName = clip.template.MouthShape.ToString();
            }

            return ScriptPlayable<RhubarbPlayabeMixerBehaviour>.Create(graph, inputCount);
        }

        // Please note this assumes only one component of type MouthShapeSetter on the same gameobject.
        public override void GatherProperties(PlayableDirector director, IPropertyCollector driver)
        {
#if UNITY_EDITOR
            RhubarbSprite trackBinding = director.GetGenericBinding(this) as RhubarbSprite;
            if (trackBinding == null)
                return;

            // These field names are procedurally generated estimations based on the associated property names.
            // If any of the names are incorrect you will get a DrivenPropertyManager error saying it has failed to register the name.
            // In this case you will need to find the correct backing field name.
            // The suggested way of finding the field name is to:
            // 1. Make sure your scene is serialized to text.
            // 2. Search the text for the track binding component type.
            // 3. Look through the field names until you see one that looks correct.
            //driver.AddFromName<RhubarbSprite>(trackBinding.gameObject, "m_MouthShape");
#endif
            base.GatherProperties(director, driver);
        }
    }
}