using COM.David.TurnManager;
using UnityEngine;

namespace COM.David.TurnCharacter
{
    public class CharacterBase : MonoBehaviour
    {
        
        [SerializeField] public int m_characterHealth;
        [SerializeField] public STATE m_type;
        private TurnBasedController m_turnBasedController;
        private bool isSelected;

        public enum STATE
        {
            PLAYER,
            CPU
        }

        // Start is called before the first frame update
        public int GetHealthPoint() {  return m_characterHealth; }
        private void OnMouseDown()
        {
            isSelected = true;

            switch (m_type)
            {
                case STATE.PLAYER:
                    m_turnBasedController.Select(this);
                    break; 
                
                case STATE.CPU:
                    m_turnBasedController.SetTarget(this);
                    break;
            }
            
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
