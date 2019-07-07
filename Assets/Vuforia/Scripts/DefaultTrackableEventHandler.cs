/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Vuforia
{
    /// <summary>
    /// A custom handler that implements the ITrackableEventHandler interface.
    /// </summary>
    public class DefaultTrackableEventHandler : MonoBehaviour,
                                                ITrackableEventHandler
    {
        #region PRIVATE_MEMBER_VARIABLES
 
        private TrackableBehaviour mTrackableBehaviour;
        private AudioSource aud;
        private AudioClip audioClip;
        public List<AudioClip> lClip = new List<AudioClip>();
        public Text avise;
        private int clipCount;
        private bool isTrack = false;

        #endregion // PRIVATE_MEMBER_VARIABLES        

        #region UNTIY_MONOBEHAVIOUR_METHODS
    
        void Start()
        {
            clipCount = 0;
            aud = GetComponent<AudioSource>();
            aud.clip = lClip[clipCount];
            avise.text = " ";
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }
        }

        #endregion // UNTIY_MONOBEHAVIOUR_METHODS
        

        #region PUBLIC_METHODS

        /// <summary>
        /// Implementation of the ITrackableEventHandler function called when the
        /// tracking state changes.
        /// </summary>
        public void OnTrackableStateChanged(
                                        TrackableBehaviour.Status previousStatus,
                                        TrackableBehaviour.Status newStatus)
        {
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                OnTrackingFound();
                aud.Play();
                isTrack = true;
                avise.text = "Touch the Screen";
            }
            else
            {
                OnTrackingLost();
                aud.Stop();
                isTrack = false;
                avise.text = " ";
            }
        }

        #endregion // PUBLIC_METHODS



        #region PRIVATE_METHODS
        
        private void Update()
        {
            if(Input.touchCount > 0 && Input.touchCount <2)
            {
                avise.text = "";
                if (clipCount < lClip.Capacity)
                {
                    aud.clip = lClip[clipCount];
                    clipCount++;
                    if(isTrack == true && aud.isPlaying == false)
                    {
                        aud.Play();
                    }
                    Debug.Log("Cuenta de Lista: " + clipCount);
                }
                else
                {
                    clipCount = 0;
                }
            }
        }
        
        private void OnTrackingFound()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Enable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = true;
            }

            // Enable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = true;
            }

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
        }


        private void OnTrackingLost()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Disable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = false;
            }

            // Disable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = false;
            }

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
        }

        #endregion // PRIVATE_METHODS
    }
}
