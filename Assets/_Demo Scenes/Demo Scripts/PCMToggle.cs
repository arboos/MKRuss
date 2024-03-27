using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InfinityPBR.Old
{
    [System.Serializable]
    public class PCMToggle : MonoBehaviour
    {
        private PrefabChildManager pcm;
        public GameObject player;
        public bool findPlayerByTag = true;
        public float turnOnDistance = 10f;

        private List<bool> groupStatus = new List<bool>();

        private bool playerInRange = false;

        // Start is called before the first frame update
        void Awake()
        {
            pcm = GetComponent<PrefabChildManager>();
            if (findPlayerByTag)
            {
                player = GameObject.FindWithTag("Player");
            }

            if (pcm)
            {
                foreach (PrefabGroup group in pcm.prefabGroups)
                {
                    groupStatus.Add(group.isActive);
                }
            }
        }
    
        // Update is called once per frame
        void Update()
        {
            if (!pcm)
                return;

            if (ShouldToggleOn())
                ToggleOn(true);
            else if (ShouldToggleOff())
                ToggleOn(false);
        }

        private bool ShouldToggleOn()
        {
            if (Vector3.Distance(player.transform.position, transform.position) < turnOnDistance && !playerInRange)
            {
                playerInRange = true;
                return true;
            }
                
            return false;
        }
        
        private bool ShouldToggleOff()
        {
            if (Vector3.Distance(player.transform.position, transform.position) > turnOnDistance && playerInRange)
            {
                playerInRange = false;
                return true;
            }
            return false;
        }

        private void ToggleOn(bool on)
        {
            for (int i = 0; i < pcm.prefabGroups.Count; i++)
            {
                if (groupStatus[i])
                {
                    if (on)
                        pcm.ActivateGroup(i);
                    else
                        pcm.DeactivateGroup(i);
                }
            }
        }
    }
}

