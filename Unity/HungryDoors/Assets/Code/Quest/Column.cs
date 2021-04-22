using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class Column : MonoBehaviour
{
    [Inject]
    private ItemManager itemManager;
    public int BricksCount;
    public Item brickPrefab;
    public float explosionForce = 100f;
    void Start()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Tags.PICKUP_ITEM))
        {
            var item = collision.gameObject.GetComponent<Item>();
            if (item.data.type == ItemType.Weapon && item.data.weaponType == WeaponType.projectile)
            {
                SpawnBricks();
                Destroy(gameObject);
            }
        }
    }

    public void SpawnBricks()
    {
        for (int i = 0; i < BricksCount; i++)
        {
            var brick = itemManager.InstantiateItem(brickPrefab, transform.position, Quaternion.identity);
            var brickRB = brick.GetComponent<Rigidbody>();
            brickRB.isKinematic = false;
            Vector3 point = Random.onUnitSphere;
            point.y = transform.position.y;
            brickRB.AddForce((point - transform.position).normalized * explosionForce);
        }
    }

}
