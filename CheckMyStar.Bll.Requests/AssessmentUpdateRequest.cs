namespace CheckMyStar.Bll.Requests
{
    public class AssessmentUpdateRequest
    {
        public int Identifier { get; set; }
        public int FolderIdentifier { get; set; }
        public byte TargetStarLevel { get; set; }
        public int Capacity { get; set; }
        public int NumberOfFloors { get; set; }
        public bool IsWhiteZone { get; set; }
        public bool IsDromTom { get; set; }
        public bool IsHighMountain { get; set; }
        public bool IsBuildingClassified { get; set; }
        public bool IsStudioNoLivingRoom { get; set; }
        public bool IsParkingImpossible { get; set; }
        public decimal TotalArea { get; set; }
        public int NumberOfRooms { get; set; }
        public decimal TotalRoomsArea { get; set; }
        public decimal SmallestRoomArea { get; set; }
        public bool IsComplete { get; set; }
        public List<AssessmentCriterionInput> Criteria { get; set; } = new();
    }
}
