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
        public float m_currentHealth;
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
            m_textHealthPoint.transform.position = Camera.main.WorldToScreenPoint(transform.position + (Vector3.down) * 0.75f);
            RefreshHealthBar();
        }

        public void RefreshHealthBar()
        {
                       
            m_textHealthPoint.text = m_currentHealth + "/" + m_characterStats.m_maxHealthPoints;         
        }

        // Start is called before the first frame update
        public float GetHealthPoint() {  return m_currentHealth; }
        
        public void GetHit(CharacterBase _attacker)
        {
            m_currentHealth = Mathf.Clamp(m_currentHealth - _attacker.GetAttackPoint(), 0,float.MaxValue);
        }
        
        private void OnMouseDown()
        {
            m_turnBasedController.selectionV2(this);
        }
        public float  GetAttackPoint()
        {
            return m_characterStats.m_damagePoints;
        }
        public void Deselect() { }

        // A finir Apres
        public void ConnectToTurnBasedController(TurnBasedController _controller)
        {
            m_turnBasedController = _controller;
        }


    }
   
}
