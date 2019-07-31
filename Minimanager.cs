using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Minimanager : MonoBehaviour
{

    public int NrMonster = 3;
    public Animator anim;

    public void KillOne(){
        NrMonster--;
        if(NrMonster == 0){
            StartCoroutine(GoNextScene());
        }
    }
    private IEnumerator GoNextScene(){
        anim.SetTrigger("Is");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("halfBrokenIntro");
    }

}
