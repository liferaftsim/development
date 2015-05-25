using UnityEngine;
using System.Collections;

public class Buoyancy : MonoBehaviour
{
    [SerializeField]
    private float yOffset = 0.0f;

    [SerializeField]
    private string waterGameObjectName = "Water";

    [SerializeField]
    private float yConsineSpeed = 1.0f;

    [SerializeField]
    private float yConsineMultiplier = 1.0f;

    private Transform water;

    private void Start()
    {
        this.CacheWater();
    }

    private void Update()
    {
        this.MoveY();
    }

    private void CacheWater()
    {
        var waterGameObject = GameObject.Find(this.waterGameObjectName);
        if (waterGameObject == null)
        {
            Debug.LogError(this.name + ": No game object named \"" + this.waterGameObjectName + "\" found.");
            this.enabled = false;
            return;
        }
        this.water = waterGameObject.transform;
    }

    private void MoveY()
    {
        var position = this.transform.position;
        position.y = this.water.position.y + this.yOffset + Mathf.Cos(Time.time * this.yConsineSpeed) * this.yConsineMultiplier;
        this.transform.position = position;
    }
}
