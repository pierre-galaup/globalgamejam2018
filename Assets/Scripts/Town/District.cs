using System;
using System.Collections;
using Game;
using GameTime;
using Map;
using UnityEngine;

namespace Town
{
    public class District : MonoBehaviour
    {
        public int Level = 1;
        public int Peoples = 0;

        private float _growthRate = 0.1f;
        private Coroutine _coroutine;
        private bool _gameIsPaused = false;
        private readonly System.Random _random = new System.Random();
        private Cell _cell;

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
            _cell = transform.parent.GetComponent<Cell>();
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

                if (_cell != null && _cell.InNetworkRange)
                    Peoples += (int)Math.Ceiling(Peoples * (_growthRate + variation));
                else
                    Peoples += (int)Math.Ceiling(Peoples * (_growthRate + variation) * 0.02);

                if (!_cell.InNetworkRange && Peoples >= 12)
                    Peoples = 11;

                if (Peoples > 700)
                    Peoples = 700;

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