using COM.David.TurnManager;
using UnityEngine;

namespace COM.David.TurnCharacter
{
    public class CharacterBase : MonoBehaviour
    {
        
        [SerializeField] public int m_characterHealth;
        private TurnBasedController m_turnBasedController;

        private bool isSelected;

        // Start is called before the first frame update
        public int GetHealthPoint() {  return m_characterHealth; }
        private void OnMouseDown()
        {
            isSelected = true;
            m_turnBasedController.Select(this);
        }
        public void Deselect() { }

        // A finir Apres
        public void ConnectToTurnBasedController(TurnBasedController _controller)
        {
            m_turnBasedController = _controller;
        }
        public bool IsSelected() { return isSelected; }


    }
   
}
