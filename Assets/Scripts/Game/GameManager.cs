using GameTime;
using Map;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [HideInInspector]
        public TimeManager TimeManager;

        [HideInInspector]
        public MapManager MapManager;

        [SerializeField]
        private int _heightMap = 20;

        [SerializeField]
        private int _widthMap = 20;

        // Use this for initialization
        private void Awake()
        {
            if (Instance == null)
                Instance = this;

            TimeManager = transform.parent.GetComponentInChildren<TimeManager>();
            MapManager = transform.parent.GetComponentInChildren<MapManager>();
        }

        private void Start()
        {
            StartGame();
        }

        public void StartGame()
        {
            MapManager.CreateMap(_heightMap, _widthMap);
            TimeManager.StartTimer();
        }

        public void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}