using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField NameInputField;
    public TMP_Text BestScoreText;

    private void Start()
    {
        if (BestScoreText != null && DataManager.Instance != null)
        {
            var bestName = DataManager.Instance.BestScorePlayerName;
            var bestScore = DataManager.Instance.BestScore;
            if (!string.IsNullOrEmpty(bestName))
                BestScoreText.text = $"Best Score : {bestName} : {bestScore}";
            else
                BestScoreText.text = "Best Score : 0";
        }
    }

    public void OnStartClicked()
    {
        if (NameInputField != null && DataManager.Instance != null)
        {
            DataManager.Instance.SetPlayerName(NameInputField.text);
        }
        SceneManager.LoadScene("main");
    }

    public void OnQuitClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}


