using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public class TokenDto
    {
        private string _accessToken;
        private string _refreshToken;

        public TokenDto(string accessToken, string refreshToken)
        {
            _accessToken = accessToken;
            _refreshToken = refreshToken;
        }

        public string AccessToken
        {
            get => _accessToken;
            set => _accessToken = value;
        }

        public string RefreshToken
        {
            get => _refreshToken;
            set => _refreshToken = value;
        }
    }
}
