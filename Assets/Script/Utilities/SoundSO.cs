using UnityEngine;
using System.Collections.Generic;

public enum SoundName
{
    SwingSword1
}

[CreateAssetMenu(fileName = "SoundSO", menuName = "Bhekti/SoundSO")]
public class SoundSO : ScriptableObject
{
    [System.Serializable]
    public struct Sound
    {
        public SoundName name;
        public AudioClip clip;

        [Range(0f, 1f)]
        public float volume;

        [Range(.1f, 3f)]
        public float pitch;
    }

    public List<Sound> ClipList;
}