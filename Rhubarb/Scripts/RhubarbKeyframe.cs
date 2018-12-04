namespace FriendlyMonster.RhubarbTimeline
{
    public class RhubarbKeyframe
    {
        public int frame;
        public MouthShape phoneme;

        public bool Deserialize(string text)
        {
            string[] parts = text.Split("\t"[0]);
            if (parts.Length == 2)
            {
                frame = Rhubarb.StringToFrame(parts[0]);
                phoneme = Rhubarb.GetPhonemeType(parts[1]);
                return true;
            }

            return false;
        }

        public string Serialize()
        {
            return Rhubarb.FrameToString(frame) + "\t" + Rhubarb.GetPhonemeName(phoneme);
        }
    }
}