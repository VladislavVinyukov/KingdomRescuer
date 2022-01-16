using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSound : MonoBehaviour
{
    void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        switch(sceneName)
        {
            case "ScoreCounter":
                SoundManager.PlaySound(SoundManager.Sound.scoreCounterMusic, transform.position);
                break;
            case "Level 1":
                SoundManager.PlayMainSound(SoundManager.Sound.level_1_Music);
                break;
            case "Level 2":
                SoundManager.PlayMainSound(SoundManager.Sound.level_2_Music);
                break;
            case "Level 3_BossStage":
                SoundManager.PlayMainSound(SoundManager.Sound.level_2_Music);
                break;

        }
            
    }
}
