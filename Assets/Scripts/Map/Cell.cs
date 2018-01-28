using Game;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Map
{
    public class Cell : MonoBehaviour
    {
        public float X = 0;
        public float Y = 0;

        public bool IsConstructible = true;
        public bool HaveBuilding = false;
        public bool InNetworkRange = false;

        public GameObject Building = null;

        public void OnMouseOver()
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                GameManager.Instance.MapManager.ClickOnCell(this);
            }
        }
    }
}