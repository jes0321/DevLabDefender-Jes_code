using System;
using UnityEngine;

namespace Works.JES._01.Scripts.Towers
{
    [CreateAssetMenu(fileName = "TowerInfoSO", menuName = "SO/Tower/Info")]
    public class TowerInfoSO : ScriptableObject,ICloneable
    {
        public int level=0;
        public int Level
        {
            get=> level;
            set
            {
                level = Mathf.Clamp(value, 0, maxLevel);
            }
        }

        public BuildingSO buildingSo;
        public int maxLevel=3;
        public string towerName;
       
        public bool IsMaxLevel => level >= maxLevel;
        public TowerUpgradeSO[] upgradeSoList;
        public object Clone()=>Instantiate(this);

        public int GetCurrentSellPrice()
        {
            int result=buildingSo.BuildingCost;
            for(int i=Level;i>0;i--)
            {
                result += upgradeSoList[i].upgradeCost;
            }
            return result/3;
        }
        public bool GetUpgradeCost(out int cost)
        {
            cost = 0;
            if (IsMaxLevel) return false;
            cost = upgradeSoList[level+1].upgradeCost;
            return true;
        }
    }
}
