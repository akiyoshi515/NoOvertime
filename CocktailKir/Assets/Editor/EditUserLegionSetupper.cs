
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
    private static EDUtilFoldout m_collapsed = new EDUtilFoldout(true);
    private static EDUtilFoldout m_collapsedBasis = new EDUtilFoldout();

    private static EDUtilFoldout m_collapsedUserChar = new EDUtilFoldout(true);
    private static EDUtilFoldout m_collapsedUserLauncher = new EDUtilFoldout(true);
    private static EDUtilFoldout m_collapsedUserCamera = new EDUtilFoldout(true);

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

                EditorGUILayout.LabelField("UserMesh");
                EditorGUI.indentLevel++;
                UpdateEditorObjectField("UserMeshP1", ref gen.m_baseUserMesh[0]);
                UpdateEditorObjectField("UserMeshP2", ref gen.m_baseUserMesh[1]);
                UpdateEditorObjectField("UserMeshP3", ref gen.m_baseUserMesh[2]);
                UpdateEditorObjectField("UserMeshP4", ref gen.m_baseUserMesh[3]);
                EditorGUI.indentLevel--;

                EditorGUILayout.LabelField("UI UserNo");
                EditorGUI.indentLevel++;
                UpdateEditorObjectField("UserNoP1", ref gen.m_baseUserUIUserNo[0]);
                UpdateEditorObjectField("UserNoP2", ref gen.m_baseUserUIUserNo[1]);
                UpdateEditorObjectField("UserNoP3", ref gen.m_baseUserUIUserNo[2]);
                UpdateEditorObjectField("UserNoP4", ref gen.m_baseUserUIUserNo[3]);
                EditorGUI.indentLevel--;

                EditorGUILayout.LabelField("UI Magazine");
                EditorGUI.indentLevel++;
                UpdateEditorObjectField("MagazineP1", ref gen.m_baseUserUIMagazine[0]);
                UpdateEditorObjectField("MagazineP2", ref gen.m_baseUserUIMagazine[1]);
                UpdateEditorObjectField("MagazineP3", ref gen.m_baseUserUIMagazine[2]);
                UpdateEditorObjectField("MagazineP4", ref gen.m_baseUserUIMagazine[3]);
                EditorGUI.indentLevel--;

                EditorGUILayout.Space();
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

    private void UpdateUserPrefab(UserLegionSetupper setupper)
    {
        EDUtilFunctions.EditApplySerializedPrefab<CharAnimateCtrl>(
            setupper.m_baseUserCtrl, 
            (ser) => 
            {
                ser.FindProperty("m_moveSpeed").floatValue = setupper.m_moveSpeed;
                ser.FindProperty("m_jumpPower").floatValue = setupper.m_jumpPower;
            });

        EDUtilFunctions.EditApplySerializedPrefab<LauncherCtrl>(
            setupper.m_baseUserCtrl,
            (ser) =>
            {
                ser.FindProperty("m_pitchSpeed").floatValue = setupper.m_pitchSpeed;
                ser.FindProperty("m_minPitchAngle").floatValue = setupper.m_minPitchAngle;
                ser.FindProperty("m_maxPitchAngle").floatValue = setupper.m_maxPitchAngle;
                ser.FindProperty("m_yawSpeed").floatValue = setupper.m_yawSpeed;
                ser.FindProperty("m_shotPower").floatValue = setupper.m_shotPower;
                ser.FindProperty("m_shot3WayAngle").floatValue = setupper.m_shot3WayAngle;
                ser.FindProperty("m_knockbackTime").floatValue = setupper.m_knockbackTime;
                ser.FindProperty("m_chargeShotTime").floatValue = setupper.m_chargeShotTime;
            });

        EDUtilFunctions.EditApplySerializedPrefab<LauncherMagazine>(
            setupper.m_baseUserCtrl,
            (ser) =>
            {
                SerializedProperty reloadTime = ser.FindProperty("m_reloadTime");
                reloadTime.floatValue = setupper.m_reloadTime;
            });

        EDUtilFunctions.EditApplySerializedPrefab<UserCameraAutoCtrl>(
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
            // Target Unit
            GameObject unit = targetLegion.GetChild(idx);

            // Ctrl
            GameObject ctrl = GameObject.Instantiate(setupper.m_baseUserCtrl);
            ctrl.name = setupper.m_baseUserCtrl.name;

            // Mesh
            GameObject mesh = GameObject.Instantiate(setupper.m_baseUserMesh[idx]);
            mesh.name = "Mesh";

            // UI UserNo
            GameObject uiUserNo = GameObject.Instantiate(setupper.m_baseUserUIUserNo[idx]);
            uiUserNo.name = "UIUserNo";
            uiUserNo.SetParent(ctrl, false);

            // UI Magazine
            GameObject uiMagazine = GameObject.Instantiate(setupper.m_baseUserUIMagazine[idx]);
            uiMagazine.name = "UIMagazine";
            uiMagazine.SetParent(ctrl, false);

            // Switch Mesh
            GameObject defmesh = ctrl.FindChild("DefMesh");
            int meshindex = defmesh.transform.GetSiblingIndex();
            GameObject.DestroyImmediate(defmesh);
            mesh.SetParent(ctrl, false);
            mesh.transform.SetSiblingIndex(meshindex);  // DefMeshと同じヒエラルキー順位にソート

            // Set Ctrl for TargerUnit
            ctrl.SetParent(unit, false);

            // Camera
            GameObject camera = GameObject.Instantiate(setupper.m_baseUserCamera);
            camera.name = setupper.m_baseUserCamera.name;

            // Set Camera for TargerUnit
            camera.SetParent(unit, false);

            // Setup Parameter
            SetupUnit(ctrl, camera, idx);
        };

        act.Invoke(0);
        act.Invoke(1);
        act.Invoke(2);
        act.Invoke(3);

        SetupLegion(targetLegion);
    }

    private void SetupLegion(GameObject targetLegion)
    {
        // TODO
        EDUtilFunctions.EditApplySerializedPrefab<UserLegionCtrl>(
            targetLegion, 
            (ser) => 
            {
                GameObject user1 = targetLegion.GetChild(0).gameObject;
                GameObject user2 = targetLegion.GetChild(1).gameObject;
                GameObject user3 = targetLegion.GetChild(2).gameObject;
                GameObject user4 = targetLegion.GetChild(3).gameObject;

                /* TypeName
                SerializedProperty uiUserNo1 = ser.FindProperty("m_uiUserNo1");
                uiUserNo1.objectReferenceValue = user1GetComponentInChildren<ISwitchViewCtrl>();
                SerializedProperty uiUserNo2 = ser.FindProperty("m_uiUserNo2");
                uiUserNo2.objectReferenceValue = user2.GetComponentInChildren<ISwitchViewCtrl>();
                SerializedProperty uiUserNo3 = ser.FindProperty("m_uiUserNo3");
                uiUserNo3.objectReferenceValue = user3.GetComponentInChildren<ISwitchViewCtrl>();
                SerializedProperty uiUserNo4 = ser.FindProperty("m_uiUserNo4");
                uiUserNo4.objectReferenceValue = user4.GetComponentInChildren<ISwitchViewCtrl>();

                SerializedProperty uiMagazine1 = ser.FindProperty("m_uiMagazine1");
                uiMagazine1.objectReferenceValue = user1.GetComponentInChildren<ISwitchViewCtrl>();
                SerializedProperty uiMagazine2 = ser.FindProperty("m_uiMagazine2");
                uiMagazine2.objectReferenceValue = user2.GetComponentInChildren<ISwitchViewCtrl>();
                SerializedProperty uiMagazine3 = ser.FindProperty("m_uiMagazine3");
                uiMagazine3.objectReferenceValue = user3.GetComponentInChildren<ISwitchViewCtrl>();
                SerializedProperty uiMagazine4 = ser.FindProperty("m_uiMagazine4");
                uiMagazine4.objectReferenceValue = user4.GetComponentInChildren<ISwitchViewCtrl>();
                */
            });

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