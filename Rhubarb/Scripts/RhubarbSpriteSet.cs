using UnityEngine;
#pragma warning disable 0649

namespace FriendlyMonster.RhubarbTimeline
{
    [CreateAssetMenu(menuName = "Rhubarb/Sprite Set")]
    public class RhubarbSpriteSet : ScriptableObject
    {
        [SerializeField] private Sprite A;
        [SerializeField] private Sprite B;
        [SerializeField] private Sprite C;
        [SerializeField] private Sprite D;
        [SerializeField] private Sprite E;
        [SerializeField] private Sprite F;
        [SerializeField] private Sprite G;
        [SerializeField] private Sprite H;
        [SerializeField] private Sprite X;

        public Sprite GetSprite(MouthShape mouthShape)
        {
            switch (mouthShape)
            {
                case MouthShape.A:
                    return A;
                case MouthShape.B:
                    return B;
                case MouthShape.C:
                    return C;
                case MouthShape.D:
                    return D;
                case MouthShape.E:
                    return E;
                case MouthShape.F:
                    return F;
                case MouthShape.G:
                    return G;
                case MouthShape.H:
                    return H;
                case MouthShape.X:
                    return X;
            }

            return null;
        }
    }
}