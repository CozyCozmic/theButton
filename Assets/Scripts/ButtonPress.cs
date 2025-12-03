using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    public float pressDepth = 0.05f;
    public float pressSpeed = 10f;

    private Vector3 originalPosition;
    private bool isPressing = false;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    public void Press()
    {
        if (!isPressing)
        {
            StartCoroutine(AnimatePress());
        }
    }

    private System.Collections.IEnumerator AnimatePress()
    {
        isPressing = true;

        Vector3 targetDown = originalPosition + new Vector3(0, -pressDepth, 0);

        while (Vector3.Distance(transform.localPosition, targetDown) > 0.001f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetDown, Time.deltaTime * pressSpeed);
            yield return null;
        }

        yield return new WaitForSeconds(0.05f);

        while (Vector3.Distance(transform.localPosition, originalPosition) > 0.001f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, Time.deltaTime * pressSpeed);
            yield return null;
        }

        transform.localPosition = originalPosition;
        isPressing = false;
    }
}
