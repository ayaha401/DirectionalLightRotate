using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace AyahaGraphicDevelopTools.DLRotate
{

    [CustomEditor(typeof(UniversalAdditionalLightData))]
    public class DirectionalLightRotate : Editor
    {
        public enum Hours
        {
            [InspectorName("0��")]
            Hour0,
            [InspectorName("1��")]
            Hour1,
            [InspectorName("2��")]
            Hour2,
            [InspectorName("3��")]
            Hour3,
            [InspectorName("4��")]
            Hour4,
            [InspectorName("5��")]
            Hour5,
            [InspectorName("6��")]
            Hour6,
            [InspectorName("7��")]
            Hour7,
            [InspectorName("8��")]
            Hour8,
            [InspectorName("9��")]
            Hour9,
            [InspectorName("10��")]
            Hour10,
            [InspectorName("11��")]
            Hour11,
            [InspectorName("12��")]
            Hour12,
            [InspectorName("13��")]
            Hour13,
            [InspectorName("14��")]
            Hour14,
            [InspectorName("15��")]
            Hour15,
            [InspectorName("16��")]
            Hour16,
            [InspectorName("17��")]
            Hour17,
            [InspectorName("18��")]
            Hour18,
            [InspectorName("19��")]
            Hour19,
            [InspectorName("20��")]
            Hour20,
            [InspectorName("21��")]
            Hour21,
            [InspectorName("22��")]
            Hour22,
            [InspectorName("23��")]
            Hour23
        }

        private const int MORNING = 8;
        private const int NOON = 12;
        private const int NIGHT = 19;

        private bool _isDirectionalLight;
        private Hours _selectedHour;

        private void OnEnable()
        {
            var lightData = (UniversalAdditionalLightData)target;
            if (lightData.GetComponent<Light>().type == LightType.Directional)
            {
                _isDirectionalLight = true;
            }
            else
            {
                _isDirectionalLight = false;
            }
        }

        public override void OnInspectorGUI()
        {
            // �f�B���N�V���i�����C�g�ȊO�͏������Ȃ�
            if (!_isDirectionalLight)
            {
                return;
            }

            var loadAssembly = Assembly.Load("Unity.RenderPipelines.Universal.Editor");
            var type = loadAssembly.GetType("UnityEditor.Rendering.Universal.UniversalAdditionLightDataEditor");

            if (type == null)
            {
                return;
            }
            var editor = CreateEditor(target, type);
            editor?.OnInspectorGUI();

            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("��"))
                {
                    SetLightRotation((UniversalAdditionalLightData)target, MORNING);
                }

                if (GUILayout.Button("��"))
                {
                    SetLightRotation((UniversalAdditionalLightData)target, NOON);
                }

                if (GUILayout.Button("��"))
                {
                    SetLightRotation((UniversalAdditionalLightData)target, NIGHT);
                }
            }

            _selectedHour = (Hours)EditorGUILayout.EnumPopup("�ݒ莞��", _selectedHour);

            if (GUILayout.Button("��]"))
            {
                SetLightRotation((UniversalAdditionalLightData)target, (int)_selectedHour);
            }
        }

        /// <summary>
        /// �ݒ莞�Ԃŉ�]������
        /// </summary>
        private void SetLightRotation(UniversalAdditionalLightData lightData, int hour)
        {
            float rotationAngleX = hour * 15f + 270f;
            Quaternion rotation = Quaternion.Euler(rotationAngleX, -30f, 0f);
            lightData.transform.rotation = rotation;
        }
    }
}
