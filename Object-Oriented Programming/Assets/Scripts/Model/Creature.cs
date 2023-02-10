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

        protected IEnumerable<bool> _deathSavingThrows;
        public IEnumerable<bool> deathSavingThrows => _deathSavingThrows;

        protected int _deathSavingThrowSucces;
        public int deathSavingThrowSucces => _deathSavingThrowSucces;

        protected int _deathSavingThrowFailures;
        public int deathSavingThrowFailures => _deathSavingThrowFailures;

        protected LifeStatus _lifeStatus;
        public LifeStatus lifeStatus
        {
            get => _lifeStatus;
            protected set
            {
                _lifeStatus = value;
                presenter.UpdateStableStatus();
            }
        }

        public Creature(string monsterName, Sprite bodySprite, SizeCategory sizeCategory)
        {
            _displayName = monsterName;
            _bodySprite = bodySprite;
            SizeHelper.spaceInFeetPerSizeCategory.TryGetValue(sizeCategory, out _spaceInFeet);
            _sizeCategory = sizeCategory;
            _lifeStatus = LifeStatus.Conscious;

        }

        protected void InitializeHitPoint()
        {
            _hitPoints = _hitPointsMaximum;
        }

        public void InitializePresenter(CreaturePresenter presenter)
        {
            _presenter = presenter;
        }

        public virtual IEnumerator ReactToDamage(int damageAmount)
        {
            _hitPoints = Mathf.Max(0, _hitPoints - damageAmount);
            yield return presenter.TakeDamage();

            if (hitPoints == 0)
            {
                lifeStatus = LifeStatus.Dead;
                yield return presenter.Die();
            }
        }

        public override string ToString()
        {
            return displayName;
        }


    }
}
