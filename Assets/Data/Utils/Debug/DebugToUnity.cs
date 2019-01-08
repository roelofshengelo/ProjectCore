//using UnityEngine;
//using UnityEngine.UI;

//namespace Assets.Data.Utils.Debug
//{


//    public class DebugToUnity : MonoBehaviour
//    {

//        // Needs to be public for Unity
//        // ReSharper disable once MemberCanBePrivate.Global
//        public Text text = null;

//        private string _message = "Debug is loading ...";
//        private string _prevMessage = "Debug is loading...";

//        private bool debugIsLoaded = false;

//        private void OnEnable()
//        {
//            if (text is null) return;
//            else debugIsLoaded = true;

//            SetMessage("Debug loaded correctly!");
//        }

//        private void Update()
//        {
//            if (!debugIsLoaded) return;
//            if (_message.Equals(_prevMessage)) return;

//            // Does this work as planned?
//            text.text = _prevMessage = _message;
//        }

//        public void SetMessage(string message)
//        {
//            //text.text = message;
//            _message = message;
//        }
//    }
//}
