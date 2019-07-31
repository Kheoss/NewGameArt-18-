using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Networking;

public class GuideLogics : MonoBehaviour {

    public GameObject player;
    private float _playerDistance;  
    public TMP_Text dialogueText;
    public GameObject hintPrefab;
    private bool boughtFromAlexa;
    private GameObject curHint;
    public Animator firstGate;
    private void Start(){
        boughtFromAlexa = false;
         Queue<string> _sentences = new Queue<string>();
        _sentences.Enqueue("Oh..Hello there");
        _sentences.Enqueue("You got through the darkness");
        _sentences.Enqueue("Impressive");
        _sentences.Enqueue("But now you need a weapon!");
        _sentences.Enqueue("Go ask Alexa for The BlackSmith");
        _sentences.Enqueue("He may help you!");
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(Dialogue(_sentences,FinishedFirstDialogue));
    }
    private void Update(){

        firstGate.SetTrigger("Open");
        return;
        _playerDistance = Vector2.Distance(transform.position, player.transform.position);
        if(_playerDistance <= 7.5f){
            
            Debug.Log("asdas");
        }
    }
    private IEnumerator Dialogue(Queue<string> _sentences, Action callback){
        dialogueText.transform.parent.gameObject.SetActive(true);
        dialogueText.transform.parent.gameObject.GetComponent<Animator>().SetTrigger("Appear");
        while (_sentences.Count > 0){
            dialogueText.text = "";
            string sentence = _sentences.Peek();
            _sentences.Dequeue();
            foreach(char letter in sentence){
                dialogueText.text += letter;
                yield return null;
            }
            yield return new WaitForSeconds(2f);
        }

        dialogueText.transform.parent.gameObject.GetComponent<Animator>().SetTrigger("Disappear");
        yield return new WaitForSeconds(2f);
        dialogueText.transform.parent.gameObject.SetActive(false);
        callback();
    }
    private IEnumerator CheckForAlexa(){
        while (!boughtFromAlexa){
            yield return new WaitForSeconds(2f);
            //REQUEST TO ALEXA
            UnityWebRequest req = UnityWebRequest.Get("http://3a20ccc4.ngrok.io/CheckWeapon");
            yield return req.SendWebRequest();
            if (req.isNetworkError || req.isHttpError){
                Debug.Log(req.error);
            }
            else{
                boughtFromAlexa = (req.downloadHandler.text == "TRUE") ? true : false;
            }
            
        }
        Destroy(curHint);
        player.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        Queue<string> newDialogue = new Queue<string>();
        newDialogue.Enqueue("Ooo...I see the blacksmith is still a man of honor");
        newDialogue.Enqueue("Now don't chill the bones!You have to fix you mind!");
        StartCoroutine(Dialogue(newDialogue, () =>{
            Debug.Log("Done");
            firstGate.SetTrigger("Open");
        }));
        yield return null;
    }
    private void FinishedFirstDialogue(){
        curHint = Instantiate(hintPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        curHint.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = "You can buy it from Amazon Alexa";
        curHint.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = "Hint";
        curHint.GetComponent<PopUpHint>().enabled = false;
        curHint.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        StartCoroutine(CheckForAlexa());
    }
}
