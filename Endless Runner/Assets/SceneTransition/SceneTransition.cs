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

    public static SceneTransition instance;
    private static bool _shouldPlayOpeningAnim = false;

    private Animator _animator;
    private AsyncOperation loadingSceneOperation;

    void Start()
    {
        instance = this;
        _animator = GetComponent<Animator>();
        if (_shouldPlayOpeningAnim)
        {
            instance._animator.SetTrigger("Open");
            instance._progressBar.fillAmount = 1;
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
        instance._animator.SetTrigger("Close");
        instance.loadingSceneOperation = SceneManager.LoadSceneAsync(index);
        instance.loadingSceneOperation.allowSceneActivation = false;
        instance._progressBar.fillAmount = 0;
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

//        // Чтобы сцена не начала переключаться пока играет анимация closing:
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

//            // Чтобы если следующий переход будет обычным SceneManager.LoadScene, не проигрывать анимацию opening:
//            shouldPlayOpeningAnimation = false;
//        }
//    }

//    private void Update()
//    {
//        if (loadingSceneOperation != null)
//        {
//            LoadingPercentage.text = Mathf.RoundToInt(loadingSceneOperation.progress * 100) + "%";

//            // Просто присвоить прогресс:
//            //LoadingProgressBar.fillAmount = loadingSceneOperation.progress; 

//            // Присвоить прогресс с быстрой анимацией, чтобы ощущалось плавнее:
//            LoadingProgressBar.fillAmount = Mathf.Lerp(LoadingProgressBar.fillAmount, loadingSceneOperation.progress,
//                Time.deltaTime * 5);
//        }
//    }

//    public void OnAnimationOver()
//    {
//        // Чтобы при открытии сцены, куда мы переключаемся, проигралась анимация opening:
//        shouldPlayOpeningAnimation = true;

//        loadingSceneOperation.allowSceneActivation = true;
//    }
//}