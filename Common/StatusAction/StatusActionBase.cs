namespace Common.StatusAction
{
    public abstract class StatusActionBase
    {
        public string Message { get; set; }

        public StatusActionBase(string message = "Success")
        {
            this.Message = message;
        }
    }
}
