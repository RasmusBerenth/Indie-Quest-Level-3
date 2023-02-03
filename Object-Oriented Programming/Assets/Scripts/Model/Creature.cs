using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterQuest
{
    public class Creature
    {
        protected string _displayName;
        public string displayName => _displayName;

        protected int _hitPointsMaximum;
        protected int _hitPoints;

        public int hitPointsMaximum => _hitPointsMaximum;
        public int hitPoints => _hitPoints;

        private float _spaceInFeet;
        public float spaceInFeet => _spaceInFeet;

        protected Sprite _bodySprite;
        public Sprite bodySprite => _bodySprite;

        private CreaturePresenter _presenter;
        public CreaturePresenter presenter => _presenter;

        protected SizeCategory _sizeCategory;
        public SizeCategory sizeCategory => _sizeCategory;

        //Polymorphism mission 1 properties between here
        private IEnumerable<bool> _deathSavingThrows;
        public IEnumerable<bool> deathSavingThrows => _deathSavingThrows;

        private int _deathSavingThrowSucces;
        public int deathSavingThrowSucces => _deathSavingThrowSucces;

        private int _deathSavingThrowFailures;
        public int deathSavingThrowFailures => _deathSavingThrowFailures;

        protected LifeStatus _lifeStatus;
        public LifeStatus LifeStatus => _lifeStatus;
        //and here

        public Creature(string monsterName, Sprite bodySprite, SizeCategory sizeCategory, LifeStatus lifeStatus)
        {
            _displayName = monsterName;
            _bodySprite = bodySprite;
            SizeHelper.spaceInFeetPerSizeCategory.TryGetValue(sizeCategory, out _spaceInFeet);
            _sizeCategory = sizeCategory;
            _lifeStatus = lifeStatus;
        }

        protected void InitializeHitPoint()
        {
            _hitPoints = _hitPointsMaximum;
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

        public override string ToString()
        {
            return displayName;
        }


    }
}
