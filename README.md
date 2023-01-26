# Metrics application.

## Description 📋

Welcome everyone 👋, who have watched my metrics app's repository. I know so many people
who have been worried on their own computer, when they downloaded any program or game from an untrusted web-site. So, I decided to help similar people and I developmented the metrics application.  The metrics application helps to observe the condition computer's accessories such as CPU, GPU and RAM in the real time.

## How to install and run the project:

 - Clone this repository on your computer.
 - Open repository with the following IDE/Code-editor: Rider, Visual Studio, Visual Studio Code etc.
 - Install the necessary packeges to run the application.(See “Additional information” below) 
 - You need to tune Bot configuration. Insert the “Bot Token”, “Secret Token” and “Host Address” into the appsettings.json’s MetricsTelegramBot project.(See “Additional information” below, where to receive the necessary data for the appsettings.json).
 - Run the 3 projects: MetricsAgent, MetricsManager, MetricsTelegramBot.

## Additional information to help you get started with MetricsApp:

Please make sure you have .NET 6 or newer installed. You can download .NET runtime from the [official site.](https://dotnet.microsoft.com/en-us/download) This is a short description how you can test your bot locally. 

### Necessary packages 📦

If you use the Rider or Visual studio - you can skip this topic, otherwise - stay here. So if use the Code-editor such as Visual Studio Code, Sublime Text etc - you probably need to install a following list of packages:

#### MetricsAgent’s project:
 - Microsoft.EntityFrameworkCore.Design
 - Microsoft.EntityFrameworkCore.SqlServer
 - Swashbuckle.AspNetCore
 - AutoMapper.Extensions.Microsoft.DependencyInjection
 - Hangfire.AspNetCore
 - Hangfire.Core
 - Hangfire.SqlServer
 - Microsoft.Extensions.Logging.Log4Net.AspNetCore
 - System.Diagnostics.PerformanceCounter
 
#### MetricsManager’s project:
 - Microsoft.Extensions.Http.Polly
 - Swashbuckle.AspNetCore
 - Newtonsoft.Json

#### MetricsTelegramBot’s project:
 - Microsoft.AspNetCore.Mvc.NewtonsoftJson
 - Swashbuckle.AspNetCore
 - Microsoft.Extensions.Http.Polly
 - Telegram.Bot

How to install the above packages for Visual Studio Code - [see this documentation.](https://code.visualstudio.com/docs/editor/extension-marketplace) I have given a link of installing packages in the Visual Code, because that’s obviously the most popular code-editor. You can find a necessary instruction for your code-editor, - just type “How to install extensions  in {your code-editor}”.  



### Bot configuration 🤖

You have to specify your Bot Token and Secret Token in appsettings.json. Insert in the “BotToken”’s and “SecretToken”’s field actual your bot’s data. How to get a “BotToken” and “SecretToken” - [see Documentation.](https://telegrambots.github.io/book/1/quickstart.html) Also you have to specify endpoint(“HostAdress” field in the appsettings.json), to which Telegram will send new updates. How to get a necessary endpoint - [see this README.md](https://github.com/TelegramBots/Telegram.Bot.Examples/tree/master/Telegram.Bot.Examples.WebHook)(Topic - Ngrok).

## Useful links:

 - [Telegram Bots Book](https://telegrambots.github.io/book/index.html)
 - [Hangfire](https://www.hangfire.io/)
 - [ASP.NET Core Webhook Example](https://github.com/TelegramBots/Telegram.Bot.Examples/tree/master/Telegram.Bot.Examples.WebHook)
 - [Polly](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/implement-http-call-retries-exponential-backoff-polly)
	
