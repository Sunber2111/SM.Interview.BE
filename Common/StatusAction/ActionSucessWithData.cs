namespace Common.StatusAction
{
    public class ActionSucessWithData<T> : StatusActionBase
    {
        public T Data { get; set; }

        public ActionSucessWithData(T data)
        {
            this.Data = data;
        }
    }
}
