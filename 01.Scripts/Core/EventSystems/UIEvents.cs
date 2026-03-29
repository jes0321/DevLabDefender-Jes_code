namespace Works.JES._01.Scripts.Core.EventSystems
{
    public class UIEvents
    {
        public static OnOffEscEvent OnOffEscEvent = new OnOffEscEvent();
        public static BlockEscEvent BlockEscEvent = new BlockEscEvent();
    }

    public class OnOffEscEvent : GameEvent
    {
        public bool isOn;

        public OnOffEscEvent SetEvent(bool value)
        {
            isOn = value;
            return this;
        }
    }

    public class BlockEscEvent : GameEvent
    {
        public bool isOn;
        public BlockEscEvent SetEvent(bool value)
        {
            isOn = value;
            return this;
        }
    }
}