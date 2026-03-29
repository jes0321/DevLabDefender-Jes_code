using System;
using System.Collections.Generic;
using Tild.Upgrade;
using UnityEngine;
using Works.JES._01.Scripts.Towers;

namespace Works.JES._01.Scripts.Buildings
{
    [Serializable]
    public struct TierBuild
    {
        public TowerTierSO tierSo;
        public BuildingSO buildingSo;
        public Tower[] towers;
    }
    [CreateAssetMenu(fileName = "BuildingSoList", menuName = "SO/Build/List", order = 0)]
    public class BuildingSoList : ScriptableObject 
    {
        public TierBuild[] tierBuilds;

        public List<BuildingSO> GetBuildingSOs()
        {
            List<BuildingSO> builds = new List<BuildingSO>();
            foreach (TierBuild tierBuild in tierBuilds)
            {
                Tower tower = tierBuild.towers[tierBuild.tierSo.towerUpgraded];
                BuildingSO buildSo = tierBuild.buildingSo;
                buildSo.buildingPrefab = tower.gameObject;
                builds.Add(buildSo);
            }
            return builds;
        }
    }
}