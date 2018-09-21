using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SV.Application.Output;

namespace SV.API.Jwt
{
    public static class JwtAuthorize
    {
	    public static object IssueToken(JwtSettings jwtSettings, LoginOutput output)
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

		internal static AuthUser AuthUser(Controller controller)
		{
			if (!controller.HttpContext.Request.Headers.ContainsKey("Authorization"))
			{
				return null;
			}
			var tokenHeader = controller.HttpContext.Request.Headers["Authorization"].ToString();
			return SerializeToken(tokenHeader);
		}
		
		private static AuthUser SerializeToken(string jwtStr)
		{
		    if (jwtStr.Contains("Bearer ")) jwtStr = jwtStr.Replace("Bearer ", "");
		    var jwtHandler = new JwtSecurityTokenHandler();
		    var jwtToken = jwtHandler.ReadJwtToken(jwtStr);
		    object id; object name; object role;
			try
		    {
			    jwtToken.Payload.TryGetValue(ClaimTypes.Sid, out id);
				jwtToken.Payload.TryGetValue(ClaimTypes.Name, out name);
				jwtToken.Payload.TryGetValue(ClaimTypes.Role, out role);
		    }
		    catch (Exception e)
		    {
			    Console.WriteLine(e);
			    throw;
		    }
		    if (id == null||name == null|| role == null) return null;
			var tm = new AuthUser
		    {
			    UserId = long.Parse(id.ToString()),
			    User = name.ToString(),
			    Role = role.ToString()
		    };
		    return tm;
		}

	}
}
