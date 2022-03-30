using UnityEngine;

namespace GameStudioTest1
{
    class TimescalePauseSystem : PauseSystem
    {
        private float _timeScaleBeforePause = 1;

        public override void Pause()
        {
            _timeScaleBeforePause = Time.timeScale;
            Time.timeScale = 0;
        }

        public override void Unpause()
        {
            Time.timeScale = _timeScaleBeforePause;
        }
    }
}
