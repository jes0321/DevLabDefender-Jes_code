using System;
using UnityEngine;
using Works.JES._01.Scripts.Core.StatSystem;

namespace Works.JES._01.Scripts.Towers
{
    [Serializable]
    public struct StatUpgrade
    {
        public StatSo targetStat;
        public float value;
    }
    [CreateAssetMenu(menuName = "SO/Tower/Upgrade")]
    public class TowerUpgradeSO : ScriptableObject
    {
        public Mesh changeMesh;
        public int level;
        public StatUpgrade[] upgradeStats;
        public int upgradeCost;
    }
}