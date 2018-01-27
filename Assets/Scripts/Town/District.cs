﻿using System.Collections;
using Game;
using GameTime;
using UnityEngine;

namespace Town
{
    public class District : MonoBehaviour
    {
        public int Level = 1;
        public int Peoples = 10000;

        private float _growthRate = 0.05f;
        private Coroutine _coroutine;
        private bool _gameIsPaused = false;

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
                if (_gameIsPaused)
                {
                    while (_gameIsPaused)
                        yield return null;
                }

                yield return new WaitForSeconds(2 * Time.timeScale);

                Peoples += (int)(Peoples * _growthRate);

                if (Peoples >= 120000 && Level != 5)
                {
                    Level = 5;
                    UpgradeDistrict(GameManager.Instance.TownExpensionManager.GetBuilding(Level));
                }
                else if (Peoples >= 80000 && Level != 4)
                {
                    Level = 4;
                    UpgradeDistrict(GameManager.Instance.TownExpensionManager.GetBuilding(Level));
                }
                else if (Peoples >= 50000 && Level != 3)
                {
                    Level = 3;
                    UpgradeDistrict(GameManager.Instance.TownExpensionManager.GetBuilding(Level));
                }
                else if (Peoples >= 30000 && Level != 2)
                {
                    Level = 2;
                    UpgradeDistrict(GameManager.Instance.TownExpensionManager.GetBuilding(Level));
                }
                else if (Peoples < 30000 && Level != 1)
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
            }
        }
    }
}