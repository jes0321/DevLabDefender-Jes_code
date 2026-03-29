using System;
using UnityEngine;
using UnityEngine.UI;

namespace Works.JES._01.Scripts.Sounds
{
    public class BtnClickSound : MonoBehaviour
    {
        [HideInInspector]
        public Button button;
        public void Init()
        {
            button = GetComponent<Button>();
        }
    }
}