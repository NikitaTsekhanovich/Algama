using System;
using Photon.Pun;
using UnityEngine;

namespace GameLogic.LevelHandlers
{
    public static class PhotonGarbageCollector
    {
        private static readonly string[] _objectTags = new []
        {
            "DeadPlayer",
            "Player",
            "PlayerAbility"
        };

        public static void FindUnnecessaryObjects()
        {
            foreach (var tag in _objectTags)
            {
                var garbage = GameObject.FindGameObjectsWithTag(tag);
                DestroyObjects(garbage);
            }
        }

        private static void DestroyObjects(GameObject[] garbage)
        {
            foreach (var obj in garbage)
            {
                PhotonNetwork.Destroy(obj);
            }
        }
    }
}
