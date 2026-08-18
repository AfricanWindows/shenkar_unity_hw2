using TMPro;
using UnityEngine;

/// <summary>
/// Shows any ICounter value in a TextMeshPro label.
/// Works for coins, lives, axes... without changing this class.
/// </summary>
public class UI_CounterView : MonoBehaviour
{
    [Tooltip("GameObject that holds a component implementing ICounter")]
    [SerializeField] private GameObject counterSource;

    [Tooltip("{0} is replaced by the counter value")]
    [SerializeField] private string format = "Coins: {0}";

    private TextMeshProUGUI label;
    private ICounter counter;

    private void OnEnable()
    {
        label = GetComponent<TextMeshProUGUI>();

        if (counterSource != null)
            counter = counterSource.GetComponent<ICounter>();

        if (counter == null)
        {
            Debug.LogError("UI_CounterView: no ICounter found on counterSource", this);
            return;
        }

        counter.OnValueChanged += SetValue;
        SetValue(counter.Value);
    }

    private void Start()
    {
        // Safety refresh: Awake order between scene objects is not guaranteed.
        if (counter != null)
            SetValue(counter.Value);
    }

    private void OnDisable()
    {
        if (counter != null)
            counter.OnValueChanged -= SetValue;
    }

    private void SetValue(int value)
    {
        if (label != null)
            label.text = string.Format(format, value);
    }
}
