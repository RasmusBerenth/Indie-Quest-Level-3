using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    public class Creature
    {
        protected string _displayName;

        protected int _hitPointMaximum;
        protected int _hitPoints;

        private float _spaceInFeet;

        protected Sprite _bodySprite;

        private CreaturePresenter _presenter;

        public string displayName => _displayName;

        public int hitPointMaximum => _hitPointMaximum;
        public int hitPoints
        {
            get => _hitPoints;
            set => _hitPoints = Mathf.Max(0, value);
        }

        public float spaceInFeet => _spaceInFeet;

        public Sprite bodySprite => _bodySprite;

        public CreaturePresenter presenter => _presenter;


        public Creature(int hitPointMaximum, string monsterName, Sprite bodySprite, SizeCategory sizeCategory)
        {
            hitPoints = hitPointMaximum;
            _displayName = monsterName;
            _bodySprite = bodySprite;
            SizeHelper.spaceInFeetPerSizeCategory.TryGetValue(sizeCategory, out _spaceInFeet);
        }

        public void InitializePresenter(CreaturePresenter presenter)
        {
            _presenter = presenter;
        }

        public void ReactToDamage(int damageAmount)
        {
            _hitPoints = Mathf.Max(0, _hitPoints - damageAmount);
        }
    }
}
