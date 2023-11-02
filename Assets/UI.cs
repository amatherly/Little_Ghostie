
using UnityEngine;

public class UI : MonoBehaviour
{

    [SerializeField] private GameObject gameOver;
    
    
    public void GameOver()
    {
        gameOver.SetActive(true);
    }
    
}
