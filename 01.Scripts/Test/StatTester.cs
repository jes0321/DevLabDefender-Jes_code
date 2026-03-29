using Code.Entities;
using UnityEngine;
using Works.JES._01.Scripts.Core.StatSystem;

namespace Code.Test
{
    public class StatTester : MonoBehaviour
    {
        [SerializeField] private EntityStat statCompo;

        [SerializeField] private StatSo targetStat;
        [SerializeField] private float modifyValue;

        [ContextMenu("Test Execute")]
        private void TestExecute()
        {
            statCompo.GetStat(targetStat).AddModifier(this, modifyValue);
        }

        [ContextMenu("Test Rollback")]
        private void TestRollback()
        {
            statCompo.GetStat(targetStat).RemoveModifier(this);
        }

        [ContextMenu("Print Stat")]
        private void PrintStatValue()
        {
            Debug.Log(statCompo.GetStat(targetStat).Value);
        }
    }
}