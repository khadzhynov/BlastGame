using System;
using System.Collections.Generic;
using UnityEngine;

namespace BlastGame.Core.Models
{
    [CreateAssetMenu]
    public class LevelProgressModel : ScriptableObject
    {
        public event Action<int> OnMovesChanged;

        [SerializeField]
        private List<Goal> _goals;

        [SerializeField]
        private int _moves;

        [SerializeField]
        private int _initialMoves = 30;

        private bool _hasWin = false;

        public List<Goal> Goals => _goals;

        public int InitialMoves => _initialMoves;

        public int Moves
        {
            get => _moves;

            set
            {
                if (value != _moves)
                {
                    _moves = value;
                    OnMovesChanged?.Invoke(_moves);
                }
            }
        }

        public bool HasWin { get => _hasWin; set => _hasWin = value; }
    }
}