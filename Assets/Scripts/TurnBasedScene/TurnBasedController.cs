using COM.David.TurnCharacter;
using System;
using System.Collections;
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
        [SerializeField] public GameObject m_selectionTarget;
        [SerializeField] public GameObject m_attackButton;

        public static CharacterBase m_currentSelectedCharacter;
        public static CharacterBase m_currentSelectedTarget;

        public enum STATES
        {
            PLAYER,
            CPU
        }

        private TextMeshProUGUI m_textPlayer;
        private TextMeshProUGUI m_textGameResult;
        STATES currentState;
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
            currentState = STATES.PLAYER;
            DisplayCurrentPlayer();
            DeselectAll();
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
            if(m_currentSelectedCharacter && m_currentSelectedTarget) m_attackButton.SetActive(true);

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
            m_selectionPointer.transform.position = (Vector2) _character.transform.position + Vector2.up*1f;
            if (m_currentSelectedCharacter != null) m_currentSelectedCharacter.Deselect();
            m_currentSelectedCharacter = _character;
        }
        public void SetTarget(CharacterBase _character)
        {
            m_selectionTarget.SetActive(true);
            m_selectionTarget.transform.position = (Vector2)_character.transform.position + Vector2.up;
            if (m_currentSelectedTarget != null) m_currentSelectedTarget.Deselect();
            m_currentSelectedTarget = _character;
        }

        public void selectionV2(CharacterBase _character)
        {
            switch( currentState)
            {
                case STATES.PLAYER :
                    if ((STATES)_character.m_type == STATES.PLAYER) { Select(_character); } else { SetTarget(_character); }
                    break;
                case STATES.CPU :
                    if ((STATES)_character.m_type == STATES.PLAYER) { SetTarget(_character); } else { Select(_character); }
                    break;
            }
        }
        public void Attack()
        {
            m_currentSelectedTarget.GetHit(m_currentSelectedCharacter);
            m_currentSelectedTarget.RefreshHealthBar();
            NextTurn();
        }
        public void NextTurn()
        {
            DeselectAll();
            currentState = (currentState == STATES.CPU) ? STATES.PLAYER : STATES.CPU;
            if (currentState == STATES.CPU) StartCoroutine("SimulateCPUTurn");
            DisplayCurrentPlayer();
        }

        IEnumerator SimulateCPUTurn()
        {
            int _source = UnityEngine.Random.Range(0, m_cpuTeam.Count);
            int _target = UnityEngine.Random.Range(0, m_playerTeam.Count);
            selectionV2(m_cpuTeam[_source]);          
            yield return new WaitForSeconds(2f);
            selectionV2(m_playerTeam[_target]);
            yield return new WaitForSeconds(2f);
            Attack();
        }

        private void DeselectAll()
        {
            m_currentSelectedTarget = null;
            m_currentSelectedCharacter = null;
            m_selectionTarget.SetActive(false);
            m_selectionPointer.SetActive(false);
            m_attackButton.SetActive(false);
        }
        public void DisplayCurrentPlayer()
        {
            switch (currentState)
            {
                case STATES.PLAYER:
                    m_textPlayer.text = "Player";
                    break;
                case STATES.CPU:
                    m_textPlayer.text = "CPU";
                    break;
            }
        }
    }
}

