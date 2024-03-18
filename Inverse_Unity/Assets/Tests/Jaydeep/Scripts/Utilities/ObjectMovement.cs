using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minimilist.Utilities
{
    public class ObjectMovement : MonoBehaviour
    {
        [SerializeField] private bool flip;
        [SerializeField] private float speed = 5f;
        [SerializeField] private Transform startPos = null;
        [SerializeField] private Transform targetPos = null;
        private bool isReached = false;

        public bool IsMoving { get; private set; } = true;


        private void FixedUpdate()
        {
            if (!IsMoving) return;

            if (Vector3.Distance(transform.position, startPos.position) < 1f)
                isReached = false;
            else if (Vector3.Distance(transform.position, targetPos.position) < 1f)
                isReached = true;

            var target = isReached ? startPos : targetPos;

            var xScale = Mathf.Abs(transform.localScale.x);
            bool isMovingRight = target.position.x - transform.position.x < 0;
            transform.localScale = new Vector2(isMovingRight ? -xScale : xScale, transform.localScale.y);
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
        }

        public void StartMoving() { IsMoving = true; }
        public void StopMoving() { IsMoving = false; }

        private void OnDrawGizmos()
        {
            if (startPos)
                Gizmos.DrawWireSphere(startPos.position, 1);
            if (targetPos)
                Gizmos.DrawWireSphere(targetPos.position, 1);
        }
    }
}