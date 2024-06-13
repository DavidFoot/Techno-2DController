using UnityEngine;
namespace COM.David.scriptableObject
{
    [CreateAssetMenu(menuName ="Turnbase/stats")]
    public class CharacterScriptableObject : ScriptableObject
    {
        public enum AttackType
        {
            BASIC,
            MAGIC,
            HEAL
        }
        public AttackType m_type;
        public int m_maxHealthPoints;
        public float m_damagePoints;
        public int m_magicPoints;
        public GameObject m_attackParticleSystem;
    }

}

