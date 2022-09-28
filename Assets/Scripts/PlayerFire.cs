using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // �ǰ� ����Ʈ ������Ʈ
    public GameObject bulletEffect;
    // �ǰ� ��ƼŬ �ý���
    ParticleSystem ps;
    
    void Start()
    {
        ps = bulletEffect.GetComponent<ParticleSystem>();
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(Camera.main.transform.position, 
                Camera.main.transform.forward);
            //���̰� �΋H�� ���� ����
            RaycastHit hitInfo = new RaycastHit();
            //�ε��� ��ü�� ������ �ǰ� ����Ʈ ǥ��
            if (Physics.Raycast(ray, out hitInfo))
            {
                bulletEffect.transform.position = hitInfo.point;
                ps.Play();
            }
        }
    }
}
