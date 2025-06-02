namespace Assets.Scripts.Platform
{

    public class H5VideoExpo : IExpo
    {

        private ExpoOptions _options;

        public void Initialize(ExpoOptions options)
        {
            _options = options;
        }

        public void Destroy()
        {

            _options = null;
        }

        public void Show(ExpoOptions options)
        {
            Initialize(options);
            if (_options == null)
            {
                return;
            }

            _options.OnClose?.Invoke(true);
        }

    }

}
