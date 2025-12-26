using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{

    public string GetMachineFingerprint()
    {
        string fingerprint = string.Empty;

        // Use system information to create a fingerprint
        string cpuId = GetCpuId();
        string macAddress = GetMacAddress();

        // Combine and hash the information
        string combined = cpuId + macAddress;
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(combined));
            fingerprint = BitConverter.ToString(hash).Replace("-", "").ToLower();
        }

        return fingerprint;
    }

    private string GetCpuId()
    {
        string cpuId = string.Empty;
        using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT ProcessorId FROM Win32_Processor"))
        {
            foreach (ManagementObject obj in searcher.Get())
            {
                cpuId = obj["ProcessorId"].ToString();
                break;
            }
        }
        return cpuId;
    }

    private string GetMacAddress()
    {
        string macAddress = string.Empty;
        using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT MACAddress FROM Win32_NetworkAdapter WHERE MACAddress IS NOT NULL"))
        {
            foreach (ManagementObject obj in searcher.Get())
            {
                macAddress = obj["MACAddress"].ToString();
                break;
            }
        }
        return macAddress;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {

        //string uuid = GetMachineFingerprint();

        //// get uuid of computer


        //if (uuid == "11848E2E-673E-4544-A7F5-17BEBA962157")
        //{
        //    // not logged in
        //    context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        //}


    }

}