using TMPro;
using UnityEngine;

namespace YG
{
    public class PlayerName : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nameField;
        
        private void Awake()
        {
            _nameField.text = YG2.player.name;
        }
    }
}