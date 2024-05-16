using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarColor : MonoBehaviour
{
    public void StarsColor()
    {
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();

        var main = particleSystem.main;

        float x = Random.Range(0f, 1f);
        float y = Random.Range(0f, 1f);
        float z = 2 * x + 4 * y;

        UnityEngine.Color color = new UnityEngine.Color(x, y, z);
        main.startColor = color;
    }
}
