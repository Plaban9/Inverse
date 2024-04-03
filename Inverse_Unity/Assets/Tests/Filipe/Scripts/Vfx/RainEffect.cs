using Managers.BWEffectManager;
using Minimalist.Level;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainEffect : MonoBehaviour
{
    [SerializeField] List<GameObject> rainList;
    public Material material;
    [SerializeField] float position = 9f;
    float multiplier = 1f;

    private void Start()
    {
        material = rainList[0].GetComponent<Renderer>().sharedMaterial;
    }

    public void SetLightMode(LevelType lightdark)
    {
        multiplier *= -1;
        foreach(GameObject g in rainList)
        {
            if(g.TryGetComponent<ParticleSystem>(out ParticleSystem rain))
            {
                var main = rain.main;

                if (lightdark == LevelType.Light)
                {
                    main.gravityModifier = 5;
                    g.transform.position = new Vector3(g.transform.position.x, 9f, g.transform.position.z);
                }else if(lightdark == LevelType.Dark)
                {
                    main.gravityModifier = -5;
                    g.transform.position = new Vector3(g.transform.position.x, -9f, g.transform.position.z);
                }
            }
                
        }
    }

}
