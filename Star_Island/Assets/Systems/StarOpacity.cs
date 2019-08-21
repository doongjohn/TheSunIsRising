using UnityEngine;

public class StarOpacity : MonoBehaviour
{
    SpriteRenderer sprite;
    Color newColor;

    [SerializeField]
    private float minOpacity = 0;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        newColor = sprite.color;
    }
    private void Update()
    {
        newColor.a = Mathf.Clamp((GameManager.Inst.DistOfSun / 200f), minOpacity, 1);
        sprite.color = newColor;
    }
}
