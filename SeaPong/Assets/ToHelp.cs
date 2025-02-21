using UnityEngine;
using UnityEngine.SceneManagement;
public class ToHelpScreen : MonoBehaviour
{
    public void HelpScreen() => SceneManager.LoadSceneAsync("Help");
}
