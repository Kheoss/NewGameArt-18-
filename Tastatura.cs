using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class Tastatura : MonoBehaviour
{
    public List<string> messages = new List<string>();
    private string CurrentMessage;
    private int messagesIndex;
    public TMP_Text CurMessageText;
    private int curMessageLength;
    public bool canType;
    public GameObject messagesHandler;
    public GameObject handlerForHistory;
    public List<Animator> anims = new List<Animator>();
    private Vector3 vc, vc1;
    public GameObject lowBattery;
    public Animator phoneAnim;
    public GameObject goPhoneButton;
    public Animator blinkTransition;
    public TMP_Text hourText;
    public TMP_Text hourText2;
    private void Start(){
        hourText.text = DateTime.Now.ToShortDateString();
        hourText2.text = DateTime.Now.ToShortTimeString();
        Screen.autorotateToPortrait = true;
        Screen.autorotateToPortraitUpsideDown = true;
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
        Screen.orientation = ScreenOrientation.AutoRotation;

        canType = true;
        CurMessageText.text = "";
        curMessageLength = 0;
        messagesIndex = 1;
        messages.Add("What?");
        messages.Add("Who is this??? What is happening???");
        messages.Add("NO MORE JOKES");
        messages.Add("GOOD NIGHT M$@T#$@");
        CurrentMessage = "";
        foreach (Animator anm in anims){
            anm.gameObject.SetActive(false);
        }
    }
    public void AddElement(string element){
        if (!canType) return;
        if (curMessageLength >= messages[messagesIndex/2].Length) return;
            CurrentMessage += element;
        CurMessageText.text += messages[messagesIndex/2][curMessageLength];
        curMessageLength++;
    }
    public void SemiSend()
    {
        if (curMessageLength == messages[messagesIndex/2].Length)
        {
            StartCoroutine(SendMessage());
        }
    }
    private IEnumerator SendMessage(){
        PlayerPrefs.SetString("Raspuns" + messagesIndex.ToString(), CurrentMessage);
        CurMessageText.text = "";
        curMessageLength = 0;
        CurrentMessage = "";
        if (messagesIndex >= 3)
        {
            vc1.y += 1.2f;
            if(messagesIndex!=3) anims[messagesIndex - 4].gameObject.SetActive(false);
            anims[messagesIndex - 3].gameObject.SetActive(false);
        }
        anims[messagesIndex].gameObject.SetActive(true);
        anims[messagesIndex].SetTrigger("Appear");
        yield return new WaitForSeconds(2f);
        if (messagesIndex != 7)
        {
            anims[messagesIndex + 1].gameObject.SetActive(true);
            anims[messagesIndex + 1].SetTrigger("Appear");
        }
        else
        {
            yield return new WaitForSeconds(1.5f);
            blinkTransition.SetTrigger("Appear");
            vc.y = -10;
            yield return new WaitForSeconds(2f);
            phoneAnim.SetTrigger("Disappear");
            StartCoroutine(GoNextScene());
        }
        if (messagesIndex >= 3)
        {
            vc1.y += 1.2f;
        }

        messagesIndex +=2;
        if (messagesIndex == 3){
            Handheld.Vibrate();
            lowBattery.SetActive(true);
            while(SystemInfo.batteryStatus != BatteryStatus.Charging){
                yield return new WaitForSeconds(1f);
                Debug.Log("please charge");
            }
            lowBattery.SetActive(false);
        }
        yield return null;
    }
    private void Update()
    {
        if (messagesIndex > 8) return;
        handlerForHistory.transform.position = Vector2.MoveTowards(vc1, vc, 1f * Time.deltaTime);
        handlerForHistory.transform.position = new Vector3(
        handlerForHistory.transform.position.x, handlerForHistory.transform.position.y,-1);
    }
    public void GoPhone(){
        phoneAnim.SetTrigger("Appear");
        goPhoneButton.SetActive(false);
        CurMessageText.gameObject.SetActive(true);
        StartCoroutine(firstMesage());
    }
    IEnumerator GoNextScene()
    {
        blinkTransition.SetTrigger("Appear");
           yield return new WaitForSeconds(8f);
        SceneManager.LoadScene("BlindDateScene");
    }
    IEnumerator firstMesage()
    {
        yield return new WaitForSeconds(2f);
        anims[0].gameObject.SetActive(true);
        anims[0].SetTrigger("Appear");
        vc = handlerForHistory.transform.position;
        vc1 = handlerForHistory.transform.position;
        //y=1.38
    }
}