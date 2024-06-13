using COM.David.scriptableObject;
using COM.David.TurnManager;
using TMPro;
using UnityEngine;

namespace COM.David.TurnCharacter
{
    public class CharacterBase : MonoBehaviour
    {
        
        [SerializeField] public STATE m_type;
        [SerializeField] CharacterScriptableObject m_characterStats;
        private TurnBasedController m_turnBasedController;
        private int m_currentHealth;
        private TextMeshProUGUI m_textHealthPoint;
        public enum STATE
        {
            PLAYER,
            CPU
        }
        private void Awake()
        {
            m_currentHealth = m_characterStats.m_maxHealthPoints;
            m_textHealthPoint = GetComponentInChildren<TextMeshProUGUI>();
            m_textHealthPoint.text = m_currentHealth  + "/" + m_characterStats.m_maxHealthPoints;
            m_textHealthPoint.transform.position = Camera.main.WorldToScreenPoint(transform.position + (Vector3.down)*0.75f);
        }
        // Start is called before the first frame update
        public int GetHealthPoint() {  return m_characterStats.m_maxHealthPoints; }
        private void OnMouseDown()
        {
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


    }
   
}
