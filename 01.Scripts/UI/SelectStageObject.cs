using TMPro;
using Unity.Cinemachine;
using UnityEngine;

namespace Works.JES._01.Scripts.UI
{
    public enum MenuType
    {
        Stage,Infinity,Gacha,Upgrade, Rank
    }
    public class SelectStageObject : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera lookCam;
        [SerializeField] private string desc;
        public MenuType menuType;

        public int CameraPriority
        {
            get => lookCam.Priority;
            set => lookCam.Priority = value;
        }

        public string Desc () => desc;
    }
}