// Data/Player.cs

using System.Text.Json.Serialization;


namespace FCCopenhagen.Data
{
        public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Position { get; set; } = string.Empty;
        public string DetailedPosition { get; set; } = string.Empty;
        public string Image_Path { get; set; } = string.Empty;
    }

    public class ApiResponse
    {
        public List<SquadData> Data { get; set; } = new List<SquadData>();
    }

    public class SquadData
    {
        public PlayerData Player { get; set; } = new PlayerData();
        public PositionData Position { get; set; } = new PositionData();
        public DetailedPositionData DetailedPosition { get; set; } = new DetailedPositionData();
    }

    public class PlayerData
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public string Date_Of_Birth { get; set; } = string.Empty;
        public string Image_Path { get; set; } = string.Empty;
    }

    public class PositionData
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class DetailedPositionData
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
