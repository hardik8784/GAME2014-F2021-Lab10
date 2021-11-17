using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [Header("On Screen Controls")]
    public GameObject OnScreenControls;

    [Header("Button Control Events")]
    public static bool JumpButtonDown;
    // Start is called before the first frame update
    void Start()
    {
        CheckPlatform();
    }

    private void CheckPlatform()
    {
        switch(Application.platform)
        {
            case RuntimePlatform.Android:
            case RuntimePlatform.IPhonePlayer:
            case RuntimePlatform.WindowsEditor:
                OnScreenControls.SetActive(true);
                break;
            default:
                OnScreenControls.SetActive(false);
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnJumpButton_Down()
    {
        JumpButtonDown = true;
    }

    public void OnJumpButton_Up()
    {
        JumpButtonDown = false;
    }
}
