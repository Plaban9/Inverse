using Managers.BWEffectManager;
using Minimalist.Level;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainEffect : MonoBehaviour
{
    public Material material;
    [SerializeField] private Transform topPos;
    [SerializeField] private Transform bottomPos;
    [SerializeField] float position = 9f;
    [SerializeField] List<GameObject> rainList;

    private void Start()
    {
        material = rainList[0].GetComponent<Renderer>().sharedMaterial;
    }

    public void SetLightMode(LevelType lightdark)
    {
        foreach(GameObject g in rainList)
        {
            if(g.TryGetComponent<ParticleSystem>(out ParticleSystem rain))
            {
                var main = rain.main;

                if (lightdark == LevelType.Light)
                {
                    main.gravityModifier = 5;
                    g.transform.position = topPos.position;
                }else if(lightdark == LevelType.Dark)
                {
                    main.gravityModifier = -5;
                    g.transform.position = bottomPos.position;
                }
            }
                
        }
    }

}
