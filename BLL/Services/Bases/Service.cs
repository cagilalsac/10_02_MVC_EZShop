using BLL.DAL;

namespace BLL.Services.Bases
{
    public abstract class Service
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; } = string.Empty;

        protected virtual string OperationFailed => "Operation failed!";


        protected readonly Db _db;

        protected Service(Db db)
        {
            _db = db;
        }

        public virtual Service Success(string message = "")
        {
            IsSuccessful = true;
            Message = message;
            return this;
        }

        public virtual Service Error(string message = "")
        {
            IsSuccessful = false;
            Message = $"{OperationFailed} {message}";
            return this;
        }
    }
}
