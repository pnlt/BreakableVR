using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

namespace _Project.Scripts.Cups
{
    public class PlasticCup : MonoBehaviour
    {
        [SerializeField] private Rigidbody cupRigid;
        [SerializeField] private float velocityThreshold = .1f;
        [SerializeField] private float checkInterval = .1f;
        private bool isDrop = false;
        private bool isBeingMonitored = false;
        
        
        public bool IsDrop { get => isDrop; private set => isDrop = value; }

        private void Awake()
        {
            cupRigid = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Ball"))
            {
                if (!isBeingMonitored)
                {
                    StartCoroutine(MonitorMovement());
                }
            }
        }

        private IEnumerator MonitorMovement()
        {
            isBeingMonitored = true;
            Vector3 lastPosition = transform.position;

            while (isBeingMonitored)
            {
                yield return new WaitForSeconds(checkInterval);
                float velocity = Vector3.Distance(transform.position, lastPosition) / checkInterval;
                if (velocity < velocityThreshold && cupRigid.linearVelocity.magnitude < velocityThreshold)
                {
                    isBeingMonitored = false;
                    isDrop = true;
                }
            }
            lastPosition = transform.position;
        }
    }
}