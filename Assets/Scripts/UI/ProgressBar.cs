using UnityEngine;
using UnityEngine.UI;

namespace GameStudioTest1.UI
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Image _fillingImage;


        private void OnValidate()
        {
            Debug.Assert(_fillingImage != null, $"{nameof(_fillingImage)} need to be assigned", this);
        }
        public void SetProgress01(float progress)
        {
            _fillingImage.fillAmount = progress;
        }

    }
}