using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using NaughtyAttributes;
using DG.Tweening;
public class DoorController : MonoBehaviour
{
    [Inject]
    private GUIManager guiManager;
    [Inject]
    private SoundManager soundManager;

    [ShowAssetPreview(64, 64)]
    public Sprite doorImage;
    public FoodType preferedFoodType;
    public Conversation conversation;
    public Animator doorAnim;
    public bool isDoorOpened = false;

    [Header("Spawn settings")]

    public Transform spawnPoint;
    public Transform MoveToPoint;
    public int enemyCount;
    public int maxEnemyCount;
    public float spawnInterval;

    public GameObject enemyPrefab;
    public GameObject enemyWithQuestItem;
    public float enemyWithQuestItemProbability;

    private Coroutine spawnInProgress;
    void Start()
    {
        conversation = new Conversation();
        string phrase = conversation.GetPhraseByWeaponType(WeaponType.shoot);
        Debug.Log(phrase);
    }


    void Update()
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == Tags.PICKUP_ITEM)
        {
            var item = collision.GetComponentInParent<Item>();
            string phrase;
            doorAnim.SetTrigger("Eat");
            soundManager.PlaySfx(SFX.DoorEating);
            switch (item.data.type)
            {
                case ItemType.Food:
                    bool isCorrect = item.data.foodType == preferedFoodType ? true : false;
                    phrase = conversation.GetPhraseByFoodType(item.data.foodType,
                        isCorrect);
                    guiManager.ShowDialogBox(doorImage, phrase, 2f);
                    if (isCorrect)
                    {
                        doorAnim.SetBool("IsGoodFood", true);
                        OpenDoor();
                    }
                    else
                    {
                        SpawnEnemies();
                        doorAnim.SetBool("IsGoodFood", false);
                    }

                    break;
                case ItemType.Weapon:
                    phrase = conversation.GetPhraseByWeaponType(item.data.weaponType);
                    guiManager.ShowDialogBox(doorImage, phrase, 2f);
                    doorAnim.SetTrigger("Talk");
                    SpawnEnemies();
                    break;
            }
        }
        if(collision.CompareTag(Tags.PLAYER) && !isDoorOpened)
        {
            var player = collision.gameObject.GetComponent<LifeController>();
            player.GetDamage(100);
        }
    }

    public void OpenDoor()
    {
        isDoorOpened = true;
        soundManager.PlaySfxWithDelay(SFX.OpenDoor, 3.4f);
        Debug.Log("You Win!");
    }

    public void GiveCheat(int level, CheatType type)
    {
        string phrase = conversation.GetCheatByHintAndLevel(level, type.ToString());
        doorAnim.SetTrigger("Talk");
        guiManager.ShowDialogBox(doorImage, phrase, 2f);
    }

    public void SpawnEnemies()
    {
        if(spawnInProgress != null)
        {
            maxEnemyCount += 5;
            return;
        }
        spawnInProgress = StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        yield return new WaitUntil(() => enemyCount == maxEnemyCount);
        doorAnim.SetTrigger("EnemySpawned");
        enemyCount = 0;
        spawnInProgress = null;
    }

    public void SpawnEnemy()
    {
        var prefab = Random.Range(0f, 1f) < enemyWithQuestItemProbability ? enemyWithQuestItem : enemyPrefab;
        var enemy = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
        enemy.transform.DOMove(MoveToPoint.position, 0.5f);
        enemyCount++;
    }

    [Button]
    private void DEV_OpenDoor()
    {
        doorAnim.SetTrigger("Eat");
        soundManager.PlaySfx(SFX.DoorEating);
        doorAnim.SetBool("IsGoodFood", true);
        OpenDoor();
    }
}


