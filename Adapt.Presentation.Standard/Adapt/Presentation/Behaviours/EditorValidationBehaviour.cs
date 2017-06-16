﻿using System.ComponentModel;
using Xamarin.Forms;

namespace Adapt.Presentation.Behaviours
{
    public static class EditorValidationBehaviour
    {
        public static readonly BindableProperty AttachBehaviorProperty =
            BindableProperty.CreateAttached(
                "AttachBehavior",
                typeof(bool),
                typeof(EditorValidationBehaviour),
                false,
                propertyChanged: OnAttachBehaviorChanged);

        public static bool GetAttachBehavior(BindableObject view)
        {
            return (bool)view.GetValue(AttachBehaviorProperty);
        }

        public static void SetAttachBehavior(BindableObject view, bool value)
        {
            view.SetValue(AttachBehaviorProperty, value);
        }

        static void OnAttachBehaviorChanged(BindableObject view, object oldValue, object newValue)
        {
            var Editor = view as Editor;
            if (Editor == null)
            {
                return;
            }

            bool attachBehavior = (bool)newValue;
            if (attachBehavior)
            {
                Editor.TextChanged += OnEditorTextChanged;
            }
            else
            {
                Editor.TextChanged -= OnEditorTextChanged;
            }

            var notifyDataErrorInfo = Editor.BindingContext as INotifyDataErrorInfo;
            if (notifyDataErrorInfo != null)
            {
                notifyDataErrorInfo.ErrorsChanged += NotifyDataErrorInfo_ErrorsChanged;
            }

        }

        private static void NotifyDataErrorInfo_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            Refresh(sender);
        }

        static void OnEditorTextChanged(object sender, TextChangedEventArgs args)
        {
            Refresh(sender);
        }

        private static void Refresh(object sender)
        {
            var Editor = sender as Editor;
            if (Editor == null)
            {
                return;
            }

            DoBackgroundColour(Editor);
        }

        private static void DoBackgroundColour(Editor Editor)
        {
            var notifyDataErrorInfo = Editor.BindingContext as INotifyDataErrorInfo;
            if (notifyDataErrorInfo == null)
            {
                return;
            }

            Editor.BackgroundColor = notifyDataErrorInfo.HasErrors ? Color.Red : Color.Default;
        }
    }
}