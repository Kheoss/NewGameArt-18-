using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class AlexaDialogRoom : MonoBehaviour
{

    public Animator Anim;
    public List<string> DialogueParts = new List<string>();
    public TMP_Text text;
    private bool hasLeftNode;
    private void Start(){
        hasLeftNode = false;
        Anim = transform.GetChild(0).GetComponent<Animator>();
        text = Anim.gameObject.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        StartCoroutine(AlexaStuff());
    }
    private IEnumerator AlexaStuff(){
     for(int phraseIndex = 0; phraseIndex < DialogueParts.Count; phraseIndex++){
            text.text = "";
            yield return new WaitForSeconds(1f);
            Anim.SetTrigger("Appear");
            yield return new WaitForSeconds(0.6f);
            for(int letterIndex = 0; letterIndex < DialogueParts[phraseIndex].Length; letterIndex++){
                text.text += DialogueParts[phraseIndex][letterIndex];
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(2f);
            Anim.SetTrigger("DisAppear");
            yield return new WaitForSeconds(1f);
        }
        StartCoroutine(CheckForAlexa());
    }

    
    private IEnumerator CheckForAlexa()
    {
        while (!hasLeftNode)
        {
            yield return new WaitForSeconds(2f);
            //REQUEST TO ALEXA
            UnityWebRequest req = UnityWebRequest.Get("https://3a20ccc4.ngrok.io/CheckNote");
            yield return req.SendWebRequest();
            if (req.isNetworkError || req.isHttpError){
                Debug.Log(req.error);
            }
            else{
                hasLeftNode = (req.downloadHandler.text == "TRUE") ? true : false;
            }
        }
        Debug.Log("A mers");
        transform.parent.SendMessage("RoomUnlock");
    }



}
