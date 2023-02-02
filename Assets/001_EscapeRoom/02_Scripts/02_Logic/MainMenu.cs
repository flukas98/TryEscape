using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  #region Singleton
  public static MainMenu Instance { get; private set; }

  private void Awake()
  {
    // If there is an instance, and it's not me, delete myself.
    if (Instance != null && Instance != this)
      Destroy(this);
    else
      Instance = this;
  }
  #endregion

  public void Exit()
  {
    Debug.Log("Quitting application");
    Application.Quit();
  }

  public void LoadMainMenu()
  {
    SceneManager.LoadScene("MainMenu");
  }

  public void PlayRoom1()
  {
    SceneManager.LoadScene("Room1");
  }

  public void PlayRoom2()
  {
    SceneManager.LoadScene("Room2");
  }

  public void PlayRoom3()
  {
    SceneManager.LoadScene("Room3");
  }

  public void PlayRoom4()
  {
    SceneManager.LoadScene("Room4");
  }
}
