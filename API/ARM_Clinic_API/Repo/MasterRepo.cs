using ARM_Clinic_API.Model;
using ARM_Clinic_API_Helper;
using Dapper;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Xml.Linq;


namespace ARM_Clinic_API.Repo
{
    public class MasterRepo
    {
        Helper _help = new Helper();
        public object BanchList(RequestMessage<MASBanchList> request)
        {
            ResponseMessage<List<Object>> response = new ResponseMessage<List<Object>>();
            try
            {
                var p = new DynamicParameters();
                p.Add("@Module", request.Module);
                p.Add("@SessionEmpID", request.SessionEmpID);


                foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(request.body))
                {
                    p.Add($"@{prop.Name}", prop.GetValue(request.body));
                }
                p.Add("@StatusId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@StatusText", dbType: DbType.String, direction: ParameterDirection.Output, size: 200);
                using (var conn = new SqlConnection(ConnectionStrings.ConnectionStringss()))
                {
                    response.body = conn.Query<Object>("USP_MAS_BanchList", p, commandType: CommandType.StoredProcedure).ToList();
                    response.Status = (p.Get<int>("@StatusId") == 0 ? true : false);
                    response.ErrorMessage = p.Get<string>("@StatusText");
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public object EmployeeList(RequestMessage<MASEmployeeList> request)
        {
            ResponseMessage<List<Object>> response = new ResponseMessage<List<Object>>();
            try
            {
                var p = new DynamicParameters();
                p.Add("@Module", request.Module);
                p.Add("@SessionEmpID", request.SessionEmpID);


                foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(request.body))
                {
                    p.Add($"@{prop.Name}", prop.GetValue(request.body));
                }
                p.Add("@StatusId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@StatusText", dbType: DbType.String, direction: ParameterDirection.Output, size: 200);
                using (var conn = new SqlConnection(ConnectionStrings.ConnectionStringss()))
                {
                    response.body = conn.Query<Object>("USP_MAS_EmployeeList", p, commandType: CommandType.StoredProcedure).ToList();
                    response.Status = (p.Get<int>("@StatusId") == 0 ? true : false);
                    response.ErrorMessage = p.Get<string>("@StatusText");
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }


        public object ProductList(RequestMessage<MASProductInfo> request)
        {
            ResponseMessage<List<Object>> response = new ResponseMessage<List<Object>>();
            try
            {
                var p = new DynamicParameters();
                p.Add("@Module", request.Module);
                p.Add("@SessionEmpID", request.SessionEmpID);


                foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(request.body))
                {
                    p.Add($"@{prop.Name}", prop.GetValue(request.body));
                }
                p.Add("@StatusId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@StatusText", dbType: DbType.String, direction: ParameterDirection.Output, size: 200);
                using (var conn = new SqlConnection(ConnectionStrings.ConnectionStringss()))
                {
                    response.body = conn.Query<Object>("USP_MAS_ProductInfo", p, commandType: CommandType.StoredProcedure).ToList();
                    response.Status = (p.Get<int>("@StatusId") == 0 ? true : false);
                    response.ErrorMessage = p.Get<string>("@StatusText");
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }
        public object ProductStockList(RequestMessage<MASProductStock> request)
        {
            ResponseMessage<List<Object>> response = new ResponseMessage<List<Object>>();
            try
            {
                var p = new DynamicParameters();
                p.Add("@Module", request.Module);
                p.Add("@SessionEmpID", request.SessionEmpID);


                foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(request.body))
                {
                    p.Add($"@{prop.Name}", prop.GetValue(request.body));
                }
                p.Add("@StatusId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@StatusText", dbType: DbType.String, direction: ParameterDirection.Output, size: 200);
                using (var conn = new SqlConnection(ConnectionStrings.ConnectionStringss()))
                {
                    response.body = conn.Query<Object>("USP_MAS_ProductStock", p, commandType: CommandType.StoredProcedure).ToList();
                    response.Status = (p.Get<int>("@StatusId") == 0 ? true : false);
                    response.ErrorMessage = p.Get<string>("@StatusText");
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }
        public object ServiceList(RequestMessage<MASServiceInfo> request)
        {
            ResponseMessage<List<Object>> response = new ResponseMessage<List<Object>>();
            try
            {
                var p = new DynamicParameters();
                p.Add("@Module", request.Module);
                p.Add("@SessionEmpID", request.SessionEmpID);


                foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(request.body))
                {
                    p.Add($"@{prop.Name}", prop.GetValue(request.body));
                }
                p.Add("@StatusId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@StatusText", dbType: DbType.String, direction: ParameterDirection.Output, size: 200);
                using (var conn = new SqlConnection(ConnectionStrings.ConnectionStringss()))
                {
                    response.body = conn.Query<Object>("USP_MAS_ServiceInfo", p, commandType: CommandType.StoredProcedure).ToList();
                    response.Status = (p.Get<int>("@StatusId") == 0 ? true : false);
                    response.ErrorMessage = p.Get<string>("@StatusText");
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        //public object BuyServiceList(RequestMessage<BuyService> request)
        //{
        //    ResponseMessage<List<Object>> response = new ResponseMessage<List<Object>>();
        //    try
        //    {
        //        var p = new DynamicParameters();
        //        p.Add("@Module", request.Module);
        //        p.Add("@SessionEmpID", request.SessionEmpID);


        //        foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(request.body))
        //        {
        //            p.Add($"@{prop.Name}", prop.GetValue(request.body));
        //        }
        //        p.Add("@StatusId", dbType: DbType.Int32, direction: ParameterDirection.Output);
        //        p.Add("@StatusText", dbType: DbType.String, direction: ParameterDirection.Output, size: 200);
        //        using (var conn = new SqlConnection(ConnectionStrings.ConnectionStringss()))
        //        {
        //            response.body = conn.Query<Object>("USP_MAS_ServiceInfo", p, commandType: CommandType.StoredProcedure).ToList();
        //            response.Status = (p.Get<int>("@StatusId") == 0 ? true : false);
        //            response.ErrorMessage = p.Get<string>("@StatusText");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Status = false;
        //        response.ErrorMessage = ex.Message;
        //    }
        //    return response;
        //}

        public object BuyServiceList(RequestMessage<List<BuyService>> request)
        {
            ResponseMessage<List<object>> response = new ResponseMessage<List<object>>();
            response.body = new List<object>();

            try
            {
                using (var conn = new SqlConnection(ConnectionStrings.ConnectionStringss()))
                {
                    conn.Open();

                    foreach (var item in request.body)
                    {
                        var p = new DynamicParameters();
                        p.Add("@Module", request.Module);
                        p.Add("@SessionEmpID", request.SessionEmpID);

                        foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(item))
                        {
                            p.Add($"@{prop.Name}", prop.GetValue(item));
                        }

                        p.Add("@StatusId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                        p.Add("@StatusText", dbType: DbType.String, direction: ParameterDirection.Output, size: 200);

                        var result = conn
                            .Query<object>("USP_Buy_Service", p, commandType: CommandType.StoredProcedure)
                            .ToList();

                        response.body.AddRange(result);
                        if (p.Get<int>("@StatusId") != 0)
                        {
                            response.Status = false;
                            response.ErrorMessage = p.Get<string>("@StatusText");
                            return response;
                        }
                    }
                }

                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }

        //public object BuyProductList(RequestMessage<BuyProduct> request)
        //{
        //    ResponseMessage<List<Object>> response = new ResponseMessage<List<Object>>();
        //    try
        //    {
        //        var p = new DynamicParameters();
        //        p.Add("@Module", request.Module);
        //        p.Add("@SessionEmpID", request.SessionEmpID);


        //        foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(request.body))
        //        {
        //            p.Add($"@{prop.Name}", prop.GetValue(request.body));
        //        }
        //        p.Add("@StatusId", dbType: DbType.Int32, direction: ParameterDirection.Output);
        //        p.Add("@StatusText", dbType: DbType.String, direction: ParameterDirection.Output, size: 200);
        //        using (var conn = new SqlConnection(ConnectionStrings.ConnectionStringss()))
        //        {
        //            response.body = conn.Query<Object>("USP_MAS_ServiceInfo", p, commandType: CommandType.StoredProcedure).ToList();
        //            response.Status = (p.Get<int>("@StatusId") == 0 ? true : false);
        //            response.ErrorMessage = p.Get<string>("@StatusText");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Status = false;
        //        response.ErrorMessage = ex.Message;
        //    }
        //    return response;
        //}


        public object BuyProductList(RequestMessage<List<BuyProduct>> request)
        {
            ResponseMessage<List<object>> response = new ResponseMessage<List<object>>();
            response.body = new List<object>();

            try
            {
                using (var conn = new SqlConnection(ConnectionStrings.ConnectionStringss()))
                {
                    conn.Open();

                    foreach (var item in request.body)
                    {
                        var p = new DynamicParameters();
                        p.Add("@Module", request.Module);
                        p.Add("@SessionEmpID", request.SessionEmpID);

                        foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(item))
                        {
                            p.Add($"@{prop.Name}", prop.GetValue(item));
                        }

                        p.Add("@StatusId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                        p.Add("@StatusText", dbType: DbType.String, direction: ParameterDirection.Output, size: 200);

                        var result = conn
                            .Query<object>("USP_Buy_Product", p, commandType: CommandType.StoredProcedure)
                            .ToList();

                        response.body.AddRange(result);
                        if (p.Get<int>("@StatusId") != 0)
                        {
                            response.Status = false;
                            response.ErrorMessage = p.Get<string>("@StatusText");
                            return response;
                        }
                    }
                }

                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }



        public object CustomerList(RequestMessage<MASCustomerList> request)
        {
            ResponseMessage<List<Object>> response = new ResponseMessage<List<Object>>();
            try
            {
                var p = new DynamicParameters();
                p.Add("@Module", request.Module);
                p.Add("@SessionEmpID", request.SessionEmpID);


                foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(request.body))
                {
                    p.Add($"@{prop.Name}", prop.GetValue(request.body));
                }
                p.Add("@StatusId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@StatusText", dbType: DbType.String, direction: ParameterDirection.Output, size: 200);
                using (var conn = new SqlConnection(ConnectionStrings.ConnectionStringss()))
                {
                    response.body = conn.Query<Object>("USP_MAS_CustomerList", p, commandType: CommandType.StoredProcedure).ToList();
                    response.Status = (p.Get<int>("@StatusId") == 0 ? true : false);
                    response.ErrorMessage = p.Get<string>("@StatusText");
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public object DDLGetList(RequestMessage<M_DDL> request)
        {
            ResponseMessage<object> response = new ResponseMessage<object>();

            try
            {
                var p = new DynamicParameters();
                var reqBody = request.body;

                p.Add("@Module", request.Module);
                p.Add("@SessionEmpID", request.SessionEmpID);

                foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(reqBody))
                {
                    p.Add($"@{prop.Name}", prop.GetValue(reqBody));
                }

                p.Add("@StatusId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@StatusText", dbType: DbType.String, direction: ParameterDirection.Output, size: 200);


                using (var conn = new SqlConnection(ConnectionStrings.ConnectionStringss()))
                {
                    response.body = conn.Query<object>("USP_MAS_GETDDL", p, commandType: CommandType.StoredProcedure).ToList();
                    response.Status = (p.Get<int>("@StatusId") == 0 ? true : false);
                    response.ErrorMessage = p.Get<string>("@StatusText");
                }

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

    }
}
