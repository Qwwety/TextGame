using System.Text;
using TextGame.DTOs;
using TextGame.Enums;

namespace TextGame.Interpreters
{
    public class Interpreter
    {
        public CommandDTO ConvertPlayerCommands(string PlayerCommand)
        {
            var result = new CommandDTO();

            var reducedToFormat = PlayerCommand.Trim().ToLower();
            var words = reducedToFormat.Split(' ');

            result.PlayerCommand = words[0] switch
            {
                "стоп" => PlayerActionsEnum.StopGame,
                "осмотреться" => PlayerActionsEnum.LookAround,
                "идти" => PlayerActionsEnum.GoTo,
                "взять" => PlayerActionsEnum.TakeItem,
                _=> PlayerActionsEnum.Undefined
            };

            if (words.Length>1 && result.PlayerCommand!= PlayerActionsEnum.Undefined)
            {
                switch (result.PlayerCommand)
                {
                    case PlayerActionsEnum.GoTo:
                        result.Location = GetLocationEnumByName(words[1]);
                        break;

                    case PlayerActionsEnum.TakeItem:
                        result.Item=GetItemsEnumByName(words[1]);
                        break;
                }
            }

            return result;
        }

        public LocationEnums GetLocationEnumByName(string LocationName)
        {
            var reducedToFormat= LocationName.Trim().ToLower();

            LocationEnums result = reducedToFormat switch
            {
                "кухня" => LocationEnums.Kitchen,
                "коридор" => LocationEnums.Hallway,
                "комната" => LocationEnums.Room,
                "улица" => LocationEnums.Street
            };

            return result;
        }

        public ItemsEnum GetItemsEnumByName(string ItemName)
        {
            var reducedToFormat = ItemName.Trim().ToLower();

            ItemsEnum result = reducedToFormat switch
            {
                "рюкзак" => ItemsEnum.Backpack,
                "ключи" => ItemsEnum.Keys,
                "конспекты" => ItemsEnum.Notes,
                _=>ItemsEnum.Empty
            };

            return result;
        }

        public string GetLocationLocalizationsNames(List<LocationEnums> locations)
        {
            var stringBuilder = new StringBuilder();

            foreach (LocationEnums location in locations)
            {
               var localResult=GetLocationLocalizationsName(location);

                stringBuilder.Append(localResult);
            }
            var result = stringBuilder.ToString();
            return result;

        }

        public string GetLocationLocalizationsName(LocationEnums location)
        {
            var localResult = location switch
            {
                LocationEnums.Kitchen => "Кухня \n",
                LocationEnums.Hallway => "Коридор \n",
                LocationEnums.Room => "Комната \n",
                LocationEnums.Street => "Улица \n",
                _ => ""
            };
            return localResult;
        }

        public string GetItemsLocalizationsNames(List<ItemsEnum> items)
        {
            var stringBuilder = new StringBuilder();
            foreach (var item in items)
            {
               var localResult = GetItemLocalizationsName(item);
                stringBuilder.Append(localResult);

            }
            var result = stringBuilder.ToString();
            return result;
        }

        public string GetItemLocalizationsName(ItemsEnum item)
        {
            var localResult = item switch
            {
                ItemsEnum.Backpack => "Рюкзак \n",
                ItemsEnum.Keys => "Ключи \n",
                ItemsEnum.Notes => "Конспекты \n",
                _ => ""
            };
            return localResult;
        }

    }
}
