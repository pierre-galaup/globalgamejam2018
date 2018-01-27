﻿using System.Collections.Generic;
using UnityEngine;

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
                // TODO : Display info sur le quartier
            }
            else if (!cell.HaveBuilding) // Si la cellule n'a rien dessus et qu'elle est constructible
            {
                _buildMenu.SetActive(true);
            }
            else if (cell.HaveBuilding) // Si la cellule a un truc dessus
            {
                _infosMenu.SetActive(true);
            }
        }

        public void BuildOnCurrentCell(GameObject building)
        {
            if (_currentCell.IsConstructible && !_currentCell.HaveBuilding)
            {
                GameObject infrastructure = Instantiate(building);
                infrastructure.transform.SetParent(_currentCell.transform, false);

                _currentCell.Building = infrastructure;
                _currentCell.HaveBuilding = true;
            }

            _buildMenu.SetActive(false);
            _currentCell.GetComponent<MeshRenderer>().material = _unSelectedMaterial;
            _currentCell = null;
        }

        public void InformationsOnCurrentCell()
        {
            if (_currentCell.IsConstructible && _currentCell.HaveBuilding)
            {
                // TODO : UI ?
            }
        }

        public void UpgradeOnCurrentCell()
        {
            if (_currentCell.IsConstructible && _currentCell.HaveBuilding)
            {
                // TODO : UI
            }

            _infosMenu.SetActive(false);
            _currentCell.GetComponent<MeshRenderer>().material = _unSelectedMaterial;
            _currentCell = null;
        }

        public void SellOnCurrentCell()
        {
            if (_currentCell.IsConstructible && _currentCell.HaveBuilding)
            {
                // TODO

                _currentCell.Building.SetActive(false);

                _currentCell.Building = null;
                _currentCell.HaveBuilding = false;
            }

            _infosMenu.SetActive(false);
            _currentCell.GetComponent<MeshRenderer>().material = _unSelectedMaterial;
            _currentCell = null;
        }
    }
}