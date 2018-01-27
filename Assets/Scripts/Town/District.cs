using System;
using System.Collections;
using Game;
using GameTime;
using UnityEngine;

namespace Town
{
    public class District : MonoBehaviour
    {
        public int Level = 1;
        public int Peoples = 10000;

        private float _growthRate = 0.1f;
        private Coroutine _coroutine;
        private bool _gameIsPaused = false;
        private readonly System.Random _random = new System.Random();

        private void Awake()
        {
            TimeManager.OnTimerPaused += OnTimerPaused;
            TimeManager.OnTimerResumed += OnTimerResumed;
        }

        private void OnTimerResumed(TimeManager.GameTime time)
        {
            _gameIsPaused = false;
        }

        private void OnTimerPaused(TimeManager.GameTime time)
        {
            _gameIsPaused = true;
        }

        private void Start()
        {
            Peoples = _random.Next(2, 9);
            _coroutine = StartCoroutine(GrowthDistrict());
        }

        private void OnDestroy()
        {
            StopCoroutine(_coroutine);
            TimeManager.OnTimerPaused -= OnTimerPaused;
            TimeManager.OnTimerResumed -= OnTimerResumed;
        }

        private IEnumerator GrowthDistrict()
        {
            while (true)
            {
                float waitTime = _random.Next(2, 6);
                yield return new WaitForSeconds(waitTime * Time.timeScale);

                if (_gameIsPaused)
                {
                    while (_gameIsPaused)
                        yield return null;
                }

                float variation = _random.Next(-15, 16) / 1000f;
                Peoples += (int)Math.Ceiling(Peoples * (_growthRate + variation));

                if (Peoples >= 500 && Level != 5)
                {
                    Level = 5;
                    UpgradeDistrict(GameManager.Instance.TownExpensionManager.GetBuilding(Level));
                }
                else if (Peoples >= 100 && Peoples < 500 && Level != 4)
                {
                    Level = 4;
                    UpgradeDistrict(GameManager.Instance.TownExpensionManager.GetBuilding(Level));
                }
                else if (Peoples >= 30 && Peoples < 100 && Level != 3)
                {
                    Level = 3;
                    UpgradeDistrict(GameManager.Instance.TownExpensionManager.GetBuilding(Level));
                }
                else if (Peoples >= 12 && Peoples < 30 && Level != 2)
                {
                    Level = 2;
                    UpgradeDistrict(GameManager.Instance.TownExpensionManager.GetBuilding(Level));
                }
                else if (Peoples < 12 && Level != 1)
                {
                    Level = 1;
                    UpgradeDistrict(GameManager.Instance.TownExpensionManager.GetBuilding(Level));
                }
            }
        }

        private void UpgradeDistrict(GameObject building)
        {
            if (building != null)
            {
                GameObject newBuilding = Instantiate(building);
                Destroy(transform.GetChild(0).gameObject);
                newBuilding.transform.SetParent(transform, false);
                int rotation = _random.Next(0, 4);
                newBuilding.transform.localRotation = Quaternion.Euler(new Vector3(0, 90 * rotation, 0));
            }
        }
    }
}