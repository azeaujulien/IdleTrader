using System;
using TMPro;
using UnityEngine;

public class TapAlert : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Awake()
    {
        // Instantiate alert with good text
        gameObject.GetComponent<TextMeshProUGUI>().text = "+ " + GameManager.Instance.currentTapGain +"$";
    }

    private void Update()
    {
        // Animate alert
        transform.position += new Vector3(0.25f, 1) * (speed * Time.deltaTime);
    }
}
