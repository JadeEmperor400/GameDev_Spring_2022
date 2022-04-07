using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;


public enum State{
    PlayerPhase,
    Calculating,
    EnemyPhase,
    GameOver,
    Victory,
    Start, // battle system is rready to go
    Off
};

public class BattleManager : MonoBehaviour
{

    public MusicMotor musicMotor;
    public MusicState overworldMusicState;
    public MusicState battleMusicState;
    public MusicState bossBattleMusicState;

    public static BattleManager BM;
    public UltimateJoystick joystick;
    public PlayerMovement playerMovement;
    public GridManager gridManager;
    public GridComboManager comboManager;
    public TimerSlider timer;

    public PlayerStats player;

    public List<EnemyStats> enemy = new List<EnemyStats>();
    [SerializeField]
    private List<EnemyStats> randomizedEnemies = new List<EnemyStats>();
    [SerializeField]
    private int targetEnemy = 0;

    public EnemyStats TargetEnemy
    {
        get {
            if (enemy == null || targetEnemy >= enemy.Count) {
                return null;
            }

            return enemy[targetEnemy];
        }
    }

    [SerializeField]
    private int enemyTurnNo = 0;

    public PlayerAction redPlayerAction;
    public PlayerAction bluePlayerAction;
    public PlayerAction greenPlayerAction;

    public EnemyAction defaultEnemyAttack;

    private Coroutine batteRoutine;
    private Coroutine mesRoutine;

    [SerializeField]
    private List<Sprite> numbers = new List<Sprite>();
    [SerializeField]
    private SpriteRenderer _numBit;
    [SerializeField]
    private EnemyHPBAR enemyHPBAR;
    [SerializeField]
    private EnemyHPBAR playerHPBar;

    public State state{
        get; 
        private set;
    } = State.Off;

    [SerializeField]
    private BattleMessenger messenger;

    [SerializeField]
    private List<EnemyStats> testTeam;

    public int eventID = -1;

    void Awake()
    {
        if (BM == null)
        {
            BM = this;
        }
        else if (BM != this) {
            Destroy(this.gameObject);
        }
        player.gameObject.SetActive(false);
        gridManager.gameObject.SetActive(false);
        timer.gameObject.SetActive(false);
        state = State.Off;
        //BattleStart();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(KeyCode.T) && Input.GetKeyDown(KeyCode.B)) {
            if (testTeam != null && testTeam.Count > 0)
            {
                if (state == State.Off)
                {
                    BeginBattle(testTeam);
                }
                else {
                    Debug.Log("Battle in Progress");
                }
            }
            else {
                Debug.Log("No test team available");
            }
        }

