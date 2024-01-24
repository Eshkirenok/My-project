using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
private bool isPaused = false;
public GameObject pauseMenu;
public GameObject settingsMenu;
// Update is called once per frame
void Update()
{
if (Input.GetKeyDown(KeyCode.Escape))
{
if (isPaused)
{
Resume();
}
else
{
Pause();
}
}
}

public void Pause()
{
pauseMenu.SetActive(true);
Time.timeScale = 0f;
isPaused = true;
}

public void Resume()
{
pauseMenu.SetActive(false);
Time.timeScale = 1f;
isPaused = false;
}

public void OpenSettings()
{
 SceneManager.LoadScene(1);
}

public void CloseSettings()
{
settingsMenu.SetActive(false);
}

public void QuitToMainMenu()
{
Time.timeScale = 1f;
SceneManager.LoadScene(0);
}
}