using UnityEngine;

[CreateAssetMenu(fileName = "MiniGameScore", menuName = "Score/MiniGameScore")]
public class MiniGameScore : ScriptableObject
{
    [Header("Score")]
    [SerializeField] private int _score;

    public int Score => _score;

    public void ChangeScore(int score) => _score = score;
}
