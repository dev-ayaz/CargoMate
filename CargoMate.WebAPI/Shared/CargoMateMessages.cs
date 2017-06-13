namespace CargoMate.WebAPI.Shared
{
    public class CargoMateMessages
    {
        public static object ModelError = new
        {
            IsError = true,
            MessageHeader = "Operation Fail",
            Message = "Validate your inputs & try again"
        };

        public static object SuccessResponse = new
        {
            IsError = false,
            MessageHeader = "Operation Success",
            Message = "Operation successfully completed"
        };

        public static object FailureResponse = new
        {
            IsError = true,
            MessageHeader = "Operation Fail",
            Message = "No record affected"
        };


    }

    public class PostResult
    {
        public bool IsError { get; set; }
        public string MessageHeader { get; set; }
        public string Message { get; set; }
    }
}