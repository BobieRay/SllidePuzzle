using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SlidePuzzleSceneDirector : MonoBehaviour
{
    // ピースリスト
    [SerializeField] List<GameObject> pieces;

    // クリアジャッジリスト
    [SerializeField] List<ClearJudge> clearJudgeList;

    // クリア判定
    bool isClear = false;

    // クリアテキスト
    [SerializeField] GameObject clearText;

    // ゲームクリア時に表示されるボタン
    [SerializeField] GameObject buttonRetry;

    // シャッフル回数
    [SerializeField] int shuffleCount;

    // 初期位置
    List<Vector3> startPositions;


    // Start is called before the first frame update
    void Start()
    {
        // 初期位置を保存
        startPositions = new List<Vector3>();
        foreach (var item in pieces)
        {
            startPositions.Add(item.transform.position);
        }

        // 指定回数シャッフルする
        for(int i = 0; i < shuffleCount; i++)
        {
            // 0番と隣接するピース
            List<GameObject> movablePieces = new List<GameObject>();

            // 0番と隣接するピースをリストに追加
            foreach (var item in pieces)
            {
                if (GetemptyPiece(item) != null)
                {
                    movablePieces.Add(item);
                }
            }

            // 隣接するピースをランダムで入れ替える
            int rnd = Random.Range(0, movablePieces.Count);
            GameObject piece = movablePieces[rnd];
            SwapPiece(piece, pieces[0]);
        }
       

        // ボタン非表示
        buttonRetry.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // タッチ処理
        if (isClear == false)
        {
            if (Input.GetMouseButtonUp(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit = new RaycastHit();

                // 当たり判定があった
                if (Physics.Raycast(ray, out hit))
                {
                    // ヒットしたゲームオブジェクト
                    GameObject hitPiece = hit.collider.gameObject;

                    // 0番のピースと隣接していればデータが入る
                    GameObject emptyPiece = GetemptyPiece(hitPiece);

                    // 選んだピースと0番のピースを入れ替える
                    SwapPiece(hitPiece, emptyPiece);
                }
            }
        }

        
    }

    // 引数のピースが0番のピースと隣接していたら0番のピースを返す
    GameObject GetemptyPiece(GameObject piece)
    {
        float dist = Vector3.Distance(piece.transform.position, pieces[0].transform.position);

        if (dist == 1)
        {
            return pieces[0];
        }

        return null;
    }

    // 2つのピースの位置を入れ替える
    void SwapPiece(GameObject pieceA,GameObject pieceB)
    {
        // どちらかがnullなら処理をしない
        if(pieceA == null || pieceB == null)
        {
            return;
        }

        // AとBのポジションを入れ替える
        Vector3 position = pieceA.transform.position;
        pieceA.transform.position = pieceB.transform.position;
        pieceB.transform.position = position;
    }

    // リトライボタン
    public void OnClickBack()
    {
        Initiate.Fade("StageSelectScene", Color.black, 1.0f);
    }

    public void JudgeClear()
    {
        foreach(ClearJudge clearJudge in clearJudgeList)
        {
            if (clearJudge.IsClearJudge == false)
            {
                return;
            }
        }
        // クリア判定
        buttonRetry.SetActive(true);

        isClear = true;

        clearText.SetActive(true);
    }
}
