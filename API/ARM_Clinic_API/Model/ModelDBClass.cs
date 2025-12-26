namespace ARM_Clinic_API.Model
{

    public class MASBanchList
    {
        public int? BanchID { get; set; }
        public string? BanchCode { get; set; }
        public string? BanchName { get; set; }
        public string? BanchDate { get; set; }
    }


    public class MASCustomerList
    {
        public int? CusID { get; set; }
        public string? CusCode { get; set; }
        public string? Prefix { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Telephone { get; set; }
        public int? BanchID { get; set; }
        public string? Email { get; set; }
    }

    public class MASEmployeeList
    {
        public int? EmpID { get; set; }
        public int? BanchID { get; set; }
        public string? EmpCode { get; set; }
        public string? Prefix { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Telephone { get; set; }
        public string? RoleID { get; set; }
        public string? Department { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Remark { get; set; }
    }


    public class BuyProduct
    {
        public int? BuyPID { get; set; }
        public int? ProductID { get; set; }        
        public int? ProductQTY { get; set; }
        public int? UserID { get; set; }
        public string? BuyDate { get; set; }
    }


    public class MASServiceInfo
    {
        public int? ServiceID { get; set; }
        public string? ServiceCode { get; set; }
        public string? ServiceName { get; set; }
        public string? ServiceType { get; set; }
        public string? ServiceWeight { get; set; }
        public string? ServiceDetail { get; set; }
        public string? ServicePrice { get; set; }
        public string? ServicePicsBase64 { get; set; }
    }


    public class MASProductStock
    {
        public int? ProductStockID { get; set; }
        public int? ProductID { get; set; }
        public string? Amount { get; set; }
        public string? StockDate { get; set; }
    }


    public class BuyService
    {
        public int? BuySID { get; set; }
        public int? ServiceID { get; set; }
        public int? UserID { get; set; }
        public string? BuyDate { get; set; }
    }





    public class MASProductInfo
    {
        public int? ProductID { get; set; }
        public string? ProductCode { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDetail { get; set; }
        public string? ProductType { get; set; }
        public string? ProductWeight { get; set; }
        public string? ProductPrice { get; set; }
        public string? ProductPicsBase64 { get; set; }
    }








}
