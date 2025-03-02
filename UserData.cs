namespace MovieTracker;

class UserData 
{
    public DSA.DynamicArray<UserList> UserLists { get; private set; }

    public UserData()
    {
        UserLists = new();
    }
}