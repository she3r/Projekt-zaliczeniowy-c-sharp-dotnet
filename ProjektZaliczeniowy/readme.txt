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
        "teacherName": "edward szczypka",
        "value": 100,
        "createdDate": "2023-01-06T13:22:28.9913835+00:00"
    },
    {
        "id": "3942c0ca-fba9-4e67-9b90-ebff943b5abc",
        "studentName": null,
        "teacherName": "michal lipinski",
        "value": 17,
        "createdDate": "2023-01-06T13:29:55.3849411+00:00"
    },
    {
        "id": "c2d59069-04bf-4807-b7ca-c44b738b33a5",
        "studentName": null,
        "teacherName": "michal lipinski",
        "value": 18,
        "createdDate": "2023-01-06T17:19:28.4858008+00:00"
    }
]

