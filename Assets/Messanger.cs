using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Messanger : MonoBehaviour
{
    public enum MessageType
    {
        DamageMessage,MoneyGatheringMessage, ChainLightningMessage
    }
    public Color damageMessageColor = Color.red;
    public Color moneyGatheringMessageColor = Color.white;
    public Color chainLightningMessageColor = Color.yellow;
    
    public static Messanger messanger;
    Transform canvasTrans;
    public GameObject messagePrefab;

    private void Awake()
    {
        damageMessageColor.a = 1f;
        moneyGatheringMessageColor.a = 1f;
        chainLightningMessageColor.a = 1f;
        if (messanger == null)
        {
            messanger = this;
            return;
        }
        print("messanger already assigned");
        Application.Quit();
    }
    private void Start()
    {
        canvasTrans = GameObject.Find("Canvas").transform;
    }

    public void FloatMessage(MessageType type, Vector3 hitPosition, string data)
    {
        GameObject messageObject = Instantiate(messagePrefab, hitPosition, Quaternion.identity,canvasTrans);
        TextMesh message = messageObject.GetComponent<TextMesh>();
        //print(message);
        messageObject.GetComponent<RectTransform>().localScale *= 2;
        message.text = data;
        message.fontSize = 300;
        message.GetComponent<Rigidbody>().AddForce(Vector2.up / 2, ForceMode.Impulse);
        Destroy(message.gameObject, 0.6f);
        switch (type)
        {
            case MessageType.DamageMessage:
                message.color = damageMessageColor;
                break;
            case MessageType.MoneyGatheringMessage:
                message.color = moneyGatheringMessageColor;
                break;
            case MessageType.ChainLightningMessage:
                message.color = chainLightningMessageColor;
                break;
        }
        return;
    }
}