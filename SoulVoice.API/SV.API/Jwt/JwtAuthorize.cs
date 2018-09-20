using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SV.Application.Output;

namespace SV.API.Jwt
{
    public static class JwtAuthorize
    {
	    public static object GenToken(JwtSettings jwtSettings, LoginOutput output)
	    {
		    var authTime = DateTime.UtcNow;
		    var expiresAt = authTime.AddDays(jwtSettings.Validity);

		    var claim = new[]{
			    new Claim(ClaimTypes.Sid,output.UserId.ToString()),
				new Claim(ClaimTypes.Name,output.User),
				new Claim(ClaimTypes.Role,output.Role)
		    };

		    //对称秘钥
		    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));
		    //签名证书(秘钥，加密算法)
		    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		    //生成token  [注意]需要nuget添加Microsoft.AspNetCore.Authentication.JwtBearer包，并引用System.IdentityModel.Tokens.Jwt命名空间
		    var token = new JwtSecurityToken(jwtSettings.Issuer, jwtSettings.Audience, claim, authTime, expiresAt, creds);

		    return new
		    {
			    token = new JwtSecurityTokenHandler().WriteToken(token),
			    profile = new
			    {
				    sid = output.UserId.ToString(),
				    name = output.User,
					role= output.Role,
					auth_time = new DateTimeOffset(authTime).ToUnixTimeSeconds(),
				    expires_at = new DateTimeOffset(expiresAt).ToUnixTimeSeconds()
			    }
		    };
		}

		//public static FunctionUser GetControllerAuthUser(ApiController controller)
		//{
		//	if (LoginUser != null) return LoginUser;
		//	FunctionUser u = new FunctionUser();



		//	try
		//	{
		//		ClaimsPrincipal user = controller.User as ClaimsPrincipal;

		//		u.EID = user.Claims.First(x => x.Type == "samaccount_name").Value;

		//		if (new Regex(@"T09493.ECH.[\d-]{3}").IsMatch(u.EID))
		//		{


		//			if (u.EID == "T09493.ECH.118" || u.EID == "T09493.ECH.119" || u.EID == "T09493.ECH.120" || u.EID == "T09493.ECH.121")
		//			{
		//				u.IsThirdParty = true;
		//			}
		//			else
		//			{
		//				u.IsThirdParty = false;
		//			}
		//			// to be implement after eso ready end
		//		}
		//		else
		//		{

		//			u.Email = user.Claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
		//			u.PeopleKey = user.Claims.First(x => x.Type == "peoplekey").Value;
		//			u.PersonnelNumber = user.Claims.First(x => x.Type == "personnelnbr").Value;
		//			u.FirstName = user.Claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname").Value;
		//			u.LastName = user.Claims.First(x => x.Type == "sn").Value;
		//			u.DisplayName = u.LastName + " " + u.FirstName;
		//			u.CompanyCd = user.Claims.First(x => x.Type == "company_cd").Value;
		//			u.Groups = (from claim in user.Claims
		//						where claim.Type == "http://schemas.xmlsoap.org/claims/Group"
		//						select claim.Value).ToArray();
		//		}
		//	}
		//	catch (Exception e)
		//	{

		//	}

		//	return u;
		//}
	}
}
