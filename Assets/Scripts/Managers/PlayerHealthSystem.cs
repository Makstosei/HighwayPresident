using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PlayerHealthSystem : MonoBehaviour
{
    public int playerHealth;
    public int playerMaxHealth;

    private void Awake()
    {
        playerMaxHealth = 2;
        playerHealth = playerMaxHealth;        
    }

    public void TakeDamage(int damage)
    {
        if (playerHealth - damage == 0)
        {
            Destroy(GameObject.Find("[DOTween]"));
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
        else
        {
            playerHealth -= damage;
        }

    }

}
