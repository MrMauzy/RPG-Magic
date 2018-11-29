using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;
using Engine.Factories;
using Engine.EventArgs;
using System.ComponentModel;

namespace Engine.ViewModels
{
    public class GameSession : BaseNotificationClass
    {
        public event EventHandler<GameMessageEventArgs> OnMessageRaised;

        #region Properties

        private Player _currentPlayer;
        private Location _currentLocation;
        private Monster _currentMonster;
        private Trader _currentTrader;

        public World CurrentWorld { get; }

        public Player CurrentPlayer
        {
            get { return _currentPlayer; }
            set
            {
                if(_currentPlayer != null)
                {
                    _currentPlayer.OnLeveledUp -= OnCurrentPlayerLeveledUp;
                    _currentPlayer.OnKilled -= OnCurrentPlayerKilled;
                }

                _currentPlayer = value;

                if(_currentPlayer != null)
                {
                    _currentPlayer.OnLeveledUp += OnCurrentPlayerLeveledUp;
                    _currentPlayer.OnKilled += OnCurrentPlayerKilled;
                }
            }
        }

        public Location CurrentLocation
        {
            get { return _currentLocation; }
            set
            {
                _currentLocation = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasLocationNorth));
                OnPropertyChanged(nameof(HasLocationEast));
                OnPropertyChanged(nameof(HasLocationWest));
                OnPropertyChanged(nameof(HasLocationSouth));

                CompleteQuestsAtLocation();
                GivePlayerQuestsAtLocation();
                GetMonsterAtLocation();

