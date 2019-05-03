### Ссылки используемые при реализации:

#### SMTP-клиент:

* Особенности использования асинхронных методов в SMTPClient: [StackOverflow](https://stackoverflow.com/questions/7276375/what-are-best-practices-for-using-smtpclient-sendasync-and-dispose-under-net-4)
* Документация System.Net.Mail с описанием всех специфик-свойств: [Microsoft Docs](https://docs.microsoft.com/en-us/dotnet/api/system.net.mail.smtpclient.sendasync?view=netframework-4.8)
* Вариант настройки локального SMTP-сервера (альтератива существующим хостам или IIS): [Medium](https://medium.com/@coffmans/setup-your-own-simple-smtp-server-how-to-c9159cfc7934)
* Примеры использования: https://metanit.com/sharp/aspnet5/21.1.php и https://metanit.com/sharp/net/8.1.php
* Конфигурирование IIS для SMTP [Telerik Progress Blog](https://support.microsoft.com/en-us/help/230235/xcon-how-to-configure-the-iis-smtp-service-to-relay-smtp-mail)

#### UDP-клиент:

* Документация на низкоуровневые сокеты со всеми полезными специфик-свойствами: [Microsoft Docs](https://docs.microsoft.com/en-us/dotnet/api/system.net.sockets.udpclient?view=netframework-4.8)
* Различия между Multicast, Broadcact, Unicast: [КОНТУР](http://www.konturm.ru/tech.php?id=iptvpe)
* Работа с мульткастингом: [Microsoft Docs](https://docs.microsoft.com/en-us/dotnet/framework/wcf/feature-details/creating-multicasting-applications-using-the-udp-transport)


#### Клиентская часть:

* Полная документация и примеры по MDIX (содержит проект-пример с возможность копировать описатели стилей в XAML): [GitHub](https://github.com/MaterialDesignInXAML)
* Остальные материалы по сути отсутствуют ввиду достаточно продолжительного изучения WPF. Основной упор делался именно на специфику библиотеки Material Design In XAML (MDIX)

#### Конфигурирование:

К конфигурированию пришлось подойти серьезно, т.к. .NET сильно ограничиват разработчика при использовании кроссплатформенных библиотек для .NET Standard

* Описание работы с Microsoft.Extensions.Configuration на базе .NET Core: [Microsoft Docs](https://github.com/MaterialDesignInXAML)
* Best prectices по работе с App.config и стандартной сборкой System.configuration.dll: [SubMain](https://blog.submain.com/app-config-basics-best-practices/)
* Дополнительные советы по конфигурации .NET: [Habr](https://habr.com/ru/post/128517/)


#### Дополнительно:

Для создания библиотеки, которая работала бы одинаково стабильно в Linux и Ubuntu существует .NET Standard, который является больше протоколом, нежели версией платформы. Пришлось изучить особенности конфигурирования, которые описал выше, а так же специфику .NET Standard-сборок: [Blog](http://www.codedigest.com/quick-start/9/what-is-netstandard)  

Хорошее чтиво по таргетированию сборок, важно при перекомпиляции (в том числе, если будет желание перекомпилировать клиент на линукс): [Microsoft Docs](https://docs.microsoft.com/en-us/dotnet/standard/net-standard)

Еще отмечу книгу Саши Голдштейна **"Pro .NET Perfomance"**. Начал её изучать перед началом курсача и много вещей оттуда повлияли на решения при разработке. В частности - выбор .NET Standard, асинхронности, низкоуровневых компонентов (книга достаточноп подробно останавливается на этом конкретно для оптимизаций).

Часть ссылок была утеряна. Но указанные выше позволяют выстроить дорожную карту по пониманию подходов к разработке этого проекта
