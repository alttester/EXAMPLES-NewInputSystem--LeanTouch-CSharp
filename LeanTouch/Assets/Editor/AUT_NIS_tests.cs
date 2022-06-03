using NUnit.Framework;
using Altom.AltUnityDriver;
using System.Threading;

public class AUT_NIS_tests
{
    public AltUnityDriver altUnityDriver;
    string testScene = "Assets/Plugins/CW/LeanTouch/Examples/05    Direction 4.unity";
    string testScene2 = "Assets/Plugins/CW/LeanTouch/Examples/18    Destroy.unity";
    string testScene3 = "Assets/Plugins/CW/LeanTouch/Examples/15 Tap To Select.unity";
    string testScene4 = "Assets/Plugins/CW/LeanTouch/Examples/01 Finger Down.unity";
    string testScene5 = "Assets/Plugins/CW/LeanTouch/Examples/07 Finger Tap.unity";
    //Before any test it connects with the socket
    [OneTimeSetUp]
    public void SetUp()
    {
        altUnityDriver =new AltUnityDriver();
    }

    //At the end of the test closes the connection with the socket
    [OneTimeTearDown]
    public void TearDown()
    {
        altUnityDriver.Stop();
    }


    [Test]
    public void TestTapAtCoordinates()
    {
        altUnityDriver.LoadScene(testScene4);
        Thread.Sleep(1000);
        var capsule = altUnityDriver.FindObject(By.NAME,"Red Sphere");
        altUnityDriver.Tap(new AltUnityVector2(100,100));
        Thread.Sleep(500);
        var newCapsule = altUnityDriver.FindObject(By.NAME,"Red Sphere");
        var positionAfterTap = newCapsule.getScreenPosition();
        Assert.AreEqual(positionAfterTap.x,100f);
        Assert.AreEqual(positionAfterTap.y,100f);
    }

    [Test]
    public void TestSwipeUp()
    {
        altUnityDriver.LoadScene(testScene);
        var positions = new[]
        {
            new AltUnityVector2(100,100),
            new AltUnityVector2(100,120),
            new AltUnityVector2(100,150)
        };

        altUnityDriver.MultipointSwipe(positions, 0.1f);
        altUnityDriver.WaitForObject(By.PATH,"//Canvas/Text[@text=N]");
    }
    [Test]
    public void TestSwipeDown()
    {
        altUnityDriver.LoadScene(testScene);
        var positions = new[]
        {
            new AltUnityVector2(200,200),
            new AltUnityVector2(200,180),
            new AltUnityVector2(200,150),
            new AltUnityVector2(200,120),
            new AltUnityVector2(200,100)

        };
        altUnityDriver.MultipointSwipe(positions, 0.1f);
        altUnityDriver.WaitForObject(By.PATH,"//Canvas/Text[@text=S]");
    }
     
    [Test]
    public void TestSwipeRight()
    {
        altUnityDriver.LoadScene(testScene);
        altUnityDriver.Swipe(new AltUnityVector2(100,100),new AltUnityVector2(150,100),0.1f);
        altUnityDriver.WaitForObject(By.PATH,"//Canvas/Text[@text=E]");
    }
     
    [Test]
    public void TestSwipeLeft()
    {
        altUnityDriver.LoadScene(testScene);
        altUnityDriver.Swipe(new AltUnityVector2(100,100),new AltUnityVector2(50,100),0.1f);
        altUnityDriver.WaitForObject(By.PATH,"//Canvas/Text[@text=W]");
    }

    [Test]
    public void TestBeginEndTouch()
    {
        altUnityDriver.LoadScene(testScene4);
        Thread.Sleep(1000);
        var capsule = altUnityDriver.FindObject(By.NAME,"Red Sphere");
        var position =  capsule.getScreenPosition();
        var fingerId = altUnityDriver.BeginTouch(new AltUnityVector2(100,100));
        altUnityDriver.EndTouch(fingerId);
        Thread.Sleep(500);
        var newCapsule = altUnityDriver.FindObject(By.NAME,"Red Sphere");
        var positionAfterTap = newCapsule.getScreenPosition();
        Assert.AreNotEqual(position.x,positionAfterTap.x);
        Assert.AreNotEqual(position.y,positionAfterTap.y);
    }


}