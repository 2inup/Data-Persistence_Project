using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using TMPro;
using Unity.Android.Gradle.Manifest;






public class MenuUIHandler : MonoBehaviour
{



    public TMP_InputField NameInputField;
    public TMP_Text BestScoreText;

    private void Start()
    {
        if (BestScoreText != null && DataManager.Instance != null)
        {
            var bestName = DataManager.Instance.BestUserName;
            var bestScore = DataManager.Instance.BestScore;
            if (!string.IsNullOrEmpty(bestName))
                BestScoreText.text = $"Best Score : {bestName} : {bestScore}";
            else
                BestScoreText.text = $"Best Score : 0";
        }
    }



    public void StartNew()
    {

        if (NameInputField != null && DataManager.Instance != null)
        {
            DataManager.Instance.SetCurrentPlayerName(NameInputField.text);
        }

        SceneManager.LoadScene(1);
    }


    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

}
