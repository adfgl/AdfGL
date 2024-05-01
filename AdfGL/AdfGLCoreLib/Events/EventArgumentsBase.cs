namespace AdfGLCoreLib.Events
{
    public abstract class EventArgumentsBase
    {
        public long TimeStamp { get; private set; }

        public EventArgumentsBase()
        {
            TimeStamp = DateTime.Now.Ticks;
        }
    }
}
