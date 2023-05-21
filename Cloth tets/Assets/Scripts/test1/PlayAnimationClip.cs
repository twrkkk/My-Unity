using UnityEngine;

namespace ScriptableObjectExample
{
    [CreateAssetMenuAttribute(fileName = "New Play Animation Clip", menuName = "Example/PlayAnimationClip")]
    public class PlayAnimationClip : TimeLineClip
    {
        public AnimationClip clip;
        public ExposedReference<GameObject> target;
        public ExposedReference<MoveTarget> moveTarget;

        private void OnEnable()
        {
        }
    }
}