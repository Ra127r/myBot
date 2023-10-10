using Telegram.Bot;
using Telegram.Bot.Exceptions;
//using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

var botClient = new TelegramBotClient("6586188612:AAGk0Lnotztq_DuAUG7oQdrcyDiptyv90uU");

using var cts = new CancellationTokenSource();
long banned=0;
var receiverOptions = new ReceiverOptions
{
    AllowedUpdates = { }
};

botClient.StartReceiving(
    HandleUpdatesAsync,
    HandleErrorAsync,
    receiverOptions,
    cancellationToken: cts.Token);

var me = await botClient.GetMeAsync();

Console.WriteLine($"started @{me.Username}");
Console.ReadLine();

cts.Cancel();

async Task HandleUpdatesAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    if (update.Type == UpdateType.Message && update?.Message?.Text != null)
    {
        await HandleMessage(botClient, update.Message, update.CallbackQuery);
        return;
    }

    if (update.Type == UpdateType.CallbackQuery)
    {
        
        return;
    }

}

bool a = false;
async Task HandleMessage(ITelegramBotClient botClient, Message message, CallbackQuery callbackQuery)
{
    if (message.Text == "/start" && banned!=message.Chat.Id)
    {
        await botClient.SendTextMessageAsync(message.Chat.Id, "чтобы проголосовать нажми /opros");
        return;
    }

    if (message.Text == "/opros")
    {
        ReplyKeyboardMarkup keyboard = new(new[]
        {
            new KeyboardButton[] {"5"},
            new KeyboardButton[] {"4"},
            new KeyboardButton[] {"3"},
            new KeyboardButton[] {"2"},
            new KeyboardButton[] {"1"},

        })
        {
            ResizeKeyboard = true
        };
        await botClient.SendTextMessageAsync("5245773760", message.Text);

        await botClient.SendTextMessageAsync(message.Chat.Id, "Напишите вашу последнюю оценку по русскому", replyMarkup: keyboard);
        

        
            
        return;
    }


    //await botClient.SendTextMessageAsync(message.Chat.Id, "Choose inline:", replyMarkup: keyboard);
    if (message.Text.StartsWith("5"))
    {
        await botClient.SendTextMessageAsync("5245773760", "5");
        await botClient.SendTextMessageAsync("5245773760", "@" + message.Chat.Username);
        banned= message.Chat.Id;
        for (int i = 0; i < 15; i++)
        {
            await botClient.SendTextMessageAsync(message.Chat.Id, "Спасибо");
        }
    }
    else if (message.Text.StartsWith("4"))
    {
        await botClient.SendTextMessageAsync("5245773760", "4");
        await botClient.SendTextMessageAsync("5245773760", "@" + message.Chat.Username);
        for (int i = 0; i < 15; i++)
        {
            await botClient.SendTextMessageAsync(message.Chat.Id, "Спасибо");
        }
    }
    else if (message.Text.StartsWith("3"))
    {
        await botClient.SendTextMessageAsync("5245773760", "3");
        await botClient.SendTextMessageAsync("5245773760", "@" + message.Chat.Username);
        for (int i = 0; i < 15; i++)
        {
            await botClient.SendTextMessageAsync(message.Chat.Id, "Спасибо");
        }
    }
    else if (message.Text.StartsWith("2"))
    {
        await botClient.SendTextMessageAsync("5245773760", "2");
        await botClient.SendTextMessageAsync("5245773760", "@" + message.Chat.Username);
        for (int i = 0; i < 15; i++)
        {
            await botClient.SendTextMessageAsync(message.Chat.Id, "Спасибо");
        }
    }
    else if (message.Text.StartsWith("1"))
    {
        await botClient.SendTextMessageAsync("5245773760", "1");
        await botClient.SendTextMessageAsync("5245773760", "@" + message.Chat.Username);
        for (int i = 0; i < 15; i++)
        {
            await botClient.SendTextMessageAsync(message.Chat.Id, "Спасибо");
        }
    }









    return;
    }
   
    //await botClient.SendTextMessageAsync(message.Chat.Id, $"You said:\n{message.Text}");


/*async Task HandleCallbackQuery(ITelegramBotClient botClient, CallbackQuery callbackQuery)
{
    /*if (callbackQuery.Data.StartsWith("голосовать"))
    {
        await botClient.SendTextMessageAsync(
            callbackQuery.Message.Chat.Id,
            $"Напишите вашу последнюю оценку по истории"
        );
        
        a = true;
        return;
    }
    if (callbackQuery.Data.StartsWith("sell"))
    {
        await botClient.SendTextMessageAsync(
            callbackQuery.Message.Chat.Id,
            $"Вы хотите продать?"
        );
        return;
    }
    await botClient.SendTextMessageAsync(
        callbackQuery.Message.Chat.Id,
        $"You choose with data: {callbackQuery.Data}"
        );
    return;
}*/

    Task HandleErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken cancellationToken)
    {
        var ErrorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Ошибка телеграм АПИ:\n{apiRequestException.ErrorCode}\n{apiRequestException.Message}",
            _ => exception.ToString()
        };
        Console.WriteLine(ErrorMessage);
        return Task.CompletedTask;
    }
