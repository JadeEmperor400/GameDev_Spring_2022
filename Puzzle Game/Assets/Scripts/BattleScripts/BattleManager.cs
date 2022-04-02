using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum State{
        PlayerPhase, Calculating, EnemyPhase, GameOver, Victory, Start
    };

public class BattleManager : MonoBehaviour
{
    public static BattleManager BM;

    public GridManager gridManager;
    public GridComboManager comboManager;
    public TimerSlider timer;

    public PlayerStats player;

    public List<EnemyStats> enemy = new List<EnemyStats>();
    [SerializeField]
    private List<EnemyStats> randomizedEnemies = new List<EnemyStats>();
    [SerializeField]
    private int targetEnemy = 0;
    [SerializeField]
    private int enemyTurnNo = 0;

    public PlayerAction redPlayerAction;
    public PlayerAction bluePlayerAction;
    public PlayerAction greenPlayerAction;

    public EnemyAction defaultEnemyAttack;

    private Coroutine batteRoutine;

    public State state{
        get; 
        private set;
    } = State.Start;

    
    void Start()
    {
        if (BM == null)
        {
            BM = this;
            state = State.Start;
            BattleStart();
        }
        else if (BM != this) {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        if (timer.getIsReset() && state == State.PlayerPhase) {
            playerCalc();
        }     
    }

    void BattleStart() {
        player.Init();
        foreach (EnemyStats e in enemy) {
            e.Init();
        }
        playerTurn();
    }

    private void playerTurn(){
        // Opens puzzle grid (calls puzzleOpen method)
        puzzleOpen();
        player.StartTurn();
        state = State.PlayerPhase;
        // Waits for player to finish their puzzle (calls playerCalc method)

        // Take players attack type to choose attack, calculate support effects and dmg (calls playerCalc method)

    }

    public void puzzleOpen(){
        //Method that opens puzzle grid  
        gridManager.gameObject.SetActive(true);//turn on gridmanager
        gridManager.RegenerateGrid();
        timer.gameObject.SetActive(true);//turn on slider
        timer.TimerStart(player.BaseTime , player.timeReduction);
        //Apply Time Reduction to timer
    }

    public void puzzleClose() {
        gridManager.gameObject.SetActive(false);//turn on gridmanager
        timer.normalTime();
        timer.isPause = true;
        timer.gameObject.SetActive(false);//turn on slider
        comboManager.ClearCombo();
        comboManager.ResetCountFall();
    }

    public void playerCalc() {
        if (state != State.PlayerPhase) {
            Debug.Log("Player cannot act outside player phase");
            return;
        }

        state = State.Calculating;

        // Play Attack Animation

        // Close Puzzle

        // Calculate Dmg, Heals, and Debuffs
        // Get Combo Info
        // First connection color determines attack
        //Subsequent connections stack support effects
        timer.isPause = true;
        try
        {
            batteRoutine = StartCoroutine(CalculatePlayerAttack(comboManager.currentComboQueue()));
        }
        catch (NullReferenceException n) {
            Debug.Log("NULL REF : " + n);
            batteRoutine = StartCoroutine(CalculatePlayerAttack(null));
        }
        

        //apply attack to enemy

        // Check Transition to EnemyPhase or Victory
            // Check all enemies
                // If 1 or more enimies have more than 0HP
                    // Move to enemy phase
                // else
                    // Move to victory screen and end the battle
        

        //no enemies have health greater than 0, moving to victory screen
        //state = State.Victory;
    }

    public void enemyPhase(){
        // Randomize Enemy Turn order
        state = State.EnemyPhase;
        enemyTurnNo = 0;
        Debug.Log("In Enemy Phase");
        randomizedEnemies = RandomizeEnemyTurnOrder(enemy);
        enemyAct();
        // For each enemy
        // Execute enemy behavior script
        // Play Action Animation
        // Calculate Dmg, Healing, and Debuffs
        // Check if the player has less than 0HP
        // If so, move to Game Over
        // If not, return to playerTurn   
        
    }

    public void enemyAct() {
        if (state != State.EnemyPhase) {
            Debug.Log("Enemy cannot act outside enemy phase");
            return;
        }

        if (randomizedEnemies == null || randomizedEnemies.Count < 1|| enemyTurnNo >= randomizedEnemies.Count) {
            //All Enemies have acted
            Debug.Log("Pass turn to player");
            playerTurn();
            return;
        }

        int actions = 1; //number of actions to order

        if (randomizedEnemies[enemyTurnNo].staggerCount >= randomizedEnemies[enemyTurnNo].staggerLimit) {
            //Display Enemy Stun Message
            randomizedEnemies[enemyTurnNo].staggerCount = 0;

            Debug.Log(randomizedEnemies[enemyTurnNo].name + " is stunned");
            batteRoutine = StartCoroutine(EnemyStunned(randomizedEnemies[enemyTurnNo]));
            return;
        }

        if (randomizedEnemies[enemyTurnNo].extraCounter >= enemy[enemyTurnNo].extraTurnTimer) {
            actions = 2;
            randomizedEnemies[enemyTurnNo].extraCounter = 0;
        }

        List<EnemyAction> ea = new List<EnemyAction>();
        
        for (int i = 0; i < actions; i++) {
            EnemyAction getAct = randomizedEnemies[enemyTurnNo].SelectAction();

            if (getAct == null) {
                getAct = defaultEnemyAttack;
            }

            ea.Add(getAct);
        }

        if (actions < 2) {
            randomizedEnemies[enemyTurnNo].extraCounter++;
        }

        batteRoutine = StartCoroutine(EnemyActionPlay(randomizedEnemies[enemyTurnNo], ea));
    }

    private IEnumerator EnemyStunned(EnemyStats user) {
        Debug.Log("ENEMY ACTING : " + user.name + " IS STUNNED!! ");
        yield return new WaitForSecondsRealtime(0.5f);
        enemyTurnNo++;
        enemyAct();
        yield break;
    }

    private IEnumerator EnemyActionPlay(EnemyStats user, List<EnemyAction> enemyActions) {

        Debug.Log("ENEMY ACTING : " + user.name + " START " );
        yield return new WaitForSecondsRealtime(0.5f);

        for (int i = 0; i < enemyActions.Count; i++) {
            //TODO: ADD ACTION NOTIFIACTION
            Debug.Log("ENEMY ACTING : " + user.name + " used " + enemyActions[i].name + ".");

            //TODO: SPAWN DAMAGE NUMBER
            switch (enemyActions[i].actionType) {
                case ActType.Heal:
                    user.HealDamage(enemyActions[i].power);
                    break;
                case ActType.Status:
                    player.timeReduction += enemyActions[i].timeReduction;
                    break;
                case ActType.Attack:
                default:
                    switch (enemyActions[i].targetType) {
                        case TargetType.Player:
                            int attackPower = (int) (enemyActions[i].power * (1 - player.Barrier));
                            int drainPower = (int)(attackPower * enemyActions[i].drainRt);
                            player.timeReduction += enemyActions[i].timeReduction;
                            player.TakeDamage(attackPower);
                            break;
                    }
                    break;
            }

            yield return new WaitForSecondsRealtime(0.5f);

            //TODO call animation and wait for it 

            //Check for player death
            if (player.HP <= 0) {
                state = State.GameOver;
                Debug.Log("GAME OVER");
                yield break;
            }
        }

        enemyTurnNo++;
        enemyAct();
        yield break;
    }

    private IEnumerator CalculatePlayerAttack(Queue<Connection> currentCombo)
    {
        puzzleClose();

        if (currentCombo == null || currentCombo.Count < 1) //if player did no connections, then leave this method 
        {
            Debug.Log("No combo to make player attack");
        }
        else
        {
            var firstConnection = currentCombo.Dequeue();
            ColorEnum firstConnectionColorType = firstConnection.getColorType();
            int baseDamage = DetermineBaseDamage(firstConnectionColorType);
            int baseStagger = DetermineSupportStagger(firstConnectionColorType);
            int staggerBoost = 0;
            int supportDMG = 0;

            for (int i = 1; i < currentCombo.Count; i++)
            {
                //subsequent attacks
                var combo = currentCombo.Dequeue();
                var colorType = combo.getColorType();
                supportDMG += DetermineSupportDamage(colorType);
                staggerBoost += DetermineSupportDamage(colorType);

            }
            int fullStagger = baseStagger + staggerBoost;
            int fullDmg = DealDamage(baseDamage, supportDMG, fullStagger, currentCombo.Count);

            int drainHeal = (int)(fullDmg * DetermineDrainRt(firstConnectionColorType));

            player.HealDamage(drainHeal);
            Debug.Log("Player Used : " + firstConnection.getColorType() + " action  POWER: " + fullDmg + " / HEAL: " + drainHeal + " / STAGGER : " + fullStagger);
            
            yield return new WaitForSecondsRealtime(0.5f);
            //TODO: Start Animation and Wait for it
            //TODO: Spawn Damage Notifications
        }

        player.PassTurn();
        CheckPlayerToEnemy();
        //Check for enemies alive or victory
        yield break;
    }

    private void CheckPlayerToEnemy() {
        if (enemy == null || enemy.Count == 0) {
            //Victory
            Debug.Log("VICTORY : NO ENMEY");
            state = State.Victory;
            return;
        }

        for (int i = 0; i < enemy.Count; i++) {
            if (enemy[i].HP > 0) {
                if (enemy[targetEnemy].HP <= 0)
                {
                    TargetValidEnemy();//Change Target to Valid Enemy
                }
                enemyPhase();//1 or more enemy is alive

                return;
            }
        }

        //Victory
        Debug.Log("VICTORY : ALL DEAD");
        state = State.Victory;
    }

    private int DealDamage(int baseDMG, int supportDMG, int stagger, int comboSize)
    {
        int fullDamage = baseDMG + supportDMG;

        float comboMult = 1;
        
        if (comboSize > 1) {
            comboMult += (comboSize - 1) * 0.25f; ;
        }

        fullDamage = (int)(fullDamage * comboMult);

        enemy[targetEnemy].TakeDamage(fullDamage);
        enemy[targetEnemy].staggerCount += stagger;

        //TODO: Call EnemyDamageNotification

        return fullDamage;
    }

    private int DetermineSupportDamage(ColorEnum colorType)
    {

        switch (colorType)
        {
            case ColorEnum.RED:
                return 20;
            default:
                break;
        }

        return 0;
    }

    private int DetermineSupportStagger(ColorEnum colorType)
    {
        switch (colorType)
        {
            case ColorEnum.BLUE:
                return 3;
            default:
                break;
        }

        return 1;
    }

    private int DetermineSupportBarrier(ColorEnum colorType)
    {

        switch (colorType)
        {
            case ColorEnum.RED:
                return 0;
            case ColorEnum.GREEN:
                return 0;
            case ColorEnum.BLUE:
                return 3;
            default:
                break;
        }

        return 0;
    }

    private int DetermineBaseDamage(ColorEnum colorType)
    {
        int baseAtk = 10;

        switch (colorType)
        {
            case ColorEnum.RED:
                baseAtk = redPlayerAction.power;
                break;
            case ColorEnum.GREEN:
                baseAtk = greenPlayerAction.power;
                break;
            case ColorEnum.BLUE:
                baseAtk = bluePlayerAction.power;
                break;

            default:
                break;
        }

        return baseAtk;
    }

    private float DetermineDrainRt(ColorEnum colorType)
    {
        float dr = 0;

        switch (colorType)
        {
            case ColorEnum.RED:
                dr = redPlayerAction.drainRt;
                break;
            case ColorEnum.GREEN:
                dr = greenPlayerAction.drainRt;
                break;
            case ColorEnum.BLUE:
                dr = bluePlayerAction.drainRt;
                break;

            default:
                break;
        }

        return dr;
    }

    public bool ChangeTarget(EnemyStats e) {
        if (!(state == State.PlayerPhase || state == State.EnemyPhase)) {
            Debug.Log("Target Cannot be changed during calulation or end");
            return false;
        }

        if (!enemy.Contains(e)) {
            Debug.Log("Change Target Enemy [Fail] : " + e.name + " Not in enemy list for this battle");
            return false;
        }

        if (e.HP <= 0) {
            Debug.Log("Change Target Enemy [Fail] : " + e.name + " is dead");
            return false;
        }

        targetEnemy = enemy.IndexOf(e);

        Debug.Log("Change Target Enemy [Success] : " + e.name + " is valid");

        return true;
    }

    public void TargetValidEnemy() {

        for (int i = 0; i < enemy.Count; i++) {
            if (enemy[i].HP <= 0)
            {
                continue;
            }
            else {
                targetEnemy = i;
                return;
            }
        }

        
        //Victory if You get here
    }

    public List<EnemyStats> RandomizeEnemyTurnOrder(List<EnemyStats> OrderedEnemyStats)
    {
        randomizedEnemies = new List<EnemyStats>();

        for (int i = 0; i < enemy.Count; i++) {
            if (enemy[i].HP <= 0) {
                //Enemy is dead
                continue;
            }

            int position = UnityEngine.Random.Range(0, i);

            if (randomizedEnemies.Count > 0)
            {
                randomizedEnemies.Insert(position, enemy[i]);
            }
            else {
                randomizedEnemies.Add(enemy[i]);
            }
            
        }

        return randomizedEnemies;
    }

}
