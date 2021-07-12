using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : Singleton<EffectManager>
{
    public Material flashMaterial;
    public Material spriteLitMaterial;

    bool alteringTime = false;

    public IEnumerator SlowTime(float seconds, float percentage100)
    {
        if(!alteringTime)
        {
            alteringTime = true;
            float multiplier = percentage100 * 0.01f;
            Time.timeScale = multiplier;
            yield return new WaitForSeconds(seconds);
            Time.timeScale = 1;
            alteringTime = false;
        }
    }
}
