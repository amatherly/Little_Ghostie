
using UnityEngine;

public class UI : MonoBehaviour
{

    [SerializeField] private GameObject gameOver;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void GameOver()
    {
        gameOver.SetActive(true);
    }
    
}
