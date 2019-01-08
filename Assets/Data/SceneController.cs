using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    //public void GotoMainScene()
    //{
    //    SceneManager.LoadScene("main");
    //}

    //public void GotoMenuScene()
    //{
    //    SceneManager.LoadScene("menu");
    //}

    public void GotoPreviewScene()
    {
        SceneManager.LoadScene("_previewScene");
    }
    //public void GotoPreviewSceneNoUI()
    //{
    //    SceneManager.LoadScene("_previewSceneno_UI");
    //}
}