using TextGame.Enums;
using TextGame.Interpreters;

namespace TextGame.Locations
{
/// <summary>
/// Класс для работы с локациями
/// </summary>
    public class Location
    {
        /// <summary>
        /// Содержит свзяь локаций между друг-другом
        /// </summary>
        private Dictionary<LocationEnums, List<LocationEnums>> LocationConnections { get; set; }

        /// <summary>
        /// Содержит свзять между предметами на локациях и локациями
        /// </summary>
        private Dictionary<LocationEnums, List<ItemsEnum>> LocationItems { get; set; }
        private Interpreter interpreter { get; set; }

        public Location(Interpreter interpreter_)
        {
            LocationConnections = new Dictionary<LocationEnums, List<LocationEnums>>
            {
                {LocationEnums.Kitchen,new List<LocationEnums> {LocationEnums.Hallway}},

                {LocationEnums.Hallway, new List<LocationEnums>
                                        {LocationEnums.Kitchen,
                                         LocationEnums.Street,
                                         LocationEnums.Room}},

                {LocationEnums.Room, new List<LocationEnums>{LocationEnums.Hallway}},

                {LocationEnums.Street, new List<LocationEnums>{LocationEnums.Hallway}},

                {LocationEnums.UndescribedLocation, new List<LocationEnums>{LocationEnums.Kitchen} }
            };

            LocationItems = new Dictionary<LocationEnums, List<ItemsEnum>>
            {
                {LocationEnums.Room, new List<ItemsEnum>
                {
                    ItemsEnum.Backpack,
                    ItemsEnum.Notes,
                    ItemsEnum.Keys
                }
                }
            };
            interpreter = interpreter_;

        }

        public Response GetLocationLookAround(LocationEnums location)
        {
            var response = new Response();

            var message = GetLocationLookAroundPrefix(location) + GetLocationConnections(location);

            response.IsSuccess = true;
            response.Message = message;

            return response;
        }

        public Response TryGetLocationEnter(LocationEnums currentLocation, LocationEnums goToLocation)
        {

            var response = new Response();

            var isAccessExist = TryGetLocationAccess(currentLocation, goToLocation);

            if (!isAccessExist)
            {
                response.SetParameters(false, "Нет пути в" + interpreter.GetLocationLocalizationsName(goToLocation));
                return response;
            }

            var result = GetLocationEnterPrefix(goToLocation) + GetLocationConnections(goToLocation);

            response.SetParameters(true, result);
            return response;
        }

        public Response RemoveItemFromTheLocation(LocationEnums location, ItemsEnum item)
        {
            try
            {
                var response = new Response();

                var ItemsDictionary = LocationItems.TryGetValue(location, out var items) ? items : null;

                if (ItemsDictionary == null)
                {
                    response.SetParameters(false, EmptyLocationMessage(location));
                    return response;
                }

                if (!ItemsDictionary.Contains(item))
                {
                    response.SetParameters(false, "нет такого");
                    return response;
                }

                ItemsDictionary.Remove(item);

                var result = "Предмет добавлен в инвентарь: " + interpreter.GetItemLocalizationsName(item);

                response.SetParameters(true, result);

                return response;
            }
            catch
            {
                return null;
            }
        }


        // Все фнункции ниже выполняет рутинную работу с текстом, создают префиксы,
        // выводят связанный локации или вещи

        private string TryGetLocationItems(LocationEnums location)
        {
            var localResult = LocationItems.TryGetValue(location, out var items) ? items : null;

            if (localResult == null || localResult.Count()<=0)
                return "";

            var result = TryGetLocationItemPrefix(location) + interpreter.GetItemsLocalizationsNames(localResult);

            return result;
        }
        private string GetLocationConnections(LocationEnums location)
        {
            var localResult = LocationConnections.TryGetValue(location, out var locations) ? locations : null;
            var result = "Можно пройти - " + interpreter.GetLocationLocalizationsNames(localResult);

            return result;
        }
        private string TryGetLocationItemPrefix(LocationEnums location)
        {
            var result = location switch
            {
                LocationEnums.Room => "На столе: \n",
                _ => ""
            };

            return result;
        }
        private string GetLocationLookAroundPrefix(LocationEnums location)
        {
            var result = TryGetLocationItems(location);

            if (!String.IsNullOrEmpty(result))
                return result;


            result = location switch
            {
                LocationEnums.Kitchen => "Я на кухне, надо собрать рюкзак и идти в универ. ",
                LocationEnums.Hallway => "Коридор, хотя..., да коридор. Кстати, я тут одну шутку вспомнил..., а ладно потом. ",
                LocationEnums.Room => "Пустая комната",
                LocationEnums.Street => "Дурацкий дождь, в школу не пойду. ",
                LocationEnums.UndescribedLocation => "А где я ? Ладно хочу кушать. ",
                _ => ""
            };

            return result;
        }
        private string GetLocationEnterPrefix(LocationEnums location)
        {
            var result = location switch
            {
                LocationEnums.Room => "Я в своей комнате. ",
                LocationEnums.Hallway => "Ничего интересного. ",
                LocationEnums.Kitchen => "Кухня, ничего интересного. ",
                LocationEnums.Street => "На улице весна. ",
                LocationEnums.UndescribedLocation => "",
                _ => ""
            };

            return result;
        }
        private bool TryGetLocationAccess(LocationEnums currentLocation, LocationEnums goToLocation)
        {
            var locationConnections = LocationConnections.TryGetValue(currentLocation, out var rooms) ? rooms : null;

            if (locationConnections == null)
                return false;

            var result = locationConnections.Any(x => x == goToLocation);

            return result;
        }
        private string EmptyLocationMessage(LocationEnums location)
        {
            var result = location switch
            {
                LocationEnums.Room => "Пустая команата",
                LocationEnums.Hallway => "В  Коридое нет",
                LocationEnums.Kitchen => "На Кухня нет",
                LocationEnums.Street => "На Улице нет",
                LocationEnums.UndescribedLocation => "Пустое что-то",
                _ => "Пустое что-то"
            };

            return result;
        }
    }
}
