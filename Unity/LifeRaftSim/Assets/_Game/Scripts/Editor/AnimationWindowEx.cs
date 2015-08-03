using System;
using System.Collections;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using cls = Game.AnimationWindowEx;

namespace Game
{
    /// <summary>
    /// Extends the animation window in the Unity editor.
    /// </summary>
    public class AnimationWindowEx
    {
        /// <summary>
        /// The hotkey for moving to the first frame of the current animation.
        /// </summary>
        private const KeyCode moveToFirstFrameHotKey = KeyCode.Z;

        /// <summary>
        /// The hotkey for moving the the last frame of the current animation.
        /// </summary>
        private const KeyCode moveToLastFrameHotKey = KeyCode.X;

        /// <summary>
        /// The reference to the <see cref="T:UnityEditor.AnimEditor"/> type.
        /// </summary>
        private static readonly Type animEditorType = Type.GetType("UnityEditor.AnimEditor, UnityEditor");

        /// <summary>
        /// The reference to the <see cref="M:UnityEditor.AnimEditor.GetAllAnimationWindows"/> method.
        /// </summary>
        private static readonly MethodInfo getAllAnimationWindowsMethod = animEditorType.GetMethod("GetAllAnimationWindows", BindingFlags.Public | BindingFlags.Static);

        /// <summary>
        /// The reference to the <see cref="F:UnityEditor.AnimEditor.m_State"/> field.
        /// </summary>
        private static readonly FieldInfo stateField = animEditorType.GetField("m_State", BindingFlags.NonPublic | BindingFlags.Instance);

        /// <summary>
        /// The reference to the <see cref="M:UnityEditor.AnimEditor.Repaint"/> method.
        /// </summary>
        private static readonly MethodInfo repaintMethod = animEditorType.GetMethod("Repaint", BindingFlags.Public | BindingFlags.Instance);

        /// <summary>
        /// The reference to the <see cref="T:UnityEditor.AnimationWindowState"/> type.
        /// </summary>
        private static readonly Type animationWindowStateType = Type.GetType("UnityEditorInternal.AnimationWindowState, UnityEditor");

        /// <summary>
        /// The reference to the <see cref="P:UnityEditor.AnimationWindowState.frame"/> property.
        /// </summary>
        private static readonly PropertyInfo frameProperty = animationWindowStateType.GetProperty("frame", BindingFlags.Public | BindingFlags.Instance);

        /// <summary>
        /// The reference to the <see cref="P:UnityEditor.AnimationWindowState.minFrame"/> property.
        /// </summary>
        private static readonly PropertyInfo minFrameProperty = animationWindowStateType.GetProperty("minFrame", BindingFlags.Public | BindingFlags.Instance);

        /// <summary>
        /// The reference to the <see cref="P:UnityEditor.AnimationWindowState.maxFrame"/> property.
        /// </summary>
        private static readonly PropertyInfo maxFrameProperty = animationWindowStateType.GetProperty("maxFrame", BindingFlags.Public | BindingFlags.Instance);

        /// <summary>
        /// Gets or sets the current frame in the animation window.
        /// </summary>
        public static int Frame
        {
            get
            {
                return (int)cls.frameProperty.GetValue(cls.State, null);
            }
            set
            {
                cls.frameProperty.SetValue(cls.State, value, null);
                cls.repaintMethod.Invoke(cls.CurrentAnimEditor, null);
            }
        }

        /// <summary>
        /// Gets the minimum frame index in the current animation window.
        /// </summary>
        public static int MinFrame
        {
            get
            {
                return (int)cls.minFrameProperty.GetValue(cls.State, null);
            }
        }

        /// <summary>
        /// Gets the maximum frame index in the current animation window.
        /// </summary>
        public static int MaxFrame

        {
            get
            {
                return (int)cls.maxFrameProperty.GetValue(cls.State, null);
            }
        }

        /// <summary>
        /// Gets the state object of the current animation window.
        /// </summary>
        public static object State
        {
            get
            {
                return cls.stateField.GetValue(cls.CurrentAnimEditor);
            }
        }

        /// <summary>
        /// Gets the current animation window.
        /// </summary>
        public static object CurrentAnimEditor
        {
            get
            {
                var windows = cls.getAllAnimationWindowsMethod.Invoke(null, null);
                var window = cls.GetFirstElementInArray(windows);
                return window;
            }
        }

        /// <summary>
        /// Selects the first frame.
        /// </summary>
        [MenuItem("Edit/Animation/First Frame _Z", validate = false)]
        private static void SelectFirstFrame()
        {
            cls.Frame = cls.MinFrame;
        }

        /// <summary>
        /// Returns a value indicating whether or not an animation window is available.
        /// </summary>
        /// <returns>
        /// true if an animation windows is available; otherwise false.
        /// </returns>
        [MenuItem("Edit/Animation/First Frame _Z", validate = true)]
        private static bool ValidateSelectsFirstFrame()
        {
            return cls.CurrentAnimEditor != null;
        }

        /// <summary>
        /// Selects the last frame.
        /// </summary>
        [MenuItem("Edit/Animation/Last Frame _X", validate = false)]
        private static void SelectLastFrame()
        {
            cls.Frame = cls.MaxFrame;
        }

        /// <summary>
        /// Returns a value indicating whether or not an animation window is available.
        /// </summary>
        /// <returns>
        /// true if an animation windows is available; otherwise false.
        /// </returns>
        [MenuItem("Edit/Animation/Last Frame _X", validate = true)]
        private static bool ValidateMoveToLastFrame()
        {
            return cls.CurrentAnimEditor != null;
        }

        /// <summary>
        /// Returns the first element of the specified <paramref name="array"/>.
        /// </summary>
        /// <param name="array">
        /// The array whose first element to return.
        /// </param>
        /// <returns>
        /// The first element of the specified array; null if the array is empty.
        /// </returns>
        private static object GetFirstElementInArray(object array)
        {
            var enumerable = array as IEnumerable;
            foreach (var element in enumerable)
            {
                return element;
            }
            return null;
        }
    }
}
