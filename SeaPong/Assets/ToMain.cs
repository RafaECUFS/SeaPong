using UnityEngine;
using UnityEngine.SceneManagement;
public class ToMain : MonoBehaviour
{
    public void ToMainMenu() => SceneManager.LoadSceneAsync("Main_Menu 1");
}
