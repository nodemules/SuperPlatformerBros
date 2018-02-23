using System.Collections.Generic;
using Enemy;
using Environment;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    public class Level5Controller : MonoBehaviour
    {
        private const string SceneName = "Level5";
        private List<IBoss> _bosses = new List<IBoss>();
        private bool _bossesCleared;
        private Scene _scene;

        public void Start()
        {
            _scene = SceneManager.GetActiveScene();
        }

        public void Update()
        {
            if (_scene.name != SceneName || _bossesCleared)
            {
                return;
            }

            _bosses = new List<IBoss>(GetComponentsInChildren<IBoss>());
            _bossesCleared = _bosses.Count == 0;

            if (_bossesCleared)
            {
                ShowBlocks();
            }
        }

        private void ShowBlocks()
        {
            HiddenBlock[] blocks = GetComponentsInChildren<HiddenBlock>();

            foreach (HiddenBlock b in blocks)
            {
                b.Show();
            }
        }
    }
}