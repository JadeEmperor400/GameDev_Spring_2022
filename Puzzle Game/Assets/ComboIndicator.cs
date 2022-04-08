using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboIndicator : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer numberRenderer;
    [SerializeField]
    SpriteRenderer numberRenderer_10;
    [SerializeField]
    SpriteRenderer spriteRenderer;

    [SerializeField]
    List<Sprite> numberSprites;

    [SerializeField]
    Coroutine coroutine;

    [SerializeField]
    Color red = Color.red, blue = Color.blue, green = Color.green;

    [SerializeField]
    Vector3 localPos;
    [SerializeField]
    Vector3 localPos_10;
    // Start is called before the first frame update
    void Awake()
    {
        if (numberSprites == null || numberSprites.Count != 10)
        {
            Debug.Log("Combo ID : numberSprites list not set");
            enabled = false;
            return;
        }

        if (numberRenderer == null)
        {
            Debug.Log("Combo ID : no numberRenderer set");
            enabled = false;
            return;
        }

        localPos = new Vector3(numberRenderer.transform.localPosition.x, numberRenderer.transform.localPosition.y, 0);

        if (numberRenderer_10 == null)
        {
            Debug.Log("Combo ID : no numberRenderer for tens set");
            enabled = false;
            return;
        }

        localPos_10 = new Vector3(numberRenderer_10.transform.localPosition.x, numberRenderer_10.transform.localPosition.y, 0);

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        ClearComboIndicator();
    }

    public void Boost(int comboSize) {
        if (comboSize < 1)
        {
            ClearComboIndicator();
            return;
        }

        spriteRenderer.forceRenderingOff = false;

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        if(isActiveAndEnabled)
            coroutine = StartCoroutine(ShowCombo(comboSize));
    }

    public void ClearComboIndicator() {
        spriteRenderer.forceRenderingOff = true;
        numberRenderer.forceRenderingOff = true;
        numberRenderer_10.forceRenderingOff = true;

        if (coroutine != null) {
            StopCoroutine(coroutine);
        }

        numberRenderer.transform.localScale = Vector3.one;
        numberRenderer.transform.localPosition = localPos;
        numberRenderer_10.transform.localPosition = localPos_10;

        spriteRenderer.color = Color.white;
        numberRenderer.color = Color.white;
        numberRenderer_10.color = Color.white;
    }

    private void ResetPositions() {
        numberRenderer.transform.localScale = Vector3.one;
        numberRenderer.transform.localPosition = localPos;
        numberRenderer_10.transform.localPosition = localPos_10;
    }

    public void SetColor(Connection connection) {
        switch (connection.getColorType()) {
            case ColorEnum.RED:
                spriteRenderer.color = red;
                numberRenderer.color = red;
                numberRenderer_10.color = red;
                break;
            case ColorEnum.BLUE:
                spriteRenderer.color = blue;
                numberRenderer.color = blue;
                numberRenderer_10.color = blue;
                break;
            case ColorEnum.GREEN:
                spriteRenderer.color = green;
                numberRenderer.color = green;
                numberRenderer_10.color = green;
                break;
            default:
                spriteRenderer.color = Color.white;
                numberRenderer.color = Color.white;
                numberRenderer_10.color = Color.white;
                break;
        }
    }

    private IEnumerator ShowCombo(int comboSize) {
        ResetPositions();

        if (comboSize > 99) {
            comboSize = 99;
        }

        if (comboSize > 0)
        {
            numberRenderer.forceRenderingOff = false;
            numberRenderer.sprite = numberSprites[comboSize % 10];
            if (comboSize >= 10) {
                numberRenderer.transform.localScale = new Vector3(1.25f,1.25f,1);
            }
        }
        else
        {
            numberRenderer.forceRenderingOff = true;
        }

        if (comboSize > 9)
        {
            numberRenderer_10.forceRenderingOff = false;
            numberRenderer_10.sprite = numberSprites[comboSize / 10];
        }
        else {
            numberRenderer_10.forceRenderingOff = true;
        }

        for (int i = 0; i < 8; i++) {
            numberRenderer.transform.localPosition = new Vector3(localPos.x, localPos.y + i/16.0f, 0);
            yield return new WaitForSecondsRealtime(1/60.0f);
        }
        for (int i = 0; i < 8; i++)
        {
            numberRenderer_10.transform.localPosition = new Vector3(localPos_10.x, localPos_10.y + i / 16.0f, 0);
            numberRenderer.transform.localPosition = new Vector3(localPos.x, localPos.y + (7-i) / 16.0f, 0);
            yield return new WaitForSecondsRealtime(1 / 60.0f);
        }
        for (int i = 0; i < 8; i++)
        {
            numberRenderer_10.transform.localPosition = new Vector3(localPos_10.x, localPos_10.y + (7-i) / 16.0f, 0);
            yield return new WaitForSecondsRealtime(1 / 60.0f);
        }

        numberRenderer.transform.localPosition = localPos;
        numberRenderer_10.transform.localPosition = localPos_10;

        coroutine = null;
        yield break;
    }
}
