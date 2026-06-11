using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("Timer settings")]
    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] private float remainingTime;

    [Header("")]
    [SerializeField] private GameObject skyRef;
    [SerializeField] private float fromAlpha;
    [SerializeField] private float toAlpha;
    [SerializeField] private float durationFade;
    private SpriteRenderer _spriteRenderer;
 
    void Start()
    {
        _spriteRenderer = skyRef.GetComponent<SpriteRenderer>();
        StartCoroutine(FadeAlpha(fromAlpha, toAlpha, remainingTime));
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
        }
        //Countdown functionality
        
        int minutes =Mathf.FloorToInt( remainingTime / 60);
        int seconds = Mathf.FloorToInt( remainingTime % 60);
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
}
