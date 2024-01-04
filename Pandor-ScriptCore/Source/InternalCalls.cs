using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Pandor
{
    public static class InternalCalls
    {
        // GameObject Class
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static bool Object_HasComponent(ulong objectID, Type componentType);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static ulong Object_GetComponent(ulong objectID, Type componentType);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Object_GetComponents(ulong objectID, out ulong[] components);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static ulong Object_AddComponent(ulong objectID, Type componentType);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static object Object_GetScriptInstance(ulong objectID, ScriptComponent scriptClass);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static object Object_Destroy(ulong objectID);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static ulong Object_GetParent(ulong objectID);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static ulong Object_GetChild(ulong objectID, uint id);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Object_GetChildren(ulong objectID, out ulong[] children);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static string Object_GetName(ulong objectID);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Object_SetName(ulong objectID, ref string name);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Object_ClearParent(ulong objectID);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Object_SetParent(ulong objectID, ulong parentID);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Object_SetActive(ulong objectID, bool active);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static bool Object_GetActive(ulong objectID);

        // BaseComponent Class
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static bool Component_GetEnable(ulong objectID, ulong componentID);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Component_SetEnable(ulong objectID, ulong componentID, ref bool value);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Component_Destroy(ulong objectID, ulong componentID);

        // ScriptComponent Class
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static ulong Script_FindObjectByName(string name);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static ulong Script_Instantiate(ulong objectID, Vector3 position, Quaternion rotation, ulong parent);

        // Rigidbody
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static float Rigidbody_GetMass(ulong objectID, ulong componentID);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Rigidbody_SetMass(ulong objectID, ulong componentID, ref float mass);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Rigidbody_GetVelocity(ulong objectID, ulong componentID, out Vector3 velocity);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Rigidbody_SetVelocity(ulong objectID, ulong componentID, ref Vector3 velocity);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Rigidbody_GetAngularVelocity(ulong objectID, ulong componentID, out Vector3 velocity);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Rigidbody_SetAngularVelocity(ulong objectID, ulong componentID, ref Vector3 velocity);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static bool Rigidbody_GetGravity(ulong objectID, ulong componentID);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Rigidbody_SetGravity(ulong objectID, ulong componentID, ref bool value);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static bool Rigidbody_GetKinematic(ulong objectID, ulong componentID);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Rigidbody_SetKinematic(ulong objectID, ulong componentID, ref bool value);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static bool Rigidbody_GetRotationX(ulong objectID, ulong componentID);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Rigidbody_SetRotationX(ulong objectID, ulong componentID, ref bool value);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static bool Rigidbody_GetRotationY(ulong objectID, ulong componentID);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Rigidbody_SetRotationY(ulong objectID, ulong componentID, ref bool value);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static bool Rigidbody_GetRotationZ(ulong objectID, ulong componentID);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Rigidbody_SetRotationZ(ulong objectID, ulong componentID, ref bool value);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Rigidbody_AddForce(ulong objectID, ulong componentID, Vector3 force);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Rigidbody_AddTorque(ulong objectID, ulong componentID, Vector3 torque);

        // Transform Class
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Transform_GetPosition(ulong objectID, out Vector3 translation);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Transform_SetPosition(ulong objectID, ref Vector3 translation);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Transform_GetRotation(ulong objectID, out Quaternion rotation);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Transform_SetRotation(ulong objectID, ref Quaternion rotation);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Transform_GetEulerAngles(ulong objectID, out Vector3 eulerAngles);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Transform_SetEulerAngles(ulong objectID, ref Vector3 eulerAngles);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Transform_GetLocalPosition(ulong objectID, out Vector3 localPosition);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Transform_SetLocalPosition(ulong objectID, ref Vector3 localPosition);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Transform_GetLocalRotation(ulong objectID, out Quaternion localRotation);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Transform_SetLocalRotation(ulong objectID, ref Quaternion localRotation);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Transform_GetLocalScale(ulong objectID, out Vector3 localScale);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Transform_SetLocalScale(ulong objectID, ref Vector3 localScale);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Transform_GetLocalEulerAngles(ulong objectID, out Vector3 localEulerAngles);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Transform_SetLocalEulerAngles(ulong objectID, ref Vector3 localEulerAngles);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Transform_GetForward(ulong objectID, out Vector3 forward);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Transform_GetUp(ulong objectID, out Vector3 up);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Transform_GetRight(ulong objectID, out Vector3 right);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Transform_RotateArround(ulong objectID, Vector3 target, Vector3 axis, float angle);

        // Input Class
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static bool Input_IsKeyDown(Key key);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static bool Input_IsKeyPressed(Key key);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static bool Input_IsKeyReleased(Key key);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Input_GetMousePos(out Vector2 vec);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Input_EnableCursor();
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Input_DisableCursor();
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Input_FixCursor();

        // Debug Class
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Debug_Print(string log);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Debug_PrintWarning(string log);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Debug_PrintError(string log);

        // Physic
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static bool Physic_Raycast(Vector3 origin, Vector3 direction, float distanceMax,
            out bool hit, out Vector3 position, out Vector3 normal, out Collider collider, out float distance, out uint faceIndex);

        // Text
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static string Text_GetText(ulong objectID, ulong componentID);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Text_SetText(ulong objectID, ulong componentID, string value);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static float Text_GetScale(ulong objectID, ulong componentID);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Text_SetScale(ulong objectID, ulong componentID, ref float value);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Text_GetColor(ulong objectID, ulong componentID, out Vector4 color);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Text_SetColor(ulong objectID, ulong componentID, ref Vector4 color);

        // Button
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static string Button_GetDefaultColor(ulong objectID, ulong componentID, out Vector4 color);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Button_SetDefaultColor(ulong objectID, ulong componentID, ref Vector4 color);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Button_GetHighlightedColor(ulong objectID, ulong componentID, out Vector4 color);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Button_SetHighlightedColor(ulong objectID, ulong componentID, ref Vector4 color);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Button_GetPressedColor(ulong objectID, ulong componentID, out Vector4 color);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Button_SetPressedColor(ulong objectID, ulong componentID, ref Vector4 color);
        // SceneManager
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static bool SceneManager_LoadScene(string name);
        // Animator
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static bool Animator_SetBoolean(ulong objectID, ulong componentID, string name, bool value);
        // Application
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static bool Application_QuitRequest();
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void Application_SetTimeScale(float value);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static float Application_GetTimeScale();

        //SoundEmitter
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void SoundEmitter_Play(ulong objectID, ulong componentID);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void SoundEmitter_Stop(ulong objectID, ulong componentID);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void SoundEmitter_Pause(ulong objectID, ulong componentID);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void SoundEmitter_FadeIn(ulong objectID, ulong componentID, int millisecondsFading);
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static void SoundEmitter_SetLooping(ulong objectID, ulong componentID, bool value);

        //AudioManager
        [MethodImpl(MethodImplOptions.InternalCall)]
        internal extern static bool AudioManager_PlaySoundByName(string soundName);
    }
}
