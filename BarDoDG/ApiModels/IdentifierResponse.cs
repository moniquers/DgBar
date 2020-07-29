namespace BarDoDG.ApiModels
{
    public class IdentifierResponse
    {
        public IdentifierResponse()
        {
        }

        public IdentifierResponse(int id, string description)
        {
            Id = id;
            Description = description;
        }

        public int Id { get; set; }
        public string Description { get; set; }
    }
}
