using Tild.Upgrade;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Works.JES._01.Scripts.UI
{
    public class ClearUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI pointText;
        [SerializeField] private CurrentSO currentSO;
        [SerializeField] private int randomNum=1000;
        public void GiveMoney()
        {
            int num = Random.Range(randomNum - 100, randomNum+100);
            currentSO.playerCurrency += num;
            pointText.text = $"+{num}";
        }
        public void ExitBtn()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("LobbyScene");
        }

        public void StageBtn()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("StageScene");
        }
    }
}