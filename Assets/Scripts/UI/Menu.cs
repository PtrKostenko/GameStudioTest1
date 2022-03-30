using UnityEngine;

namespace GameStudioTest1.UI
{
    public abstract class Menu : MonoBehaviour
    {
        public virtual void Activate()
        {
            this.gameObject.SetActive(true);
        }
        public virtual void Deactivate()
        {
            this.gameObject.SetActive(false);
        }
    }
}