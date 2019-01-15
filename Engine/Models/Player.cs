using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Player : LivingEntity
    {
        #region Properties

        private string _characterClass;
        private int _magicPoints;
        private int _experiencePoints;
        private int _maxMana;
        private GameItem _currentSpell;

        public string CharacterClass
        {
            get { return _characterClass; }
            set
            {
                _characterClass = value;
                OnPropertyChanged();
            }
        }

        public int ExperiencePoints
        {
            get { return _experiencePoints; }
            private set
            {
                _experiencePoints = value;
                OnPropertyChanged();

                SetLevelAndMaximumHitPoints();
            }
        }

        public int MagicPoints
        {
            get { return _magicPoints; }
            set
            {
                _magicPoints = value;
                OnPropertyChanged();
            }
        }

        public int MaxMana
        {
            get { return _maxMana; }
            protected set
            {
                _maxMana = value;
                OnPropertyChanged();
            }
        }

        public GameItem CurrentSpell
        {
            get { return _currentSpell; }
            set
            {
                if (_currentSpell != null)
                {
                    _currentSpell.Action.OnActionPerformed -= RaiseActionPerformedEvent;
                }

                _currentSpell = value;

                if (_currentSpell != null)
                {
                    _currentSpell.Action.OnActionPerformed += RaiseActionPerformedEvent;
                }
            }
        }

        public ObservableCollection<Magic> SpellList { get; }

        public List<GameItem> Magic =>
            Inventory.Where(i => i.Category == GameItem.ItemCategory.Magic).ToList();

        public ObservableCollection<QuestStatus> Quests { get; }

        public ObservableCollection<Recipe> Recipes { get; }

        public new event EventHandler<string> OnActionPerformed;

        #endregion

        public event EventHandler OnLeveledUp;

        public Player(string name, string characterClass, int experiencePoints,
            int maxHitPoints, int currentHitPoints, int magicPoints, int maxMana,
            int gold) :
            base(name, maxHitPoints, currentHitPoints, gold)
        {
            MagicPoints = magicPoints;
            MaxMana = maxMana;
            CharacterClass = characterClass;
            ExperiencePoints = experiencePoints;

            Quests = new ObservableCollection<QuestStatus>();
            SpellList = new ObservableCollection<Magic>();
            Recipes = new ObservableCollection<Recipe>();
        }

        public void AddExperience(int experiencePoints)
        {
            ExperiencePoints += experiencePoints;
        }

        public void SpendMana(int manaCost)
        {
            MagicPoints -= manaCost;
        }

        public void FullManaRestore()
        {
            MagicPoints = MaxMana;
        }

        public void LearnRecipe(Recipe recipe)
        {
            if(!Recipes.Any(r => r.ID == recipe.ID))
            {
                Recipes.Add(recipe);
            }
        }

        private void SetLevelAndMaximumHitPoints()
        {
            int originalLevel = Level;

            Level = (ExperiencePoints / 100) + 1;

            if(Level != originalLevel)
            {
                MaxHitPoints = Level * 20;

                MaxMana = Level * 9;

                OnLeveledUp?.Invoke(this, System.EventArgs.Empty);
            }
        }

        private void RaiseActionPerformedEvent(object sender, string result)
        {
            OnActionPerformed?.Invoke(this, result);
        }
    }
}
