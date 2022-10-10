using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlarmManager : MonoBehaviour
{

    public GameObject alertPipPrefab;

    public List<Image> alertPips = new List<Image>();

    public int alertLevel;
    public int maxAlertLevel;

    public Sprite emptyPip;
    public Sprite filledPip;

    public AudioSource alarmSound;

    private void Awake()
    {
        References.alarmManager = this;
        alarmSound = GetComponent<AudioSource>();
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (AlarmHasSounded() && alarmSound.isPlaying == false)
        {
            alarmSound.Play();
        }
        if (AlarmHasSounded() == false && alarmSound.isPlaying)
        {
            alarmSound.Stop();
        }
    }

    public void SetUpLevel(int desiredMaxAlertLevel)
    {
        for (int i = 0; i < alertPips.Count; i++)
        {
            Destroy(alertPips[i].gameObject);
        }
        alertPips.Clear();
        maxAlertLevel = desiredMaxAlertLevel;

        for (int i = 0; i < maxAlertLevel; i++)
        {
            GameObject newPip = Instantiate(alertPipPrefab, transform);
            alertPips.Add(newPip.GetComponent<Image>());
        }
        alertPips.Reverse();

    }

    public void RaiseAlertLevel()
    {
        alertLevel++;
        UpdatePips();
    }

    public void SoundTheAlarm()
    {
        alertLevel = maxAlertLevel;
        UpdatePips();
    }

    public void StopTheAlarm()
    {
        alertLevel = 0;
        UpdatePips();
    }

    public bool AlarmHasSounded()
    {
         return alertLevel >= maxAlertLevel;
    }

    void UpdatePips()
    {

        for (int i = 0; i < alertPips.Count; i++)
        {
            if (i < alertLevel)
            {
                alertPips[i].sprite = filledPip;
            }
            else
            {
                alertPips[i].sprite = emptyPip;
            }
        }

    }



}
