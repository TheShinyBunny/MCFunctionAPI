namespace MCFunctionAPI.Scoreboard
{
    internal class QueriedScore : Score
    {
        private ResultCommand cmd;

        public QueriedScore(ResultCommand cmd)
        {
            this.cmd = cmd;
        }

        public override void Set(Objective obj, string field)
        {
            new Execute().Store(cmd.storage, field, obj).RunRaw(cmd.command);
        }
    }
}