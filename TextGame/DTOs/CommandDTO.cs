
using TextGame.Enums;

namespace TextGame.DTOs
{

    /// <summary>
    /// ДТОха нужна для нормальный обработки команд от пользователя . Также выполняет условие 
    /// "Задача - сделать правильно. Под правильным понимается универсально, 
    /// чтобы можно было без проблем что-то добавить или убрать"
    /// Надо сделать, чтобы у комманд было доп условие, дписываем сюда парамет, в котором оно будет храниться,
    /// редачим функцию ConvertPlayerCommands- все. Или потрубется, сделать так, чтобы персонаж мог 
    /// подбирать несколько предметов, меняем  ItemsEnum Item на list, небольшая доработка ConvertPlayerCommands,
    /// в остальном логика не меняется
    /// </summary>
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
