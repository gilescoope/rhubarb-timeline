using FriendlyMonster.RhubarbTimeline;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
    public class RhubarbPlayabeMixerBehaviour : PlayableBehaviour
    {
        MouthShape m_DefaultMouthShape = MouthShape.X;
        MouthShape m_AssignedMouthShape;

        RhubarbSprite m_TrackBinding;

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            m_TrackBinding = playerData as RhubarbSprite;

            if (m_TrackBinding == null)
                return;

            int inputCount = playable.GetInputCount();

            float totalWeight = 0f;
            float greatestWeight = 0f;
            int currentInputs = 0;

            for (int i = 0; i < inputCount; i++)
            {
                float inputWeight = playable.GetInputWeight(i);
                ScriptPlayable<RhubarbPlayableBehaviour> inputPlayable = (ScriptPlayable<RhubarbPlayableBehaviour>) playable.GetInput(i);
                RhubarbPlayableBehaviour input = inputPlayable.GetBehaviour();

                totalWeight += inputWeight;

                if (inputWeight > greatestWeight)
                {
                    m_AssignedMouthShape = input.MouthShape;
                    m_TrackBinding.MouthShape = m_AssignedMouthShape;
                    greatestWeight = inputWeight;
                }

                if (!Mathf.Approximately(inputWeight, 0f))
                    currentInputs++;
            }

            if (currentInputs != 1 && 1f - totalWeight > greatestWeight)
            {
                m_TrackBinding.MouthShape = m_DefaultMouthShape;
            }
        }
    }
}
