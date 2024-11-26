using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float verticalForce = 400f;
    [SerializeField] private float restarDelay = 1f;

    [SerializeField] private ParticleSystem playerParticles;

    [SerializeField] private Color yellowColor;
    [SerializeField] private Color violetColor;
    [SerializeField] private Color cyanColor;
    [SerializeField] private Color pinkColor;
    private string currentColor;

    Rigidbody2D playerRb;
    SpriteRenderer playerSR;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerSR = GetComponent<SpriteRenderer>();

        ChangeColor();
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.velocity = Vector2.zero;
            playerRb.AddForce(new Vector2(0, verticalForce));
        }
    }

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colision con: " +  collision.gameObject.name);
        collision.gameObject.GetComponent<SpriteRenderer>().color = Color.red; 
    }
    */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ColorChanger"))
        {
            ChangeColor();
            Destroy(collision.gameObject);
            return;
        }

        if (collision.gameObject.CompareTag("FinishLine"))
        {
            gameObject.SetActive(false);
            Instantiate(playerParticles, transform.position, Quaternion.identity);
            Invoke("LoadNextScene", restarDelay);
            return;
        }

        if (!collision.gameObject.CompareTag(currentColor)) 
        {
            gameObject.SetActive(false);
            Instantiate(playerParticles, transform.position, Quaternion.identity);
            Invoke("RestartScene", restarDelay);
        }
    }

    void LoadNextScene()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex + 1);
    }

    void RestartScene() 
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex);
    }

    void ChangeColor() 
    {
        int randomNumber = Random.Range(0, 4);

        if (randomNumber == 0)
        {
            playerSR.color = yellowColor;
            currentColor = "Yellow";
        }
        else if (randomNumber == 1)
        {
            playerSR.color = violetColor;
            currentColor = "Violet";
        }
        else if (randomNumber == 2)
        {
            playerSR.color = cyanColor;
            currentColor = "Cyan";
        }
        else if (randomNumber == 3) 
        {
            playerSR.color = pinkColor;
            currentColor = "Pink";
        }
    }
}
