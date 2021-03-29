using UnityEngine;

namespace BlastGame.Core.Models
{
    [CreateAssetMenu]
    public class GameplayModel : ScriptableObject
    {
        [SerializeField]
        private int _minNumberToCollapse = 2;

        [SerializeField]
        private int _maxColors = 5;

        [SerializeField]
        private float _maxDistanceToCollapse = 0.65f;

        [SerializeField]
        private int _initialStonesAmount = 50;

        [SerializeField]
        private bool _lockInputOnActions = true;

        [SerializeField]
        private bool _isInputLocked = false;

        public int MinNumberToCollapse => _minNumberToCollapse;
        public int MaxColors => _maxColors;
        public int InitialStonesAmount => _initialStonesAmount;
        public float MaxDistanceToCollapse => _maxDistanceToCollapse;

        public bool LockInputOnActions => _lockInputOnActions;
        public bool IsInputLocked
        {
            get => LockInputOnActions ? _isInputLocked : false;
            set => _isInputLocked = value;
        }
    }
}