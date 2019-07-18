using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample1.Interfaces {
    public interface IAuthService {
        bool Authenticate(string username, string password);
        bool IsAdmin(string username);
    }

    public class SimpleUserAuth : IAuthService {
        public bool Authenticate(string username, string password) {
            // simple hardcoded username
            if ((username == "abc" || username == "admin") && password == "123") {
                return true;
            }
            return false;
        }

        public bool IsAdmin(string username) {
            if (username == "admin") {
                return true;
            }
            return false;
        }
    }
}
