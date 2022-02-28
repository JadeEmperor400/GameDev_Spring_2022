using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecisionBox : MonoBehaviour
{
    public const int renderLimit = 4;
    public const float buffer = 0.33f;
    public static DecisionBox S;
    [SerializeField] private GameObject Choice;
    [SerializeField] private GameObject arrow;
    private RectTransform arrowRect;
    private List<GameObject> choices = new List<GameObject>();
    private int selectedChoice = 0;
    private bool deciding = false;
    public bool Deciding { get { return deciding; } }
    public Coroutine coroutine;
    private float selectBuffer;

    private void Awake()
    {
        if (S == null)
        {
            S = this;
            selectedChoice = 0;
            arrowRect = arrow.GetComponent<RectTransform>();
        }
        else {
            Destroy(gameObject);
        }
        Clear();
    }

    public void Open(List<LinkSet> linkSets) {
        int rend = 0;
        

        foreach (LinkSet s in linkSets) {
            GameObject gO = (Instantiate(Choice));
            
            RectTransform r = gO.GetComponent<RectTransform>();
            
            r.SetParent(gameObject.GetComponent<RectTransform>());

            gO.GetComponentInChildren<Text>().text = s.option;

            r.anchoredPosition = new Vector2(0, 24 * -rend);
            r.localScale = Vector3.one;
            rend++;
            choices.Add(gO);

            if (rend >= 4)
            {
                break;
            }
        }

        arrow.SetActive(true);
        deciding = true;
    }

    private void Clear() {
        deciding = false;
        int count = choices.Count;
        arrow.SetActive(false);

        for (int i = 0; i < count; i++) {
            Destroy(choices[i].gameObject);
        }

        for (int i = 0; i < count; i++)
        {
            choices.RemoveAt(0);
        }
    }

    public void Update()
    {
        if (deciding) {
            if (selectBuffer <= 0)
            {
                if (Input.GetAxisRaw("Vertical") > 0  && selectedChoice > 0)
                {
                    selectedChoice = (selectedChoice - 1);
                    arrowRect.anchoredPosition = new Vector2(arrowRect.anchoredPosition.x, 24 * -selectedChoice);
                    selectBuffer = buffer;
                    return;
                }
                else if (Input.GetAxisRaw("Vertical") < 0 && selectedChoice < choices.Count - 1)
                {
                    selectedChoice = (selectedChoice + 1);
                    arrowRect.anchoredPosition = new Vector2(arrowRect.anchoredPosition.x, 24 * -selectedChoice);
                    selectBuffer = buffer;
                    return;
                }
            }
            else {
                selectBuffer -= Time.deltaTime;
            }

            if (Input.GetButtonDown("Jump")) {
                //Send Dialogue based on choice
                Textbox.T.currentSet.sendLinkedDialogue(selectedChoice % choices.Count);
                Clear();
            }
        }
    }

}
