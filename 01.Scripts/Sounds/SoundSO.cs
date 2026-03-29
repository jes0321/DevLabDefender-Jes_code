using UnityEngine;
using UnityEngine.Audio;

namespace Works.JES._01.Scripts.Sounds
{
    public enum OutputType
    {
        BGM,SFX
    }
    [CreateAssetMenu(fileName = "SoundSo", menuName = "SO/Sound", order = 0)]
    public class SoundSO : ScriptableObject
    {
        public AudioResource audioResource;
        public OutputType outputType;
        
        [Range(0,1f)]
        public float volume=1f;

        public bool isRandomPitch=false;
        [Range(0.1f, 1f)] 
        public float randomPitch;
        public float GetRandomPitch()=>isRandomPitch ? Random.Range(0, randomPitch) : 0f;

        public bool isLoop = false;

        public bool is3D = false;

        public bool isTime=false;
        public float playTime;
    }
}