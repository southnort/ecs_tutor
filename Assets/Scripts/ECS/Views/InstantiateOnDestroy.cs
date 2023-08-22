using UnityEngine;


namespace Game
{
    internal sealed class InstantiateOnDestroy : MonoBehaviour
    {
        [SerializeField] private GameObject instantiateOnDestroy;
        private bool _isQuitting;


        private void OnApplicationQuit()
        {
            _isQuitting = true;
        }

        private void OnDestroy()
        {
            if (!_isQuitting)
            {
                Instantiate(instantiateOnDestroy, transform.position, transform.rotation);
            }
        }
    }
}
