using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using GameTime;
using Map;
using UnityEngine;

namespace Town
{
    public class TownExpensionManager : MonoBehaviour
    {
        public delegate void TotalPeopleEvent(int peoples);

        public static event TotalPeopleEvent OnNewPeople;

        [Serializable]
        public class DisctrictBuilding
        {
            public GameObject Building;
            public int Level = 1;
        }

        [SerializeField]
        private List<DisctrictBuilding> _disctricts;

        private readonly List<District> _allDistricts = new List<District>();

        private Cell _beginCell;
        private readonly System.Random _random = new System.Random();

        private void Awake()
        {
            TimeManager.OnTimerStarted += OnTimerStarted;
            TimeManager.OnNewMonth += OnNewMonth;
        }

        private void OnNewMonth(TimeManager.GameTime time)
        {
            if (time.Months % 3 == 0)
            {
                int number = _random.Next(0, 101);
                int numberOfBuildings = 1;

                if (number == 100)
                {
                    numberOfBuildings = 3;
                }
                else if (number >= 89)
                {
                    numberOfBuildings = 2;
                }

                TestToConstruct(_beginCell, -1, numberOfBuildings);
            }
        }

        private void TestToConstruct(Cell centerCell, int lastNumber, int numberOfBuildingRemaining)
        {
            Cell cell = centerCell;
            float x = centerCell.X;
            float y = centerCell.Y;
            bool result = false;
            int number = _random.Next(0, 4);

            if (number != lastNumber)
            {
                switch (number)
                {
                    case 0:
                        cell = GameManager.Instance.MapManager.GetCell(x, y + 1);
                        break;

                    case 1:
                        cell = GameManager.Instance.MapManager.GetCell(x, y - 1);
                        break;

                    case 2:
                        cell = GameManager.Instance.MapManager.GetCell(x + 1, y);
                        break;

                    case 3:
                        cell = GameManager.Instance.MapManager.GetCell(x - 1, y);
                        break;
                }

                if (cell == null)
                {
                    --numberOfBuildingRemaining;
                    TestToConstruct(centerCell, number, numberOfBuildingRemaining);
                    return;
                }

                result = ConstructNewDistrict(cell, GetBuilding(1));

                if (result)
                    --numberOfBuildingRemaining;
            }

            if (!result || numberOfBuildingRemaining != 0)
            {
                TestToConstruct(cell, number, numberOfBuildingRemaining);
            }
        }

        private void OnDestroy()
        {
            TimeManager.OnTimerStarted -= OnTimerStarted;
            TimeManager.OnNewMonth -= OnNewMonth;
        }

        private void OnTimerStarted(TimeManager.GameTime time)
        {
            _beginCell = GameManager.Instance.MapManager.GetCell(0, 0);
            ConstructNewDistrict(_beginCell, GetBuilding(1));

            StartCoroutine(SendEvent());
        }

        private IEnumerator SendEvent()
        {
            while (true)
            {
                yield return new WaitForSeconds(2 * Time.timeScale);
                OnNewPeople?.Invoke(GetPeoplesNumber());
            }
        }

        private bool ConstructNewDistrict(Cell cell, GameObject disctrict)
        {
            if (cell.IsConstructible && !cell.HaveBuilding)
            {
                GameObject disctrictGameObject = new GameObject("District");
                disctrictGameObject.AddComponent<District>();
                _allDistricts.Add(disctrictGameObject.GetComponent<District>());

                GameObject disctrictBuilding = Instantiate(disctrict);
                disctrictBuilding.transform.SetParent(disctrictGameObject.transform, false);
                disctrictGameObject.transform.SetParent(cell.transform, false);

                cell.Building = disctrictGameObject;
                cell.HaveBuilding = true;
                cell.IsConstructible = false;
                return true;
            }

            return false;
        }

        public int GetPeoplesNumber()
        {
            int number = 0;
            foreach (District district in _allDistricts)
            {
                number += district.Peoples;
            }

            return number;
        }

        private void ShuffleList<T>(IList<T> list)
        {
            int n = list.Count;
            System.Random rnd = new System.Random();
            while (n > 1)
            {
                int k = rnd.Next(0, n) % n;
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public GameObject GetBuilding(int level)
        {
            ShuffleList(_disctricts);
            foreach (DisctrictBuilding disctrictBuilding in _disctricts)
            {
                if (disctrictBuilding.Level == level)
                    return disctrictBuilding.Building;
            }

            return null;
        }
    }
}