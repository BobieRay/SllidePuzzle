using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SlidePuzzleSceneDirector : MonoBehaviour
{
    // �s�[�X���X�g
    [SerializeField] List<GameObject> pieces;

    // �N���A�W���b�W���X�g
    [SerializeField] List<ClearJudge> clearJudgeList;

    // �N���A����
    bool isClear = false;

    // �N���A�e�L�X�g
    [SerializeField] GameObject clearText;

    // �Q�[���N���A���ɕ\�������{�^��
    [SerializeField] GameObject buttonRetry;

    // �V���b�t����
    [SerializeField] int shuffleCount;

    // �����ʒu
    List<Vector3> startPositions;


    // Start is called before the first frame update
    void Start()
    {
        // �����ʒu��ۑ�
        startPositions = new List<Vector3>();
        foreach (var item in pieces)
        {
            startPositions.Add(item.transform.position);
        }

        // �w��񐔃V���b�t������
        for(int i = 0; i < shuffleCount; i++)
        {
            // 0�ԂƗאڂ���s�[�X
            List<GameObject> movablePieces = new List<GameObject>();

            // 0�ԂƗאڂ���s�[�X�����X�g�ɒǉ�
            foreach (var item in pieces)
            {
                if (GetemptyPiece(item) != null)
                {
                    movablePieces.Add(item);
                }
            }

            // �אڂ���s�[�X�������_���œ���ւ���
            int rnd = Random.Range(0, movablePieces.Count);
            GameObject piece = movablePieces[rnd];
            SwapPiece(piece, pieces[0]);
        }
       

        // �{�^����\��
        buttonRetry.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // �^�b�`����
        if (isClear == false)
        {
            if (Input.GetMouseButtonUp(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit = new RaycastHit();

                // �����蔻�肪������
                if (Physics.Raycast(ray, out hit))
                {
                    // �q�b�g�����Q�[���I�u�W�F�N�g
                    GameObject hitPiece = hit.collider.gameObject;

                    // 0�Ԃ̃s�[�X�Ɨאڂ��Ă���΃f�[�^������
                    GameObject emptyPiece = GetemptyPiece(hitPiece);

                    // �I�񂾃s�[�X��0�Ԃ̃s�[�X�����ւ���
                    SwapPiece(hitPiece, emptyPiece);
                }
            }
        }

        
    }

    // �����̃s�[�X��0�Ԃ̃s�[�X�Ɨאڂ��Ă�����0�Ԃ̃s�[�X��Ԃ�
    GameObject GetemptyPiece(GameObject piece)
    {
        float dist = Vector3.Distance(piece.transform.position, pieces[0].transform.position);

        if (dist == 1)
        {
            return pieces[0];
        }

        return null;
    }

    // 2�̃s�[�X�̈ʒu�����ւ���
    void SwapPiece(GameObject pieceA,GameObject pieceB)
    {
        // �ǂ��炩��null�Ȃ珈�������Ȃ�
        if(pieceA == null || pieceB == null)
        {
            return;
        }

        // A��B�̃|�W�V���������ւ���
        Vector3 position = pieceA.transform.position;
        pieceA.transform.position = pieceB.transform.position;
        pieceB.transform.position = position;
    }

    // ���g���C�{�^��
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
        // �N���A����
        buttonRetry.SetActive(true);

        isClear = true;

        clearText.SetActive(true);
    }
}
