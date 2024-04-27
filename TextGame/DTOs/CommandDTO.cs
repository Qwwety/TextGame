
using TextGame.Enums;

namespace TextGame.DTOs
{
    public class CommandDTO
    {
        public PlayerActionsEnum PlayerCommand { get; set; }
        public LocationEnums Location { get; set; }
        public ItemsEnum Item { get; set; }

        public CommandDTO() 
        {
            PlayerCommand = PlayerActionsEnum.Undefined;
            Location = LocationEnums.UndescribedLocation;
            Item = ItemsEnum.Empty;
        }

    }
}
