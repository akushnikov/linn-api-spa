namespace Linnworks.Client.Messages.Categories
{
    public class GetCategoriesMessage : BaseMessage
    {
        public GetCategoriesMessage(string token)
            : base(token)
        { }


        protected override string Path => "Inventory/GetCategories";
    }
}