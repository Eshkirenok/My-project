using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
[SerializeField] private Dropdown resolutions; // разрешение экрана
[SerializeField] private Button applyButton; // кнопка применения настроек и их сохранения
[SerializeField] private Toggle fullscreenToggle; // вкл/выкл полноэкранный режим
private Resolution[] resolutionsList;

void Awake()
{
resolutionsList = Screen.resolutions;
Load();
BuildMenu();
}

void Load()
{
fullscreenToggle.isOn = Screen.fullScreen;
}

void Save()
{
PlayerPrefs.Save();
}

string ResToString(Resolution res)
{
return res.width + " x " + res.height;
}

void RefreshDropdown()
{
resolutions.RefreshShownValue();
}

void BuildMenu()
{
resolutions.options = new List<Dropdown.OptionData>();
for(int i = 0; i < resolutionsList.Length; i++)
{
Dropdown.OptionData option = new Dropdown.OptionData();
option.text = ResToString(resolutionsList[i]);
resolutions.options.Add(option);
if(resolutionsList[i].height == Screen.height && resolutionsList[i].width == Screen.width) resolutions.value = i;
}
applyButton.GetComponent<Button>().onClick.AddListener(()=>{ApplySettings();});
}

void ApplySettings()
{
Screen.SetResolution(resolutionsList[resolutions.value].width, resolutionsList[resolutions.value].height, fullscreenToggle.isOn);
Save();
}

public void Back()
    {
        SceneManager.LoadScene(0);
    }



}
