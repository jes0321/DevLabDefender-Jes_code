using System;
using UnityEngine;

namespace Works.JES._01.Scripts.Systems
{
    [DefaultExecutionOrder(-100)]
    [CreateAssetMenu(fileName = "StageManager", menuName = "SO/System/Stage", order = 0)]
    public class StageManagerSO : ScriptableObject
    {
        public int currentStage = -1;
        private string saveKey = "StageManager";

        private void OnEnable()
        {
            currentStage = PlayerPrefs.GetInt(saveKey, -1);
        }

        private void OnDisable()
        {
            PlayerPrefs.SetInt(saveKey, currentStage);
        }
        public bool IsComplete(int stageNum) => stageNum <= currentStage;
    }
}