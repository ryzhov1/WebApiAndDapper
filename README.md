# WebApiAndDapper
Тестовое задание

Необходимо реализовать импорт контрагентов из файла выписок.

Описание формата выписок в файле 1C_to_KL.doc

Пример выписки в файле kl_to_1c_TEST.txt

Требования к программе:
Backend: ASP.Net Web API (http://www.asp.net/web-api)
Frontend: html, css, js(jquery)
БД: MS SQL Server. Для взаимодействия с БД использовать мини orm - dapper
Функциональность:
Страница отображения текущего списка контрагентов с возможностью просмотра карточки контрагента. Список контрагентов хранится в БД и изначально пуст.
Страничка импорта: выбор и загрузка файла выписки, парсинг контрагентов из файла, предварительный просмотр найденных контрагентов (с возможностью просмотра карточки контрагента как в предыдущем пункте). Если останется время, реализовать поиск дубликатов.
