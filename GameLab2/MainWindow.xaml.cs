using System.Windows;
using System.IO;
using System.Windows.Media.Imaging;

namespace GameLab2
{
    public partial class MainWindow : Window
    {
        private Player _player;
        private Enemy _currentEnemy;
        private EnemyList _enemyStorage;
        private int _defeatedCount = 0;

        string jsonPath = //"C:/Users/";

        public MainWindow()
        {
            InitializeComponent();

            _player = new Player();
            _enemyStorage = new EnemyList();

            _enemyStorage.LoadFromJson(jsonPath);

            SpawnNewEnemy();
        }

        private void SpawnNewEnemy()
        {
            _currentEnemy = _enemyStorage.GetNextEnemy(_defeatedCount);
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (_player == null || _currentEnemy == null) return;

            PlayerDamage.Text = _player.Damage.ToString();
            PlayerGold.Text = _player.Gold.ToString();
            PlayerLevel.Text = _player.Lvl.ToString();

            EnemyHitPoint.Text = _currentEnemy.CurrentHitPoints.ToString();
            EnemyName.Text = _currentEnemy.Name;
            GoldFromenemy.Text = _currentEnemy.GoldReward.ToString();
            UpgradeCost.Text = _player.UpgradeCost.ToString();

        }
    }
}