        if (timer.getIsReset() && state == State.PlayerPhase) {
            playerCalc();
        }     
    }

    public void BeginBattle(List<EnemyStats> SpawnThese ) {
       StartCoroutine( musicMotor.changeState(battleMusicState));

        if (SpawnThese == null || SpawnThese.Count <= 0)
        {
            return;
        }

        if (state == State.Off) {
            playerMovement.FreezePlayer();
            joystick.gameObject.SetActive(false);
            state = State.Start;
            //DisableSprites();
            player.gameObject.SetActive(true);

            if (enemy.Count != 0)
            {
                foreach (EnemyStats e in enemy)
                {
                    if (e == null) { continue; }
                    Destroy(e.gameObject);
                }

                enemy.Clear();
            }

            for (int i = 0; i < SpawnThese.Count; i++) {
                if (i == 3) {
                    break;
                }

                EnemyStats e = Instantiate(SpawnThese[i]);
                e.GetComponent<Renderer>().sortingLayerName = "Battle";
                switch (i) {
                    case 0:
                        e.gameObject.transform.position = new Vector3(5,-0.5f,0);
                        break;
                    case 1:
                        e.gameObject.transform.position = new Vector3(7, -3.5f, 0);
                        break;
                    case 2:
                        e.gameObject.transform.position = new Vector3(7, 1.5f, 0);
                        break;
                }

                enemy.Add(e);
                Instantiate(enemyHPBAR.gameObject, e.transform);
            }

            EnemyHPBAR playerBar = player.GetComponentInChildren<EnemyHPBAR>();
            if (playerBar != null) {
                Destroy(playerBar.gameObject);
            }
            Instantiate(playerHPBar.gameObject, player.transform);
            BattleStart();
        }
    }

    void BattleStart() {
        gridManager.gameObject.SetActive(false);
        timer.gameObject.SetActive(false);
        SetMessage("THEY APPROACH!", 2.0f);
        player.Init();
        foreach (EnemyStats e in enemy) {
            e.gameObject.SetActive(true);
            e.Init();
        }

        

        StartCoroutine(StartUp());
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
        SetMessage(user.name + " Is Stunned!!", 1.5f);
        yield return new WaitForSecondsRealtime(2.25f);
        enemyTurnNo++;
        enemyAct();
        yield break;
    }

    private IEnumerator EnemyActionPlay(EnemyStats user, List<EnemyAction> enemyActions) {

        Debug.Log("ENEMY ACTING : " + user.name + " START " );

        for (int i = 0; i < enemyActions.Count; i++) {
            //TODO: ADD ACTION NOTIFIACTION
            Debug.Log("ENEMY ACTING : " + user.name + " used " + enemyActions[i].name + ".");
            SetMessage(user.name + " used " + enemyActions[i].name, 1.5f);
            yield return new WaitForSecondsRealtime(0.5f);
            //TODO: SPAWN DAMAGE NUMBER
            switch (enemyActions[i].actionType) {
                case ActType.Heal:
                    user.HealDamage(enemyActions[i].power);
                    StartCoroutine(DisplayNumber(enemyActions[i].power, user.gameObject, new Color(0.5f, 1, 0.5f)));
                    yield return new WaitForSecondsRealtime(0.5f);
                    break;
                case ActType.Status:
                    player.timeReduction += enemyActions[i].timeReduction;
                    player.Barrier = player.Barrier - enemyActions[i].barrierReduction;
                    break;
                case ActType.Attack:
                default:
                    switch (enemyActions[i].targetType) {
                        case TargetType.Player:
                            int attackPower = (int) (enemyActions[i].power * (1 - player.Barrier));
                            int drainPower = (int)(attackPower * enemyActions[i].drainRt);
                            player.timeReduction += enemyActions[i].timeReduction;
                            player.Barrier = player.Barrier - enemyActions[i].barrierReduction;
                            player.TakeDamage(attackPower);
                            StartCoroutine(DisplayNumber(attackPower, player.gameObject, new Color(0.75f, 0.75f, 0.5f)));
                            yield return new WaitForSecondsRealtime(0.25f);

                            if (drainPower > 0) {
                                user.HealDamage(drainPower);
                                StartCoroutine(DisplayNumber(drainPower, user.gameObject, new Color(0.5f, 1, 0.5f)));
                            }
                            yield return new WaitForSecondsRealtime(0.25f);
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
                GameOver();
                yield break;
            }
        }

        enemyTurnNo++;
        enemyAct();
        yield break;
    }

    private IEnumerator CalculatePlayerAttack(Queue<Connection> currentCombo)
    {
        while (gridManager.Falling)
        {
            yield return new WaitForEndOfFrame();
        }

        puzzleClose();

        if (currentCombo == null || currentCombo.Count < 1) //if player did no connections, then leave this method 
        {
            Debug.Log("No combo to make player attack");
        }
        else
        {
            int comboSize = currentCombo.Count;
            var firstConnection = currentCombo.Dequeue();
            ColorEnum firstConnectionColorType = firstConnection.getColorType();
            int baseDamage = DetermineBaseDamage(firstConnectionColorType);
            int baseStagger = DetermineSupportStagger(firstConnectionColorType);
            int staggerBoost = 0;
            int supportDMG = 0;

            for(int i = 1; i < comboSize; i++)
            {
                //subsequent attacks
                Connection combo = currentCombo.Dequeue();
                ColorEnum colorType = combo.getColorType();
                Debug.Log("ColorType : " + colorType);
                supportDMG += DetermineSupportDamage(colorType);
                staggerBoost += DetermineSupportStagger(colorType);
                player.Barrier = player.Barrier + DetermineSupportBarrier(colorType);

            }
            int fullStagger = baseStagger + staggerBoost;
            int fullDmg = DealDamage(baseDamage, supportDMG, fullStagger, comboSize, firstConnectionColorType);

            StartCoroutine(DisplayNumber(fullDmg, enemy[targetEnemy].gameObject, Color.white));
            if (TargetEnemy.HP <= 0) {
                TargetEnemy.gameObject.SetActive(false);
            }
            yield return new WaitForSecondsRealtime(0.25f);

            int drainHeal = (int)(fullDmg * DetermineDrainRt(firstConnectionColorType));
            
            player.HealDamage(drainHeal);
            if (drainHeal > 0) {
                StartCoroutine(DisplayNumber(drainHeal, player.gameObject, new Color(0.5f, 1, 0.5f)));
            }
            
            Debug.Log("Player Used : " + firstConnection.getColorType() + " action  POWER: " + fullDmg + " / HEAL: " + drainHeal + " / STAGGER : " + fullStagger);
            yield return new WaitForSecondsRealtime(0.25f);
            yield return new WaitForSecondsRealtime(0.1f);
            //TODO: Start Animation and Wait for it
        }

        while (gridManager.Falling)
        {
            yield return new WaitForEndOfFrame();
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
            Victory();
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
        Victory();
    }

    private int DealDamage(int baseDMG, int supportDMG, int stagger, int comboSize, ColorEnum attackType)
    {
        int fullDamage = baseDMG + supportDMG;

        float comboMult = 1;
        
        if (comboSize > 1) {
            comboMult += (comboSize - 1) * 0.25f; ;
        }

        float colorMult = 1.0f;

        switch (attackType) {
            case ColorEnum.RED:
                colorMult = enemy[targetEnemy].RedAff;
                break;
            case ColorEnum.BLUE:
                colorMult = enemy[targetEnemy].BlueAff;
                break;
            case ColorEnum.GREEN:
                colorMult = enemy[targetEnemy].GreenAff;
                break;
        }

        Debug.Log("DAMAGE MULT:: Combo: " + comboMult + " / ColorAff : " + colorMult);

        fullDamage = (int)(fullDamage * comboMult * colorMult);
        
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

    private float DetermineSupportBarrier(ColorEnum colorType)
    {

        switch (colorType)
        {
            case ColorEnum.GREEN:
                return 0.25f;
            default:
                break;
        }

        return 0.0f;
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

    public IEnumerator DisplayNumber(int number, GameObject target, Color color) {
        if (number > 999) {
            number = 999;
        }

        int amounts = 1;
        if (number > 99)
        {
            amounts = 3;
        }
        else if (number > 9)
        {
            amounts = 2;
        }
        List<GameObject> bits = new List<GameObject>();
        Debug.Log("StartDisplay 0");
        for (int i = amounts; i > 0; i--) {
            int checkNum = (int)(number/ (Mathf.Pow(10,i-1)));
            number -= checkNum * (int)((Mathf.Pow(10, i - 1)));
            GameObject g = GameObject.Instantiate(_numBit.gameObject);
            g.transform.position = new Vector3(target.transform.position.x + (amounts - i + 1) / 2.0f, target.transform.position.y + 1,0);
            g.transform.localScale = Vector3.zero;
            g.GetComponent<SpriteRenderer>().color = color;
            g.GetComponent<SpriteRenderer>().sprite = numbers[checkNum % 10];
            bits.Add(g);
        }

        Vector3 startScale = new Vector3(0.5f, 0, 1);
        Vector3 endScale = new Vector3(0.5f, 0.5f, 1);

        Debug.Log("StartDisplay 1");
        for (float i = 0; i <= 1.0f; i += (1 / 10.0f)) {
            foreach (GameObject g in bits) {
                g.transform.localScale = Vector3.Lerp(startScale, endScale, i);
                g.transform.position += new Vector3(0,1/120.0f,0);
            }
            yield return new WaitForSecondsRealtime(1 /60.0f);
        }

        yield return new WaitForSecondsRealtime(0.5f);

        Color startColor = color;
        Color endColor = Color.clear;
        Debug.Log("StartDisplay 2");
        for (float i = 0; i <= 1.0f; i += (1 / 6.0f))
        {
            foreach (GameObject g in bits)
            {
                g.GetComponent<SpriteRenderer>().color = Color.Lerp(startColor,endColor,i);
            }
            yield return new WaitForSecondsRealtime(1 / 60.0f);
        }

        foreach (GameObject g in bits)
        {
            Destroy(g);
        }
        Debug.Log("End Display");
        yield break;
    }

    public void SetMessage(string message, float delay = 0.5f) {
        if (mesRoutine != null) {
            StopCoroutine(mesRoutine);
        }
        mesRoutine = StartCoroutine(ShowMessage( message, delay));
    }

    private IEnumerator ShowMessage(string message, float delay = 0.5f) {
        if (delay < 0) {
            delay = 0;
        }
        messenger.Message = message;
        Vector3 startScale = new Vector3(1, 0, 1);
        Vector3 endScale = new Vector3(1,1,1);

        for (float i = 0; i <= 1.0f; i += 1 / 15.0f) {
            messenger.transform.localScale = Vector3.Lerp(startScale, endScale, i);
            yield return new WaitForSecondsRealtime(1 / 60.0f);
        }

        yield return new WaitForSecondsRealtime(delay);

        for (float i = 0; i <= 1.0f; i += 1 / 15.0f)
        {
            messenger.transform.localScale = Vector3.Lerp(endScale, startScale, i);
            yield return new WaitForSecondsRealtime(1 / 60.0f);
        }
        mesRoutine = null;
        yield break;
    }

    //Starts Battle
    private IEnumerator StartUp() {
        yield return new WaitForSecondsRealtime(3.5f);
        playerTurn();
        yield break;
    }


    private void Victory()
    {
        StartCoroutine(musicMotor.changeState(overworldMusicState));
        playerMovement.UnFreezePlayer();
        joystick.gameObject.SetActive(true);
        StartCoroutine(VictoryAnimate());
        DeterminingEndingWin();
    }
    private void DeterminingEndingWin()
    {

        StartCoroutine(waitTimeMethod(3.51f));
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if(enemy.Count != 0)
        {
            var enemyDialogue = enemy[0].gameObject.GetComponent<EnemyDialogue>();  
            if(enemyDialogue.GetEnemyType() == EnemyType.BossEnemy)
            {
                AudioManager.Instance.StopMusicFadeOut();
                //SceneManager.LoadScene("EndCreditsScene");
            }
        }
    }
    private IEnumerator VictoryAnimate() {
        SetMessage("You Win!!", 2.0f);
        yield return new WaitForSecondsRealtime(3.5f);
        if (enemy.Count != 0)
        {
            foreach (EnemyStats e in enemy)
            {
                if(e != null)
                    Destroy(e.gameObject);
            }

            enemy.Clear();
        }
        player.gameObject.SetActive(false);
        EventSystem.eventController.BattleEndEvent(EventSystem.eventController.killID);
        EventSystem.eventController.killID = -1;
        //EnableSprites();
        state = State.Off;
    }

    private void GameOver() {
        playerMovement.UnFreezePlayer();
        joystick.gameObject.SetActive(true);
        StartCoroutine(GameOverAnimate());
       
        DeterminingEndingLoss();
           
    }

    private void DeterminingEndingLoss()
    {

        StartCoroutine(waitTimeMethod(3.51f));
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator waitTimeMethod(float timeToWait)
    {
        yield return new WaitForSecondsRealtime(timeToWait);
    }
    private IEnumerator GameOverAnimate()
    {
        player.gameObject.SetActive(false);
        SetMessage("You Lose!!", 2.0f);
        yield return new WaitForSecondsRealtime(3.5f);
        if (enemy.Count != 0)
        {
            foreach (EnemyStats e in enemy)
            {
                if(e != null)
                    Destroy(e.gameObject);
            }

            enemy.Clear();
        }
        //EnableSprites();
        state = State.Off;
    }

    private void DisableSprites() {
        SpriteRenderer[] renderers = FindObjectsOfType<SpriteRenderer>();

        foreach (SpriteRenderer sr in renderers) {
            if (sr.sortingLayerName != "BattleBG" && sr.sortingLayerName != "Battle" && sr.sortingLayerName != "Display" && sr.sortingLayerName != "Overlay") {
                sr.forceRenderingOff = true;
            }
        }

        TilemapRenderer[] tilemapRenderers = FindObjectsOfType<TilemapRenderer>();
        foreach (TilemapRenderer tr in tilemapRenderers)
        {
            if (tr.sortingLayerName != "BattleBG" && tr.sortingLayerName != "Battle" && tr.sortingLayerName != "Display" && tr.sortingLayerName != "Overlay")
            {
                tr.forceRenderingOff = true;
            }
        }

    }

    private void EnableSprites()
    {
        SpriteRenderer[] renderers = FindObjectsOfType<SpriteRenderer>();

        foreach (SpriteRenderer sr in renderers)
        {
            if (sr.sortingLayerName != "BattleBG" && sr.sortingLayerName != "Battle" && sr.sortingLayerName != "Display" && sr.sortingLayerName != "Overlay")
            {
                sr.forceRenderingOff = false;
            }
        }

        TilemapRenderer[] tilemapRenderers = FindObjectsOfType<TilemapRenderer>();
        foreach (TilemapRenderer tr in tilemapRenderers)
        {
            if (tr.sortingLayerName != "BattleBG" && tr.sortingLayerName != "Battle" && tr.sortingLayerName != "Display" && tr.sortingLayerName != "Overlay")
            {
                tr.forceRenderingOff = false;
            }
        }

    }
}
