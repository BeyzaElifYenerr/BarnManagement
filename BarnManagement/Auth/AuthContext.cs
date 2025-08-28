namespace BarnManagement.Auth
{
    public static class AuthContext
    {
        public static int? CurrentUserId { get; private set; }

        public static bool IsAuthenticated => CurrentUserId.HasValue;

        public static void SignIn(int userId) => CurrentUserId = userId;

        public static void SignOut() => CurrentUserId = null;
    }
}
