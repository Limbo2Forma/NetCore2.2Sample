using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample1.Interfaces {
    public interface IAuthService {
        bool Authenticate(string username, string password);
    }

    public class SimpleUserAuth : IAuthService {
        public bool Authenticate(string username, string password) {
            if (username == "abc" && password == "123") {
                return true;
            }
            return false;
        }
    }
}
