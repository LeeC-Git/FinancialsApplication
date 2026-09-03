namespace Personal_Finance_Tracker
{
    internal interface IAccountPersistence
    {
        void Save(List<Account> accounts);
        List<Account> Load();
    }
}
