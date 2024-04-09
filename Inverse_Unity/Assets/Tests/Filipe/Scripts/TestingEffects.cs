using Minimalist.Effect.Animations;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class TestingEffects : MonoBehaviour
{
    private GameObject player = null;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null) {
            player = GameObject.FindWithTag("Player");
        }
        if (Input.GetKey(KeyCode.Space))
        {
            VfxManager.Instance.CreateEffect(VfxEnum.PLAYER_JUMPDUST, player.transform.position);
        }

        if(Input.GetKey(KeyCode.B)) {
            VfxManager.Instance.CreateEffect(VfxEnum.PLAYER_DAMAGEDBLOOD, player.transform.position);
        }

    }
}
