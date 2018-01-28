using System.Collections.Generic;
using System.Globalization;
using BusinessCore;
using Game;
using Town;
using Translation;
using UnityEngine;
using UnityEngine.UI;

namespace Map
{
    public class MapManager : MonoBehaviour
    {
        [Header("Map")]
        [SerializeField]
        private GameObject _prefabMap;

        [SerializeField]
        private GameObject _prefabCell;

        [SerializeField]
        private Material _selectedMaterial;

        [SerializeField]
        private Material _unSelectedMaterial;

        [Header("GUI")]
        [SerializeField]
        private GameObject _buildMenu;

        [SerializeField]
        private GameObject _infosMenu;

        [SerializeField]
        private GameObject _disctrictMenu;

        [SerializeField]
        private Text _infoNameText;

        [SerializeField]
        private Text _infoTypeText;

        [SerializeField]
        private Text _infoMaintenanceCostText;

        [SerializeField]
        private Text _infoLevelDistrictText;

        [SerializeField]
        private Text _infoPeopleDistrictText;

        private Dictionary<Vector2, GameObject> _mapCells;
        private GameObject _map;
        private Cell _currentCell = null;

        private void Awake()
        {
            _mapCells = new Dictionary<Vector2, GameObject>();
        }

        private void Start()
        {
            _infosMenu.SetActive(false);
            _buildMenu.SetActive(false);
            _disctrictMenu.SetActive(false);
        }

        public Cell GetCell(float x, float y)
        {
            Vector2 vector = new Vector2(x, y);
            if (_mapCells.ContainsKey(vector))
            {
                return _mapCells[vector].GetComponent<Cell>();
            }

            return null;
        }

        public void CreateMap(int height, int width)
        {
            _map = Instantiate(_prefabMap);
            _map.name = "Map_" + height + "x" + width;
            _map.transform.localPosition = Vector3.zero;
            _map.transform.localScale = new Vector3(height, 1, width);
            _map.GetComponent<MeshRenderer>().material.mainTextureScale = new Vector2(2.5f * height, 2.5f * width);

            float centerHeight = (float)height / 2 - 0.5f;
            float centerWidth = (float)width / 2 - 0.5f;
            float factorHeight = (float)height / 10;
            float factorWidth = (float)width / 10;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    GameObject cell = Instantiate(_prefabCell);

                    cell.transform.SetParent(_map.transform, true);
                    cell.transform.localPosition = new Vector3((i - centerHeight) / factorHeight, 0.01f, (j - centerWidth) / factorWidth);

                    cell.GetComponent<Cell>().X = i - centerHeight - 0.5f;
                    cell.GetComponent<Cell>().Y = j - centerWidth - 0.5f;

                    cell.name = "Cell_" + cell.GetComponent<Cell>().X + "x" + cell.GetComponent<Cell>().Y;

                    _mapCells.Add(new Vector2(cell.GetComponent<Cell>().X, cell.GetComponent<Cell>().Y), cell);
                }
            }
        }

        public void ClickOnCell(Cell cell)
        {
            _buildMenu.SetActive(false);
            _disctrictMenu.SetActive(false);
            _infosMenu.SetActive(false);

            if (_currentCell == cell)
            {
                _currentCell.GetComponent<MeshRenderer>().material = _unSelectedMaterial;
                _currentCell = null;
                return;
            }

            if (_currentCell != null)
            {
                _currentCell.GetComponent<MeshRenderer>().material = _unSelectedMaterial;
            }

            _currentCell = cell;
            cell.GetComponent<MeshRenderer>().material = _selectedMaterial;

            if (!cell.IsConstructible)
            {
                District district = cell.Building.GetComponent<District>();
                _infoLevelDistrictText.text = district.Level.ToString(CultureInfo.InvariantCulture);
                _infoPeopleDistrictText.text = district.Peoples.ToString(CultureInfo.InvariantCulture);

                _disctrictMenu.SetActive(true);
            }
            else if (!cell.HaveBuilding) // Si la cellule n'a rien dessus et qu'elle est constructible
            {
                _buildMenu.SetActive(true);
            }
            else if (cell.HaveBuilding) // Si la cellule a un truc dessus
            {
                IInfrastructure infrastructure = cell.Building.GetComponent<IInfrastructure>();

                _infoMaintenanceCostText.text = infrastructure.MaintenanceCost.ToString(CultureInfo.InvariantCulture);
                _infoNameText.text = infrastructure.Name;
                _infoTypeText.GetComponent<TranslateText>().SetText(infrastructure.InfrastructureType == InfrastructureType.CellularNetwork ? "Cellular" : "Wired");

                _infosMenu.SetActive(true);
            }
        }

        public void BuildOnCurrentCell(GameObject building)
        {
            if (_currentCell.IsConstructible && !_currentCell.HaveBuilding)
            {
                GameObject infrastructure = Instantiate(building);
                infrastructure.SetActive(false);
                if (!GameManager.Instance.BusinessManager.CanBuild(infrastructure.GetComponent<IInfrastructure>()))
                {
                    Debug.Log("Cannot build");
                    Destroy(infrastructure);
                    return;
                }
                GameManager.Instance.BusinessManager.Build(infrastructure.GetComponent<IInfrastructure>());
                infrastructure.SetActive(true);
                infrastructure.transform.SetParent(_currentCell.transform, false);

                _currentCell.Building = infrastructure;
                _currentCell.HaveBuilding = true;
            }

            _buildMenu.SetActive(false);
            _currentCell.GetComponent<MeshRenderer>().material = _unSelectedMaterial;
            _currentCell = null;
        }

        public void UpgradeTechnologyOnCurrentCell()
        {
            if (_currentCell.IsConstructible && _currentCell.HaveBuilding)
            {
                IInfrastructure infrastructure = _currentCell.Building.GetComponent<IInfrastructure>();
                if (infrastructure == null)
                    return;
                if (!GameManager.Instance.BusinessManager.CanUpgradeTechnology(infrastructure))
                {
                    Debug.Log("Cannot update");
                    return;
                }
                GameManager.Instance.BusinessManager.UpgradeTechnology(infrastructure);
            }

            _infosMenu.SetActive(false);
            _currentCell.GetComponent<MeshRenderer>().material = _unSelectedMaterial;
            _currentCell = null;
        }

        public void UpgradeCapacityOnCurrentCell()
        {
            if (_currentCell.IsConstructible && _currentCell.HaveBuilding)
            {
                IInfrastructure infrastructure = _currentCell.Building.GetComponent<IInfrastructure>();
                if (infrastructure == null)
                    return;
                if (!GameManager.Instance.BusinessManager.CanUpgradeCapacity(infrastructure))
                {
                    Debug.Log("Cannot update");
                    return;
                }
                GameManager.Instance.BusinessManager.UpgradeCapacity(infrastructure);
            }

            _infosMenu.SetActive(false);
            _currentCell.GetComponent<MeshRenderer>().material = _unSelectedMaterial;
            _currentCell = null;
        }

        public void DestroyOnCurrentCell()
        {
            if (_currentCell.IsConstructible && _currentCell.HaveBuilding)
            {
                // TODO

                DestroyImmediate(_currentCell.Building);
                _currentCell.Building = null;
                _currentCell.HaveBuilding = false;
            }

            _infosMenu.SetActive(false);
            _currentCell.GetComponent<MeshRenderer>().material = _unSelectedMaterial;
            _currentCell = null;
        }
    }
}