using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    public class Creature
    {
        protected string _displayName;

        protected int _hitPointsMaximum;
        protected int _hitPoints;

        private float _spaceInFeet;

        protected Sprite _bodySprite;

        private CreaturePresenter _presenter;

        protected SizeCategory _sizeCategory;

        public string displayName => _displayName;

        public int hitPointsMaximum => _hitPointsMaximum;
        public int hitPoints => _hitPoints;


        public float spaceInFeet => _spaceInFeet;

        public Sprite bodySprite => _bodySprite;

        public CreaturePresenter presenter => _presenter;

        public SizeCategory sizeCategory => _sizeCategory;

        public Creature(int hitPointsMaximum, string monsterName, Sprite bodySprite, SizeCategory sizeCategory)
        {
            _hitPointsMaximum = hitPointsMaximum;
            _hitPoints = hitPointsMaximum;
            _displayName = monsterName;
            _bodySprite = bodySprite;
            SizeHelper.spaceInFeetPerSizeCategory.TryGetValue(sizeCategory, out _spaceInFeet);
            _sizeCategory = sizeCategory;
        }

        public void InitializePresenter(CreaturePresenter presenter)
        {
            _presenter = presenter;
        }

        public IEnumerator ReactToDamage(int damageAmount)
        {
            _hitPoints = Mathf.Max(0, _hitPoints - damageAmount);

            if (hitPoints <= 0)
            {
                yield return presenter.Die();
            }

            if (hitPoints > 0)
            {
                yield return presenter.TakeDamage();
            }
        }
    }
}
