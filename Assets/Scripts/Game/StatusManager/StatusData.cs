using Game.Elements;

namespace Game.StatusManager
{
    public class StatusData
    {
        public StatusType statusType;
        public string StatusName;
        public float StatusValue;
        public ElementType StatusElementType;
        public float Time;

        public void Update(float deltaTime)
        {
            Time -= deltaTime;
        }
    }
}