                CurrentTrader = CurrentLocation.TraderHere;
            }
        }

        public Monster CurrentMonster
        {
            get { return _currentMonster; }
            set
            {

                if (_currentMonster != null)
                {
                    _currentMonster.OnKilled -= OnCurrentMonsterKilled;
                }

                _currentMonster = value;

                if(_currentMonster != null)
                {
                    _currentMonster.OnKilled += OnCurrentMonsterKilled;

                    RaiseMessage("");
                    RaiseMessage($"You see a {CurrentMonster.Name} here!");
                }

                OnPropertyChanged();
                OnPropertyChanged(nameof(HasMonster));
            }
        }

        public Trader CurrentTrader
        {
            get { return _currentTrader; }
            set
            {
                _currentTrader = value;

                OnPropertyChanged();
                OnPropertyChanged(nameof(HasTrader));
            }
        }

        public GameItem CurrentWeapon { get; set; }

        public Magic CurrentSpell { get; set; }

        public bool HasLocationNorth =>
            CurrentWorld.LocationAt(CurrentLocation.XCoordinate, 
                    CurrentLocation.YCoordinate + 1) != null;

        public bool HasLocationEast =>
            CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1,
                    CurrentLocation.YCoordinate) != null;

        public bool HasLocationWest =>
            CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1,
                    CurrentLocation.YCoordinate) != null;

        public bool HasLocationSouth => 
            CurrentWorld.LocationAt(CurrentLocation.XCoordinate,
                    CurrentLocation.YCoordinate - 1) != null;

        public bool HasMonster => CurrentMonster != null;

        public bool HasTrader => CurrentTrader != null;

        #endregion

        public GameSession()
        {
            CurrentPlayer = new Player("Merek", "Wizard", 0, 20, 20, 9, 9, 101);

            if(!CurrentPlayer.Weapons.Any())
            {
                CurrentPlayer.AddItemToInventory(ItemFactory.CreateGameItem(1001));
                CurrentPlayer.AddItemToInventory(ItemFactory.CreateGameItem(1005));
            }

            if (!CurrentPlayer.Spells.Any())
            {
                CurrentPlayer.SpellList.Add(MagicFactory.CreateMagicSpell(8001));
            }

            CurrentWorld = WorldFactory.CreateWorld();

            CurrentLocation = CurrentWorld.LocationAt(1, 1);
        }

        public void MoveNorth()
        {
            if (HasLocationNorth)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate,
                    CurrentLocation.YCoordinate + 1);
            }
        }

        public void MoveEast()
        {
            if (HasLocationEast)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1,
                    CurrentLocation.YCoordinate);
            }
        }

        public void MoveWest()
        {
            if (HasLocationWest)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1,
                    CurrentLocation.YCoordinate);
            }
        }

        public void MoveSouth()
        {
            if (HasLocationSouth)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate,
                    CurrentLocation.YCoordinate - 1);
            }
        }

        private void CompleteQuestsAtLocation()
        {
            foreach (Quest quest in CurrentLocation.QuestsAvaliable)
            {
                QuestStatus questToComplete =
                    CurrentPlayer.Quests.FirstOrDefault(q => q.PlayerQuest.ID == quest.ID &&
                                                             !q.IsCompleted);

                if (questToComplete != null)
                {
                    if (CurrentPlayer.HasAllTheseItems(quest.ItemsToComplete))
                    {
                        // Remove the quest completion items from the player's inventory
                        foreach (ItemQuantity itemQuantity in quest.ItemsToComplete)
                        {
                            for (int i = 0; i < itemQuantity.Quantity; i++)
                            {
                                CurrentPlayer.RemoveItemFromInventory(CurrentPlayer.Inventory.First(item => item.ItemTypeID == itemQuantity.ItemID));
                            }
                        }

                        RaiseMessage("");
                        RaiseMessage($"You completed the '{quest.Name}' quest");

                        // Give the player the quest rewards
                        CurrentPlayer.AddExperience(quest.RewardExperiencePoints);
                        RaiseMessage($"You receive {quest.RewardExperiencePoints} experience points");

                        CurrentPlayer.Gold += quest.RewardGold;
                        RaiseMessage($"You receive {quest.RewardGold} gold");

                        foreach (ItemQuantity itemQuantity in quest.RewardItems)
                        {
                            GameItem rewardItem = ItemFactory.CreateGameItem(itemQuantity.ItemID);

                            CurrentPlayer.AddItemToInventory(rewardItem);
                            RaiseMessage($"You receive a {rewardItem.Name}");
                        }

                        // Mark the Quest as completed
                        questToComplete.IsCompleted = true;
                    }
                }
            }
        }

        private void GivePlayerQuestsAtLocation()
        {
            foreach (Quest quest in CurrentLocation.QuestsAvaliable)
            {
                if (!CurrentPlayer.Quests.Any(q => q.PlayerQuest.ID == quest.ID))
                {
                    CurrentPlayer.Quests.Add(new QuestStatus(quest));

                    RaiseMessage("");
                    RaiseMessage($"You receive the '{quest.Name}' quest");
                    RaiseMessage(quest.Description);

                    RaiseMessage("Return with:");
                    foreach (ItemQuantity itemQuantity in quest.ItemsToComplete)
                    {
                        RaiseMessage($"   {itemQuantity.Quantity} {ItemFactory.CreateGameItem(itemQuantity.ItemID).Name}");
                    }

                    RaiseMessage("And you will receive:");
                    RaiseMessage($"   {quest.RewardExperiencePoints} experience points");
                    RaiseMessage($"   {quest.RewardGold} gold");
                    foreach (ItemQuantity itemQuantity in quest.RewardItems)
                    {
                        RaiseMessage($"   {itemQuantity.Quantity} {ItemFactory.CreateGameItem(itemQuantity.ItemID).Name}");
                    }
                }
            }
        }

        private void GetMonsterAtLocation()
        {
            CurrentMonster = CurrentLocation.GetMonster();
        }

        public void MagicAttackCurrentMonster()
        {
            // Checks to make sure a Monster is on the location
            if (CurrentMonster == null)
            {
                RaiseMessage("Just who are you trying to Magically Attack?");
                return;
            }

            // If there is no current spell it will give this error
            if (CurrentSpell == null)
            {
                RaiseMessage("You must select a spell, to do the magics.");
                return;
            }

            // Makes sure you have enough mana to cast spell
            if (CurrentPlayer.MagicPoints >= CurrentSpell.ManaCost)
            {
                RaiseMessage($"You attack with a {CurrentSpell.Name} spell and do {CurrentSpell.MagicDamage} damage.");
                CurrentMonster.TakeDamage(CurrentSpell.MagicDamage);
                CurrentPlayer.SpendMana(CurrentSpell.ManaCost);
            }
            else
            {
                RaiseMessage($"You have insufficent mana to cast {CurrentSpell.Name}");
            }

            // If monster if killed, collect rewards and loot
            if (CurrentMonster.IsDead)
            {
                GetMonsterAtLocation();
            }
            else
            {
                // This is where the Monster will attack you
                int damageToPlayer = RandomNumberGenerator.NumberBetween(CurrentMonster.MinDamage,
                    CurrentMonster.MaxDamage);

                if (damageToPlayer == 0)
                {
                    RaiseMessage($"The {CurrentMonster.Name} attacks, but misses you");
                }
                else
                {
                    RaiseMessage($"The {CurrentMonster.Name} hit you for {damageToPlayer} points");
                    CurrentPlayer.TakeDamage(damageToPlayer);
                }
            }
        }

        public void AttackCurrentMonster()
        {
            if (CurrentMonster == null)
            {
                RaiseMessage("There is no one to attack here.");
                return;
            }

            if (CurrentWeapon == null)
            {
                RaiseMessage("You must select a weapon, to attack.");
                return;
            }

            // Determine damage to monster
            int damageToMonster = RandomNumberGenerator.NumberBetween(CurrentWeapon.MinDamage, CurrentWeapon.MaxDamage);

            if (damageToMonster == 0)
            {
                RaiseMessage($"You missed the {CurrentMonster.Name}.");
            }
            else
            {
                RaiseMessage($"You hit the {CurrentMonster.Name} for {damageToMonster} points.");
                CurrentMonster.TakeDamage(damageToMonster);
            }

            // If monster if killed, collect rewards and loot
            if (CurrentMonster.IsDead)
            {
                GetMonsterAtLocation();
            }
            else
            {
                //Let the monster attack
                int damageToPlayer = RandomNumberGenerator.NumberBetween(CurrentMonster.MinDamage,
                    CurrentMonster.MaxDamage);

                if(damageToPlayer == 0)
                {
                    RaiseMessage($"The {CurrentMonster.Name} attacks, but misses you");
                }
                else
                {
                    RaiseMessage($"The {CurrentMonster.Name} hot you for {damageToPlayer} points");
                    CurrentPlayer.TakeDamage(damageToPlayer);
                }
            }
        }

        private void OnCurrentPlayerKilled(object sender, System.EventArgs eventArgs)
        {
            RaiseMessage("");
            RaiseMessage("You have been descimated and can no longer go on.");

            CurrentLocation = CurrentWorld.LocationAt(1, 1);
            CurrentPlayer.CompleteHeal();
            CurrentPlayer.FullManaRestore();
        }

        private void OnCurrentMonsterKilled(object sender, System.EventArgs eventArgs)
        {
            RaiseMessage("");
            RaiseMessage($"You defeated the {CurrentMonster.Name}.");

            RaiseMessage($"You receive {CurrentMonster.RewardExperiencePoints} experience point");
            CurrentPlayer.AddExperience(CurrentMonster.RewardExperiencePoints);

            RaiseMessage($"You receive {CurrentMonster.Gold} gold");
            CurrentPlayer.ReceiveGold(CurrentMonster.Gold);

            foreach (GameItem gameItem in CurrentMonster.Inventory)
            {
                RaiseMessage($"You receive one {gameItem.Name}");
                CurrentPlayer.AddItemToInventory(gameItem);
            }
        }

        private void OnCurrentPlayerLeveledUp(object sender, System.EventArgs eventArgs)
        {
            RaiseMessage($"You are now level {CurrentPlayer.Level}!");
        }

        private void RaiseMessage(string message)
        {
            OnMessageRaised?.Invoke(this, new GameMessageEventArgs(message));
        }
    }
}
