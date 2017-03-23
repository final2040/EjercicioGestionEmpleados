namespace Empleados.Models
{
    public class ActionResponse
    {
        public ActionResponse(bool actionResult, string message)
        {
            ActionResult = actionResult;
            Message = message;
        }

        public bool ActionResult { get; }
        public string Message { get; }
    }
}