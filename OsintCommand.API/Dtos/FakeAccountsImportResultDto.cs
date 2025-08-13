namespace OsintCommand.API.Dtos
{
    public class FakeAccountsImportResultDto
    {
        //public int RowIndex { get; set; }
        public string Platform { get; set; }
        public string Uid { get; set; }
        public string Password { get; set; }
        public string TwoFA { get; set; }
        public string Email { get; set; }
        public string EmailPassword { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }


        public FakeAccountsImportResultDto(string platform, string uid, string password, string twoFA, string email, string emailPassword, string message, bool success)
        {
            //RowIndex = rowIndex;
            Platform = platform;
            Uid = uid;
            Password = password;
            TwoFA = twoFA;
            Email = email;
            EmailPassword = emailPassword;
            Message = message;
            Success = success;
        }
    }
}
