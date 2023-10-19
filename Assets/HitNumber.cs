using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HitNumber : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    int value;
    [SerializeField] float lifetime = 1f;
    float timeElapsed = 0f;
    [SerializeField] float speed = 1f;
    public void SetValue(int damage)
    {
        value = damage;
        text.text = value.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.up * speed);
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        if(timeElapsed > lifetime)
        {
            Destroy(gameObject);
        }
    }
}
