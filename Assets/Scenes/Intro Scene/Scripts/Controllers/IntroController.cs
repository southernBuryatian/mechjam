using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IntroController : MonoBehaviour
{
    public StoryScene currentScene;
    public BottomBarContorller bottomBar;
    public BackgroundController backgroundController;

    void Start()
    {
        bottomBar.PlayScene(currentScene);
        backgroundController.SetImage(currentScene.background);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (bottomBar.IsCompleted())
            {
                if (bottomBar.IsLastSentence())
                {
                    currentScene = currentScene.nextScene;
                    if (currentScene == null)
                    {
                        UnityEngine.SceneManagement.SceneManager.LoadScene("PipesScene");
                    }
                    bottomBar.PlayScene(currentScene);
                    backgroundController.SwitchImage(currentScene.background);
                }
                else
                {
                    bottomBar.PlayNextSentence();
                }
            }
        }
    }
}
