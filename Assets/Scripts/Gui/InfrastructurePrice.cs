using System.Globalization;
using BusinessCore;
using UnityEngine;
using UnityEngine.UI;

namespace Gui
{
    public class InfrastructurePrice : MonoBehaviour
    {
        [SerializeField] private GameObject _infrastructure;

        private void Start()
        {
            GameObject infra = Instantiate(_infrastructure);
            infra.SetActive(false);
            GetComponent<Text>().text = infra.GetComponent<IInfrastructure>().BuildCost.ToString(CultureInfo.InvariantCulture) + " €";
            Destroy(infra);
        }
    }
}