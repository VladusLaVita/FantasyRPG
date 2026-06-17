using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CombatUI : MonoBehaviour
{
    [Header("Preferences")]
    public GameObject baseButtons;
    public GameObject attackButtons;
    public RawImage CharacterImage;
    public CombatSystem system;

    public Slider sliderPlayer;
    public Slider sliderEnemy;

    public RawImage defeatImage;


    public void Start()
    {
        this.gameObject.SetActive(false);
        defeatImage.enabled = false;

    }

    public void SwitchUI()
    {
        baseButtons.SetActive(!baseButtons.activeSelf);
        attackButtons.SetActive(!attackButtons.activeSelf);
    }

    public void Reload(RPGStats rpgStats, RPGStats playerStats)
    {
        Debug.Log("RPGStats were got! UI Reloaded");
        this.gameObject.SetActive(true);
        baseButtons.SetActive(true);
        attackButtons.SetActive(false);
        CharacterImage.texture = rpgStats.sprite;

        system.Load(rpgStats, playerStats);

        // Обновляем ползунки сразу при входе в бой (чтобы полоски были полными)
        sliderPlayer.value = 1f;
        sliderEnemy.value = 1f;
    }

    // Удалите методы RefreshUI(RPGStats defender) и RefreshEnemyUI(RPGStats defender)
    // Вместо них используйте этот один метод:
    public void RefreshAllUI()
    {
        // Проверяем игрока через ссылки из CombatSystem
        if (system.playerStats != null && sliderPlayer != null)
        {
            sliderPlayer.value = system.playerStats.currentHP / system.playerMaxHP;
        }

        // Проверяем врага через ссылки из CombatSystem
        if (system.enemyStats != null && sliderEnemy != null)
        {
            sliderEnemy.value = system.enemyStats.currentHP / system.enemyMaxHP;
        }

        // Возвращаем кнопки в исходное состояние
        baseButtons.SetActive(true);
        attackButtons.SetActive(false);
    }

    public void Defeat()
    {
        defeatImage.enabled = true;
        StartCoroutine(Wait());
    }

    public void Win()
    {
        SwitchUI();
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.0f);

        SceneChanger sgner = new SceneChanger();

        sgner.Load("000");
    }
}
