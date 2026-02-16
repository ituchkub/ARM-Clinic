namespace ARM_Clinic_API.Model
{

    public abstract class BaseMessage<T>
    {
        protected string? _MessageId = string.Empty;
        public string? MessageId { get { return _MessageId; } }
        public string? TimeStamp { get { return DateTime.Now.ToString("yyyyMMddHHmmss"); } }
        public int SessionEmpID { get; set; }
        public T body { get; set; }

    }
    public class ResponseMessage<T> : BaseMessage<T>
    {
        public bool Status { get; set; }
        public int StatusId { get; set; }
        public string? ErrorMessage { get; set; }
        public string? Token { get; set; }
        public ResponseMessage()
        {
            this._MessageId = Guid.NewGuid().ToString();
        }
        public ResponseMessage(string? requestMessageId)
        {
            this._MessageId = requestMessageId;
        }
    }
    public class RequestMessage<T> : BaseMessage<T>
    {
        public string? Module { get; set; }
        public RequestMessage()
        {
            this._MessageId = Guid.NewGuid().ToString();
        }
    }
    public class MailConfiguration
    {
        public string? HostMailName { get; set; }
        public string? HostEMailUserName { get; set; }
        public string? HostEMailPassword { get; set; }
        public bool EnableSsl { get; set; }
        public string? SiteWeb { get; set; }
        public string? TempMailDirectory { get; set; }
        public int Port { get; set; }
        public bool IsPasswordMail { get; set; }
        public bool IsTestMail { get; set; }
        public bool IsSubjectTestMail { get; set; }
        public bool IsSendMail { get; set; }

    }
    public class UserProfileResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Fname { get; set; }
        public string Telephone { get; set; }
        public string RoleId { get; set; }
        public string Email { get; set; }
        public string BanchID { get; set; }
        public string BanchQRCode { get; set; }

        //public UserProfileResponse()
        //{

        //}
        //public UserProfileResponse(int Id, string Name, string Surename, string Fname, string Telephone, string RoleId, string Email)
        //{
        //    this.Id = Id;
        //    this.Name = Name;
        //    this.Fname = Fname;
        //    this.Surname = Surname;
        //    this.Telephone = Telephone;
        //    this.RoleId = RoleId.Substring(1);
        //    this.Email = Email;
        //}
    }
    public class UserAuthen
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }

    public class UserUserManagement
    {
        public int? EmpID { get; set; }
        public string? EmpCode { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? Prefix { get; set; }
        public string? Fname { get; set; }
        public string? Lname { get; set; }
        public string? Address { get; set; }
        public string? Telephone { get; set; }
        public string? Email { get; set; }
        public string? RoleID { get; set; }
        public string? Department { get; set; }
        public string? DelFlag { get; set; } = "1";

    }
    public class M_DDL
    {

        public string? TextSearch { set; get; }
    }

    public class ResponseGrid<T> : BaseMessage<T>
    {
        public bool Status { get; set; }
        public string? ErrorMessage { get; set; }
        public int Total { get; set; }

        public ResponseGrid()
        {
            this._MessageId = Guid.NewGuid().ToString();
        }
        public ResponseGrid(string? requestMessageId)
        {
            this._MessageId = requestMessageId;
        }
    }

}
