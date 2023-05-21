using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{

    [SerializeField] private bool unlocked;//Default value is false;
    public Image unlockImage;
    public GameObject[] stars;

    public Sprite starSprite;

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        UpdateLevelStatus();//TODO MOve this method later
        UpdateLevelImage();//TODO MOve this method later
    }

    private void UpdateLevelStatus()
    {
        //if the current lv is 5, the pre should be 4
        int LevelNum = int.Parse(gameObject.name);
        int previousLevelNum;
        if (LevelNum > 1) previousLevelNum = LevelNum - 1; else previousLevelNum = 1;
        if (PlayerPrefs.GetInt("Lv" + previousLevelNum.ToString()) > 0)//If the firts level star is bigger than 0, second level can play
        {
            unlocked = true;
        }
    }

    private void UpdateLevelImage()
    {
        if(!unlocked)//MARKER if unclock is false means This level is clocked!
        {
            unlockImage.gameObject.SetActive(true);
            for(int i = 0; i < stars.Length; i++)
            {
                stars[i].gameObject.SetActive(false);
            }
        }
        else//if unlock is true means This level can play !
        {
            unlockImage.gameObject.SetActive(false);
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i].gameObject.SetActive(true);
            }

            for(int i = 0; i < PlayerPrefs.GetInt("Lv" + gameObject.name); i++)
            {
                stars[i].gameObject.GetComponent<Image>().sprite = starSprite;
            }
        }
    }

    public void PressSelection(string _LevelName)//When we press this level, we can move to the corresponding Scene to play
    {
        if(unlocked)
        {
            SceneTransition.SceneToSwitch(_LevelName);
        }
    }

}
