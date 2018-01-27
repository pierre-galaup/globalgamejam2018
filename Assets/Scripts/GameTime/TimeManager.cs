using System;
using System.Collections;
using UnityEngine;

namespace GameTime
{
    public class TimeManager : MonoBehaviour
    {
        public delegate void StartTimerEvent(GameTime time);

        public static event StartTimerEvent OnTimerStarted;

        public delegate void StopTimerEvent(GameTime time);

        public static event StopTimerEvent OnTimerStopped;

        public delegate void PauseTimerEvent(GameTime time);

        public static event PauseTimerEvent OnTimerPaused;

        public delegate void ResumeTimerEvent(GameTime time);

        public static event ResumeTimerEvent OnTimerResumed;

        public delegate void NewMonthEvent(GameTime time);

        public static event NewMonthEvent OnNewMonth;

        public delegate void NewYearEvent(GameTime time);

        public static event NewYearEvent OnNewYear;

        public class GameTime : IEquatable<GameTime>
        {
            public int Months;

            public int Years;

            public bool Equals(GameTime other)
            {
                if (ReferenceEquals(null, other))
                    return false;
                if (ReferenceEquals(this, other))
                    return true;
                return Months == other.Months && Years == other.Years;
            }

            public static bool operator <(GameTime left, GameTime right)
            {
                return left.Years < right.Years || left.Years == right.Years && left.Months < right.Months;
            }

            public static bool operator >(GameTime left, GameTime right)
            {
                return left.Years > right.Years || left.Years == right.Years && left.Months > right.Months;
            }

            public static bool operator <=(GameTime left, GameTime right)
            {
                return left.Years <= right.Years || left.Years == right.Years && left.Months <= right.Months;
            }

            public static bool operator >=(GameTime left, GameTime right)
            {
                return left.Years >= right.Years || left.Years == right.Years && left.Months >= right.Months;
            }
        }

        [SerializeField]
        private int _beginYear = 1980;

        [SerializeField]
        private int _realSecondsForOneMonth = 10;

        private float _timer = 0;
        private bool _isPaused = false;
        private bool _timerIsOn = false;
        private Coroutine _coroutine;

        public void StartTimer()
        {
            _timer = 0;
            _timerIsOn = true;
            OnTimerStarted?.Invoke(GetGameTime());
            OnNewMonth?.Invoke(GetGameTime());
            OnNewYear?.Invoke(GetGameTime());
            _coroutine = StartCoroutine(NextTime());
        }

        public void StopTimer()
        {
            _isPaused = true;
            _timerIsOn = false;
            StopCoroutine(_coroutine);
            OnTimerStopped?.Invoke(GetGameTime());
        }

        public void PauseTimer()
        {
            _isPaused = true;
            OnTimerPaused?.Invoke(GetGameTime());
        }

        public void UnPauseTimer()
        {
            _isPaused = false;
            OnTimerResumed?.Invoke(GetGameTime());
        }

        public GameTime GetGameTime()
        {
            int months = (int)_timer / _realSecondsForOneMonth + 1;
            int years = months / 12;
            months %= 12;

            return new GameTime
            {
                Months = months,
                Years = _beginYear + years
            };
        }

        private IEnumerator NextTime()
        {
            while (_timerIsOn)
            {
                if (_isPaused)
                    continue;

                yield return new WaitForSeconds(1 * Time.timeScale);
                _timer += 1;

                if ((int)_timer % _realSecondsForOneMonth == 0)
                {
                    OnNewMonth?.Invoke(GetGameTime());
                }

                if ((int)_timer % (_realSecondsForOneMonth * 12) == 0)
                {
                    OnNewYear?.Invoke(GetGameTime());
                }

                //
            }
        }
    }
}