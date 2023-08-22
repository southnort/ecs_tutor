using UnityEngine;
using Yrr.Utils;


namespace Game
{
    internal sealed class Explosion : MonoBehaviour
    {
        [SerializeField] private GameObject[] randomEffects;

        private void OnEnable()
        {
            randomEffects.GetRandomItem().SetActive(true);
        }
    }
}
