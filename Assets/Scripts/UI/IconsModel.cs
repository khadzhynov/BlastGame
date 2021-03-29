using BlastGame.Core.Models;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace BlastGame.UI
{
    [CreateAssetMenu]
    public class IconsModel : ScriptableObject
    {
        [Serializable]
        private class GoalIconItem
        {
            [SerializeField]
            private GoalType _goalType;

            [SerializeField]
            private int _id;

            [SerializeField]
            private Sprite _icon;

            public GoalType GoalType => _goalType;
            public int Id => _id;
            public Sprite Icon => _icon;
        }

        [SerializeField]
        private List<GoalIconItem> _goalIcons;

        public Sprite GetGoalIcon(GoalType goalType, int id)
        {
            var goalItem = _goalIcons.Find(x => x.GoalType == goalType && x.Id == id);
            Assert.IsNotNull(goalItem);
            return goalItem.Icon;
        }
    }
}