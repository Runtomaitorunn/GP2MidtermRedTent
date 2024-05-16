using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DayNightControl : MonoBehaviour
{
    [Header("RotateSun")]
    public float timeMultiplier;
    public float startHour;
    public Light sun;
    public float sunriseHour;
    public float sunsetHour;
    public TimeSpan sunriseTime;
    public TimeSpan sunsetTime;
    public DateTime currentTime;
    [Header("AdjustSun")]
    public float maxSunIntensity;

    [Header("MoonLight")]
    public Color dayAmbientLight;
    public Color nightAmbientLight;
    public AnimationCurve lightChangeCurve;
    public Light moon;
    public float maxMoonIntensity;

    void Start()
    {
        currentTime = DateTime.Now + TimeSpan.FromHours(startHour);

        sunriseTime = TimeSpan.FromHours(sunriseHour);
        sunsetTime = TimeSpan.FromHours(sunsetHour);
        
    }

    void Update()
    {
        currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier);
        currentTime = currentTime.TimeOfDay < TimeSpan.FromDays(1) ? currentTime : currentTime - TimeSpan.FromDays(1);


        RotateSun();
        UpdateLightSetting();
    }
    void RotateSun()
    {
        float sunLightRotation;
        float moonLightRotation;

        if (currentTime.TimeOfDay > sunriseTime && currentTime.TimeOfDay < sunsetTime)
        {
            TimeSpan sunriseToSunsetDuration = CalculateTimeDifference(sunriseTime, sunsetTime);
            TimeSpan timeSinceSunrise = CalculateTimeDifference(sunriseTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunrise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes;

            sunLightRotation = Mathf.Lerp(0, 180, (float)percentage);
            moonLightRotation = Mathf.Lerp(180, 360, (float)percentage);
        }
        else
        {
            TimeSpan sunsetToSunriseDuration = CalculateTimeDifference(sunsetTime, sunriseTime);
            TimeSpan timeSinceSunset = CalculateTimeDifference(sunsetTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunset.TotalMinutes / sunsetToSunriseDuration.TotalMinutes;

            sunLightRotation = Mathf.Lerp(180, 360, (float)percentage);
            moonLightRotation = Mathf.Lerp(0, 180, (float)percentage);
        }

        sun.transform.rotation = Quaternion.AngleAxis(sunLightRotation, Vector3.right);
        moon.transform.rotation = Quaternion.AngleAxis(moonLightRotation, Vector3.right);
    }



    void UpdateLightSetting()
    {
        float dotProduct = Vector3.Dot(sun.transform.forward, Vector3.down);
        sun.intensity = Mathf.Lerp(0, maxSunIntensity, lightChangeCurve.Evaluate(dotProduct));
        moon.intensity = Mathf.Lerp(0, maxMoonIntensity, lightChangeCurve.Evaluate(-dotProduct));
        RenderSettings.ambientLight = Color.Lerp(nightAmbientLight,dayAmbientLight,lightChangeCurve.Evaluate(dotProduct));
    }
    private TimeSpan CalculateTimeDifference(TimeSpan fromTime,TimeSpan toTime)
    {
        TimeSpan difference = toTime - fromTime;
        if (difference.TotalSeconds < 0)
        {
            difference += TimeSpan.FromHours(24);
        }
        return difference;
    }

}
