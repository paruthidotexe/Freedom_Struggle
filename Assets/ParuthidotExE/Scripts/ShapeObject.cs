///-----------------------------------------------------------------------------
///
/// ShapeData
/// 
/// Data of Shape
///
///-----------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ParuthidotExE
{
    public class ShapeObject : MonoBehaviour
    {
        public ShapeData shapeData;
        public SpriteRenderer spriteRenderer;
        public SpriteRenderer shadowRenderer;
        public ParticleSystem trailParticles;
        public TrailRenderer[] trailRenderer;
        public ParticleSystem[] explosionPrefabs;

        Vector3 dir;
        float speed = 1;
        float levelSpeed = 0;
        bool isAlive = false;
        Vector3 startPos = Vector3.up * 100;
        Paths path;

        public delegate void VoidDelegate();
        public delegate void IntDelegate(int val);
        public static event IntDelegate OnMissed;
        public static event IntDelegate OnSafeAreaHit;


        private void OnEnable()
        {
            EventMgr.OnLevelUp += OnLevelUp;
        }


        private void OnDisable()
        {
            EventMgr.OnLevelUp -= OnLevelUp;
        }


        void Start()
        {
            transform.position = startPos;
        }


        void Update()
        {
            if (isAlive)
                transform.position += dir * speed * Time.deltaTime;
        }


        // Events
        void Raise_OnMissed(int val)
        {
            if (OnMissed != null)
                OnMissed(val);
        }


        void Raise_OnSafeAreaHit(int val)
        {
            if (OnSafeAreaHit != null)
                OnSafeAreaHit(val);
        }


        public void Init(ShapeData newShapeData)
        {
            shapeData = newShapeData;
            ApplyShapeData();
        }


        public void OnSpawn(Paths newPath)
        {
            path = newPath;
            transform.position = path.GetStartPoint();
            dir = Vector3.down;
            isAlive = true;
            ApplyShapeData();
        }


        public void OnSpawn(Vector3 newPos, Vector3 newDir)
        {
            isAlive = true;
            transform.position = newPos;
            dir = newDir;
        }


        void ApplyShapeData()
        {
            // type
            speed = 1.0f + shapeData.shapeType / 5.0f + levelSpeed;
            // sprite
            spriteRenderer.sprite = shapeData.sprite;
            shadowRenderer.sprite = shapeData.sprite;
            // apply Tint/Color
            if (shapeData.tintStatus)
                spriteRenderer.color = shapeData.color;
            // particle color
            ParticleSystem.MainModule curPS = trailParticles.main;
            curPS.startColor = shapeData.color;
            // trial color
            for (int i = 0; i < trailRenderer.Length; i++)
            {
                trailRenderer[i].startColor = shapeData.color;
            }
        }


        public void OnClicked()
        {
            isAlive = false;
            PlayEffect();
            MoveToStartPos();
        }


        public void PlayEffect()
        {
            ParticleSystem explosionPS = GameObject.Instantiate(
                                                    explosionPrefabs[Random.Range(0, explosionPrefabs.Length)],
                                                    transform.position,
                                                    Quaternion.identity);
            ParticleSystem.MainModule explosionPSMain = explosionPS.main;
            explosionPSMain.startColor = shapeData.color;
            explosionPS.Play();
        }


        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("SafeArea"))
            {
                Raise_OnSafeAreaHit(path.GetPathId());
                isAlive = false;
                StartCoroutine(MoveToStartPos(1.0f));
            }
            else if (col.CompareTag("TapArea"))
            {
                //Debug.Log("collided with " + col.name);
            }
        }


        void OnTriggerExit2D(Collider2D col)
        {
            //Debug.Log("Crossed with " + col.name);
            if (col.CompareTag("TapArea"))
            {
                Raise_OnMissed(path.GetPathId());
                //Debug.Log("collided with " + col.name);
            }
        }


        void MoveToStartPos()
        {
            transform.position = startPos;
        }


        IEnumerator MoveToStartPos(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            MoveToStartPos();
            yield return null;
        }


        void OnLevelUp()
        {
            if (!isAlive)
                levelSpeed += 0.5f;
            //Debug.Log("Speed Increased " + speed);
        }


    }


}

