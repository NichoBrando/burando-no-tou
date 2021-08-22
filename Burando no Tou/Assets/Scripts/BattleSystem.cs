using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum BATTLE_STATUS {
    PLAYER_TURN,
    PLAYER_ATTACK,
    PLAYER_HEAL,
    PLAYER_DEFEAT,
    ENEMY_ATTACK,
    ENEMY_DEFEAT

}

public class BattleSystem : MonoBehaviour
{
    public Text eventLabel;
    private bool isPlayerTurn = true;
    private int enemyHealth = 10;
    private Player player;

    public GameObject playerBar;
    public GameObject enemyBar;
    private GameController gameController;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    public void ResetFight()
    {
        enemyHealth = 10;
        enemyBar.GetComponent<Image>().fillAmount = 1;
        eventLabel.text = "It's your turn!\nSelect one action";
    }

    private bool IsDead(bool isPlayer)
    {
        return (isPlayer ? player.health : enemyHealth) <= 0;
    }

    private void updateLifePoints()
    {
        playerBar.GetComponent<Image>().fillAmount = (float)player.health / 15f;
        enemyBar.GetComponent<Image>().fillAmount = (float)enemyHealth / 10f;
    }


    IEnumerator ChangeTurn()
    {
        yield return new WaitForSeconds(1f);
        updateLifePoints();

        if(IsDead(true)){
            eventLabel.text = "You died!";
            yield return new WaitForSeconds(1f);
            ResetFight();
            gameController.EndGame();
        }
        else if(IsDead(false))
        {
            eventLabel.text = "You won the fight against the dead Knight!";
            yield return new WaitForSeconds(1f);
            ResetFight();
            gameController.EndBattle();
            
        }
        else
        {
            isPlayerTurn = !isPlayerTurn;
            if(!isPlayerTurn)
            {
                yield return new WaitForSeconds(1f);
                EnemyAttack();
            }
            else
            {
                eventLabel.text = "It's your turn!\nSelect one action";
            }
        }
    }

    public void PlayerAttack()
    {
        if(!isPlayerTurn) return;
        enemyHealth -= 5;
        eventLabel.text = "You dealt 5 damage to the Knight!";
        StartCoroutine(ChangeTurn());
    }

    public void PlayerHeal()
    {
        if(!isPlayerTurn) return;
        player.health += 5;
        if(player.health > 15) player.health = 15;
        eventLabel.text = "You healed 5 points of life!";
        StartCoroutine(ChangeTurn());
    }

    private void EnemyAttack()
    {
        player.health -= 3;
        eventLabel.text = "You was attacked by the Knight\nYou received 3 points of damage!";
        StartCoroutine(ChangeTurn());
    }

}
