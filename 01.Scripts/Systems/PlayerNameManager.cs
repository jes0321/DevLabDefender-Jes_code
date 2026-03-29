using UnityEngine;

public class PlayerNameManager : MonoBehaviour
{
    [SerializeField] private GameObject inputNameUI;

    public bool IsOpen => inputNameUI.activeSelf;

    private readonly string saveKey = "PlayerName";

    private string nickName;
    private void OnEnable()
    {
        nickName = PlayerPrefs.GetString(saveKey);
        if (string.IsNullOrEmpty(nickName))
        {
            inputNameUI.SetActive(true);
        }
    }
}
