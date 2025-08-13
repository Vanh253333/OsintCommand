
namespace OsintCommand.API.Dtos
{
    public class ActionAddRequestDto
    {
        public string Name { get; set; } = "";
        public int InteractType { get; set; }
        public Dictionary<string, object> Config { get; set; }
    }
}
