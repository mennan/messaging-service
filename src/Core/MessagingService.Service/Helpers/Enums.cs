namespace MessagingService.Service
{
    public enum AuditTypes
    {
        LoginAttempt,
        InvalidLogin,
        Login,
        Register,
        List,
        Create,
        Update,
        Delete,
        BlockUser,
        MessageSend,
        MessageRead
    }
}