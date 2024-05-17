using UnityEngine;

public class SkellySpanwer : MonoBehaviour {

    public GameObject skellyPrefab;
    public int maxSpawns = 1;
    private int curSpawns = 0;

    void Start() {
        InvokeRepeating(nameof(Spawn), 5, 5);
    }
    void Spawn() {
        if (curSpawns == maxSpawns) {
            CancelInvoke();
            return;
        }
        curSpawns += 1;
        Instantiate(skellyPrefab, transform.position, Quaternion.identity);
    }
}