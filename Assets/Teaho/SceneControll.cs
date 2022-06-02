using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneControll : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("mainScene");
    }
    public void RestartButton()
    {
        SceneManager.LoadScene("mainScene");
    }
    public void LobbyButton()
    {
        SceneManager.LoadScene("startScene");
    }
}
