using System;
using System.Collections.Generic;
using GameTime;
using Map;
using UnityEngine;

namespace Town
{
    public class TownExpensionManager : MonoBehaviour
    {
        [Serializable]
        public class Disctrict
        {
            public GameObject Building;
            public int Level = 1;
        }

        [SerializeField]
        private List<Disctrict> _disctricts;

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
                        cell = GameObject.Find("Cell_" + x + "x" + (y + 1)).GetComponent<Cell>();
                        break;

                    case 1:
                        cell = GameObject.Find("Cell_" + x + "x" + (y - 1)).GetComponent<Cell>();
                        break;

                    case 2:
                        cell = GameObject.Find("Cell_" + (x + 1) + "x" + y).GetComponent<Cell>();
                        break;

                    case 3:
                        cell = GameObject.Find("Cell_" + (x - 1) + "x" + y).GetComponent<Cell>();
                        break;
                }

                result = ConstructNewDistrict(cell, _disctricts[0]);

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
        }

        private void OnTimerStarted(TimeManager.GameTime time)
        {
            _beginCell = GameObject.Find("Cell_0x0").GetComponent<Cell>();
            ConstructNewDistrict(_beginCell, _disctricts[0]);
        }

        private bool ConstructNewDistrict(Cell cell, Disctrict disctrict)
        {
            if (cell.IsConstructible && !cell.HaveBuilding)
            {
                GameObject disctrictBuilding = Instantiate(disctrict.Building);
                disctrictBuilding.transform.SetParent(cell.transform, false);

                cell.Building = disctrictBuilding;
                cell.HaveBuilding = true;
                cell.IsConstructible = false;
                return true;
            }

            return false;
        }
    }
}