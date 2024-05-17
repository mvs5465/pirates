using UnityEngine;

public class ToolBelt : MonoBehaviour
{
    public GameObject tool1;
    public GameObject tool2;
    public GameObject tool3;
    private Tool currentTool;

    private void Start()
    {
        currentTool = Instantiate(tool1, transform.position + transform.up * 0.3f, transform.rotation).GetComponent<Tool>();
        currentTool.transform.parent = transform;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentTool.Shutdown();
            currentTool = Instantiate(tool1, transform.position + transform.up * 0.3f, transform.rotation).GetComponent<Tool>();
            currentTool.transform.parent = transform;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentTool.Shutdown();
            currentTool = Instantiate(tool2, transform.position, transform.rotation).GetComponent<Tool>();
            currentTool.transform.parent = transform;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentTool.Shutdown();
            Torch existingTorch = FindObjectOfType<Torch>();
            if (existingTorch != null) Destroy(existingTorch.gameObject);
            currentTool = Instantiate(tool3, transform.position + transform.up * 0.2f, transform.rotation).GetComponent<Tool>();
            currentTool.transform.parent = transform;
        }
    }
}