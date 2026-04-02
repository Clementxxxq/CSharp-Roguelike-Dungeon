public class RoomManager
{
    private const int FinalRoomNumber = 5;

    public int CurrentRoom { get; private set; } = 1;
    private Room currentRoomInstance;

    public void ResetProgress()
    {
        CurrentRoom = 1;
        currentRoomInstance = null;
    }

    public Room CreateCurrentRoom()
    {
        if (currentRoomInstance != null)
            return currentRoomInstance;

        RoomType roomType = IsFinalRoom() ? RoomType.Boss : RoomType.Combat;
        currentRoomInstance = RoomFactory.CreateRoom(roomType, CurrentRoom);
        return currentRoomInstance;
    }

    public bool IsFinalRoom()
    {
        return CurrentRoom >= FinalRoomNumber;
    }

    public string GetCurrentRoomTypeLabel()
    {
        return IsFinalRoom() ? "Boss Room" : "Normal Room";
    }

    public void MoveToNextRoom()
    {
        if (CurrentRoom < FinalRoomNumber)
            CurrentRoom++;

        currentRoomInstance = null;
    }
}