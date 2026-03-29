using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Works.JES._01.Scripts.UI.SettingUIs
{
    [Serializable]
    public struct SoundValues
    {
        public float master,music,sfx;
    }
    public class SoundSettingUI : MonoBehaviour
    {
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private Slider masterSlider,musicSlider,sfxSlider;
        private string jsonKey = "SoundValues";
        private void Awake()
        {
            masterSlider.onValueChanged.AddListener(HandleMasterSliderChange);
            musicSlider.onValueChanged.AddListener(HandleMusicSliderChange);
            sfxSlider.onValueChanged.AddListener(HandleSfxSliderChange);
        }

        private void Start()
        {
            LoadSaveData();
        }

        private void HandleSfxSliderChange(float value)=>
            SetSliderValue("SFX", value);

        private void HandleMusicSliderChange(float value)=>
            SetSliderValue("BGM", value);

        private void HandleMasterSliderChange(float value)=>
            SetSliderValue("Master", value);

        private void SetSliderValue(string key, float value)=>
            audioMixer.SetFloat(key, Mathf.Log10(value) * 20);
        private void OnDestroy()
        {
            SaveData();
        }

        private void LoadSaveData()
        {
            string jsonData = PlayerPrefs.GetString(jsonKey);
            
            if (string.IsNullOrEmpty(jsonData)) return;
            
            SoundValues soundValues = JsonUtility.FromJson<SoundValues>(jsonData);
            
            masterSlider.value = soundValues.master;
            HandleMasterSliderChange(soundValues.master);
            
            musicSlider.value = soundValues.music;
            HandleMusicSliderChange(soundValues.music);
            
            sfxSlider.value = soundValues.sfx;
            HandleSfxSliderChange(soundValues.sfx);
        }

        private void SaveData()
        {
            SoundValues soundValues = new SoundValues
            {
                master = masterSlider.value,
                music = musicSlider.value,
                sfx = sfxSlider.value
            };
            
            string jsonData = JsonUtility.ToJson(soundValues);
            PlayerPrefs.SetString(jsonKey, jsonData);
        }
        
        #if UNITY_EDITOR
        [ContextMenu("ClearData")]
        private void ClearData()
        {
            PlayerPrefs.DeleteKey(jsonKey);
        }
        #endif
    }
}