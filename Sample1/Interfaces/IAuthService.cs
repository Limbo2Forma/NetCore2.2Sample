namespace Sample1.Interfaces {
    public interface IAuthService {
        bool Authenticate(string username, string password);
        bool IsAdmin(string username);
    }
}
