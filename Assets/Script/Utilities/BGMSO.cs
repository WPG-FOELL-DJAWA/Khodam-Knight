using UnityEngine;
using System.Collections.Generic;

public enum BGMName
{
    MainMenu1,
    MainMenu2,
    FaunvilleVillage,
    TurnBase,
    Combat1

}

[CreateAssetMenu(fileName = "BGMSO", menuName = "Bhekti/BGMSO")]
public class BGMSO : ScriptableObject
{
    [System.Serializable]
    public struct BGM
    {
        public BGMName name;
        public AudioClip clip;

        [Range(0f, 1f)]
        public float volume;

        [Range(.1f, 3f)]
        public float pitch;
    }

    public List<BGM> ClipList;
}