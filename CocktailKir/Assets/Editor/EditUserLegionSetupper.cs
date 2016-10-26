
#if UNITY_EDITOR

using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using System;
using System.Collections;

using AkiVACO.XUnityGameObject;

using AkiVACO.EditorUtil;

[CustomEditor(typeof(UserLegionSetupper))]
public class EditUserLegionSetupper : Editor
{
    private static EditorUtilFoldout m_collapsed = new EditorUtilFoldout(true);
    private static EditorUtilFoldout m_collapsedBasis = new EditorUtilFoldout();

    private static EditorUtilFoldout m_collapsedUserChar = new EditorUtilFoldout(true);
    private static EditorUtilFoldout m_collapsedUserLauncher = new EditorUtilFoldout(true);
    private static EditorUtilFoldout m_collapsedUserCamera = new EditorUtilFoldout(true);

    public override void OnInspectorGUI()
    {
        UserLegionSetupper gen = target as UserLegionSetupper;

        UpdateEditorObjectField("セットアップターゲット", ref gen.m_targetLegion, true);

        bool isExecInstance = GUILayout.Button("セットアップ");

        m_collapsed.Invoke(
            "Setup情報",
            () =>
            {
                m_collapsedUserChar.Invoke(
                    "UserChar情報",
                    () =>
                    {
                        gen.m_moveSpeed = EditorGUILayout.FloatField("移動速度", gen.m_moveSpeed);
                        gen.m_jumpPower = EditorGUILayout.FloatField("ジャンプ力", gen.m_jumpPower);
                    });

                m_collapsedUserLauncher.Invoke(
                    "ランチャー情報",
                    () =>
                    {
                        gen.m_pitchSpeed = EditorGUILayout.FloatField("射撃角制御速度", gen.m_pitchSpeed);
                        gen.m_minPitchAngle = EditorGUILayout.FloatField("最小射撃角度", gen.m_minPitchAngle);
                        gen.m_maxPitchAngle = EditorGUILayout.FloatField("最大射撃角度", gen.m_maxPitchAngle);
                        gen.m_yawSpeed = EditorGUILayout.FloatField("旋回速度", gen.m_yawSpeed);
                        gen.m_shotPower = EditorGUILayout.FloatField("射撃力", gen.m_shotPower);
                        gen.m_shot3WayAngle = EditorGUILayout.FloatField("3Wayの角度差", gen.m_shot3WayAngle);
                        gen.m_knockbackTime = EditorGUILayout.FloatField("ノックバック時間", gen.m_knockbackTime);
                        gen.m_chargeShotTime = EditorGUILayout.FloatField("チャージショット時間", gen.m_chargeShotTime);

                        gen.m_reloadTime = EditorGUILayout.FloatField("リロード時間", gen.m_reloadTime);
                    });

                m_collapsedUserCamera.Invoke(
                    "カメラ情報",
                    () =>
                    {
                        gen.m_cameraRotateSpeed = EditorGUILayout.FloatField("自動回転速度", gen.m_cameraRotateSpeed);
                        gen.m_cameraPivotLerpTime = EditorGUILayout.FloatField("カメラ切りかえの時間", gen.m_cameraPivotLerpTime);
                    });

            });

        m_collapsedBasis.Invoke(
            "BaseObjects",
            () =>
            {
                UpdateEditorObjectField("UserCtrl", ref gen.m_baseUserCtrl);
                UpdateEditorObjectField("UserMeshP1", ref gen.m_baseUserMesh[0]);
                UpdateEditorObjectField("UserMeshP2", ref gen.m_baseUserMesh[1]);
                UpdateEditorObjectField("UserMeshP3", ref gen.m_baseUserMesh[2]);
                UpdateEditorObjectField("UserMeshP4", ref gen.m_baseUserMesh[3]);
                UpdateEditorObjectField("UserCamera", ref gen.m_baseUserCamera);
            });

        if (isExecInstance)
        {
            PrefabUtility.ReplacePrefab(gen.gameObject, PrefabUtility.GetPrefabParent(gen.gameObject));

            UpdateUserPrefab(gen);
            CleanupUsers(gen.m_targetLegion);
            InstantiateUsers(gen);
        }
    }

    private void UpdateEditorObjectField(string msg, ref GameObject obj, bool allowSceneObjects = false)
    {
        obj = EditorGUILayout.ObjectField(msg, obj, typeof(GameObject), allowSceneObjects) as GameObject;
    }

    private void UnilUpdatePrefab<T>(GameObject baseprefab, UnityAction<SerializedObject> callback)
    {
        GameObject prefab = PrefabUtility.InstantiatePrefab(baseprefab) as GameObject;
        {
            T[] table = prefab.GetComponentsInChildren<T>();
            foreach (T cs in table)
            {
                SerializedObject ser = new SerializedObject(cs as UnityEngine.Object);
                ser.Update();
                callback.Invoke(ser);
                ser.ApplyModifiedProperties();
            }
        }
        PrefabUtility.ReplacePrefab(prefab, baseprefab);
        GameObject.DestroyImmediate(prefab);
    }

