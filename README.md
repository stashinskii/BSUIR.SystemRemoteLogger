### System Remote Logger (SRL)
Coursework developed by **Herman Stashynski (Герман Сташинский), 653503**  
[Read russian version 🌍](#Russian-version)

#### This solution provides ability of logging OS interactions in Windows (and API for Linux) through network protocols (TCP/IP). 
###### Services API layer represented as cross-platform .NET Standard Library for Linux and Windows and may be used across any environments.

###### Development process were managed by Trello. See /docs/DevelopmentProcess.docx folder to read report.

Availible interactions to be logged:
* Information about processes
* Filesystem interactions
* Network usage

Information may be logged by broadcasting using UDP Multicast, send to subscribers thought Email using SMTP. All configuration data such as IP addresses, ports (for multicasting), credentials for SMTP Server, etc. stored on Presentation layer (WPF, Web App, Console App) in config files.

Small brief to provided assemleys:
###### *Full assembley name represented as SystemRemoteLogger.[Assembley]*

| Assembley | Summary |
| ------ | ------ | 
| Services | Cross-platform .NET Standard Library containing System Remote Logger (SRL) API |
| Services.Tests | Unit and mock tests for System Remote Logger (SRL) API | 
| WPF | Presentation layer built with Windows Presentation Foundation and MDIX (Material Design In XAML)|

#### Russian version:
#### Данный проект предоставляет возможность журналирования взаимодействий с операционной системой Windows (а также Linux посредством API) через сетевые протоколы (TCP/IP).

###### Процесс разработки контролировался Trello. См. /docs/DevelopmentProcess.docx (отчёт)

###### Уровень API-сервисов представлен в виде кроссплатформенной .NET Standard бибилотеки для ОС Linux и Windows и может быть использован для разработки на любых средах.

Доступные для журналирования события:
* Информация о процессах
* Взаимодействия с файловой системой
* Использование сети

Информация может логгироваться через трансляцию посредством UDP Multicast, отправляться подписчикам как Email-сообщение посредством SMTP. Вся конфигурация, такая как IP-адреса, номера портов (для трансляции), почтовые данные для SMTP-сервера и т.д. хранится на уровне представления (WPF, Веб-приложение, консольное приложение) в конфигурационных *.config* файлах.

Небольшое ознакомление с предоставленными сборками:
###### *Полное имя сборки представлено как SystemRemoteLogger.[Assembley]*

| Assembley | Summary |
| ------ | ------ | 
| Services | Кроссплатформенная .NET Standard библиотека содержащая System Remote Logger (SRL) API |
| Services.Tests | Unit и mock тесты для System Remote Logger (SRL) API | 
| WPF | Уровень представления построенный при помощи Windows Presentation Foundation и MDIX (Material Design In XAML)|

##### Main page interface screenshot (Скриншот домашней страницы приложения)
![](https://github.com/stashinskii/BSUIR.SystemRemoteLogger/blob/master/docs/mainScreenPreview.png?raw=true)

##### Sample of usage: GIF

Ниже представлены скриншоты с демонстрацией работы программы.

###### SMTP Logging, logging throught E-mail
Описание процесса: копируем файл в папке, которую отслеживаем. Действие фиксируется системой, логгируется по UDP. Письмо отправляется по почте подписчику. Демонстрируется письмо. Подпись (signature) является html-разметкой и может быть настроено пользователем по желанию. 
![](https://raw.githubusercontent.com/stashinskii/BSUIR.SystemRemoteLogger/master/docs/mail-smtp.gif)

###### UDP Logging, logging throught UDP-multicast
![](https://raw.githubusercontent.com/stashinskii/BSUIR.SystemRemoteLogger/master/docs/udp.gif)

###### SRL Interface
![](https://raw.githubusercontent.com/stashinskii/BSUIR.SystemRemoteLogger/master/docs/animations.gif)

