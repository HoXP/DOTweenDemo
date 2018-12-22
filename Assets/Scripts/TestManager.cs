public class TestManager
{
    private TestManager() { }

    private static TestManager _instance = null;
    public static TestManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new TestManager();
            }
            return _instance;
        }
    }

    private DOTweenTest _testScritp = null;
    public DOTweenTest TestScritp
    {
        get
        {
            return _testScritp;
        }
        set
        {
            _testScritp = value;
        }
    }
}