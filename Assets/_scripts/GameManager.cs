using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement; 


public class GameManager : MonoBehaviour
{
    

    [Header("Win and Lose")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private TextMeshProUGUI winStoryTXT;
    [SerializeField] private TextMeshProUGUI loseStoryTXT;
    [SerializeField] private GameObject playerBaseREF;

    [Header("Timer settings")]
    [SerializeField] private GameObject countDownPanel;
    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] private float remainingTime;
    private int minutes;
    private int seconds;

    [Header("Fading sky settings")]
    [SerializeField] private GameObject skyRef;
    [SerializeField] private float fromAlpha;
    [SerializeField] private float toAlpha;
    private SpriteRenderer _spriteRenderer;
    
    private PlayerBase _playerBase;
 
    void Start()
    {
        _spriteRenderer = skyRef.GetComponent<SpriteRenderer>();
        StartCoroutine(FadeAlpha(fromAlpha, toAlpha, remainingTime));
        _playerBase = playerBaseREF.GetComponent<PlayerBase>();

        //set up UI for start of the game
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        countDownPanel.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
       if(remainingTime > 0f)
        {
            remainingTime -= Time.deltaTime;    
        }
        else
        {
            remainingTime = 0;
            EndGame();
        }
        //Countdown functionality
        
        minutes =Mathf.FloorToInt( remainingTime / 60);
        seconds = Mathf.FloorToInt( remainingTime % 60);
        countdownText.text = string.Format("{00:00}:{01:00}", minutes,seconds);
    }
    
    private IEnumerator FadeAlpha(float from, float to, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            Color c = _spriteRenderer.color;
            c.a = Mathf.Lerp(from, to, elapsed / duration);
            _spriteRenderer.color = c;
            yield return null;
        } 
        Color final = _spriteRenderer.color;
        final.a = to;
        _spriteRenderer.color = final;
    }

    private void EndGame()
    {
        //check how much sustenance the player has 
        var sustenanceSupply = _playerBase.sustenanceSupply;
        var entertainmentSupply = _playerBase.sustenanceSupply;
        var warmthSupply = _playerBase.warmthSupply;

        bool inBase = _playerBase.IsPlayerInBase;

        if(inBase)
        {
            //check if sustenance and warmth are high values
            if(sustenanceSupply >= 10 && warmthSupply >= 10)
            {
                winStoryTXT.text = ("You had enough warmth and food!!!!! you had no entertainment though so you were bored out of your mind");
                if(entertainmentSupply > 10)
                {
                    winStoryTXT.text = ("You had enough warmth and food!!!!! when times got rough, you were able to use the cards you found to keep yourself entertained");
                }
                winPanel.SetActive(true);
                countDownPanel.SetActive(false); 
            }
            else
            {
                losePanel.SetActive(true);
                countDownPanel.SetActive(false);
                Debug.Log("did not survive");
            }
        }
        else
        {
            loseStoryTXT.text = ("You did not make it home in time. The sun was eclipsed and the monsters came out and ate you");
            losePanel.SetActive(true);
            countDownPanel.SetActive(false);
            Debug.Log("did not survive");
        }
        
    }

    public void restartGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}