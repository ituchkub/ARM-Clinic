using System;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using ARM_Clinic_API_Helper;
using ARM_Clinic_API.Model;

namespace ARM_Clinic_API.Repo
{
    public class AuthenRepo
    {
        GenToken genToken = new GenToken();
        Helper _help = new Helper();

        public ResponseMessage<UserProfileResponse> onPost_GetUserLogin(UserAuthen request)
        {
            ResponseMessage<UserProfileResponse> response = new ResponseMessage<UserProfileResponse>();
            try
            {
                #region add Parameters
                var body = request;
                var parm = new DynamicParameters();
                parm.Add("@UserName", body.Username);
                parm.Add("@Password", body.Password);
                parm.Add("@OutputEmpID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parm.Add("@StatusId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parm.Add("@StatusText", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                #endregion

                #region Excute
                using (var conn = new SqlConnection(ConnectionStrings.ConnectionStringss()))
                {
                    var user = conn.Query<UserProfileResponse>("USP_EMP_Login", parm, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    int status = parm.Get<int>("@StatusId");
                    if (status == 0)
                    {
                        response.StatusId = parm.Get<int>("@StatusId");
                        response.Status = true;
                        if (response.StatusId == 0)
                        {
                            response.body = user;
                            response.SessionEmpID = parm.Get<int>("@OutputEmpID");
                            response.ErrorMessage = "SQL";
                            response.Token = genToken.GenerateJSONWebToken(response.SessionEmpID, user);
                        }
                    }
                    else
                    {
                        response.Status = false;
                        response.ErrorMessage = parm.Get<string>("@StatusText");
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public ResponseMessage<List<object>> onPost_GetUser_Insert_Update(RequestMessage<UserUserManagement> request)
        {
            ResponseMessage<List<object>> response = new ResponseMessage<List<object>>();
            try
            {
                string GeneratorPassword = ConnectionStrings.ConfigJWT("PasswordReset");

                var p = new DynamicParameters();
                var data = request.body;
                p.Add("@Module", request.Module);
                p.Add("@SessionEmpID", request.SessionEmpID);

                p.Add("@EmpID", data.EmpID);
                p.Add("@EmpCode", data.EmpCode);
                p.Add("@Department", data.Department);
                p.Add("@RoleID", data.RoleID);
                p.Add("@Telephone", data.Telephone);
                p.Add("@Username", data.Username);
                p.Add("@Password", data.Password);
                p.Add("@OldPassword", data.OldPassword);
                p.Add("@NewPassword", data.NewPassword);
                p.Add("@GeneratorPassword", GeneratorPassword);

                p.Add("@Prefix", data.Prefix);
                p.Add("@Fname", data.Fname);
                p.Add("@Lname", data.Lname);
                p.Add("@Email", data.Email);
                p.Add("@DelFlag", data.DelFlag);

                p.Add("@OutputEmpID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@StatusId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@StatusText", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);

                using (var conn = new SqlConnection(ConnectionStrings.ConnectionStringss()))
                {
                    conn.Open();
                    response.body = conn.Query<object>("USP_EMP_Insert_Update", p, commandType: CommandType.StoredProcedure).ToList();
                    response.StatusId = p.Get<int>("@StatusId");
                    if (response.StatusId == 0)
                    {
                        response.Status = true;
                    }
                    else
                    {
                        response.ErrorMessage = p.Get<string>("@StatusText");
                        response.Status = false;
                    }
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.ErrorMessage = "[GetUserlogin]" + ex.Message;
            }

            return response;
        }
    }
}
