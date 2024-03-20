using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minimalist.Effect
{
    public class DuststormEffect : MonoBehaviour
    {
        [SerializeField] SpriteRenderer layer1;
        [SerializeField] SpriteRenderer layer2;
        [SerializeField] SpriteRenderer layer3;

        private float speed1 = 1f;
        private float speed2 = 1.5f;
        private float speed3 = 0.5f;

        private float skew1 = 0.47f;
        private float skew2 = 0.47f;
        private float skew3 = 0.47f;

        private Material layer1Mat;
        private Material layer2Mat;
        private Material layer3Mat;
        

        private void Start()
        {
            layer1Mat = layer1.material;
            layer2Mat = layer2.material;
            layer3Mat = layer3.material;


        }

        private float time = 0;
        private float layer1Offset = 0;
        private float layer2Offset = 0;
        private float layer3Offset = 0;

        // Update is called once per frame
        void Update()
        {
            time += Time.deltaTime;

            layer1Offset += speed1 * Time.deltaTime;// + Mathf.Sin(time * Mathf.PI) * speed1 * Time.deltaTime;
            layer2Offset += speed2 * Time.deltaTime;// + Mathf.Sin(time * Mathf.PI * 2) * speed2 * Time.deltaTime;
            layer3Offset += speed3 * Time.deltaTime;// + Mathf.Sin(time * Mathf.PI / 2) * speed3 * Time.deltaTime;

            layer1Mat.SetFloat("_speed", speed1);
            layer2Mat.SetFloat("_speed", speed2);
            layer3Mat.SetFloat("_speed", speed3);

            layer1Mat.SetFloat("_stretch", Mathf.Sin(time * Mathf.PI * 2) * skew1/2 + skew1/2 + 0.2f);
            layer2Mat.SetFloat("_stretch", Mathf.Sin(time * Mathf.PI / 2) * skew2/2 + skew1/2 + 0.2f);
            layer3Mat.SetFloat("_stretch", Mathf.Sin(time * Mathf.PI) * skew3/2 + skew3/2 + 0.2f);

            layer1Mat.SetFloat("_alpha", Mathf.Sin(time * Mathf.PI / 12) * 0.3f + 0.1f) ;
            layer2Mat.SetFloat("_alpha", Mathf.Sin(time * Mathf.PI / 5) * 0.3f + 0.1f);
            layer3Mat.SetFloat("_alpha", Mathf.Sin(time * Mathf.PI / 7) * 0.4f + 0.1f);

        }
    }
}

