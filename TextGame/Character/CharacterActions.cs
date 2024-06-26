﻿using TextGame.Enums;
using TextGame.Interpreters;
using TextGame.Locations;

namespace TextGame.Character
{
    public class CharacterActions
    {
        private LocationEnums CurrentLocation { get; set; }
        private List<ItemsEnum> CharacterInventory { get; set; }

        Interpreter Interpreter { get; set; }
        Location location { get; set; }
        
        public CharacterActions(Interpreter interpreter) 
        {
            CurrentLocation = LocationEnums.Kitchen;
            CharacterInventory = new List<ItemsEnum>();

            location= new Location(interpreter);
            Interpreter = interpreter;
        }

        public string LookAround()
        {
           var result=location.GetLocationLookAround(CurrentLocation);
           return result.Message;
        }

        public string GoTo(LocationEnums GoToLocation)
        {
            var result = location.TryGetLocationEnter(CurrentLocation, GoToLocation);

            if(result.IsSuccess)
                CurrentLocation = GoToLocation;

            return result.Message;
        }

        public string PickUpItem(ItemsEnum item)
        {
            if(item== ItemsEnum.Empty)
                return "Нет такого.";

            var result = location.RemoveItemFromTheLocation(CurrentLocation, item);

            if(result.IsSuccess)
                CharacterInventory.Add(item);

            return result.Message;
        }

        /// <summary>
        /// Нереализованная функция, хотел сделать, но т.к. не обязательно отказался.
        /// Показывает, что в моей системе, можно спокойно добовялть новые фичи, меня минимум. 
        /// В данном случае потребуется добавить новый енум в PlayerCommandsEnum, добавить обработку в 
        /// ConvertPlayerCommands и все 
        /// </summary>
        /// <returns></returns>
        private string GetPlayerInventoryList()
        {
            var result = Interpreter.GetItemsLocalizationsNames(CharacterInventory);
            return result;
        }
    }
}
