using Sample1.Interfaces;

namespace Sample1.Controllers {

    public class SimpleUserAuth : IAuthService {
        // users hardcoded for simplicity
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
