using UnityEngine;

public class RangeAtack : MonoBehaviour
{
    [SerializeField] private GameObject rangeAtack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float firespeed;

    private bool isRotate = true;

    //����� ����������� �� �������� �����
    public void Shooting()
    {
        //������� ������
        if (isRotate) 
        {
            GameObject currentRangeAtack = Instantiate(rangeAtack, firePoint.position, Quaternion.identity);
            Rigidbody2D currentRangeAtackVelocity = currentRangeAtack.GetComponent<Rigidbody2D>();
            currentRangeAtackVelocity.velocity = new Vector2(firespeed * 1, currentRangeAtackVelocity.velocity.y);
        }
        //������� �����
        else if (!isRotate)
        {
            GameObject currentRangeAtack = Instantiate(rangeAtack, firePoint.position, Quaternion.Euler(0,-180,0));
            Rigidbody2D currentRangeAtackVelocity = currentRangeAtack.GetComponent<Rigidbody2D>();
            currentRangeAtackVelocity.velocity = new Vector2(firespeed * -1, currentRangeAtackVelocity.velocity.y);
        }
    }
    public void DirPlayerPos(bool currentPos)
    {
        //��������� �������� �������� �� ������� PlayerMovement
        isRotate = currentPos;
    }
}
