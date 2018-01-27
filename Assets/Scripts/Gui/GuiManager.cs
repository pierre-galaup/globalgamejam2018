using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameTime;
using UnityEngine.UI;
using Town;

namespace Gui
{
    public class GuiManager : MonoBehaviour
    {
        [SerializeField]
        private Text textMonth;

        [SerializeField]
        private Text textYear;

        [SerializeField]
        private Text textPeople;
        

        private void Awake()
        {
            TownExpensionManager.OnNewPeople += ViewPeople;
            TimeManager.OnNewMonth += ViewMonth;
        }

        private void ViewPeople(int people)
        {
            textPeople.text = "Habitants : " + people;
        }

        private void ViewMonth(TimeManager.GameTime time)
        {
            textMonth.text = "Month : " + time.Months;
            textYear.text = "Year : " + time.Years;
        }

        private void OnDestroy()
        {
            TownExpensionManager.OnNewPeople -= ViewPeople;
            TimeManager.OnNewMonth -= ViewMonth;
        }
        
    
    }
}
