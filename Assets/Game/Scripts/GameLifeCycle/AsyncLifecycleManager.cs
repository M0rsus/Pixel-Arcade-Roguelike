using System.Threading;

namespace Game
{
    public static class AsyncLifecycleManager
    {
        private static CancellationTokenSource _globalCts;
        
        public static CancellationToken GlobalToken { get; private set; }

        static AsyncLifecycleManager()
        {
            Init();
        }

        private static void Init()
        {
            _globalCts?.Cancel();
            _globalCts?.Dispose();
            _globalCts = null;
            
            _globalCts = new CancellationTokenSource();
            GlobalToken = _globalCts.Token;
        }
        public static CancellationTokenSource CreateLinkedSource()
        {
            return CancellationTokenSource.CreateLinkedTokenSource(GlobalToken);
        }

        public static void CancelAll()
        {
            Init(); 
        }
    }
}