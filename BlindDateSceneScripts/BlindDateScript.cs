using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class BlindDateScript : MonoBehaviour
{
    public TMP_Text dialogueText;
    public TMP_Text dialogueText2;
    public GameObject lanternCanvas;
    public GameObject virtualFlashLight;
    private bool Active;
    private AndroidJavaObject camera1;
    private void Start(){
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.orientation = ScreenOrientation.AutoRotation;

        StartCoroutine(FirstDial());
    }
    private IEnumerator FirstDial(){
        yield return new WaitForSeconds(2f);
        Queue<string> firstDialogue = new Queue<string>();
        firstDialogue.Enqueue("Aaaa... Where am I?");
        StartCoroutine(Dialogue2(firstDialogue,()=> {
            firstDialogue.Clear();
            firstDialogue.Enqueue("Ok!Don't panic!Just open the light!");
            StartCoroutine(Dialogue(firstDialogue, () => {
                firstDialogue.Clear();
                firstDialogue.Enqueue("What?How?");
                StartCoroutine(Dialogue2(firstDialogue, () => {
                    firstDialogue.Clear();
                    firstDialogue.Enqueue("Use your lantern imbecil!");
                    StartCoroutine(Dialogue(firstDialogue, () => {
                        firstDialogue.Clear();
                        lanternCanvas.SetActive(true);
                    }));
                }));
            }));
        }));
        yield return new WaitForSeconds(1f);
    }
    private IEnumerator Dialogue(Queue<string> _sentences, Action callback)
    {
        dialogueText.transform.parent.gameObject.SetActive(true);
        dialogueText.transform.parent.gameObject.GetComponent<Animator>().SetTrigger("Appear");
        while (_sentences.Count > 0){
            dialogueText.text = "";
            string sentence = _sentences.Peek();
            _sentences.Dequeue();
            foreach (char letter in sentence)
            {
                dialogueText.text += letter;
                yield return null;
            }
            yield return new WaitForSeconds(2f);
        }

        dialogueText.transform.parent.gameObject.GetComponent<Animator>().SetTrigger("Disappear");
        yield return new WaitForSeconds(1f);
        dialogueText.transform.parent.gameObject.SetActive(false);
        callback();
    }

    private IEnumerator Dialogue2(Queue<string> _sentences, Action callback)
    {
        dialogueText2.transform.parent.gameObject.SetActive(true);
        dialogueText2.transform.parent.gameObject.GetComponent<Animator>().SetTrigger("Appear");
        while (_sentences.Count > 0)
        {
            dialogueText2.text = "";
            string sentence = _sentences.Peek();
            _sentences.Dequeue();
            foreach (char letter in sentence)
            {
                dialogueText2.text += letter;
                yield return null;
            }
            yield return new WaitForSeconds(2f);
        }

        dialogueText2.transform.parent.gameObject.GetComponent<Animator>().SetTrigger("Disappear");
        yield return new WaitForSeconds(1f);
        dialogueText2.transform.parent.gameObject.SetActive(false);
        callback();
    }

    public void OpenLantern(){
        lanternCanvas.SetActive(false);
        virtualFlashLight.SetActive(true);
        FL_Start();
    }

    private void FL_Start(){
        AndroidJavaClass cameraClass = new AndroidJavaClass("android.hardware.Camera");
        WebCamDevice[] devices = WebCamTexture.devices;
        int camID = 0;
        try{
        camera1 = cameraClass.CallStatic<AndroidJavaObject>("open", camID);
        if (camera1 != null){
            AndroidJavaObject cameraParameters = camera1.Call<AndroidJavaObject>("getParameters");
            cameraParameters.Call("setFlashMode", "torch");
            camera1.Call("setParameters", cameraParameters);
            camera1.Call("startPreview");
            Active = true;
        }
        else{
            Debug.LogError("[CameraParametersAndroid] Camera not available");
        }
        }
        catch (Exception ex){
            Debug.Log(ex.ToString());
        }
    }
    private void OnDestroy(){
        FL_Stop();
    }

    private void FL_Stop(){
        if (camera1 != null){
            camera1.Call("stopPreview");
            camera1.Call("release");
            Active = false;
        }
        else{
            Debug.LogError("[CameraParametersAndroid] Camera not available");
        }
    }
}