    private void UpdateUserPrefab(UserLegionSetupper setupper)
    {
        UnilUpdatePrefab<CharAnimateCtrl>(
            setupper.m_baseUserCtrl, 
            (ser) => 
            {
                SerializedProperty moveSpeed = ser.FindProperty("m_moveSpeed");
                moveSpeed.floatValue = setupper.m_moveSpeed;
                SerializedProperty jumpPower = ser.FindProperty("m_jumpPower");
                jumpPower.floatValue = setupper.m_jumpPower;
            });

        UnilUpdatePrefab<LauncherCtrl>(
            setupper.m_baseUserCtrl,
            (ser) =>
            {
                SerializedProperty pitchSpeed = ser.FindProperty("m_pitchSpeed");
                pitchSpeed.floatValue = setupper.m_pitchSpeed;
                SerializedProperty minPitchAngle = ser.FindProperty("m_minPitchAngle");
                minPitchAngle.floatValue = setupper.m_minPitchAngle;
                SerializedProperty maxPitchAngle = ser.FindProperty("m_maxPitchAngle");
                maxPitchAngle.floatValue = setupper.m_maxPitchAngle;
                SerializedProperty yawSpeed = ser.FindProperty("m_yawSpeed");
                yawSpeed.floatValue = setupper.m_yawSpeed;
                SerializedProperty shotPower = ser.FindProperty("m_shotPower");
                shotPower.floatValue = setupper.m_shotPower;
                SerializedProperty shot3WayAngle = ser.FindProperty("m_shot3WayAngle");
                shot3WayAngle.floatValue = setupper.m_shot3WayAngle;
                SerializedProperty knockbackTime = ser.FindProperty("m_knockbackTime");
                knockbackTime.floatValue = setupper.m_knockbackTime;
                SerializedProperty chargeShotTime = ser.FindProperty("m_chargeShotTime");
                chargeShotTime.floatValue = setupper.m_chargeShotTime;
            });

        UnilUpdatePrefab<LauncherMagazine>(
            setupper.m_baseUserCtrl,
            (ser) =>
            {
                SerializedProperty reloadTime = ser.FindProperty("m_reloadTime");
                reloadTime.floatValue = setupper.m_reloadTime;
            });

        UnilUpdatePrefab<UserCameraAutoCtrl>(
            setupper.m_baseUserCamera,
            (ser) =>
            {
                SerializedProperty rotateSpeed = ser.FindProperty("m_rotateSpeed");
                rotateSpeed.floatValue = setupper.m_cameraRotateSpeed;
                SerializedProperty pivotLerpTime = ser.FindProperty("m_pivotLerpTime");
                pivotLerpTime.floatValue = setupper.m_cameraPivotLerpTime;
            });

    }

    private void CleanupUsers(GameObject targetLegion)
    {
        for (int i = 0; i < targetLegion.GetChildCount(); ++i)
        {
            GameObject objUnit = targetLegion.GetChild(i);
            while (objUnit.GetChildCount() != 0)
            {
                GameObject.DestroyImmediate(objUnit.GetChild(0));
            }
        }
    }

    private void InstantiateUsers(UserLegionSetupper setupper)
    {
        GameObject targetLegion = setupper.m_targetLegion;

        UnityAction<int> act = (idx) => 
        {
            GameObject unit = targetLegion.GetChild(idx);
            GameObject basemesh = setupper.m_baseUserMesh[idx];
            GameObject ctrl = GameObject.Instantiate(setupper.m_baseUserCtrl);
            ctrl.name = setupper.m_baseUserCtrl.name;
            GameObject mesh = GameObject.Instantiate(basemesh);
            mesh.name = "Mesh";
            GameObject defmesh = ctrl.FindChild("DefMesh");
            int meshindex = defmesh.transform.GetSiblingIndex();
            GameObject.DestroyImmediate(defmesh);
            mesh.SetParent(ctrl, false);
            ctrl.SetParent(unit, false);
            mesh.transform.SetSiblingIndex(meshindex);  // DefMeshと同じヒエラルキー順位にソート

            GameObject camera = GameObject.Instantiate(setupper.m_baseUserCamera);
            camera.name = setupper.m_baseUserCamera.name;
            camera.SetParent(unit, false);

            SetupUnit(ctrl, camera, idx);
        };

        act.Invoke(0);
        act.Invoke(1);
        act.Invoke(2);
        act.Invoke(3);
    }

    private void SetupUnit(GameObject ctrl, GameObject camera, int index)
    {
        Rect[] viewportTable = new Rect[4]
            {
                new Rect(0.0f, 0.5f, 0.5f, 0.5f),
                new Rect(0.5f, 0.5f, 0.5f, 0.5f),
                new Rect(0.0f, 0.0f, 0.5f, 0.5f),
                new Rect(0.5f, 0.0f, 0.5f, 0.5f),
            };

        // UserData
        {
            UserData cs = ctrl.GetComponent<UserData>();
            cs.userID = (UserID)index;
        }

        // UserCharCtrl
        {
            UserCharCtrl cs = ctrl.GetComponentInChildren<UserCharCtrl>();
            Camera csCamera = camera.GetComponentInChildren<Camera>();
            cs.Edit_SetCamera(csCamera);
            csCamera.rect = viewportTable[index];
        }

        // UserCameraAutoCtrl
        {
            UserCameraAutoCtrl cs = camera.GetComponent<UserCameraAutoCtrl>();
            cs.Edit_SetTargetUser(ctrl.transform);
        }

    }

}

#endif