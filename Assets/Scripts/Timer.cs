using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] public GameObject timeObject;
    [SerializeField] public float time;
    private TMP_Text timeText;
    private int seconds;
    private int minutes;
    private bool timesUp;

    // Start is called before the first frame update
    void Start()
    {
        timeText = timeObject.gameObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        isTimeUp();
        seconds = Mathf.FloorToInt(time % 60);
        minutes = Mathf.FloorToInt(time / 60);
        if (minutes < 10)
        {
            timeText.SetText("0" + minutes.ToString() + ":" + seconds.ToString());
        }
        if (seconds < 10)
        {
            timeText.SetText(minutes.ToString() + ":" + "0" + seconds.ToString());
        }
        if (minutes < 10 && seconds < 10)
        {
            timeText.SetText("0" + minutes.ToString() + ":" + "0" + seconds.ToString());
        }
        else if (minutes >= 10 && seconds >= 10)
        {
            timeText.SetText(minutes.ToString() + ":" + seconds.ToString());
        }
        time -= Time.deltaTime;
        if (time <= 0)
        {
            time = 0;
            timesUp = true;
        }
    }

    private void isTimeUp()
    {
        if (timesUp)
        {
            SceneManager.LoadScene("Test_TimesUp");
            this.gameObject.SetActive(false);
        }
        else
        {
            return;
        }
    }
}
