using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minimilist.Utilities
{
    public class ObjectMovement : MonoBehaviour
    {
        [SerializeField] private bool flip;
        [SerializeField] private float speed = 0f;
        [SerializeField] private Transform startPos = null;
        [SerializeField] private Transform targetPos = null;
        private bool isReached = false;
        private bool isMoving = true;

        private void FixedUpdate()
        {
            if (!isMoving) return;

            if (Vector3.Distance(transform.position, startPos.position) < .5f)
                isReached = false;
            else if (Vector3.Distance(transform.position, targetPos.position) < .5f)
                isReached = true;

            var target = isReached ? startPos : targetPos;

            var xScale = Mathf.Abs(transform.localScale.x);
            transform.localScale = new Vector2(isReached ? -xScale : xScale, transform.localScale.y);
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
        }

        public void StartMoving() { isMoving = true; }
        public void StopMoving() {  isMoving = false; }
    }
}