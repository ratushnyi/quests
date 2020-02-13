public interface IQuest
{
    (bool isPassed, string info) AttemptToPass(UserData usedData);
    void AddCause(ref bool result, ref string causes, string currentCause);
}
