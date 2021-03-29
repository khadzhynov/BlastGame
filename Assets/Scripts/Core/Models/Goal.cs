using System;
using UnityEngine;

namespace BlastGame.Core.Models
{
    [Serializable]
    public class Goal
    {
        public event Action<Goal> OnChanged;

        [SerializeField]
        private GoalType _goalType;

        [SerializeField]
        private int _id;

        [SerializeField]
        private int _target;

        private int _amount = 0;

        public GoalType GoalType => _goalType;
        public int Target => _target;
        public int Amount => _amount;
        public int Id => _id;


        public void Reset()
        {
            _amount = 0;
            OnChanged?.Invoke(this);
        }

        public bool IsReached()
        {
            return _amount >= _target;
        }

        public bool Increment()
        {
            return AddAmount(1);
        }
        public bool AddAmount(int amount)
        {
            _amount += amount;
            OnChanged?.Invoke(this);
            return IsReached();
        }
    }
}