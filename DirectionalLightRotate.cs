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
            [InspectorName("0時")]
            Hour0,
            [InspectorName("1時")]
            Hour1,
            [InspectorName("2時")]
            Hour2,
            [InspectorName("3時")]
            Hour3,
            [InspectorName("4時")]
            Hour4,
            [InspectorName("5時")]
            Hour5,
            [InspectorName("6時")]
            Hour6,
            [InspectorName("7時")]
            Hour7,
            [InspectorName("8時")]
            Hour8,
            [InspectorName("9時")]
            Hour9,
            [InspectorName("10時")]
            Hour10,
            [InspectorName("11時")]
            Hour11,
            [InspectorName("12時")]
            Hour12,
            [InspectorName("13時")]
            Hour13,
            [InspectorName("14時")]
            Hour14,
            [InspectorName("15時")]
            Hour15,
            [InspectorName("16時")]
            Hour16,
            [InspectorName("17時")]
            Hour17,
            [InspectorName("18時")]
            Hour18,
            [InspectorName("19時")]
            Hour19,
            [InspectorName("20時")]
            Hour20,
            [InspectorName("21時")]
            Hour21,
            [InspectorName("22時")]
            Hour22,
            [InspectorName("23時")]
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
            // ディレクショナルライト以外は処理しない
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
                if (GUILayout.Button("朝"))
                {
                    SetLightRotation((UniversalAdditionalLightData)target, MORNING);
                }

                if (GUILayout.Button("昼"))
                {
                    SetLightRotation((UniversalAdditionalLightData)target, NOON);
                }

                if (GUILayout.Button("夜"))
                {
                    SetLightRotation((UniversalAdditionalLightData)target, NIGHT);
                }
            }

            _selectedHour = (Hours)EditorGUILayout.EnumPopup("設定時間", _selectedHour);

            if (GUILayout.Button("回転"))
            {
                SetLightRotation((UniversalAdditionalLightData)target, (int)_selectedHour);
            }
        }

        /// <summary>
        /// 設定時間で回転させる
        /// </summary>
        private void SetLightRotation(UniversalAdditionalLightData lightData, int hour)
        {
            float rotationAngleX = hour * 15f + 270f;
            Quaternion rotation = Quaternion.Euler(rotationAngleX, -30f, 0f);
            lightData.transform.rotation = rotation;
        }
    }
}
