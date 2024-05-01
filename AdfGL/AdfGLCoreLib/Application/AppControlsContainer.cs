using System.Collections;

namespace AdfGLCoreLib.Application
{
    public class AppControlsContainer : IEnumerable<AppControlBase>
    {
        AppControlBase _userIntefraceControl = null!;
        AppControlBase _mainControl = null!;
        readonly List<AppControlBase> s_controls = new List<AppControlBase>();

        public AppControlBase UserInterface
        {
            get { return _userIntefraceControl; }
            set
            {
                _userIntefraceControl = value;
            }
        }

        public AppControlBase Main
        {
            get { return _mainControl; }
            set
            {
                _mainControl = value;
            }
        }

        public void AttachChild(AppControlBase controller)
        {
            if (controller.IsEnabled) return;

            if (false == s_controls.Contains(controller))
            {
                controller.Enable();
                s_controls.Add(controller);
            }
        }

        public void DetachChild(AppControlBase controller)
        {
            if (false == controller.IsEnabled) return;

            if (true == s_controls.Contains(controller))
            {
                controller.Disable();
                s_controls.Remove(controller);
            }
        }

        public IEnumerable<AppControlBase> GetEventEnumerator()
        {
            if (_userIntefraceControl != null && _userIntefraceControl.IsEnabled == true)
            {
                yield return _userIntefraceControl;
            }

            if (_mainControl != null && _mainControl.IsEnabled == true)
            {
                yield return _mainControl;
            }

            foreach (var control in s_controls)
            {
                if (true == control.IsEnabled)
                {
                    yield return control;
                }
            }
        }

        public IEnumerable<AppControlBase> GetRenderEnumerator()
        {
            if (_mainControl != null && _mainControl.IsEnabled == true)
            {
                yield return _mainControl;
            }

            foreach (var control in s_controls)
            {
                if (true == control.IsEnabled)
                {
                    yield return control;
                }
            }

            if (_userIntefraceControl != null && _userIntefraceControl.IsEnabled == true)
            {
                yield return _userIntefraceControl;
            }
        }

        #region IEnumerable
        public IEnumerator<AppControlBase> GetEnumerator()
        {
            return s_controls.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion IEnumerable
    }
}
