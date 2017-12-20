namespace Linnworks.Client.Messages.Inventory
{
    public class GetNewItemNumberMessage : BaseMessage
    {
        public GetNewItemNumberMessage(string token) 
            : base(token)
        { }

        protected override string Path => "Inventory/GetNewItemNumber";
    }
}