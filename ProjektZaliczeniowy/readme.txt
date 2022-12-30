1. Aby móc skorzystać z bazy danych, będzie potrzebny docker i kontener, który proszę uruchomić komendą (po wcześniejszym uruchomieniu klienta)
>> docker load --input test1.tar    
>> docker run -d --rm --name mongo -p 27017:27017 -v mongodbdata:/data/db -e MONGO_INITDB_ROOT_USERNAME=mongoadmin -e MONGO_INITDB_ROOT_PASSWORD=1234 mongo
(uwaga! --rm oznacza usunięcie kontenera po zatrzymaniu pracy kontenera)
2. Następnie, zbudowanie i uruchomienie projektu spowoduje uruchomienie przeglądarki i otwarcie interfejsu swaggera z dokumentacją tego api