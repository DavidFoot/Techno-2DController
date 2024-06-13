using COM.David.TurnCharacter;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace COM.David.TurnManager
{
    public class TurnBasedController : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] GameObject m_displayCurrentPlayer;
        [SerializeField] GameObject m_displayResultat;
        [SerializeField] List<CharacterBase> m_playerTeam;
        [SerializeField] List<CharacterBase> m_cpuTeam;
        [SerializeField] public GameObject m_selectionPointer;
        public static CharacterBase m_currentSelectedCharacter;
        enum STATE
        {
            PLAYER,
            CPU
        }

        private TextMeshProUGUI m_textPlayer;
        private TextMeshProUGUI m_textGameResult;
        STATE currentState;
        private bool playerIsDead;
        private bool cpuIsDead;
        private void Awake()
        {
            ConnectWithCharacterBase(m_playerTeam);
            ConnectWithCharacterBase(m_cpuTeam);
        }

        private void ConnectWithCharacterBase(List<CharacterBase> _team)
        {
            foreach(CharacterBase character in _team) {
                character.ConnectToTurnBasedController(this);
            }
        }

        void Start()
        {
            m_textPlayer = m_displayCurrentPlayer.GetComponent<TextMeshProUGUI>();
            m_textGameResult = m_displayResultat.GetComponent<TextMeshProUGUI>();
            currentState = STATE.PLAYER;
            DisplayCurrentPlayer();
        }

        // Update is called once per frame
        void Update()
        {
            playerIsDead = IsDeadTeamFor(m_playerTeam);
            cpuIsDead = IsDeadTeamFor(m_cpuTeam);
            if (playerIsDead)
            {
                m_textGameResult.gameObject.SetActive(true);
                m_textGameResult.text = "Equipe Joueur est morte";
            }
            if (cpuIsDead)
            {
                m_textGameResult.gameObject.SetActive(true);
                m_textGameResult.text = "Equipe CPU est morte";
            }
        }

        public bool IsDeadTeamFor(List<CharacterBase> _team)
        {
            // a Remplacer par un for pour question de performance
            for (int i = 0;i< _team.Count;i++)
            {
                if (_team[i].GetHealthPoint() > 0) return false;              
            }
            return true;
        }
        public void Select(CharacterBase _character)
        {
            m_selectionPointer.SetActive(true);
            m_selectionPointer.transform.position = (Vector2) _character.transform.position + Vector2.up*1.5f;

            if (m_currentSelectedCharacter != null) m_currentSelectedCharacter.Deselect();
            m_currentSelectedCharacter = _character;
        }
        public void NextTurn()
        {
            currentState = (currentState == STATE.CPU) ? STATE.PLAYER : STATE.CPU;
            Debug.Log(currentState);
            DisplayCurrentPlayer();
        }
        public void DisplayCurrentPlayer()
        {
            switch (currentState)
            {
                case STATE.PLAYER:
                    m_textPlayer.text = "Player";
                    break;
                case STATE.CPU:
                    m_textPlayer.text = "CPU";
                    break;
            }
        }
    }
}

