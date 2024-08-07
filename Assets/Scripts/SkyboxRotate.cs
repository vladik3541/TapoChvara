using UnityEngine;

public class SkyboxRotate : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1.0f;

    private void Update()
    {
        // �������� ������� ��������� skybox
        float rotation = RenderSettings.skybox.GetFloat("_Rotation");

        // �������� ���������
        rotation += rotationSpeed * Time.deltaTime;

        // �����������, ��� ��������� ���������� � ����� 0-360 �������
        rotation %= 360f;

        // ����������� ���� �������� ���������
        RenderSettings.skybox.SetFloat("_Rotation", rotation);
    }
}
