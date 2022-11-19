using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float playerLife = 1;
    [SerializeField]
    float playerDamage = 0.1f;
    [SerializeField]
    Image lifeBar;

    [SerializeField]
    Animator animZombie;

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag.Equals("Zombie") && !animZombie.GetBool("Dead") && animZombie.GetBool("Attack")){
            Debug.Log(playerLife);
            if(playerLife <= 0){
                Debug.Log("Estas Muerto");
            } else {
                playerLife -= playerDamage;
                lifeBar.fillAmount = playerLife;
            }
        }    
    }
}
