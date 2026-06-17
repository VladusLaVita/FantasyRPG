using UnityEngine;
using System.Collections;

public class CombatSystem : MonoBehaviour
{
    public RPGStats playerStats;
    public RPGStats enemyStats;
    public CombatUI ui;

    public int move;
    public string playerAction;
    public string enemyAction;

    [HideInInspector] public float playerMaxHP;
    [HideInInspector] public float enemyMaxHP;
    [HideInInspector] public bool isWaitingForEnemy = false;

    public void Load(RPGStats enemyRPGStats, RPGStats playerRPGStats)
    {
        playerStats = Instantiate(playerRPGStats);
        enemyStats = Instantiate(enemyRPGStats);

        playerStats.currentHP = playerStats.HP;
        enemyStats.currentHP = enemyStats.HP;

        playerMaxHP = playerStats.HP;
        enemyMaxHP = enemyStats.HP;

        move = 2;
        isWaitingForEnemy = false;
    }

    public void Move()
    {
        move++;
        Debug.Log($"Enemy HP: {enemyStats.currentHP}/{enemyMaxHP}");
        Debug.Log($"Player HP: {playerStats.currentHP}/{playerMaxHP}");

        if (enemyStats.currentHP <= 0) ui.Win();
        else if (playerStats.currentHP <= 0) ui.Defeat();
    }

    public void PlayerHitAttempt()
    {
        if (move % 2 != 0 || isWaitingForEnemy) return;
        Hit();
    }

    public void Hit()
    {
        bool isPlayerTurn = (move % 2 == 0);

        RPGStats attacker = isPlayerTurn ? playerStats : enemyStats;
        RPGStats defender = isPlayerTurn ? enemyStats : playerStats;

        if (isPlayerTurn) playerAction = "hit"; else enemyAction = "hit";

        float defenderMaxHP = isPlayerTurn ? enemyMaxHP : playerMaxHP;
        float missingHealthPercent = 1f - (defender.currentHP / defenderMaxHP);
        float baseDamage = attacker.ATK - defender.DEF;
        float finalDamage = Mathf.Max(1f, baseDamage * (1f + missingHealthPercent));

        defender.currentHP -= finalDamage;

        Move();
        ui.RefreshAllUI();

        if (isPlayerTurn && enemyStats.currentHP > 0)
        {
            StartCoroutine(EnemyResponseCoroutine());
        }
    }

    private IEnumerator EnemyResponseCoroutine()
    {
        isWaitingForEnemy = true;
        ui.baseButtons.SetActive(false);

        yield return new WaitForSeconds(1.0f);

        Hit();

        isWaitingForEnemy = false;

        if (playerStats.currentHP > 0)
        {
            ui.baseButtons.SetActive(true);
        }
    }

    public void Flee()
    {

    }

}
