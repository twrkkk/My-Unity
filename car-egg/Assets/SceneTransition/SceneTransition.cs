using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private TMP_Text _percentText;
    [SerializeField] private Image _progressBar;

    private static SceneTransition _instance;
    private static bool _shouldPlayOpeningAnim = false;

    private Animator _animator;
    private AsyncOperation loadingSceneOperation;

    void Start()
    {
        _instance = this;
        _animator = GetComponent<Animator>();
        if (_shouldPlayOpeningAnim)
        {
            _instance._animator.SetTrigger("Open");
            _instance._progressBar.fillAmount = 1;
            _shouldPlayOpeningAnim = false;
        }
    }


    private void Update()
    {
        if (loadingSceneOperation != null)
        {
            _percentText.text = Mathf.RoundToInt((loadingSceneOperation.progress * 100)) + "%";
            _progressBar.fillAmount = Mathf.Lerp(_progressBar.fillAmount, loadingSceneOperation.progress, Time.deltaTime * 5);
        }
    }

    public static void SceneToSwitch(int index)
    {
        _instance._animator.SetTrigger("Close");
        _instance.loadingSceneOperation = SceneManager.LoadSceneAsync(index);
        _instance.loadingSceneOperation.allowSceneActivation = false;
        _instance._progressBar.fillAmount = 0;
    }

    public static void SceneToSwitch(string name)
    {
        _instance._animator.SetTrigger("Close");
        _instance.loadingSceneOperation = SceneManager.LoadSceneAsync(name);
        _instance.loadingSceneOperation.allowSceneActivation = false;
        _instance._progressBar.fillAmount = 0;
    }

    public void OnAnimationOver()
    {
        _shouldPlayOpeningAnim = true;
        loadingSceneOperation.allowSceneActivation = true;
    }
}

//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;

//public class SceneTransition : MonoBehaviour
//{
//    public Text LoadingPercentage;
//    public Image LoadingProgressBar;

//    private static SceneTransition instance;
//    private static bool shouldPlayOpeningAnimation = false;

//    private Animator componentAnimator;
//    private AsyncOperation loadingSceneOperation;

//    public static void SwitchToScene(string sceneName)
//    {
//        instance.componentAnimator.SetTrigger("sceneClosing");

//        instance.loadingSceneOperation = SceneManager.LoadSceneAsync(sceneName);

//        // ����� ����� �� ������ ������������� ���� ������ �������� closing:
//        instance.loadingSceneOperation.allowSceneActivation = false;

//        instance.LoadingProgressBar.fillAmount = 0;
//    }

//    private void Start()
//    {
//        instance = this;

//        componentAnimator = GetComponent<Animator>();

//        if (shouldPlayOpeningAnimation)
//        {
//            componentAnimator.SetTrigger("sceneOpening");
//            instance.LoadingProgressBar.fillAmount = 1;

//            // ����� ���� ��������� ������� ����� ������� SceneManager.LoadScene, �� ����������� �������� opening:
//            shouldPlayOpeningAnimation = false;
//        }
//    }

//    private void Update()
//    {
//        if (loadingSceneOperation != null)
//        {
//            LoadingPercentage.text = Mathf.RoundToInt(loadingSceneOperation.progress * 100) + "%";

//            // ������ ��������� ��������:
//            //LoadingProgressBar.fillAmount = loadingSceneOperation.progress; 

//            // ��������� �������� � ������� ���������, ����� ��������� �������:
//            LoadingProgressBar.fillAmount = Mathf.Lerp(LoadingProgressBar.fillAmount, loadingSceneOperation.progress,
//                Time.deltaTime * 5);
//        }
//    }

//    public void OnAnimationOver()
//    {
//        // ����� ��� �������� �����, ���� �� �������������, ����������� �������� opening:
//        shouldPlayOpeningAnimation = true;

//        loadingSceneOperation.allowSceneActivation = true;
//    }
//}