namespace OsintCommand.API.Dtos
{
    public class ScriptCreateDto
    {
        public string Name { get; set; }
        public int ChannelCode { get; set; } = 1; // 1: Facebook, 2: Tiktok, 3: Telagram, _: Facebook
        public string CreatedBy { get; set; } = "admin"; 
        //public string UpdatedBy { get; set; } = "admin"; 
        //public long CreatedAt { get; set; } // timestamp đến giây
        //public long UpdateAt { get; set; }
    }
}
