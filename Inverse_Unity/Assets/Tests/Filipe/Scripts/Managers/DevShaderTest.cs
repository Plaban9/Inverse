using Managers.BWEffectManager;
using Managers.BWState;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevShaderTest : MonoBehaviour
{

    [SerializeField] private BWEffectManager _bwManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _bwManager.SwapMode();
        }
    }

    

    
}
