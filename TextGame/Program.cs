// See https://aka.ms/new-console-template for more information

using TextGame.Enums;
using TextGame.Interpreters;
using TextGame.Character;

Interpreter Interpreter = new Interpreter();
StatusMachine statusMachine = new StatusMachine(Interpreter);


Console.WriteLine("Команды игрока:\r\n- осмотреться\r\n- идти <имя локации>  \r\n- взять <имя предмета>  \r\n\r\nЛокации: \r\n- кухня\r\n- коридор\r\n- комната\r\n- улица\r\n\r\nПредметы: \r\n- рюкзак\r\n- конспекты\r\n- ключи. \r\n \r\nЧтобы закончить игру ввидете \"Cтоп\" ");

// в ReadMe было написано "initGame делает нового игрока и задаёт ему начальное состояние."
// я отказался от этой функции, теперь при старте приложухи у нас появляться  игрок, с начальный состоянием 
while (!statusMachine.IsTimeToStop)
{
    var result = statusMachine.GetCommand(Console.ReadLine());
    Console.WriteLine(result);
}


/// <summary>
/// Класс нужен, как прослойка, между командами игрока и вызовом ф-ий
/// </summary>
class StatusMachine
{
    private Interpreter Interpreter { get; set; }
    private CharacterActions Character { get; set; }

   public bool IsTimeToStop { get; set; }

    public StatusMachine(Interpreter interpreter)
    {
        Interpreter = interpreter;
        Character = new CharacterActions(Interpreter);

        IsTimeToStop = false;
    }

    public string GetCommand(string PlayerСommand)
    {
        var convertedCommand = Interpreter.ConvertPlayerCommands(PlayerСommand);

        var result = convertedCommand.PlayerCommand switch
        {
            PlayerActionsEnum.StopGame => StopGame(),
            PlayerActionsEnum.LookAround => Character.LookAround(),
            PlayerActionsEnum.GoTo => Character.GoTo(convertedCommand.Location),
            PlayerActionsEnum.TakeItem => Character.PickUpItem(convertedCommand.Item),
            PlayerActionsEnum.Undefined => "Неизвестная команда",
        };

        return result;
    }

    public string StopGame()
    {
        IsTimeToStop = true;
        return "Спасибо за игру, дайте деняг)";
    }


}
