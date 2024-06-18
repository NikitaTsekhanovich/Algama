using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public TMPro.TMP_Dropdown Dropdown;
    private List<Resolution> Resolutions;
    public Toggle Toggle;

    // Start is called before the first frame update
    void Start()
    {
        Toggle.isOn = Screen.fullScreen;
        Resolutions = Screen.resolutions.Distinct().ToList();
        Resolutions.Reverse();
        var res = Resolutions.Select(resolution => resolution.ToString()).ToList();
        Dropdown.ClearOptions();
        Dropdown.AddOptions(res);
        Screen.SetResolution(Resolutions.First().width, Resolutions.First().height, Screen.fullScreen);
    }

    public void SetResolution()
    {
        Screen.SetResolution(Resolutions[Dropdown.value].width, Resolutions[Dropdown.value].height, Screen.fullScreen);
    }

    public void ChangeScreenMode()
    {
        Screen.fullScreen = Toggle.isOn;
    }
}