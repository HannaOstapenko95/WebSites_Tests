using System.Threading;


namespace CreditCards.UITests
{
    public class DemoHelper
    {
    /// <summary>
    /// Brief delay to slow down browser interactions
    /// </summary>
    /// <param name="secondsToPause"></param>
        public static void Pause( int secondsToPause = 800)
        {
            Thread.Sleep(secondsToPause);
        }
    }
}
