OTUS Learning

# ДЗ №1

* docs/ДЗ_1.txt: пользовательские сценарии, функциональная модель, декомпозиция
* docs/func.png: функциональная модель
* docs/services.png: сервисы, выделенные методом функционального моделирования  
* docs/src.drawio: исходники диаграмм для drawio

# ДЗ №2

## Dockerfile 
```src/VirtualAssistant/VirtualAssistant.API/Dockerfile```

## Dockerhub
```cl6wn/virtual_assistant```

# ДЗ №3

## Применить манифест
```kubectl apply -f kube/.```

## GET к сервису
```curl arch.homework/otusapp/eygerman/health```

## ingress nginx
```kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.6.4/deploy/static/provider/cloud/deploy.yaml```

# ДЗ №4

## Установить БД (helm)
```helm install postgre-release-postgresql oci://registry-1.docker.io/bitnamicharts/postgresql --set auth.username=virtual_assistant --set auth.password=virt_ass --set auth.postgres-password=virt_ass --set auth.database=virtual_assistant```

## Применить манифест
```kubectl apply -f kube/.```

## Endpoints (коллекция postman: ./postman/dz_4)
1. Создать пользователя 
POST http://arch.homework/otusapp/eygerman/user

```JSON
{
    "UserName": "uName",
    "FIrstName": "fName",
    "LastName": "lName",
    "Email": "mail",
    "Phone": "111-222-333"
}
```

2. Получить пользователя
GET http://arch.homework/otusapp/eygerman/user/{id}

3. Обновить пользователя
PUT http://arch.homework/otusapp/eygerman/user
```JSON
{
    "Id": 1, // id записи, данные которой требуется обновить
    "UserName": "uName_upd",
    "FIrstName": "fName_upd",
    "LastName": "lName_upd",
    "Email": "mail_upd",
    "Phone": "111-222-333-444"
}
```

4. Удалить пользователя
DELETE http://arch.homework/otusapp/eygerman/user/{id}

5. Получить список всех пользователей
GET http://arch.homework/otusapp/eygerman/user/all
