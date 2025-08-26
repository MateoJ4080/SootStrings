using System.Collections;

public class ShowerEvent : DayEvent
{
    public override IEnumerator Execute()
    {
        _ = FadeManager.Instance.FadeIn();
        yield return null;
    }
}
