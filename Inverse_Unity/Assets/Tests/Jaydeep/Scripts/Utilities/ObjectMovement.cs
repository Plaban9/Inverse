using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minimilist.Utilities
{
    public class ObjectMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 0f;
        [SerializeField] private Transform startPos = null;
        [SerializeField] private Transform targetPos = null;
        private bool isReached = false;

        private void FixedUpdate()
        {
            if (Vector3.Distance(transform.position, startPos.position) < .5f)
                isReached = false;
            else if (Vector3.Distance(transform.position, targetPos.position) < .5f)
                isReached = true;

            var target = isReached ? startPos : targetPos;

            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
        }
    }
}