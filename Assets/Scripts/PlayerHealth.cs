using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    int posimage;
    void Start(){
        posimage = 0;
        HideImages();
        playerLife = 3f;
        playerDamage = 1f;
    }
    private void OnTriggerEnter(Collider other) {
        Debug.Log(playerLife);
        animZombie = other.gameObject.GetComponent<Animator>();
        if(other.gameObject.tag.Equals("Enemy") && !animZombie.GetBool("Dead") && animZombie.GetBool("Attack")){
            if(playerLife <= 0){
                Debug.Log("Estas Muerto");
                gameObject.GetComponent<Rigidbody>().freezeRotation = false;
                gameObject.GetComponent<playerMov>().enabled = false;
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
}
