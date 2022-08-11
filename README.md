# О Api

## Реализовано

- Реализовано получение списко файлов с информацией о колонках файла
- Авторизация по средствам JWT тоекнов
- Получения файла с сортировкой по столбцам и/или филтрацией по средствам регулярных выражений по одному или нескольким столбцам
- Удаление файла
- Загрузка файла
###### Имеется Dockerfile под Windows, но он не протестирован
###### Фильтрация, сортировка и загрузка фалйла покрыта тестами, лежащими в отдельном проекте

## Внимание!
- Реализован задел под обработку ошибок, но на клиент на сервер будет прилетать только 500 ошибка (не доделано)
- API не тестировалось на данных большого объема а так же при высокой паралельной нагрузке 
- Context обявлятеся в участках когда явным образом, следует его перенести куда-то, например в Startup

### Дотсупые методы

 Методы принимают параметры из переданного JSON 

| Название метода | Описание              | Тип  |
| :-------- | :------------------------- | :------------------------- |
| `/GetToken` |  Получение токена | `GET` |
| `/FileInteraction/Delete` |  Удаление файла |`DELETE` |
| `/FileInteraction/GetFilesList` |  Получение спика всех файлов | `GET`|
| `/File/GetFile` |  Получение конкретного файла | `POST`|
| `/File/Upload` |  загрузка нового файла |`POST`|

### /GetToken
 ```json
 {"Login":"admin","Password":"pass"}
  ```
Для получения токена необходимо передать пароль и логин. При правильно указанном методе будет возвращены токен и логин.
 ```json
{"access_token":"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWRtaW4iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJhZG1pbiIsIm5iZiI6MTY2MDIyMTU3NCwiZXhwIjoxNjYwMjI3NTc0LCJpc3MiOiJNeUF1dGhTZXJ2ZXIiLCJhdWQiOiJNeUF1dGhDbGllbnQifQ.o6SEbVaxjRlmfD2a3mZh6OISVs_qJUj7kaadGhe45A8","username":"admin"}
  ```
  
  ### /FileInteraction/Delete
 ```json
 {"FileId":"98"}
  ```
Для удаления файла необходимо передать Id файла, а так же в заголовке должен присутсвовать ранее полученный токен.
  ```txt
Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWRtaW4iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJhZG1pbiIsIm5iZiI6MTY2MDIyMTU3NCwiZXhwIjoxNjYwMjI3NTc0LCJpc3MiOiJNeUF1dGhTZXJ2ZXIiLCJhdWQiOiJNeUF1dGhDbGllbnQifQ.o6SEbVaxjRlmfD2a3mZh6OISVs_qJUj7kaadGhe45A8
  ```
  
  ### /FileInteraction/GetFilesList
 ```json
 {"Login":"admin","Password":"pass"}
  ```
Для получения списка файлов нужно только указать в заголовке ранее полученный токен
  ```txt
Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWRtaW4iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJhZG1pbiIsIm5iZiI6MTY2MDIyMTU3NCwiZXhwIjoxNjYwMjI3NTc0LCJpc3MiOiJNeUF1dGhTZXJ2ZXIiLCJhdWQiOiJNeUF1dGhDbGllbnQifQ.o6SEbVaxjRlmfD2a3mZh6OISVs_qJUj7kaadGhe45A8
  ```
  ### /File/GetFile
 ```json
{"Descending":true,"FileId":3,"Filters":{"0":"1(\\@*)"},"ColumnSortingSequence":["col1","col2"]}
  ```
- Descending Для получения файла необходимо передать каким образом будут сортироваться стобцы Order by = Descending ==false, Order by desc = Descending == true .
- Filters Указать колоноку (её номер от 0) и регулярные выражение, по которым будет фильтроваться колонка. Передается словарь ключ-значение (ключ - ID, значение - Регулярное выражение). 
- ColumnSortingSequence Указать последовательность, согласно которой будут сортироваться колонки. Указывать нужно НАЗВАНИЕ колонок, а не их номер.
Помимо этого нужно передать в заголовке токен для Авторизации
 ```json
{"access_token":"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWRtaW4iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJhZG1pbiIsIm5iZiI6MTY2MDIyMTU3NCwiZXhwIjoxNjYwMjI3NTc0LCJpc3MiOiJNeUF1dGhTZXJ2ZXIiLCJhdWQiOiJNeUF1dGhDbGllbnQifQ.o6SEbVaxjRlmfD2a3mZh6OISVs_qJUj7kaadGhe45A8","username":"admin"}
  ```
  
  ### /File/Upload
 
Для загрузки файла нужно передать файл при помощи метода POST. Помимо этого нужно передать в заголовке токен для Авторизации.
 ```json
{"access_token":"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWRtaW4iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJhZG1pbiIsIm5iZiI6MTY2MDIyMTU3NCwiZXhwIjoxNjYwMjI3NTc0LCJpc3MiOiJNeUF1dGhTZXJ2ZXIiLCJhdWQiOiJNeUF1dGhDbGllbnQifQ.o6SEbVaxjRlmfD2a3mZh6OISVs_qJUj7kaadGhe45A8","username":"admin"}
  ```

## Развертывание
Для развертывания на локальной машине необходимо скачать решение и запустить в 2022 студии с пакетом Web и .Net Core 3.1. После этого необходимо  поменять пути в настроечном файле "filesDirectory", "tempFilesDirectory" на те, что будут использоваться на конкретной машине. "useHttps" в настройках необходимо использовать false только в случаях развертывания локально на машине, и развертывания для доступа в сети интернет ТОЛЬКО с использованием HTTP протокола.
