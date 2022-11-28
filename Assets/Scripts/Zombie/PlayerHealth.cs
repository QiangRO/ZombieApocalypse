using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    float playerLife = 3f;
    float playerDamage = 1f;
    // [SerializeField]
    // Image lifeBar;
    Animator animZombie;

    [SerializeField]
    Image [] damage;
    int posimage;

    //GameObject panel;
    public bool isDead;
    AudioSource deadSound;
    void Start(){
        deadSound = gameObject.GetComponent<AudioSource>();
        isDead = false;
        posimage = 0;
        HideImages();
        playerLife = 3f;
        playerDamage = 1f;
        //panel = GameObject.FindGameObjectWithTag("Canvas");
        //panel.SetActive(false);
    }
    private void OnTriggerEnter(Collider other) {
        animZombie = other.gameObject.GetComponentInChildren<Animator>();
        if(other.gameObject.tag.Equals("Enemy") && !animZombie.GetBool("Dead") && animZombie.GetBool("Attack")){
            if(playerLife <= 0){
                Debug.Log("Estas Muerto");
                isDead = true;
                gameObject.GetComponent<Rigidbody>().freezeRotation = false;
                gameObject.GetComponent<playerMov>().enabled = false;
                //panel.SetActive(true);
                StartCoroutine("ChangeScene");
            } else {
                playerLife -= playerDamage;
                damage[posimage].enabled = true;
                posimage++;
                // lifeBar.fillAmount = playerLife;
            }
        }    
    }

    private void HideImages(){
        foreach(Image imagen in damage){
            imagen.enabled = false;
        }
    }

    private IEnumerator ChangeScene(){
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("Menu");
    }
}
