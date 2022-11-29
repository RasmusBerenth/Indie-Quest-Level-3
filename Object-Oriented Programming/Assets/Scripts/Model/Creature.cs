using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    public class Creature
    {
        protected string _displayName;
        protected int _hitPointMaximum;
        protected Sprite _bodySprite;

        protected int _hitPoints;

        public CreaturePresenter _presenter;

        public float _spaceInFeet { get; private set; }

        public string displayName => _displayName;
        public int hitPointMaximum => _hitPointMaximum;
        public Sprite bodySprite => _bodySprite;

        public int hitPoints
        {
            get => _hitPoints;
            set => _hitPoints = Mathf.Max(0, value);
        }

        public CreaturePresenter presenter { get; private set; }

        public Creature(int hitPointMaximum)
        {
            hitPoints = hitPointMaximum;
        }

        public void InitializePresenter(CreaturePresenter presenter)
        {
            this.presenter = presenter;
        }

        public void ReactToDamage(int damageAmount)
        {
            _hitPoints = Mathf.Max(0, _hitPoints - damageAmount);
        }
    }
}
