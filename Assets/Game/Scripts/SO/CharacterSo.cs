using UnityEngine;

namespace Game.So
{
    [CreateAssetMenu(menuName = "So/Data/Character", fileName = "Character_T_SO")]
    public class CharacterSo : ScriptableObject
    {
        [Header("Character Data")] 
        
        [SerializeField] private int _initialLife;
        [SerializeField] private int _currentLife;

        public int InitialLife => _initialLife;
        public int CurrentLife => _currentLife;

        public void IncrementLife(int value)
        {
            if (value <= 0) return;
            _currentLife += value;
        }
        public void DecrementLife(int value)
        {
            if (value <= 0) return;
            _currentLife -= value;
        }
        
        private void OnEnable()
        {
            ResetData();
        }

        public void ResetData()
        {
            _currentLife = _initialLife;
        }
    }
}
