using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EasyUI.Toast;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] AllPanels;

    [SerializeField]
    Image MainLoadigSlider, PlayLoadingSlider, TimerSlider;

    bool PlayLoading, RandomOperator, Timer;
    float Speed;
    int SelectedOperator, Answer;

    [SerializeField]
    Text FirstValue, OperatorValue, SecondValue;

    [SerializeField]
    List<int> RandomValues = new List<int>();

    [SerializeField]
    List<int> RandomOptionButtonPosition = new List<int>();

    [SerializeField]
    Button[] AllAnswerButtons;

    [SerializeField]
    Button MusicButton, SoundButton;

    [SerializeField]
    Sprite MusicOnSprite, MusicOffSprite, SoundOnSprite, SoundOffSprite;

    [SerializeField]
    AudioSource MusicSource, SoundSource;

    [SerializeField]
    AudioClip[] AllSoundClips;

    [SerializeField]
    bool SoundAction, MusicAction;


    void Update()
    {

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (AllPanels[5].activeInHierarchy)
            {
                //AllPanels[5].SetActive(false);
                SettingsCloseButtonClickAction();
            }
            else if (AllPanels[9].activeInHierarchy)
            {
                //AllPanels[9].SetActive(false);
                MainMenuCloseButtonClickAction();
            }
            else if (AllPanels[3].activeInHierarchy)
            {
                //AllPanels[3].SetActive(false);
                SelectionScreenBackButtonClickAction();
            }
            else if (AllPanels[4].activeInHierarchy)
            {
                //AllPanels[4].SetActive(false);
                if (AllPanels[6].activeInHierarchy)
                {
                    EasyUI.Toast.Toast.Show("First Close GameOver Screen", 2f, ToastColor.Black);
                    Debug.Log("First Close GameOver Panel.");

                }
                else if (AllPanels[7].activeInHierarchy)
                {
                    EasyUI.Toast.Toast.Show("First Close Success Screen", 2f, ToastColor.Black);
                    Debug.Log("First Close Success Panel.");
                }
                else if (AllPanels[8].activeInHierarchy)
                {
                    EasyUI.Toast.Toast.Show("First Close TimeOver Screen", 2f, ToastColor.Black);
                    Debug.Log("First Close Time Over Panel.");
                }
                else
                {
                    GameScreenBackButtonClickAction();
                }
            }
            else if (AllPanels[10].activeInHierarchy)
            {
                AllPanels[10].SetActive(false);
            }
            else
            {
                AllPanels[10].SetActive(true);
            }
        }




        if (MainLoadigSlider.fillAmount < 1)
        {
            MainLoadigSlider.fillAmount += 0.5f * Time.deltaTime;
        }
        else
        {
            AllPanels[0].SetActive(false);
            AllPanels[1].SetActive(true);
        }

        if (PlayLoading)
        {
            if (PlayLoadingSlider.fillAmount < 1)
            {
                PlayLoadingSlider.fillAmount += 0.5f * Time.deltaTime;
            }
            else
            {
                AfterPlayLoadingScreen();
            }
        }

        if (Timer)
        {
            if (TimerSlider.fillAmount > 0)
            {
                TimerSlider.fillAmount -= 0.1f * Time.deltaTime;
            }
            else
            {
                TimeOver();
            }
        }
    }

    public void PlayButtonClickAction()
    {
        SoundPlayOnButtonClick();
        AllPanels[1].SetActive(false);
        AllPanels[2].SetActive(true);
        PlayLoadingSlider.fillAmount = 0;
        PlayLoading = true;
    }

    public void AfterPlayLoadingScreen()
    {
        PlayLoading = false;
        AllPanels[2].SetActive(false);
        AllPanels[3].SetActive(true);
    }

    public void SelectionScreenClickAction(int Value)
    {
        SoundPlayOnButtonClick();
        SelectedOperator = Value;
        AllPanels[3].SetActive(false);
        AllPanels[4].SetActive(true);
        if (Value != 5)
        {
            RandomOperator = false;
        }
        else
        {
            RandomOperator = true;
        }
        StartCoroutine(GameScreenTimerWaiting());
        GenerateRandomQuestionsAndAnswers();
    }

    IEnumerator GameScreenTimerWaiting()
    {
        yield return new WaitForSeconds(0.50f);
        Timer = true;
    }

    public void GenerateRandomQuestionsAndAnswers()
    {
        int Random1, Random2;
        int Temp;

        TimerSlider.fillAmount = 1;
        //Timer = true;

        switch (SelectedOperator)
        {
            case 1:
                //When User Select Addition Operator :- 
                OperatorValue.text = "+";
                Random1 = Random.Range(1, 10);
                Random2 = Random.Range(1, 10);
                //Debug.Log("Random Value 1 :- "+Random1);
                //Debug.Log("Random Value 2 :- "+Random2);
                Answer = Random1 + Random2;
                FirstValue.text = Random1.ToString();
                SecondValue.text = Random2.ToString();
                GenerateRandomAnswerValue();
                GenerateRandomOptionPosition();
                SetRandomValuesInRandomButtons();
                Debug.Log("Answer Is :- " + Answer);
                break;

            case 2:
                //When User Select Substraction Operator :- 
                OperatorValue.text = "-";
                Random1 = Random.Range(1, 10);
                Random2 = Random.Range(1, 10);
                if (Random1 < Random2)
                {
                    Temp = Random1;
                    Random1 = Random2;
                    Random2 = Temp;
                }
                //Debug.Log("Random Value 1 :- "+Random1);
                //Debug.Log("Random Value 2 :- "+Random2);
                Answer = Random1 - Random2;
                FirstValue.text = Random1.ToString();
                SecondValue.text = Random2.ToString();
                GenerateRandomAnswerValue();
                GenerateRandomOptionPosition();
                SetRandomValuesInRandomButtons();
                Debug.Log("Answer Is :- " + Answer);
                break;

            case 3:
                //When User Select Multiplication Operator :- 
                OperatorValue.text = "x";
                Random1 = Random.Range(1, 10);
                Random2 = Random.Range(1, 10);
                //Debug.Log("Random Value 1 :- "+Random1);
                //Debug.Log("Random Value 2 :- "+Random2);
                Answer = Random1 * Random2;
                FirstValue.text = Random1.ToString();
                SecondValue.text = Random2.ToString();
                GenerateRandomAnswerValue();
                GenerateRandomOptionPosition();
                SetRandomValuesInRandomButtons();
                Debug.Log("Answer Is :- " + Answer);
                break;

            case 4:
                //When User Select Division Operator :- 
                OperatorValue.text = "/";
                do
                {
                    Random1 = Random.Range(1, 30);
                    Random2 = Random.Range(1, 30);

                    if (Random1 < Random2)
                    {
                        Temp = Random1;
                        Random1 = Random2;
                        Random2 = Temp;
                    }
                } while (Random1 % Random2 != 0);
                //Debug.Log("Random Value 1 :- "+Random1);
                //Debug.Log("Random Value 2 :- "+Random2);
                Answer = Random1 / Random2;
                FirstValue.text = Random1.ToString();
                SecondValue.text = Random2.ToString();
                GenerateRandomAnswerValue();
                GenerateRandomOptionPosition();
                SetRandomValuesInRandomButtons();
                Debug.Log("Answer Is :- " + Answer);
                break;

            case 5:
                UserSelectRandomOption();
                break;

            default:
                Debug.Log("Wrong Choice...");
                break;
        }
    }

    public void UserSelectRandomOption()
    {
        int RandomOperator;
        RandomOperator = Random.Range(1, 5);
        SelectedOperator = RandomOperator;
        GenerateRandomQuestionsAndAnswers();
    }

    public void GenerateRandomAnswerValue()
    {
        RandomValues.Clear();
        int Value;

        for (int i = 1; i <= 3; i++)
        {
            do
            {
                Value = Random.Range(1, 40);
            } while (RandomValues.Contains(Value) || Value == Answer);

            RandomValues.Add(Value);
        }
    }

    public void GenerateRandomOptionPosition()
    {
        RandomOptionButtonPosition.Clear();
        int Value;

        for (int i = 1; i <= 4; i++)
        {
            do
            {
                Value = Random.Range(0, 4);
            } while (RandomOptionButtonPosition.Contains(Value));

            RandomOptionButtonPosition.Add(Value);
        }
    }

    public void SetRandomValuesInRandomButtons()
    {
        for (int i = 0; i < AllAnswerButtons.Length - 1; i++)
        {
            AllAnswerButtons[RandomOptionButtonPosition[i]].transform.GetChild(0).GetComponent<Text>().text = RandomValues[i].ToString();
        }

        AllAnswerButtons[RandomOptionButtonPosition[AllAnswerButtons.Length - 1]].transform.GetChild(0).GetComponent<Text>().text = Answer.ToString();

    }

    public void CheckAnswer(Text ClickedText)
    {
        string OriginalAnswer = Answer.ToString();

        if (ClickedText.text == OriginalAnswer)
        {
            
            //Success();
            if (RandomOperator == true)
            {
                SoundPlayOnRightAnswerClick();
                UserSelectRandomOption();
            }
            else
            {
                SoundPlayOnRightAnswerClick();
                GenerateRandomQuestionsAndAnswers();
            }
        }
        else
        {
            SoundPlayOnWrongAnswerClick();
            GameOver();
        }
    }

    //public void Success()
    //{
    //    SoundPlayOnSuccessPanelOpen();
    //    Timer = false;
    //    AllPanels[7].SetActive(true);
    //}

    //public void NextButtonClickAction()
    //{
    //    SoundPlayOnButtonClick();
    //    SuccessPanelClickAnimation();
    //    Timer = true;
    //    AllPanels[4].SetActive(true);
    //   // AllPanels[7].SetActive(false);
    //}

    public void GameOver()
    {
        SoundPlayOnGameOverPanelOpen();
        Timer = false;
        AllPanels[6].SetActive(true);
    }

    public void TimeOver()
    {
        SoundPlayOnGameOverPanelOpen();
        Timer = false;
        AllPanels[8].SetActive(true);
    }

    public void SelectionScreenBackButtonClickAction()
    {
        SoundPlayOnButtonClick();
        AllPanels[3].SetActive(false);
        AllPanels[1].SetActive(true);
    }

    public void GameScreenBackButtonClickAction()
    {
        SoundPlayOnButtonClick();
        Timer = false;
        AllPanels[4].SetActive(false);
        AllPanels[3].SetActive(true);
    }

    public void SettingButtonClickAction()
    {
        SoundPlayOnButtonClick();
        AllPanels[5].SetActive(true);
    }

    public void ExitButtonClickAction()
    {
        SoundPlayOnButtonClick();
        AllPanels[10].SetActive(true);
    }

    public void SettingsCloseButtonClickAction()
    {
        SoundPlayOnButtonClick();
        SettingsPanelClickAnimation();
        // AllPanels[5].SetActive(false);
    }

    public void HomeButtonClickAction()
    {
        SoundPlayOnButtonClick();
        SuccessPanelClickAnimation();
        TimeOverPanelClickAnimation();
        GameOverPanelClickAnimation();
        MainMenuPanelClickAnimation();
        AllPanels[1].SetActive(true);
        AllPanels[4].SetActive(false);
        // AllPanels[6].SetActive(false);
        // AllPanels[7].SetActive(false);
        // AllPanels[8].SetActive(false);
        // AllPanels[9].SetActive(false);
    }

    public void RetryButtonClickAction()
    {
        SoundPlayOnButtonClick();
        SuccessPanelClickAnimation();
        TimeOverPanelClickAnimation();
        GameOverPanelClickAnimation();
        MainMenuPanelClickAnimation();
        Timer = true;
        TimerSlider.fillAmount = 1;
        AllPanels[4].SetActive(true);
        // AllPanels[6].SetActive(false);
        // AllPanels[7].SetActive(false);
        // AllPanels[8].SetActive(false);
        // AllPanels[9].SetActive(false);
    }

    public void MainMenuButtonClickAction()
    {
      //  GoogleAdMobController.instance.ShowInterstitialAd();
        SoundPlayOnButtonClick();
        Timer = false;
        AllPanels[9].SetActive(true);
        SuccessPanelClickAnimation();
        TimeOverPanelClickAnimation();
        GameOverPanelClickAnimation();
    }

    public void MainMenuCloseButtonClickAction()
    {
        SoundPlayOnButtonClick();
        MainMenuPanelClickAnimation();
        Timer = true;
        // AllPanels[9].SetActive(false);
    }

    public void MusicManagement()
    {
        SoundPlayOnButtonClick();
        if (!MusicAction)
        {
            MusicButton.GetComponent<Image>().sprite = MusicOffSprite;
            MusicSource.mute = true;
            MusicAction = true;
        }
        else
        {
            MusicButton.GetComponent<Image>().sprite = MusicOnSprite;
            MusicSource.mute = false;
            MusicAction = false;
        }
    }

    public void SoundManagement()
    {
        SoundPlayOnButtonClick();
        if (!SoundAction)
        {
            SoundButton.GetComponent<Image>().sprite = SoundOffSprite;
            SoundSource.mute = true;
            SoundAction = true;
        }
        else
        {
            SoundButton.GetComponent<Image>().sprite = SoundOnSprite;
            SoundSource.mute = false;
            SoundAction = false;
        }
    }

    public void SoundPlayOnButtonClick()
    {
        SoundSource.clip = AllSoundClips[0];
        SoundSource.Play();
    }

    public void SoundPlayOnRightAnswerClick()
    {
        SoundSource.clip = AllSoundClips[1];
        SoundSource.Play();
    }

    public void SoundPlayOnWrongAnswerClick()
    {
        SoundSource.clip = AllSoundClips[4];
        SoundSource.Play();
    }

    public void SoundPlayOnSuccessPanelOpen()
    {
        SoundSource.clip = AllSoundClips[3];
        SoundSource.Play();
    }

    public void SoundPlayOnGameOverPanelOpen()
    {
        SoundSource.clip = AllSoundClips[2];
        SoundSource.Play();
    }

    public void SuccessPanelClickAnimation()
    {
        AllPanels[7].transform.GetChild(1).GetComponent<Animator>().Play("SuccessPanelExit");
        StartCoroutine(SuccessPanelBgWaiting());
    }

    IEnumerator SuccessPanelBgWaiting()
    {
        yield return new WaitForSeconds(0.50f);
        AllPanels[7].SetActive(false);
    }

    public void TimeOverPanelClickAnimation()
    {
        AllPanels[8].transform.GetChild(1).GetComponent<Animator>().Play("TimeOverPanelExit");
        StartCoroutine(TimeOverPanelBgWaiting());
    }

    IEnumerator TimeOverPanelBgWaiting()
    {
        yield return new WaitForSeconds(0.50f);
        AllPanels[8].SetActive(false);
    }

    public void GameOverPanelClickAnimation()
    {
        AllPanels[6].transform.GetChild(1).GetComponent<Animator>().Play("GameOverPanelExit");
        StartCoroutine(GameOverPanelBgWaiting());
    }

    IEnumerator GameOverPanelBgWaiting()
    {
        yield return new WaitForSeconds(0.50f);
        AllPanels[6].SetActive(false);
    }

    public void MainMenuPanelClickAnimation()
    {
        AllPanels[9].transform.GetChild(1).GetComponent<Animator>().Play("MainMenuTextExit");
        AllPanels[9].transform.GetChild(2).GetComponent<Animator>().Play("MainMenuPanelExit");
        StartCoroutine(MainMenuPanelBgWaiting());
    }

    IEnumerator MainMenuPanelBgWaiting()
    {
        yield return new WaitForSeconds(0.50f);
        AllPanels[9].SetActive(false);
    }

    public void SettingsPanelClickAnimation()
    {
        AllPanels[5].transform.GetChild(1).GetComponent<Animator>().Play("SettingsTextExit");
        AllPanels[5].transform.GetChild(2).GetComponent<Animator>().Play("SettingsPanelExit");
        StartCoroutine(SettingsPanelBgWaiting());
    }

    IEnumerator SettingsPanelBgWaiting()
    {
        yield return new WaitForSeconds(0.50f);
        AllPanels[5].SetActive(false);
    }

    public void ExitPanelCloseButtonClickAction()
    {
        ExitPanelClickAnimation();
        //AllPanels[10].SetActive(false);
        AllPanels[1].SetActive(true);
    }

    public void ExitPanelYesButtonClickAction()
    {
        UnityEngine.Application.Quit();
    }

    public void ExitPanelClickAnimation()
    {
        AllPanels[10].transform.GetChild(1).GetComponent<Animator>().Play("SettingsTextExit");
        AllPanels[10].transform.GetChild(2).GetComponent<Animator>().Play("SettingsPanelExit");
        StartCoroutine(ExitPanelBgWaiting());
    }

    IEnumerator ExitPanelBgWaiting()
    {
        yield return new WaitForSeconds(0.50f);
        AllPanels[10].SetActive(false);
    }
}
