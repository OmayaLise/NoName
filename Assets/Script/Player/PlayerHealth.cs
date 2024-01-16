using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] Player_Data data;
    [SerializeField] UnityEngine.UI.Image healthCanva;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        healthCanva.fillAmount = data.playerHealth / data.playerHealthDefault ;
        animator = GetComponent<Animator>();    
    }

    public void PlayerGainHealth(int lifeGained)
    {
        if(data.playerHealth + lifeGained <= data.playerHealthDefault)
        {
            data.playerHealth += lifeGained;
            healthCanva.fillAmount = data.playerHealth / data.playerHealthDefault;
        }
    }

    public void PlayerLooseHealth(int lifeLost)
    {
        data.playerHealth -= lifeLost;
        healthCanva.fillAmount = data.playerHealth / data.playerHealthDefault;
        animator.Play("Hurt", 0);
        CheckIfAlive();
    }

    private void CheckIfAlive()
    {
        if(data.playerHealth <= 0)
        {
            animator.SetBool("isDead", true);
        }
    }

    public void PushBack(Vector2 direction)
    {
    }
}
