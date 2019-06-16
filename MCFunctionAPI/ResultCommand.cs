namespace MCFunctionAPI
{
    public class ResultCommand
    {

        public string command;
        public Storage storage;

        public ResultCommand(string command)
        {
            this.command = command;
            this.storage = Storage.Result;
        }

        public ResultCommand IsSuccessful()
        {
            this.storage = Storage.Success;
            return this;
        }



    }
}