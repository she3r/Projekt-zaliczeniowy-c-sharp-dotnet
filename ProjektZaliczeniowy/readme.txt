W celu uruchomienia programu będzie potrzebny docker. 
Komenda do pobrania i uruchomienia dockerowego image
>> docker run -it --rm -p 8080:80 -e MongoDbSettings:Host=mongo -e MongoDbSettings:Password=1234 --network=projektZaliczeniowyNetwork she3r/restapi_catalog
    gdzie:
    
    MongoDbSettings to ustawienia autoryzacji potrzebne do uruchomienia bazy danych z dockera
    8080:80 - porty, gdzie 80 to port dedykowany port dockerowy pod ten typ aplikacji
    https://stackoverflow.com/questions/48669548/why-does-aspnet-core-start-on-port-80-from-within-docker
    she3r/restapi - ścieżka w docker hubie
    https://hub.docker.com/repository/docker/she3r/restapi_catalog/general
    projektZaliczeniowyNetwork to dockerowy network, który umożliwia komunikację między kontenerem api i kontenerem bazy danych MongoDb
    docker os: linux

Przykładowe wykorzystanie (po więcej patrz: Controllers/ScoresController.cs)

Zapytanie: http://localhost:8080/scores/piotr%20kubacki (GET piotr kubacki)
Zwraca:
[
    {
        "id": "d9cb83f9-85cb-454a-8ef5-8565d4327cc1",
        "studentName": null,
        "teacherName": "jan kowalski",
        "value": 100,
        "createdDate": "2023-01-06T13:22:28.9913835+00:00"
    }
]


Komendy, zeby zbudowac obrazy i polaczyc je we wspolny network lokalnie, a potem zrobic push na docker hub
docker run -d --rm --name mongo -p 27017:27017 -v mongodbdata:/data/db -e MONGO_INITDB_ROOT_USERNAME=mongoadmin -e MONGO_INITDB_ROOT_PASSWORD=1234 mongo
docker network create projektZaliczeniowyNetwork
docker run -d --name mongo -p 27017:27017 -v mongodbdata:/data/db -e MONGO_INITDB_ROOT_USERNAME=mongoadmin -e MONGO_INITDB_ROOT_PASSWORD=1234 --network=projektZaliczeniowyNetwork mongo
docker run -it --rm -p 8080:80 -e MongoDbSettings:Host=mongo -e MongoDbSettings:Password=1234 --network=projektZaliczeniowyNetwork projektzaliczeniowy:v1
docker tag projektzaliczeniowy:v1 she3r/projektzaliczeniowy:v1
docker push she3r/projektzaliczeniowy:v1
docker tag mongo she3r/mongo_projektzaliczeniowy:v1
docker push she3r/mongo_projektzaliczeniowy:v1

docker network create projektZaliczeniowyNetwork
docker run -it --rm -p 8080:80 -e MongoDbSettings:Host=mongo -e MongoDbSettings:Password=1234 --network=projektZaliczeniowyNetwork she3r/projektzaliczeniowy:v1
docker run -d --name mongo -p 27017:27017 -v mongodbdata:/data/db -e MONGO_INITDB_ROOT_USERNAME=mongoadmin -e MONGO_INITDB_ROOT_PASSWORD=1234 --network=projektZaliczeniowyNetwork she3r/mongo_projektzaliczeniowy:v1