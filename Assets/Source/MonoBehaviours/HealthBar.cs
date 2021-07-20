using System.Collections.Generic;

using UnityEngine;


class HealthBar : MonoBehaviour
{

    private List<GameObject> hearthObjects;

    public Hitable Hitable;
    public GameObject HearthPrefab;

    public HealthBar()
    {
        hearthObjects = new List<GameObject>();
    }

    private void Start()
    {
        Hitable.OnHealthChange += Hitable_OnHealthChange;

        for (int i = 0; i < Hitable.CurrentHealth; i++)
            CreateHearth(i);
    }

    private void CreateHearth(int index)
    {
        var hearth = Instantiate(HearthPrefab);

        var rectTransform = hearth.GetComponent<RectTransform>();
        rectTransform.SetParent(transform, false);

        float rectWidth = rectTransform.rect.width * rectTransform.localScale.x;
        float rectHeight = rectTransform.rect.height * rectTransform.localScale.y;
        float spacing = -15;
        rectTransform.position = new Vector2()
        {
            x = 25 + rectWidth / 2 + spacing + (rectTransform.rect.width + spacing) * index,
            y = Screen.height - rectHeight + 20,
        };

        hearthObjects.Add(hearth);
    }

    private void Hitable_OnHealthChange(object sender, HealthChangeEventArgs e)
    {
        foreach (var obj in hearthObjects)
            Destroy(obj);
        hearthObjects.Clear();

        for (int i = 0; i < e.CurrentHealth; i++)
            CreateHearth(i);
    }
}