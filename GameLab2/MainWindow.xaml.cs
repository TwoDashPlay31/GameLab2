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

        string jsonPath = "C:/Users/lich/Desktop/labs/2 kyrs/code/lab2/enemies.json";

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

        private void DealDamage(object sender, RoutedEventArgs e)
        {

            BigNumber damage = _player.DealDamage();

            bool isDead = _currentEnemy.TakeDamage(damage, out BigNumber reward);

            if (isDead)
            {
                _player.AddGold(reward);
                _defeatedCount++; 
                SpawnNewEnemy();  
            }

            UpdateUI();
        }

        private void UpgradeClick(object sender, RoutedEventArgs e)
        {
            _player.TryUpgrade();
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


            if (!string.IsNullOrEmpty(_currentEnemy.Icon))
            {
                try
                {
                    string cleanPath = _currentEnemy.Icon.Replace("\"", "").Trim();

                    if (System.IO.File.Exists(cleanPath))
                    {
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri(cleanPath, UriKind.Absolute);
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.EndInit();

                        EnemyImage.Source = bitmap;
                    }
                    else
                    {
                        EnemyName.Text = "Файл картинки не найден по пути!";
                        EnemyImage.Source = null;
                    }
                }
                catch (Exception ex)
                {
                    EnemyName.Text = $"Ошибка картинки: {ex.Message}";
                    EnemyImage.Source = null;
                }
            }
        }
    }
}
