namespace Prod.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Prod.Entities;

    public interface ITokenServices
    {
        TokenEntity GenerateToken(int userId);

        bool ValidateToken(string tokenId);

        bool ValidateAuthToken(string authToken);

        bool Kill(string tokenId);

        bool DeleteByUserId(int userId);
    }
}
