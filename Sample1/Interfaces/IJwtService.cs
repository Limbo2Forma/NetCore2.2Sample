using System.Collections.Generic;

namespace Sample1.Controllers {
    public interface IJwtService {
        string GenerateToken(IEnumerable<System.Security.Claims.Claim> claims);
    }
}
